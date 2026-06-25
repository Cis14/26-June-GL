define("CopilotAgentSchema", ["ext-base", "terrasoft"],
	function(Ext, Terrasoft) {
		/**
		 * @class Terrasoft.Designers.CopilotAgentSchema
		 * Copilot agent schema class.
		 */
		Ext.define("Terrasoft.Designers.CopilotAgentSchema", {
			extend: "Terrasoft.manager.BaseSchema",
			alternateClassName: "Terrasoft.CopilotAgentSchema",

			// region Properties: Private

			_localizableProperties: ["caption", "description"],

			// endregion

			// region Properties: Protected

			/**
			 * Serializable properties list.
			 * @protected
			 * @type {String[]}
			 */
			serializableProperties: ["uId", "isReadOnly", "useFullHierarchy", "userLevelSchema", "prompt",
				"intentDescription", "roleDescription", "restrictions", "llmModel", "usePageContext", "useChatHistory",
				"workflowOnly", "type", "status", "extendParent", "mode", "responseFormatJsonSchema", "id", "body",
				"dependencies", "forceSave", "isFullHierarchyDesignSchema", "name", "caption", "description",
				"useDefaultSystemPrompt"],

			/**
			 * Schema id.
			 * @type {String}
			 */
			id: null,

			/**
			 * Package information.
			 * @type {Object}
			 */
			package: null,

			/**
			 * Schema body.
			 * @type {String}
			 */
			body: null,

			/**
			 * Is read only flag.
			 * @type {Boolean}
			 */
			isReadOnly: false,

			/**
			 * Dependencies.
			 * @type {Array}
			 */
			dependencies: null,

			/**
			 * Use full hierarchy flag.
			 * @type {Boolean}
			 */
			useFullHierarchy: false,

			/**
			 * Force save flag.
			 * @type {Boolean}
			 */
			forceSave: false,

			/**
			 * User level schema flag.
			 * @type {Boolean}
			 */
			userLevelSchema: false,

			/**
			 * Is full hierarchy design schema flag.
			 * @type {Boolean}
			 */
			isFullHierarchyDesignSchema: false,

			/**
			 * Localizable strings collection.
			 * @type {Array}
			 */
			localizableStrings: null,

			/**
			 * Addon types.
			 * @type {Array}
			 */
			addonTypes: null,

			/**
			 * Prompt text.
			 * @type {String}
			 */
			prompt: null,

			/**
			 * Intent description.
			 * @type {Terrasoft.LocalizableString}
			 */
			intentDescription: null,

			/**
			 * Role description.
			 * @type {Terrasoft.LocalizableString}
			 */
			roleDescription: null,

			/**
			 * Restrictions.
			 * @type {Terrasoft.LocalizableString}
			 */
			restrictions: null,

			/**
			 * LLM model name.
			 * @type {String}
			 */
			llmModel: null,

			/**
			 * Use page context flag.
			 * @type {Boolean}
			 */
			usePageContext: false,

			/**
			 * Use chat history flag.
			 * @type {Boolean}
			 */
			useChatHistory: false,

			/**
			 * Workflow only flag.
			 * @type {Boolean}
			 */
			workflowOnly: false,

			/**
			 * Sub intents collection.
			 * @type {Array}
			 */
			subIntents: null,

			/**
			 * Actions collection.
			 * @type {Array}
			 */
			actions: null,

			/**
			 * Agent type.
			 * @type {String}
			 */
			type: null,

			/**
			 * Agent status.
			 * @type {String}
			 */
			status: null,

			/**
			 * Agent mode.
			 * @type {String}
			 */
			mode: null,

			/**
			 * Input parameters collection.
			 * @type {Array}
			 */
			inputParameters: null,

			/**
			 * Output parameters collection.
			 * @type {Array}
			 */
			outputParameters: null,

			/**
			 * Response format JSON schema.
			 * @type {Object}
			 */
			responseFormatJsonSchema: null,

			/**
			 * Workflow configuration.
			 * @type {Object}
			 */
			workflow: null,

			/**
			 * Optional properties.
			 * @type {Array}
			 */
			optionalProperties: null,

			/**
			 * Use default system prompt flag.
			 * @type {Boolean}
			 */
			useDefaultSystemPrompt: false,

			/**
			 * Parent schema reference.
			 * @type {Terrasoft.Designers.CopilotAgentSchema}
			 */
			parentSchema: null,

			// endregion

			// region Methods: Private

			_serializeLocalizableStringProperty: function(serializableObject, propertyName) {
				if (this[propertyName] instanceof Terrasoft.LocalizableString) {
					const cultureValues = this[propertyName].cultureValues;
					if (cultureValues && Object.keys(cultureValues).length > 0) {
						const values = [];
						Object.entries(cultureValues).forEach(function([cultureName, value]) {
							values.push({
								cultureName: cultureName,
								value: value
							});
						});
						serializableObject[propertyName] = values;
					} else {
						serializableObject[propertyName] = [];
					}
				} else {
					serializableObject[propertyName] = [];
				}
			},

			// endregion

			// region Methods: Protected

			/**
			 * Initializes localizable string values for FreedomUI format.
			 * @param {String} propertyName Property name.
			 * @param {Array|Object} value Property value.
			 * @param {Object} [value] Localizable string values.
			 * @param {String} [value.cultureName] Culture name.
			 * @param {String} [value.value] String value.
			 */
			initFreedomUILocalizableStringValue: function(propertyName, value) {
				value = value || [];
				if (!Terrasoft.isInstanceOfClass(value, "Terrasoft.LocalizableString")) {
					let cultureValues = {};
					if (Ext.isArray(value)) {
						Terrasoft.each(value, function(item) {
							if (item.cultureName && Ext.isDefined(item.value)) {
								cultureValues[item.cultureName] = item.value;
							}
						}, this);
					} else if (value.cultureName && Ext.isDefined(value.value)) {
						cultureValues[value.cultureName] = value.value;
					} else {
						cultureValues = value;
					}
					value = Ext.create("Terrasoft.LocalizableString", {
						cultureValues: cultureValues
					});
				}
				this[propertyName] = value;
			},

			// endregion

			// region Methods: Public

			/**
			 * Applies changes from another intent schema to this instance.
			 * Merges top-level properties (name, caption, mode, type, uId, id, etc.) from the source schema.
			 * @param {Terrasoft.Designers.CopilotAgentSchema|Object} sourceSchema Source schema to merge from.
			 * @return {Terrasoft.Designers.CopilotAgentSchema} This instance with merged properties.
			 */
			applyChanges: function(sourceSchema) {
				if (!sourceSchema) {
					return this;
				}
				const propertiesToMerge = [
					"uId", "name", "packageUId", "prompt", "llmModel",
					"usePageContext", "useChatHistory", "workflowOnly", "type", "status", "mode",
					"id", "intentDescription",
				];
				Terrasoft.each(propertiesToMerge, function(propertyName) {
					if (sourceSchema.hasOwnProperty(propertyName) && 
						!Ext.isEmpty(sourceSchema[propertyName])) {
						this[propertyName] = sourceSchema[propertyName];
					}
				}, this);
				Terrasoft.each(this._localizableProperties, function(propertyName) {
					if (sourceSchema.hasOwnProperty(propertyName)) {
						this.initFreedomUILocalizableStringValue(propertyName, sourceSchema[propertyName]);
					}
				}, this);
				if (sourceSchema.package) {
					this.package = sourceSchema.package;
				}
				if (sourceSchema.workflow) {
					this.workflow = sourceSchema.workflow;
				}
				return this;
			},

			/**
			 * @inheritdoc Terrasoft.BaseObject#constructor
			 * @override
			 */
			constructor: function(config) {
				this.callParent(arguments);
				this.initFreedomUILocalizableStringValue("caption", config && config.caption);
				this.initFreedomUILocalizableStringValue("roleDescription", config && config.roleDescription);
				this.initFreedomUILocalizableStringValue("restrictions", config && config.restrictions);
				this.description = (config && config.description) ?? [];
				this.intentDescription = config && config.intentDescription;
				this.localizableStrings = (config && config.localizableStrings) || [];
				this.addonTypes = (config && config.addonTypes) || [];
				this.subIntents = (config && config.subIntents) || [];
				this.actions = (config && config.actions) || [];
				this.inputParameters = (config && config.inputParameters) || [];
				this.outputParameters = (config && config.outputParameters) || [];
				this.optionalProperties = (config && config.optionalProperties) || [];
				this.dependencies = (config && config.dependencies) || null;
				this.useDefaultSystemPrompt = (config && config.useDefaultSystemPrompt) || false;
				if (config) {
					this.id = config.id || null;
					this.package = config.package || null;
					this.body = config.body || null;
					this.prompt = config.prompt || null;
					this.llmModel = config.llmModel || null;
					this.type = config.type || null;
					this.status = config.status || null;
					this.mode = config.mode || null;
				}
				this.isReadOnly = (config && config.isReadOnly) || false;
				this.usePageContext = (config && config.usePageContext) || false;
				this.useChatHistory = (config && config.useChatHistory) || false;
				this.workflowOnly = (config && config.workflowOnly) || false;
				this.useFullHierarchy = (config && config.useFullHierarchy) || false;
				this.forceSave = (config && config.forceSave) || false;
				this.userLevelSchema = (config && config.userLevelSchema) || false;
				this.isFullHierarchyDesignSchema = (config && config.isFullHierarchyDesignSchema) || false;
				this.responseFormatJsonSchema = (config && config.responseFormatJsonSchema) || null;
				this.workflow = (config && config.workflow) || null;
				if (config && config.parentSchema) {
					if (Terrasoft.isInstanceOfClass(config.parentSchema, "Terrasoft.CopilotAgentSchema")) {
						this.parentSchema = config.parentSchema;
					} else {
						this.parentSchema = Ext.create("Terrasoft.CopilotAgentSchema", config.parentSchema);
					}
				}
			},

			/**
			 * @inheritdoc Terrasoft.manager.BaseSchema#getSerializableObject
			 * @override
			 */
			getSerializableObject: function(serializableObject) {
				this.callParent(arguments);
				this._serializeLocalizableStringProperty(serializableObject, "caption");
				this._serializeLocalizableStringProperty(serializableObject, "description");
				if (this.workflow) {
					serializableObject.workflow = this.workflow;
				}
				if (this.parentSchema) {
					const parentSchema = {};
					this.parentSchema.getSerializableObject(parentSchema);
					serializableObject.parentSchema = parentSchema;
				}
				serializableObject.addonTypes = this.addonTypes;
				serializableObject.localizableStrings = this.localizableStrings;
				serializableObject.subIntents = this.subIntents;
				serializableObject.actions = this.actions;
				serializableObject.inputParameters = this.inputParameters;
				serializableObject.outputParameters = this.outputParameters;
				serializableObject.optionalProperties = this.optionalProperties;
				if (this.package) {
					serializableObject.package = {
						name: this.package.name,
						uId: this.package.uId,
						type: this.package.type,
					};
				}
			},

			// endregion

		});

		return Terrasoft.CopilotAgentSchema;
	}
);