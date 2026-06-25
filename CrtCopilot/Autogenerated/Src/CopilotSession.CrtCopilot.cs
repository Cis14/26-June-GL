namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using Terrasoft.Common;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Class: CopilotSession

	/// <summary>
	/// Represents a session of the Copilot.
	/// </summary>
	[DataContract]
	[Serializable]
	[KnownType(typeof(CreatioAIDocument))]
	public class CopilotSession : BaseCopilotSession
	{

		#region Constructors: Public

		static CopilotSession() {
			DerivedTypes.Add(typeof(CopilotSession));
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotSession"/> class.
		/// </summary>
		public CopilotSession() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotSession"/> class with the specified user ID and intent ID.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <param name="intentId">The unique identifier of the intent, or null if not specified.</param>
		public CopilotSession(Guid userId, Guid? intentId)
			: base(userId, intentId) {
		}

		public CopilotSession(Guid userId, List<CopilotMessage> messages, Guid? intentId) 
			: this(userId, intentId) {
			messages.CheckArgumentNull(nameof(messages));
			AddMessages(messages);
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Current context of the Copilot session.
		/// </summary>
		[DataMember(Name = "currentContext")]
		public CopilotContext CurrentContext { get; internal set; }

		/// <summary>
		/// The list of Copilot session messages.
		/// </summary>
		private List<CopilotMessage> _messages = new List<CopilotMessage>();
		[DataMember(Name = "messages")]
		public IEnumerable<CopilotMessage> Messages => _messages;

		/// <summary>
		/// The list of summary messages that were generated during the session.
		/// </summary>
		private List<CopilotMessage> _summaryMessages = new List<CopilotMessage>();
		[DataMember(Name = "summaryMessages")]
		public IEnumerable<CopilotMessage> SummaryMessages => _summaryMessages;

		/// <summary>
		/// Indicates that the session is transient and does not support continuation of execution.
		/// </summary>
		[DataMember(Name = "isTransient")]
		public bool IsTransient { get; set; }

		/// <summary>
		/// Gets or sets the list of documents associated with the given session.
		/// </summary>
		[DataMember(Name = "documents")]
		public IList<ICreatioAIDocument> Documents { get; set; } = new List<ICreatioAIDocument>();

		/// <summary>
		/// The date and time when the session was loaded last time.
		/// </summary>
		[DataMember(Name = "loadedOn")]
		public DateTime? LoadedOn { get; set; }

		/// <summary>
		/// Root Session identifier.
		/// </summary>
		[DataMember(Name = "rootSessionId")]
		public Guid? RootSessionId { get; set; }

		/// <summary>
		/// Gets or sets the process element unique identifier.
		/// </summary>
		[DataMember(Name = "processElementId")]
		public Guid? ProcessElementId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to skip intent update when adding messages to the session.
		/// </summary>
		[DataMember(Name = "skipIntentUpdate")]
		public bool SkipIntentUpdate { get; set; }

		/// <summary>
		/// Maximum number of completion tokens allowed for requests made within the session.
		/// </summary>
		[DataMember(Name = "maxOutputTokens")]
		public int? MaxOutputTokens { get; set; }

		/// <summary>
		/// Serializable filter descriptors used to restore session-scoped tool filters after reload.
		/// </summary>
		[DataMember(Name = "filterDescriptors")]
		public IList<AIToolFilterDescriptor> FilterDescriptors { get; set; }

		#endregion

		#region Methods: Private

		private Dictionary<Guid, int> GetLastIndexesOfSummaries() {
			var lastIdxOfSummary = new Dictionary<Guid, int>();
			for (int i = 0; i < _messages.Count; i++) {
				var msg = _messages[i];
				if (msg.SummarizedById is Guid sid) {
					lastIdxOfSummary[sid] = i;
				}
			}
			return lastIdxOfSummary;
		}

		#endregion

		#region Methods: Public
		public CopilotSession AddSummaryMessage(CopilotMessage copilotMessage) {
			if (!copilotMessage.IsSummary) {
				throw new ArgumentException("Message is not a summary", nameof(copilotMessage));
			}
			_summaryMessages.Add(copilotMessage);
			return this;
		}

		public CopilotSession AddSummaryMessages(IEnumerable<CopilotMessage> summaryMessages) {
			if (summaryMessages.Any(x => !x.IsSummary)) {
				throw new ArgumentException("Sequence contains non summary messages", nameof(summaryMessages));
			}
			_summaryMessages.AddRange(summaryMessages);
			return this;
		}

		/// <summary>
		/// Replaces summarized messages in the session with the corresponding summary messages.
		/// </summary>
		public List<CopilotMessage> GetMergedMessages() {
			if (!_summaryMessages.Any()) {
				return _messages.ToList();
			}

			var summaries = _summaryMessages.ToDictionary(s => s.Id);
			var usedSummaries = new HashSet<Guid>();
			var result = new List<CopilotMessage>();

			foreach (var message in _messages) {
				var summaryId = message.SummarizedById;
				if (summaryId == null) {
					result.Add(message);
				} else if (usedSummaries.Add(summaryId.Value) && summaries.ContainsKey(summaryId.Value)) {
					result.Add(summaries[summaryId.Value]);
				}
			}
			return result;
		}

		public CopilotSession AddMessage(CopilotMessage copilotMessage) {
			copilotMessage.IntentId = CurrentIntentId;
			copilotMessage.RootIntentId = RootIntentId;
			_messages.Add(copilotMessage);
			return this;
		}

		public CopilotSession AddMessages(IEnumerable<CopilotMessage> copilotMessages, bool skipIntentUpdate = false) {
			if (!skipIntentUpdate && !SkipIntentUpdate) {
				copilotMessages.ForEach(message => {
					message.IntentId = CurrentIntentId;
					message.RootIntentId = RootIntentId;
				});
			}
			_messages.AddRange(copilotMessages);
			return this;
		}

		public CopilotMessage RemoveLastUserMessage() {
			var lastUserMessage = _messages.FindLast(message => message.Role == CopilotMessageRole.User);
			if (lastUserMessage != null) {
				_messages.Remove(lastUserMessage);
			}
			return lastUserMessage;
		}

		public CopilotMessage RemoveLastMessage(string contentType) {
			var lastMessageByContentType = _messages.FindLast(message => message.ContentType == contentType);
			if (lastMessageByContentType != null) {
				_messages.Remove(lastMessageByContentType);
			}
			return lastMessageByContentType;
		}

		public List<CopilotMessage> CreateToolCallMessages(string toolCallId, string resultContent) {
			var resultMessages = new List<CopilotMessage>();
			Guid? toolRequestMessageId = null;
			Guid? toolResponseMessageId = null;
			lock (Messages) {
				CopilotMessage oldCallRequestMessage = Messages.FirstOrDefault(msg =>
					msg.ToolCalls.Any(call => call.Id == toolCallId) && msg.Role == CopilotMessageRole.Assistant);
				if (oldCallRequestMessage == null) {
					return resultMessages;
				}
				ToolCall toolCall = oldCallRequestMessage.ToolCalls.FirstOrDefault(call => call.Id == toolCallId);
				oldCallRequestMessage.ToolCalls.Remove(toolCall);
				if (oldCallRequestMessage.ToolCalls.Count == 0) {
					toolRequestMessageId = oldCallRequestMessage.Id;
					_messages.Remove(oldCallRequestMessage);
				}
				CopilotMessage oldCallResponseMessage = Messages.FirstOrDefault(msg =>
					msg.ToolCallId == toolCallId && msg.Role == CopilotMessageRole.Tool);
				if (oldCallResponseMessage != null) {
					toolResponseMessageId = oldCallResponseMessage.Id;
					_messages.Remove(oldCallResponseMessage);
				}
				CopilotMessage newCallRequestMessage = CopilotMessage.FromAssistant(toolCall);
				newCallRequestMessage.Id = toolRequestMessageId ?? newCallRequestMessage.Id; 
				CopilotMessage newCallResponseMessage = CopilotMessage.FromTool(resultContent, toolCallId);
				newCallResponseMessage.Id = toolResponseMessageId ?? newCallResponseMessage.Id;
				resultMessages.AddRange(new [] {
					newCallRequestMessage,
					newCallResponseMessage
				});
				return resultMessages;
			}
		}

		/// <summary>
		/// Set current intent for the Copilot session.
		/// </summary>
		/// <param name="intentId">New intent identifier.</param>
		public void SetCurrentIntent(Guid intentId) {
			if (CurrentIntentId == null) {
				RootIntentId = intentId;
			}
			CurrentIntentId = intentId;
			CopilotMessage lastUserMessage = _messages.FindLast(message => message.Role == CopilotMessageRole.User);
			if (lastUserMessage != null) {
				lastUserMessage.IntentId = intentId;
				lastUserMessage.IsSaved = false;
			}
		}

		/// <summary>
		/// Removes assistant messages that start a tool call
		/// but aren’t immediately followed by a matching tool response.
		/// Keeps only valid assistant→tool pairs and preserves message order.
		/// </summary>
		public void CleanToolCallsWithoutTools() {
			if (_messages == null || _messages.Count == 0) {
				return;
			}
			for (var i = 0; i < _messages.Count; i++) {
				CopilotMessage current = _messages[i];
				if (!current.IsToolCall()) {
					continue;
				}
				CopilotMessage next = i + 1 < _messages.Count ? _messages[i + 1] : null;
				if (!next.IsToolById(current.ToolCallId)) {
					_messages.RemoveAt(i);
					i--;
				} else {
					i++;
				}
			}
		}

		#endregion

	}

	#endregion

}

