define("CopilotAgentSchemaUpdateRequest", ["terrasoft"], function() {
	Ext.define("CopilotAgentSchemaUpdateRequest", {
		className: "Terrasoft.data.queries.CopilotAgentSchemaUpdateRequest",
		extend: "Terrasoft.BaseCoreRequest",
		alternateClassName: "Terrasoft.CopilotAgentSchemaUpdateRequest",

		//region Properties: Protected

		/**
		 * @inheritdoc Terrasoft.core.BaseCoreRequest#serviceName
		 * @override
		 */
		serviceName: "CopilotIntentSchemaDesignerService.svc",

		/**
		 * @inheritdoc Terrasoft.core.BaseCoreRequest#serviceMethod
		 * @override
		 */
		serviceMethod: "SaveSchema",

		/**
		 * @inheritdoc Terrasoft.BaseCoreRequest#responseClassName
		 * @overridden
		 */
		responseClassName: "Terrasoft.CopilotAgentSchemaResponse",

		/**
		 * Schema instance.
		 * @type {Terrasoft.CopilotAgentSchema}
		 */
		schema: null,

		/**
		 * @inheritdoc Terrasoft.BaseCoreRequest#getSerializableObject
		 * @overridden
		 */
		getSerializableObject: function(serializableObject) {
			this.callParent(arguments);
			Ext.apply(serializableObject, this.schema.getSerializableObject(serializableObject));
		}

	});

	return Terrasoft.CopilotAgentSchemaUpdateRequest;
});