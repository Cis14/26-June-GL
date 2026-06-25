define("AccessRightsDetailComponent", ["BaseSchemaViewModule"], function() {
	/**
	 * Component for rendering Access Rights Detail.
	 */
	Ext.define("Terrasoft.controls.AccessRightsDetailComponent", {
		extend: "Terrasoft.controls.Component",
		alternateClassName: "Terrasoft.AccessRightsDetailComponent",

		/**
		 * @public
		 * @type {String}
		 */
		rightsSchemaName: null,

		/**
		 * @public
		 * @type {String}
		 */
		recordId: null,

		/**
		 * @public
		 * @type {Number}
		 */
		accessRightsType: null,

		/**
		 * @public
		 * @type {Number}
		 */
		accessRightsValue: null,

		/**
		 * @inheritDoc
		 * @override
		 */
		getBindConfig: function() {
			const bindConfig = this.callParent(arguments);
			return Ext.apply(bindConfig, {
				rightsSchemaName: {
					changeMethod: "setRightsSchemaName"
				},
				recordId: {
					changeMethod: "setRecordId"
				},
				accessRightsType: {
					changeMethod: "setAccessRightsType"
				},
				accessRightsValue: {
					changeMethod: "setAccessRightsValue",
					changeEvent: "accessRightsValueChanged"
				},
			});
		},

		/**
		 * @inheritDoc
		 * @override
		 */
		tpl: [
			/*jshint white:false */
			/*jshint quotmark:true */
			//jscs:disable
			'<div id="{id}-wrap" style="{styles}">',
			'<crt-page-component id="{id}" schema="AccessRightsDetail"></crt-page-component>',
			'</div>',
			//jscs:enable
			/*jshint quotmark:false */
			/*jshint white:true */
		],

		/**
		 * @param {String} rightsSchemaName
		 */
		setRightsSchemaName: function(rightsSchemaName) {
			this.rightsSchemaName = rightsSchemaName;
			this._updateParameters();
		},

		/**
		 * @param {String} recordId
		 */
		setRecordId: function(recordId) {
			this.recordId = recordId;
			this._updateParameters();
		},

		/**
		 * @param {Number} accessRightsType
		 */
		setAccessRightsType: function(accessRightsType) {
			this.accessRightsType = accessRightsType;
			this._updateParameters();
		},

		/**
		 * @param {Number} accessRightsValue
		 */
		setAccessRightsValue: function(accessRightsValue) {
			this.accessRightsValue = accessRightsValue;
			this._updateParameters();
		},

		/**
		 * @private
		 */
		_updateParameters: function() {
			if (this.rendered) {
				this.widgetEl.dom.parameters = {
					"RightsSchemaName": this.rightsSchemaName,
					"RecordId": this.recordId,
					"AccessRightsType": this.accessRightsType,
					"AccessRightsValue": this.accessRightsValue,
					"ShowExpands": false,
				};
			}
		},

		/**
		 * @private
		 */
		_subscribeToParametersChange: function() {
			const widget = this.widgetEl.dom;
			if (widget) {
				this._parametersChangeHandler = this._onParametersChange.bind(this);
				widget.addEventListener("parametersChange", this._parametersChangeHandler);
			}
		},

		/**
		 * @private
		 * @param {Object} event
		 */
		_onParametersChange: function(event) {
			if (!event) {
				return;
			}
			const parameters = event.detail || event;
			let needUpdate = false;
			Terrasoft.each(parameters, function(value, key) {
				if (key === "AccessRightsValue") {
					if (this.accessRightsValue !== value) {
						this.accessRightsValue = value;
						this.fireEvent("accessRightsValueChanged", this.accessRightsValue);
						needUpdate = true;
					}
				} else {
					const propertyName = key.charAt(0).toLowerCase() + key.substring(1);
					if (this.hasOwnProperty(propertyName) && this[propertyName] !== value) {
						this[propertyName] = value;
						needUpdate = true;
					}
				}
			}, this);
			if (needUpdate) {
				this._updateParameters();
			}
		},

		/**
		 * @inheritDoc
		 * @override
		 */
		onAfterRender: function() {
			this.callParent(arguments);
			this._updateParameters();
			this._subscribeToParametersChange();
		},

		/**
		 * @inheritDoc
		 * @override
		 */
		onDestroy: function() {
			if (this._parametersChangeHandler && this.widgetEl && this.widgetEl.dom) {
				this.widgetEl.dom.removeEventListener("parametersChange", this._parametersChangeHandler);
			}
			this.callParent(arguments);
		},

		/**
		 * @inheritDoc
		 * @override
		 */
		getTplData: function() {
			const tplData = this.callParent(arguments);
			this.selectors = this.getSelectors();
			return tplData;
		},

		/**
		 * @inheritDoc
		 * @override
		 */
		getSelectors: function() {
			return {
				wrapEl: "#" + this.id + "-wrap",
				widgetEl: "#" + this.id
			};
		},

	});

	return Terrasoft.AccessRightsDetailComponent;
});
