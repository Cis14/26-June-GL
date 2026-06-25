namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;

	#region Class: CopilotIntentTypeConstants

	/// <summary>
	/// Constants for Copilot intent types.
	/// </summary>
	public static class CopilotIntentTypeConstants
	{

		#region Fields: Public

		/// <summary>
		/// Primary UI color for the <see cref="CopilotIntentType.Skill"/> intent type.
		/// </summary>
		public static readonly string SkillTypeColor = "#7848EE";

		/// <summary>
		/// Primary UI color for the <see cref="CopilotIntentType.System"/> intent type.
		/// </summary>
		public static readonly string SystemTypeColor = "#22AC14";

		/// <summary>
		/// Primary UI color for the <see cref="CopilotIntentType.Agent"/> intent type.
		/// </summary>
		public static readonly string AgentTypeColor = "#E67E22";

		/// <summary>
		/// Primary UI color for the <see cref="CopilotIntentType.WorkflowAgent"/> intent type.
		/// </summary>
		public static readonly string WorkflowAgentTypeColor = "#00ADFF";

		/// <summary>
		/// Identifier of the <see cref="CopilotIntentType.Skill"/> intent type.
		/// </summary>
		public static readonly Guid SkillIntentTypeId = Guid.Parse("6D940B75-21C8-4A90-89AB-9867E6E4A045");

		/// <summary>
		/// Identifier of the <see cref="CopilotIntentType.System"/> intent type.
		/// </summary>
		public static readonly Guid SystemIntentTypeId = Guid.Parse("35F3B644-4FA3-4D1E-8E62-5C3FDC4D3E52");

		/// <summary>
		/// Identifier of the <see cref="CopilotIntentType.Agent"/> intent type.
		/// </summary>
		public static readonly Guid AgentIntentTypeId = Guid.Parse("a8fbe253-be1b-4ec4-bc58-65b8047013da");

		/// <summary>
		/// Identifier of the <see cref="CopilotIntentType.WorkflowAgent"/> intent type.
		/// </summary>
		public static readonly Guid WorkflowAgentIntentTypeId = Guid.Parse("196ba912-742c-40fc-92a9-3eebe9021762");

		#endregion

		#region Methods: Private

		private static LocalizableString GetIntentTypeCaption(UserConnection userConnection, string resourceItemName) {
			string resource = $"LocalizableStrings.{resourceItemName}.Value";
			IResourceStorage resourceStorage = userConnection.Workspace.ResourceStorage;
			return new LocalizableString(resourceStorage, nameof(CopilotIntentTypeConstants), resource);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns a localized caption for the specified Copilot intent type.
		/// </summary>
		/// <param name="userConnection"> The current user connection used to resolve localized resources. </param>
		/// <param name="intentType"> The intent type whose caption should be returned. </param>
		/// <returns>
		/// A <see cref="LocalizableString"/> that represents the localized caption for the intent type.
		/// </returns>
		public static LocalizableString GetIntentTypeCaption(UserConnection userConnection,
				CopilotIntentType intentType) {
			switch (intentType) {
				case CopilotIntentType.Skill:
					return GetIntentTypeCaption(userConnection, "SkillIntentTypeCaption");
				case CopilotIntentType.System:
					return GetIntentTypeCaption(userConnection, "SystemIntentTypeCaption");
				case CopilotIntentType.Agent:
					return GetIntentTypeCaption(userConnection, "AgentIntentTypeCaption");
				case CopilotIntentType.WorkflowAgent:
					return GetIntentTypeCaption(userConnection, "WorkflowAgentIntentTypeCaption");
				default:
					throw new ArgumentOutOfRangeException(nameof(intentType), intentType, null);
			}
		}

		/// <summary>
		/// Returns the identifier \(Id\) of the specified Copilot intent type.
		/// </summary>
		/// <param name="intentType">The intent type whose identifier should be returned.</param>
		/// <returns>A <see cref="Guid"/> that identifies the specified intent type.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when <paramref name="intentType"/> is not a supported <see cref="CopilotIntentType"/> value.
		/// </exception>
		public static Guid GetIntentTypeId(CopilotIntentType intentType) {
			switch (intentType) {
				case CopilotIntentType.Skill:
					return SkillIntentTypeId;
				case CopilotIntentType.System:
					return SystemIntentTypeId;
				case CopilotIntentType.Agent:
					return AgentIntentTypeId;
				case CopilotIntentType.WorkflowAgent:
					return WorkflowAgentIntentTypeId;
				default:
					throw new ArgumentOutOfRangeException(nameof(intentType), intentType, null);
			}
		}

		/// <summary>
		/// Returns a read\-only list of all values defined in <see cref="CopilotIntentType"/>.
		/// </summary>
		/// <returns>
		/// An <see cref="IReadOnlyList{T}"/> containing all <see cref="CopilotIntentType"/> values.
		/// </returns>
		public static IReadOnlyList<CopilotIntentType> GetAllCopilotIntentTypes() {
			IEnumerable<CopilotIntentType> values = Enum.GetValues(typeof(CopilotIntentType)).Cast<CopilotIntentType>();
			return values.ToList().AsReadOnly();
		}

		/// <summary>
		/// Returns a string code for the specified Copilot intent type.
		/// </summary>
		/// <param name="value">The intent type to get the code for.</param>
		/// <returns>
		/// The enum member name for <paramref name="value"/> if available; otherwise,
		/// <paramref name="value"/>\.ToString\(\).
		/// </returns>
		public static string GetCode(CopilotIntentType value) {
			return Enum.GetName(typeof(CopilotIntentType), value) ?? value.ToString();
		}

		#endregion

	}

	#endregion

}

