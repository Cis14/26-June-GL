define("CopilotAgentSchemaRequest", ["ext-base", "terrasoft"], function(Ext, Terrasoft) {
	/**
	 * @class Terrasoft.data.queries.CopilotAgentSchemaRequest
	 * Copilot Agent schema request class.
	 */
	Ext.define("Terrasoft.data.queries.CopilotAgentSchemaRequest", {
		extend: "Terrasoft.BaseCoreRequest",
		alternateClassName: "Terrasoft.CopilotAgentSchemaRequest",

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
		serviceMethod: "GetSchema",

		/**
		 * @inheritdoc Terrasoft.BaseSchemaRequest#responseClassName
		 * @overridden
		 */
		responseClassName: "Terrasoft.CopilotAgentSchemaResponse",

		/**
		 * Schema identifier.
		 * @type {String}
		 */
		uId: null,

		/**
		 * System package Id.
		 * @type {String}
		 */
		packageUId: null,

		//endregion

		/**
		 * @inheritdoc Terrasoft.BaseRequest#constructor.
		 * @override
		 */
		constructor: function(config) {
			this.callParent(arguments);
			this.serviceProvider = Terrasoft.CoreServiceProvider;
		},

		/**
		 * @inheritdoc Terrasoft.Serializable#getSerializableObject.
		 * @protected
		 * @override
		 */
		getSerializableObject: function(serializableObject) {
			serializableObject.schemaUId = this.uId;
			serializableObject.useFullHierarchy = true;
			serializableObject.packageUId = this.packageUId;
		}

	});

	return Terrasoft.data.queries.CopilotAgentSchemaRequest;
});
