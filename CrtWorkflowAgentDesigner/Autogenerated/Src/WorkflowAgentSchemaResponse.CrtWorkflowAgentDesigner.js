define("WorkflowAgentSchemaResponse", ["ext-base", "terrasoft", "WorkflowAgentSchema"], function(Ext, Terrasoft) {
	/**
	 * @class Terrasoft.data.queries.WorkflowAgentSchemaResponse
	 * Workflow Agent schema response class.
	 */
	Ext.define("Terrasoft.data.queries.WorkflowAgentSchemaResponse", {
		extend: "Terrasoft.ProcessSchemaResponse",
		alternateClassName: "Terrasoft.WorkflowAgentSchemaResponse",

		//region Properties: Protected

		/**
		 * @inheritdoc Terrasoft.BaseSchemaResponse#schemaClassName
		 * @overridden
		 */
		schemaClassName: "Terrasoft.WorkflowAgentSchema",

		//endregion

		//region Methods: Protected

		//endregion
	});

	return Terrasoft.data.queries.WorkflowAgentSchemaResponse;
});
