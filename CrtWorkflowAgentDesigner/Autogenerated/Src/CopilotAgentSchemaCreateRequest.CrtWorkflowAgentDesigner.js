define("CopilotAgentSchemaCreateRequest", ["terrasoft"], function() {
	Ext.define("CopilotAgentSchemaCreateRequest", {
		className: "Terrasoft.data.queries.CopilotAgentSchemaCreateRequest",
		extend: "Terrasoft.BaseCoreRequest",
		alternateClassName: "Terrasoft.CopilotAgentSchemaCreateRequest",

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
		serviceMethod: "CreateNewSchema",

		/**
		 * @inheritdoc Terrasoft.BaseCoreRequest#responseClassName
		 * @overridden
		 */
		responseClassName: "Terrasoft.CopilotAgentSchemaResponse",

		/**
		 * Schema identifier.
		 */
		schemaUId: null,

		/**
		 * Package identifier.
		 */
		packageUId: null,

		/**
		 * @inheritdoc Terrasoft.Serializable#getSerializableObject.
		 * @protected
		 * @override
		 */
		getSerializableObject: function(serializableObject) {
			serializableObject.schemaUId = this.schemaUId;
			serializableObject.extendParent = false;
			serializableObject.packageUId = this.packageUId;
		}

	});

	return Terrasoft.CopilotAgentSchemaCreateRequest;
});