define("WorkflowAgentSchemaElementsInitializer", [
	"ext-base",
	"terrasoft",
	"WorkflowAgentSchemaElementsInitializerResources",
], function(resources) {
	Ext.define("WorkflowAgentSchemaElementsInitializer", {
		className: "Terrasoft.manager.WorkflowAgentSchemaElementsInitializer",
		extend: "Terrasoft.ProcessSchemaElementsInitializer",
		alternateClassName: "Terrasoft.WorkflowAgentSchemaElementsInitializer",

		/**
		 * @private
		 */
		_createParameter: function(config) {
			const createdInSchemaUId = Terrasoft.WorkflowAgentSchemaManager.defSchemaUId;
			const parameterConfig = {
				...config,
				uId: Terrasoft.generateGUID(),
				sourceValue: {
					source: Terrasoft.ProcessSchemaParameterValueSource.None,
					isValid: true
				},
				createdInSchemaUId: createdInSchemaUId,
				modifiedInSchemaUId: createdInSchemaUId
			};
			return Ext.create("Terrasoft.ProcessSchemaParameter", parameterConfig);
		},

		/**
		 * @private
		 */
		_createParameters: function(schema) {
			const parameter = this._createParameter({
				name: "CopilotWorkflowRootSessionId",
				caption: "Copilot workflow root session id",
				description: "Creatio.ai Copilot chat session ID used to link workflow execution to a specific user conversation",
				direction: Terrasoft.ProcessSchemaParameterDirection.IN,
				dataValueType: Terrasoft.DataValueType.GUID,
				isRequired: false,
			});
			schema.parameters.add(parameter.uId, parameter);
		},

		/**
		 * Sets schema properties with default values.
		 * @private
		 * @param {Terrasoft.WorkflowAgentSchema} schema Schema instance.
		 */
		_setSchemaProperties: function(schema) {
			schema.setPropertyValue("useUserPermissions", true);
			schema.setPropertyValue("useSystemSecurityContext", false);
		},

		/**
		 * @inheritdoc Terrasoft.ProcessSchemaElementsInitializer#initializeSchema
		 * @override
		 */
		initializeSchema: function(schema) {
			this.callParent(arguments);
			this._setSchemaProperties(schema);
			this._createParameters(schema);
		},
	});

	return Terrasoft.WorkflowAgentSchemaElementsInitializer;
});
