define("CopilotAgentSchemaResponse", ["ext-base", "terrasoft", "CopilotAgentSchema"], function(Ext, Terrasoft) {
	/**
	 * @class Terrasoft.data.queries.CopilotAgentSchemaResponse
	 * Copilot Agent schema response class.
	 */
	Ext.define("Terrasoft.data.queries.CopilotAgentSchemaResponse", {
		extend: "Terrasoft.BaseSchemaResponse",
		alternateClassName: "Terrasoft.CopilotAgentSchemaResponse",

		//region Properties: Protected

		schemaClassName: "Terrasoft.CopilotAgentSchema",

		//endregion

	});

	return Terrasoft.data.queries.CopilotAgentSchemaResponse;
});
