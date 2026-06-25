define("WorkflowAgentSchemaRequest", ["ext-base", "terrasoft"], function(Ext, Terrasoft) {
	/**
	 * @class Terrasoft.data.queries.DcmSchemaRequest
	 * Workflow Agent schema request class.
	 */
	Ext.define("Terrasoft.data.queries.WorkflowAgentSchemaRequest", {
		extend: "Terrasoft.ProcessSchemaRequest",
		alternateClassName: "Terrasoft.WorkflowAgentSchemaRequest",

		//region Properties: Protected

		/**
		 * @inheritdoc Terrasoft.BaseSchemaRequest#responseClassName
		 * @overridden
		 */
		responseClassName: "Terrasoft.WorkflowAgentSchemaResponse"

		//endregion
	});

	return Terrasoft.data.queries.WorkflowAgentSchemaRequest;
});
