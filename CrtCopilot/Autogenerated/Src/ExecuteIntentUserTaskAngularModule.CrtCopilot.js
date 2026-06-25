define("ExecuteIntentUserTaskAngularModule", ["mf!ProcessDesignerPropertiesPageComponent", "css-ltr!ProcessDesignerPropertiesPageComponentStyles"],
	function() {
		Ext.define("Terrasoft.ExecuteIntentUserTaskAngularModule", {
			alternateClassName: "Terrasoft.ExecuteIntentUserTaskAngularModule",
			extend: "Terrasoft.BaseModule",

			_customEvent: null,
			_angularHostEl: null,
			_adapter: null,

			/**
			 * @private
			 * @type {Function|null}
			 */
			_formulaEditorCallback: null,

			/**
			 * @private
			 */
			_getMessages: function() {
				return {
					"GetExecuteIntentAngularAdapter": {
						direction: Terrasoft.MessageDirectionType.PUBLISH,
						mode: Terrasoft.MessageMode.PTP
					},
					"ExecuteIntentPropertiesPageFocused": {
						direction: Terrasoft.MessageDirectionType.SUBSCRIBE,
						mode: Terrasoft.MessageMode.PTP
					}
				};
			},

			/**
			 * @inheritdoc Terrasoft.configuration.BaseModule#init
			 * @override
			 */
			init: function() {
				this._customEvent = Ext.create("Terrasoft.CustomEventDomMixin");
				this._customEvent.init(this.sandbox && this.sandbox.id);

				this.sandbox.registerMessages(this._getMessages());
				this.sandbox.subscribe("ExecuteIntentPropertiesPageFocused", this._onPropertiesPageFocused, this, [this._getParentSandboxId()]);

				this._adapter = this._getAdapter();

				this._customEvent.subscribeHandler("GetExecuteIntentUserTaskPanelState", this._onGetPanelState, this);
				this._customEvent.subscribeHandler("GetExecuteIntentUserTaskIntentParameters", this._onGetIntentParameters, this);
				this._customEvent.subscribeHandler("GetExecuteIntentUserTaskIntentFiles", this._onGetIntentFiles, this);
				this._customEvent.subscribeHandler("ExecuteIntentUserTaskSchemaChanged", this._onSchemaChanged, this);
				this._customEvent.subscribeHandler("ExecuteIntentUserTaskSchemaValidate", this._onSchemaValidated, this);
				this._customEvent.subscribeHandler("HidePropertyPage", this._onHidePropertyPage, this);
				this._customEvent.subscribeHandler("ShowFormulaEditor", this._onShowFormulaEditor, this);
				this._customEvent.subscribeHandler("CanChangeSchema", this._onCanChangeSchema, this);
				this._customEvent.subscribeHandler("CreateFile", this._createFile, this);
				this._adapter.on("change:caption", this._syncAngularHostState, this);
				this._adapter.$Parameters.on("changed", this._syncAngularHostState, this);
			},

			render: function(renderTo) {
				this._renderAngularPanel(renderTo);
			},

			destroy: function() {
				this._angularHostEl = null;
				if (this._customEvent) {
					this._customEvent.destroy();
					this._customEvent = null;
				}
				this._adapter.un("change:caption", this._syncAngularHostState, this);

			},

			_createFile: function() {
				this._adapter.createFile();
			},

			_getAdapter: function() {
				const adapter = this._resolveAdapterBySandbox();
				return adapter || null;
			},

			_getParentSandboxId: function() {
				const sandboxId = this.sandbox && this.sandbox.id;
				if (!sandboxId) {
					return "";
				}
				const marker = "_module_ExecuteIntentUserTaskAngularModule";
				const markerIndex = sandboxId.lastIndexOf(marker);
				if (markerIndex > 0) {
					return sandboxId.substring(0, markerIndex);
				}
				return sandboxId;
			},

			_resolveAdapterBySandbox: function() {
				if (!this.sandbox || !this.sandbox.publish) {
					return null;
				}
				const tags = [];
				const currentId = this.sandbox.id;
				const parentId = this._getParentSandboxId();
				if (currentId) {
					tags.push(currentId);
				}
				if (parentId && parentId !== currentId) {
					tags.push(parentId);
				}
				for (let i = 0; i < tags.length; i++) {
					const adapter = this.sandbox.publish("GetExecuteIntentAngularAdapter", null, [tags[i]]);
					if (adapter) {
						return adapter;
					}
				}
				return null;
			},

			_getCurrentIntentSchemaUId: function() {
				return this._adapter.get("IntentSchemaUId") || Terrasoft.GUID_EMPTY;
			},

			_getAngularPanelConfig: function() {
				const elementName = this._adapter.get("name");
				const elementCaption = this._adapter.get("caption");
				const intentSchemaUId = this._adapter.get("IntentSchemaUId") ?? null;
				return {
					forceReload: true,
					name: this._adapter.get("name"),
					type: "crt.ExecuteIntentUserTaskPropertiesPanel",
					itemConfig: {
						mode: "custom",
						name: elementName,
						caption: elementCaption,
						intentSchemaUId,
						visible: true
					}
				};
			},

			_syncAngularHostState: function() {
				if (!this._angularHostEl) {
					return;
				}
				this._angularHostEl.config = this._getAngularPanelConfig();
			},

			_getNormalizedIntentSchemaUId: function(payload) {
				if (Ext.isString(payload)) {
					return payload || Terrasoft.GUID_EMPTY;
				}
				const schemaUId = payload && (payload.intentSchemaUId || payload.intentSchemaUid || payload.IntentSchemaUId);
				return schemaUId || Terrasoft.GUID_EMPTY;
			},

			_onGetPanelState: function() {
				const state = this._adapter && this._adapter.getExecuteIntentAngularPanelState
					? this._adapter.getExecuteIntentAngularPanelState()
					: {
						intentSchemaUId: this._getCurrentIntentSchemaUId(),
						useHostNavigation: true
					};
				this._customEvent.publish("GetExecuteIntentUserTaskPanelState", state);
			},

			_onGetIntentParameters: function(payload) {
				const schemaUId = this._getNormalizedIntentSchemaUId(payload);
				if (this._adapter && this._adapter.getExecuteIntentAngularIntentParameters) {
					this._adapter.getExecuteIntentAngularIntentParameters(function(response) {
						this._customEvent.publish("GetExecuteIntentUserTaskIntentParameters", response || { success: false });
					}, this);
					return;
				}
				if (!this._adapter || !this._adapter._getIntentParameters) {
					this._customEvent.publish("GetExecuteIntentUserTaskIntentParameters", { success: false });
					return;
				}
				this._adapter._getIntentParameters(schemaUId, function(response) {
					this._customEvent.publish("GetExecuteIntentUserTaskIntentParameters", response || { success: false });
				}, this);
			},

			_onGetIntentFiles: function(payload) {
				this._adapter.getExecuteIntentAngularIntentFiles(function(response) {
					this._customEvent.publish("GetExecuteIntentUserTaskIntentFiles", response);
				}, this);
			},

			/**
			 * @param {Object} payload
			 * @private
			 */
			_onShowFormulaEditor: function(payload) {
				const parameterName = payload.parameterName;
				const mappingType = payload.mappingType;
				if (!this._adapter.openFormulaEditor(parameterName, mappingType)) {
					this._customEvent.publish("ShowFormulaEditor", { success: false });
					return;
				}
				this._formulaEditorCallback = function() {
					const parameterValue = this._adapter.getIntentParameterValue(parameterName) || this._adapter.getFileParameterValue(parameterName);
					const resultValue = {
						value: parameterValue.value,
						displayValue: parameterValue.displayValue
					};
					this._customEvent.publish("ShowFormulaEditor", { value: resultValue, success: true });
				};
			},

			/**
			 * @private
			 */
			_onPropertiesPageFocused: function() {
				if (Ext.isFunction(this._formulaEditorCallback)) {
					try {
						Ext.callback(this._formulaEditorCallback, this);
					} finally {
						this._formulaEditorCallback = null;
					}
				}
			},

			_onSchemaChanged: function(payload) {
				for (const item of payload) {
					switch (item.path) {
						case "itemConfig.name":
							this._adapter.set("name", item.value);
							break
						case "itemConfig.caption":
							this._adapter.set("caption", item.value);
							break;
						case "itemConfig.intentSchemaUId":
							const intentSchemaUId = this._getNormalizedIntentSchemaUId(item.value);
							this._applyIntentSchemaChange(intentSchemaUId);
							break;
						case "itemConfig.intentParameters":
							this._applyIntentParametersChange(item.value);
							break;
						case "itemConfig.intentFiles":
							this._applyIntentFilesChange(item.value);
						break;
						default:
							return;
					}
				}
			},

			_onSchemaValidated: function(payload) {
				const isValid = this._adapter.validate();
				const errors = [];
				for (const attribute in this._adapter.validationInfo.attributes) {
					if (this._adapter.validationInfo.attributes.hasOwnProperty(attribute)) {
						const attributeInfo = this._adapter.validationInfo.attributes[attribute];
						if (attributeInfo && !attributeInfo.isValid) {
							errors.push({
								attribute,
								message: attributeInfo.invalidMessage
							});
						}
					}
				}
				this._customEvent.publish("ExecuteIntentUserTaskSchemaValidated", {isValid,errors,});
			},

			_applyIntentSchemaChange: function(schemaUId) {
				this._adapter.applyExecuteIntentAngularSchemaChanged(schemaUId, function() {
					this._syncAngularHostState();
				}, this);
			},

			_applyIntentParametersChange: function(intentParameters) {
				if (Ext.isArray(intentParameters)) {
					intentParameters.forEach((parameter) => {
						const parameterValue = parameter.value;
						// TODO: support updating parameter value in ExtJS context when it's provided by Angular component
						if (!parameterValue.value && !parameterValue.displayValue) {
							this._adapter.clearIntentParameterValue(parameter.name);
						}
					});
				}
			},

			_applyIntentFilesChange: function(intentFiles) {
				if (Ext.isArray(intentFiles)) {
					intentFiles.forEach((parameter) => {
						const parameterValue = parameter.value;
						// TODO: support updating parameter value in ExtJS context when it's provided by Angular component
						if (!parameterValue.value && !parameterValue.displayValue) {
							this._adapter.clearIntentFilesValue(parameter.name);
						}
					});
				}
			},
			
			_renderAngularPanel: function(renderTo) {
				const containerElement = renderTo && renderTo.dom ? renderTo.dom : null;
				if (!containerElement) {
					return;
				}
				if (!this._angularHostEl) {
					this._angularHostEl = document.createElement("crt-execute-intent-usertask-properties-panel-component");
				}
				if (this._angularHostEl.parentElement !== containerElement) {
					containerElement.appendChild(this._angularHostEl);
				}
				this._syncAngularHostState();
			},

			_onHidePropertyPage: function() {
				this._adapter.onHidePropertyPage();
			},

			_onCanChangeSchema: function() {
				this._adapter.canChangeSchema((canChange) => {
					this._customEvent.publish("CanChangeSchema", Boolean(canChange));
				});
			},

		});

		return Terrasoft.ExecuteIntentUserTaskAngularModule;
	}
);
