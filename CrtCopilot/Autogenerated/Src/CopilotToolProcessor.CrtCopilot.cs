namespace Creatio.Copilot
{
	using Creatio.Copilot.Metadata;
	using Creatio.FeatureToggling;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core;
	using Terrasoft.Core.Applications.GenAI;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: CopilotToolProcessor

	[DefaultBinding(typeof(ICopilotToolProcessor))]
	internal class CopilotToolProcessor : ICopilotToolProcessor
	{

		#region Class: NotFoundToolExecutor

		private class NotFoundToolExecutor : IToolExecutor
		{
			public bool IsConfirmRequired => false;

			/// <summary>
			/// Gets a value indicating whether confirmation is required before proceeding with the action.
			/// </summary>
			public bool IsConfirmationRequired { get; }

			public List<CopilotMessage> Execute(string toolCallId, Dictionary<string, object> arguments,
				CopilotSession session) {
				return new List<CopilotMessage> {
					new CopilotMessage(FnNotFoundMessage, CopilotMessageRole.Tool, toolCallId),
				};
			}
		}

		#endregion

		#region Constants: Private

		private const string FnNotFoundMessage = "Function not found. Use only suggested functions (tools)!";
		private const int DefaultMaxFunctionCallingCountPerCopilotRequest = 50;

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ICopilotMessageConfirmationHandler _confirmationHandler;
		private readonly ICopilotToolContextBuilder _contextBuilder;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotToolProcessor"/> class.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="confirmationHandler">Confirmation handler.</param>
		/// <param name="contextBuilder">Tool context builder.</param>
		public CopilotToolProcessor(UserConnection userConnection,
				ICopilotMessageConfirmationHandler confirmationHandler, ICopilotToolContextBuilder contextBuilder) {
			_userConnection = userConnection;
			_confirmationHandler = confirmationHandler;
			_contextBuilder = contextBuilder;
		}

		#endregion

		#region Methods: Private

		private static Dictionary<string, object> ParseArguments(string functionCallingArguments) {
			Dictionary<string, object> result = functionCallingArguments.IsNotNullOrWhiteSpace()
				? JsonConvert.DeserializeObject<Dictionary<string, object>>(functionCallingArguments)
				: null;
			return result ?? new Dictionary<string, object>();
		}

		private List<CopilotMessage> GetLastAssistantMessages(List<CopilotMessage> messages) {
			int lastUserIndex = messages.FindLastIndex(x => x.Role == CopilotMessageRole.User);
			DateTime startDate = lastUserIndex > 0 ? messages[lastUserIndex].Date : DateTime.MinValue;
			return messages
				.Where(message => message.Date > startDate && message.Role == CopilotMessageRole.Assistant)
				.ToList();
		}

		private int GetAvailableFunctionCallingCount(List<CopilotMessage> lastAssistantMessages) {
			int maxFunctionCallingCountPerCopilotRequest =
				SystemSettings.GetValue(_userConnection, "MaxFunctionCallingCountPerCopilotRequest",
					DefaultMaxFunctionCallingCountPerCopilotRequest);
			int availableFunctionCallingCount = maxFunctionCallingCountPerCopilotRequest - lastAssistantMessages.Count;
			return availableFunctionCallingCount;
		}

		private List<ToolCall> PrepareToolCalls(ChatCompletionResponse completionResponse, CopilotSession session) {
			var lastAssistantMessages = GetLastAssistantMessages(session.Messages.ToList());
			int availableCount = GetAvailableFunctionCallingCount(lastAssistantMessages);
			List<ToolCall> survivedToolCalls = ExtractInitialToolCalls(completionResponse, availableCount);
			List<ToolCall> callsWithExcludedDuplicates = ExcludeExcessDuplicates(survivedToolCalls,
				lastAssistantMessages);
			return callsWithExcludedDuplicates;
		}

		private static List<ToolCall> ExtractInitialToolCalls(ChatCompletionResponse completionResponse,
				int availableCount) {
			if (completionResponse?.Choices == null) {
				return new List<ToolCall>();
			}
			return completionResponse.Choices.SelectMany(c => c?.Message?.ToolCalls ?? Enumerable.Empty<ToolCall>())
				.Where(IsValidToolCall)
				.Take(availableCount)
				.ToList();
		}

		private List<ToolCall> ExcludeExcessDuplicates(IEnumerable<ToolCall> toolCalls,
				IEnumerable<CopilotMessage> lastAssistantMessages) {
			int maxDuplicates = SystemSettings.GetValue(_userConnection,
				"MaxDuplicateFunctionCallingCountPerCopilotRequest", 2);
			IEnumerable<ToolCall> allToolCalls = lastAssistantMessages.SelectMany(message => message.ToolCalls);
			var allCallsGroups = allToolCalls
				.GroupBy(x => new { x.FunctionCall.Name, x.FunctionCall.Arguments })
				.Select(g => new { Group = g.Key, Count = g.Count() });
			var result = toolCalls
				.GroupBy(x => new { x.FunctionCall.Name, x.FunctionCall.Arguments })
				.SelectMany(g => g.Take(
						Math.Max(0, maxDuplicates - (allCallsGroups.FirstOrDefault(
							x => x.Group.Name == g.Key.Name && x.Group.Arguments == g.Key.Arguments)?.Count ?? 0)
						)
					)
				);
			return result.ToList();
		}

		private static bool IsValidToolCall(ToolCall toolCall) =>
			toolCall?.FunctionCall != null && toolCall.FunctionCall.Name.IsNotNullOrEmpty();

		private List<CopilotMessage> ExecuteToolCalls(IEnumerable<ToolCall> toolCalls, CopilotSession session,
			CopilotToolContext toolContext) {
			var resultMessages = new List<CopilotMessage>();
			Dictionary<string, IToolExecutor> toolMapping = toolContext.Mapping;
			IEnumerable<ToolCall> toolCallsList = toolCalls.ToList();
			if (toolCallsList.IsNullOrEmpty() || toolMapping.IsNullOrEmpty()) {
				return resultMessages;
			}
			foreach (ToolCall toolCall in toolCallsList) {
				string functionCallName = toolCall.FunctionCall.Name;
				CopilotMessage toolCallMessage = CopilotMessage.FromAssistant(toolCall);
				resultMessages.Add(toolCallMessage);
				Dictionary<string, object> arguments = ParseArguments(toolCall.FunctionCall.Arguments);
				List<CopilotMessage> copilotMessages;
				if (!toolMapping.TryGetValue(functionCallName, out IToolExecutor executor)) {
					var notFoundToolExecutor = new NotFoundToolExecutor();
					copilotMessages = notFoundToolExecutor.Execute(toolCall.Id, arguments, session);
					resultMessages.AddRange(copilotMessages);
					continue;
				}
				if (Features.GetIsEnabled<GenAIFeatures.UseActionConfirmations>() &&
					executor?.IsConfirmationRequired == true) {
					continue;
				}
				copilotMessages = executor.Execute(toolCall.Id, arguments, session);
				resultMessages.AddRange(copilotMessages);
				if (resultMessages.Any(m => m.IsPendingClarification())) {
					break;
				}
			}

			return resultMessages;
		}

		#endregion

		#region Methods: Public

		public List<CopilotMessage> HandleToolCalls(ChatCompletionResponse completionResponse, CopilotSession session,
				CopilotToolContext toolContext) {
			int? initialToolCallsCount = completionResponse?.Choices
				?.SelectMany(c => c?.Message?.ToolCalls ?? Enumerable.Empty<ToolCall>()).Where(IsValidToolCall).Count();
			List<ToolCall> toolCalls = PrepareToolCalls(completionResponse, session);
			if (initialToolCallsCount > 0 && toolCalls.Count == 0) {
				throw new GenAIException(nameof(GenAIServiceErrorCode.ToolCallsInvokesLimitExceeded),
					"Function calling limit exceeded");
			}
			List<CopilotMessage> messages = ExecuteToolCalls(toolCalls, session, toolContext);
			if (Features.GetIsEnabled<GenAIFeatures.UseActionConfirmations>()) {
				List<CopilotMessage> toolCallsWithConfirmation =
					_confirmationHandler.GetConfirmationMessages(toolCalls, toolContext, session);
				if (toolCallsWithConfirmation.IsNotNullOrEmpty()) {
					messages.AddRange(toolCallsWithConfirmation);
				}
			}
			return messages;
		}
		#endregion

	}

	#endregion

}
