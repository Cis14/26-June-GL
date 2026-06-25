define("WorkflowAgentSchemaDesignerViewModel", ["ext-base", "terrasoft", "WorkflowAgentSchemaDesignerViewModelResources",
		"WorkflowAgentSchemaManager", ],
	function(Ext, Terrasoft) {

		Ext.define("Terrasoft.Designers.WorkflowAgentSchemaDesignerViewModel", {
			extend: "Terrasoft.Designers.ProcessSchemaDesignerViewModelNew",
			alternateClassName: "Terrasoft.WorkflowAgentSchemaDesignerViewModel",

			schemaManager: Terrasoft.WorkflowAgentSchemaManager,

			/**
			 * @inheritdoc Terrasoft.Designers.ProcessSchemaDesignerViewModel#urlHashPrefix
			 * @override
			 */
			urlHashPrefix: "agent",

			/**
			 * @inheritdoc Terrasoft.Designers.BaseProcessSchemaDesignerViewModel#getIsNeedReplaceUrl
			 * @returns {boolean}
			 */
			getIsNeedReplaceUrl: function() {
				const schema = this.get("Schema");
				const workflow = schema.agent;
				return workflow && workflow.uId;
			},

			/**
			 * @inheritdoc Terrasoft.Designers.BaseProcessSchemaDesignerViewModel#getNewDesignerUrl
			 * @returns {string}
			 */
			getNewDesignerUrl: function() {
				const schema = this.get("Schema");
				const workflow = schema.agent;
				const schemaUId = workflow.uId;
				return "#" + this.urlHashPrefix + "/" + schemaUId;
			},

			/**
			 * Sets diagram process caption.
			 * @param {String} caption Process caption.
			 * @protected
			 */
			onPropertiesPageCaptionChanged: function(caption) {
				this.set("SchemaCaption", caption);
			},

			/** Initializes schema caption
			 * @protected
			 * @param {Terrasoft.WorkflowAgentSchema} schema Schema instance
			 */
			initializeSchemaCaption: function(schema) {
				this.set("SchemaCaption", schema.agent?.caption?.getValue());
			},

			/**
			 * Title change event process handler. Sets a new value in the process diagram.
			 * @protected
			 * @param {Object} model View model.
			 * @param {String} caption Caption.
			 */
			onSchemaCaptionChange: function(model, caption) {
				const schema = this.get("Schema");
				schema.workflow?.setLocalizableStringPropertyValue("caption", caption);
				this.sandbox.publish("DiagramPageCaptionChanged", caption);
			},

		});

	});