define("WorkflowAgentSchemaDesigner", ["ext-base", "terrasoft", "WorkflowAgentSchemaDesignerResources",
		"css!WorkflowAgentSchemaDesigner", "WorkflowAgentSchemaManager", "WorkflowAgentSchemaDesignerViewModel"
	], function(Ext) {
		/**
		 * @class Terrasoft.Designers.WorkflowAgentSchemaDesigner
		 */
		Ext.define("Terrasoft.Designers.WorkflowAgentSchemaDesigner", {
			extend: "Terrasoft.Designers.ProcessSchemaDesignerNew",
			alternateClassName: "Terrasoft.WorkflowAgentSchemaDesigner",

			/**
			 * Default package unique identifier.
			 * @type {String}
			 */
			packageUId: null,

			/**
			 * @inheritdoc Terrasoft.BaseProcessSchemaDesigner#designerViewModelClassName
			 * @overridden
			 */
			designerViewModelClassName: "Terrasoft.WorkflowAgentSchemaDesignerViewModel",

			/**
			 * @inheritdoc Terrasoft.BaseProcessSchemaDesigner#createDesignContainer
			 * @overridden
			 */
			getDesignerViewModelConfig: function() {
				var config = this.callParent(arguments);
				config.values.PackageUId = this.packageUId;
				return config;
			},

			/**
			 * @inheritdoc Terrasoft.BaseProcessSchemaDesigner#initDesignerManagers
			 * @overridden
			 */
			initDesignerManagers: function(callback, scope) {
				this.callParent([function() {
					Terrasoft.WorkflowAgentSchemaManager.initialize(callback, scope);
				}, this]);
			},

		});

		return Terrasoft.WorkflowAgentSchemaDesigner;
	}
);

