namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using Creatio.Copilot.Metadata;

	#region Interface: ICopilotIntentsStorage

	internal interface ICopilotIntentsStorage
	{

		#region Properties: Public

		IIntentSchemaService IntentSchemaService { get; set; }
		/// <summary>
		/// Gets system intents.
		/// </summary>
		IEnumerable<CopilotIntentSchema> SystemIntents { get; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Finds agents.
		/// </summary>
		/// <returns>Collection of agent schemas.</returns>
		IEnumerable<CopilotIntentSchema> FindAgents();

		/// <summary>
		/// Finds schema by name.
		/// </summary>
		/// <param name="name">Schema name.</param>
		/// <returns>Intent schema.</returns>
		CopilotIntentSchema FindSchemaByName(string name);

		/// <summary>
		/// Checks if execution is permitted for the intent.
		/// </summary>
		/// <param name="intentId">Intent identifier.</param>
		/// <returns><c>true</c> if execution is permitted; otherwise - <c>false</c>.</returns>
		bool HasExecutionPermitted(Guid? intentId);

		/// <summary>
		/// Gets intent caption by intent identifier.
		/// </summary>
		/// <param name="rootIntentId">Root intent identifier.</param>
		/// <returns>Intent caption.</returns>
		string GetIntentCaptionByIntentId(Guid? rootIntentId);

		/// <summary>
		/// Gets sub-intents.
		/// </summary>
		/// <param name="intentUid">Intent schema unique identifier.</param>
		/// <returns>Collection of sub-intent schemas.</returns>
		IEnumerable<CopilotIntentSchema> GetSubIntents(Guid intentUid);

		/// <summary>
		/// Gets system intent actions meta items.
		/// </summary>
		/// <returns>Collection of action meta items.</returns>
		IEnumerable<CopilotActionMetaItem> GetSystemIntentActionsMetaItems();

		/// <summary>
		/// Finds schema by unique identifier.
		/// </summary>
		/// <param name="schemaUid">Schema unique identifier.</param>
		/// <returns>Intent schema.</returns>
		CopilotIntentSchema FindSchemaByUId(Guid schemaUid);

		/// <summary>
		/// Gets system sub-intents.
		/// </summary>
		/// <param name="intentId">Intent identifier.</param>
		/// <returns>Collection of system sub-intent schemas.</returns>
		IEnumerable<CopilotIntentSchema> GetSystemSubIntents(Guid? intentId);

		/// <summary>
		/// Gets actions from intent system sub-intents.
		/// </summary>
		/// <param name="intentId">Intent identifier.</param>
		/// <returns>Collection of action meta items.</returns>
		IEnumerable<CopilotActionMetaItem> GetActionsFromIntentSystemSubIntents(Guid? intentId);

		/// <summary>
		/// Gets actions meta items by intent.
		/// </summary>
		/// <param name="intent">Intent schema.</param>
		/// <returns>Collection of action meta items.</returns>
		IEnumerable<CopilotActionMetaItem> GetActionsMetaItemsByIntent(CopilotIntentSchema intent);

		/// <summary>
		/// Gets system actions meta items for use page context.
		/// </summary>
		/// <returns>Collection of action meta items.</returns>
		IEnumerable<CopilotActionMetaItem> GetSystemActionsMetaItemsForUsePageContext();

		/// <summary>
		/// Finds agents for chat.
		/// </summary>
		/// <param name="excludeIntentId">Exclude intent identifier.</param>
		/// <returns>Collection of agent schemas.</returns>
		IEnumerable<CopilotIntentSchema> FindAgentsForChat(Guid? excludeIntentId);

		/// <summary>
		/// Gets actions meta items by intent identifier.
		/// </summary>
		/// <param name="intentId">Intent identifier.</param>
		/// <returns>Collection of action meta items.</returns>
		IEnumerable<CopilotActionMetaItem> GetActionsMetaItemsByIntent(Guid? intentId);

		/// <summary>
		/// Gets sub-intents for chat.
		/// </summary>
		/// <param name="intentId">Intent identifier.</param>
		/// <param name="excludeIntentId">Exclude intent identifier.</param>
		/// <returns>Collection of sub-intent schemas.</returns>
		IEnumerable<CopilotIntentSchema> GetSubIntentsForChat(Guid? intentId, Guid? excludeIntentId);

		#endregion
	}

	#endregion

}
