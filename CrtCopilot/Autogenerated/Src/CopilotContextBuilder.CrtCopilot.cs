namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// Defines a builder for creating message content from a <see cref="CopilotContext"/>.
	/// </summary>
	public interface ICopilotContextBuilder
	{
		/// <summary>
		/// Builds the message content string based on the provided copilot context.
		/// </summary>
		/// <param name="copilotContext">The context containing parts to be included in the message.</param>
		/// <returns>The constructed message content as a string.</returns>
		string BuildMessageContent(CopilotContext copilotContext);
	}

	[DefaultBinding(typeof(ICopilotContextBuilder))]
	internal class CopilotContextBuilder: ICopilotContextBuilder
	{

		#region Field: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public CopilotContextBuilder(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private IContextPartBuilder GetPartBuilder(BaseContextPart part) {
			try {
				var type = part.Type.IsNullOrEmpty() ? nameof(CreatioPageContextPart) : part.Type;
				return ClassFactory.Get<IContextPartBuilder>(type, new ConstructorArgument("userConnection", _userConnection));
			} catch (InstanceActivationException ex) {
				throw new InstanceActivationException($"IContextPartBuilder implementation for \"{part.Type}\" not found", ex);
			}
		}

		#endregion

		#region Methods: Public

		public string BuildMessageContent(CopilotContext copilotContext) {
			if (copilotContext == null || copilotContext.Parts.Count == 0) {
				return null;
			}
			var prompts = new List<string>();
			foreach (var part in copilotContext.Parts) {
				var partsBuilder = GetPartBuilder(part);
				prompts.Add(partsBuilder.BuildMessageContent(copilotContext));
			}
			return string.Join("\n", prompts.Where(p => !p.IsNullOrEmpty()));
		}

		#endregion

	}

}

