define("WorkflowAgentSchema", ["ext-base", "terrasoft", "WorkflowAgentSchemaResources", 
		"EntitySchemaDesignerUtilities", "PackageAwareEntitySchemaDesignerUtilities", "CopilotAgentSchema"],
	function(Ext, Terrasoft, resources) {
		Ext.define("Terrasoft.Designers.WorkflowAgentSchema", {
			extend: "Terrasoft.manager.ProcessSchema",
			alternateClassName: "Terrasoft.WorkflowAgentSchema",

			/**
			 * @inheritdoc Terrasoft.manager.ProcessSchema#managerName
			 * @overridden
			 */
			managerName: "WorkflowAgentSchemaManager",

			/**
			 * @inheritdoc Terrasoft.manager.BaseProcessSchema#color
			 * @override
			 */
			color: "#00ADFF",

			/**
			 * @inheritdoc Terrasoft.manager.BaseProcessSchema#typeCaption
			 * @override
			 */
			typeCaption: resources.localizableStrings.TypeCaption,

			/**
			 * Agent configuration.
			 * @type {Terrasoft.CopilotAgentSchema}
			 */
			agent: null,

			/**
			 * @inheritdoc Terrasoft.manager.ProcessSchema#getEditPageSchemaName
			 * @override
			 */
			getEditPageSchemaName: function(callback, scope) {
				callback.call(scope, "WorkflowAgentSchemaPropertiesPage");
			},

			/**
			 * @inheritdoc Terrasoft.BaseObject#constructor
			 * @override
			 */
			constructor: function(config) {
				this.callParent(arguments);
				if (config && config.agent) {
					if (Terrasoft.isInstanceOfClass(config.agent, "Terrasoft.CopilotAgentSchema")) {
						this.agent = config.agent;
					} else {
						this.agent = Ext.create("Terrasoft.CopilotAgentSchema", config.agent);
					}
				}
			}
		});

		return Terrasoft.WorkflowAgentSchema;
	}
);