define("WorkflowDesigner", ["ext-base", "terrasoft", "SchemaDesigner", "WorkflowAgentSchemaDesigner", "css!WorkflowDesigner"],
	function(Ext, Terrasoft) {
		/**
		 * @class Terrasoft.configuration.WorkflowDesigner
		 */
		Ext.define("Terrasoft.configuration.WorkflowDesigner", {
			extend: "Terrasoft.SchemaDesigner",
			alternateClassName: "Terrasoft.WorkflowDesigner",

			/**
			 * @inheritdoc Terrasoft.SchemaDesigner#initDesigner
			 * @overridden
			 */
			initDesigner: function(config) {
				this.callParent(arguments);
				var params = this.parseHash(config.hash);
				this.designer = Ext.create("Terrasoft.WorkflowAgentSchemaDesigner", {
					sandbox: this.sandbox,
					schemaUId: params.id,
					packageUId: params.packageUId
				});
			}
		});

		return Terrasoft.configuration.WorkflowDesigner;
	}
);
