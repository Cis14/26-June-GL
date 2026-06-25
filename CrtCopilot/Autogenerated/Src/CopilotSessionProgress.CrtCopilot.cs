namespace Creatio.Copilot
{
	using System;
	using System.Linq;
	using System.Runtime.Serialization;
	using Terrasoft.Common;
	using Terrasoft.Core;

	[DataContract]
	public class CopilotSessionProgress
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the Copilot session identifier.
		/// </summary>
		[DataMember(Name = "copilotSessionId")]
		public Guid SessionId { get; set; }

		/// <summary>
		/// Gets or sets the Copilot session title.
		/// </summary>
		[DataMember(Name = "copilotSessionTitle")]
		public string SessionTitle { get; set; }

		/// <summary>
		/// Root intent's caption.
		/// </summary>
		[DataMember(Name = "rootIntentCaption")]
		public string RootIntentCaption { get; set; }

		/// <summary>
		/// Current intent's caption.
		/// </summary>
		[DataMember(Name = "currentIntentCaption")]
		public string CurrentIntentCaption { get; set; }

		/// <summary>
		/// Gets or sets the state of the progress.
		/// </summary>
		[DataMember(Name = "state")]
		public string State { get; set; } = nameof(CopilotSessionProgressStates.WaitingForUserMessage);

		/// <summary>
		/// Gets or sets the description of the current progress.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		#endregion

		#region Methods: Private

		private static string GetIntentCaption(UserConnection userConnection, Guid? intentId) {
			if (!intentId.HasValue) {
				return null;
			}
			CopilotIntentSchemaManager intentSchemaManager = userConnection.GetIntentSchemaManager();
			IManagerItem intent = intentSchemaManager.FindItemByUId(intentId.Value);
			return intent?.Caption;
		}

		private static LocalizableString GetDescriptionTemplate(UserConnection userConnection,
				CopilotSessionProgressStates state) {
			return new LocalizableString(userConnection.ResourceStorage,
				nameof(CopilotSessionProgress), $"LocalizableStrings.{state.ToString()}.Value");
		}

		#endregion

		#region Methods: Public 

		public static CopilotSessionProgress Create(UserConnection userConnection, 
				CopilotSession copilotSession, CopilotSessionProgressStates state, params object[] descriptionArgs) {
			string currentIntentCaption = GetIntentCaption(userConnection, copilotSession.CurrentIntentId);
			string rootIntentCaption = GetIntentCaption(userConnection, copilotSession.RootIntentId);
			LocalizableString descriptionTemplate = GetDescriptionTemplate(userConnection, state);
			string description = descriptionTemplate?.Value != null 
				? string.Format(descriptionTemplate, descriptionArgs)
				: string.Join(Environment.NewLine, descriptionArgs);
			return new CopilotSessionProgress {
				SessionId = copilotSession.Id,
				RootIntentCaption = rootIntentCaption,
				CurrentIntentCaption = currentIntentCaption,
				SessionTitle = copilotSession.Title,
				State = state.ToString(),
				Description = description
			};
		}

		#endregion

	}
}

