define("WorkflowAgentSchemaManager", [
	"ext-base",
	"terrasoft",
	"WorkflowAgentSchemaManagerResources",
	"WorkflowAgentSchemaManagerItem",
	"WorkflowAgentSchema",
	"WorkflowAgentSchemaElementsInitializer",
], function(Ext, Terrasoft) {

	/**
	 * Workflow Agent schema manager class.
	 */
	Ext.define("Terrasoft.manager.WorkflowAgentSchemaManager", {
		extend: "Terrasoft.BaseProcessSchemaManager",
		alternateClassName: "Terrasoft.WorkflowAgentSchemaManager",
		singleton: true,

		/**
		 * @inheritdoc Terrasoft.BaseManager#itemClassName
		 * @overridden
		 */
		itemClassName: "Terrasoft.WorkflowAgentSchemaManagerItem",

		/**
		 * @inheritdoc Terrasoft.BaseSchemaManager#managerName
		 * @override
		 */
		managerName: "WorkflowAgentSchemaManager",

		/**
		 * @inheritdoc Terrasoft.BaseProcessSchemaManager#schemaElementsInitializerClassName
		 * @override
		 */
		schemaElementsInitializerClassName: "Terrasoft.WorkflowAgentSchemaElementsInitializer",

		/**
		 * @inheritdoc Terrasoft.BaseSchemaManager#defSchemaUId
		 * @override
		 */
		defSchemaUId: "bb4d6607-026b-4b27-b640-8f5c77c1e89d",

		/**
		 * @inheritdoc Terrasoft.BaseProcessSchemaManager#getUniqueNameAndCaption
		 * @override
		 */
		getUniqueNameAndCaption: function(config, callback, scope) {
			const newConfig = Ext.apply({
				namePrefix: "WFAgent",
				captionPrefix: "Workflow Agent",
				managerName: "CopilotIntentSchemaManager"
			}, config);
			this.callParent([newConfig, callback, scope]);
		},

	});

	return Terrasoft.WorkflowAgentSchemaManager;
});