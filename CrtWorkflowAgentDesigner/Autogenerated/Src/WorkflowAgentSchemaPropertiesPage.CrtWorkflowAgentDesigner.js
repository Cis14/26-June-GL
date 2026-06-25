/**
 * Page schema for workflow agent properties.
 * Parent: ProcessSchemaPropertiesPage => BaseProcessSchemaPropertiesPage => ProcessFlowElementPropertiesPage => BaseProcessSchemaElementPropertiesPage
 */
define("WorkflowAgentSchemaPropertiesPage",
	[
		"terrasoft", "WorkflowAgentSchemaPropertiesPageResources", "AccessRightsDetailComponent", 
		"css!WorkflowAgentSchemaPropertiesPage"
	],
	function(Terrasoft, resources) {
		return {
			attributes: {
				"AgentName": {
					dataValueType: Terrasoft.DataValueType.TEXT,
					type: Terrasoft.ViewModelColumnType.VIRTUAL_COLUMN,
					value: null
				},
				"AgentCaption": {
					dataValueType: Terrasoft.DataValueType.TEXT,
					type: Terrasoft.ViewModelColumnType.VIRTUAL_COLUMN,
					value: null
				},
				"AgentDescription": {
					dataValueType: Terrasoft.DataValueType.TEXT,
					type: Terrasoft.ViewModelColumnType.VIRTUAL_COLUMN,
					value: null
				},
				"WorkflowSchemaUId": {
					dataValueType: Terrasoft.DataValueType.GUID,
					type: Terrasoft.ViewModelColumnType.VIRTUAL_COLUMN,
				},
				"AccessRightsValue": {
					dataValueType: Terrasoft.DataValueType.CUSTOM_OBJECT,
					type: Terrasoft.ViewModelColumnType.VIRTUAL_COLUMN,
				},
			},
			modules: {},
			methods: {
				/**
				 * @private
				 */
				_executeAccessRightsComponentEvent: function(eventType, callback, scope) {
					const event = new CustomEvent(
						"TSAccessRightsChangeEvent",
						{
							detail: {
								type: eventType,
								callback: callback.bind(scope),
							}
						}
					);
					document.dispatchEvent(event);
				},

				/**
				 * @private
				 */
				_getIsAccessRightsHidden: function() {
					return this.getIsNewProcess();
				},

				/**
				 * @private
				 */
				_getIsAccessRightsVisible: function() {
					return !this._getIsAccessRightsHidden();
				},

				/**
				 * @inheritdoc ProcessSchemaPropertiesPage#onElementDataLoad.
				 * @overridden
				 */
				onElementDataLoad: function(process, callback, scope) {
					this.callParent([process, function() {
						const agent = process.agent;
						this.set("AgentName", agent.name);
						this.set("AgentCaption", agent.caption.getValue());
						this.set("AgentDescription", agent.intentDescription);
						this.set("WorkflowSchemaUId", agent.uId);
						callback.call(scope);
					}, this]);
				},

				/**
				 * @inheritdoc ProcessSchemaPropertiesPage#saveValues.
				 * @overridden
				 */
				saveValues: function() {
					this.callParent(arguments);
					const process = this.get("ProcessElement");
					const agent = process.agent;
					agent.name = this.get("AgentName");
					agent.setLocalizableStringPropertyValue("caption", this.get("AgentCaption"));
					agent.intentDescription = this.get("AgentDescription");
					if (!this.getIsNewProcess()) {
						this._executeAccessRightsComponentEvent('saveData', Terrasoft.emptyFn, this);
					}
				},

				/**
				 * @inheritdoc BaseProcessSchemaPropertiesPage#onDiagramCaptionChanged.
				 * @overridden
				 */
				onDiagramCaptionChanged: function(schemaCaption) {
					this.set("AgentCaption", schemaCaption);
				},

			},
			diff: /**SCHEMA_DIFF*/[
				{
					"operation": "insert",
					"parentName": "ControlGroup",
					"propertyName": "items",
					"name": "AgentName",
					"values": {
						"wrapClass": ["top-caption-control"],
						"layout": {
							"column": 0,
							"row": 15,
							"colSpan": 24,
							"rowSpan": 1
						}
					}
				},
				{
					"operation": "insert",
					"parentName": "ControlGroup",
					"propertyName": "items",
					"name": "AgentCaption",
					"values": {
						"wrapClass": ["top-caption-control"],
						"layout": {
							"column": 0,
							"row": 16,
							"colSpan": 24,
							"rowSpan": 1
						}
					}
				},
				{
					"operation": "insert",
					"parentName": "ControlGroup",
					"propertyName": "items",
					"name": "AgentDescription",
					"values": {
						"wrapClass": ["top-caption-control"],
						"isRequired": true,
						"layout": {
							"column": 0,
							"row": 17,
							"colSpan": 24,
							"rowSpan": 1
						}
					}
				},
				{
					"operation": "insert",
					"name": "AccessRightsContainer",
					"parentName": "ControlGroup",
					"propertyName": "items",
					"values": {
						"itemType": Terrasoft.ViewItemType.CONTAINER,
						"markerValue": "AccessRightsContainer",
						"wrapClass": ["top-caption-control"],
						"items": [],
						"layout": {
							"column": 0,
							"row": 18,
							"colSpan": 24,
							"rowSpan": 1
						}
					}
				},
				{
					"operation": "insert",
					"name": "RecommendationLabel",
					"parentName": "AccessRightsContainer",
					"propertyName": "items",
					"values": {
						"itemType": Terrasoft.ViewItemType.LABEL,
						"caption": {"bindTo": "Resources.Strings.AccessRightsTitle"},
						"classes": {
							"labelClass": ["t-title-label-proc"]
						}
					}
				},
				{
					"operation": "insert",
					"name": "PageSchemaViewModule",
					"parentName": "AccessRightsContainer",
					"propertyName": "items",
					"values": {
						"itemType": Terrasoft.ViewItemType.COMPONENT,
						"className": "Terrasoft.AccessRightsDetailComponent",
						"rightsSchemaName": "CopilotIntentSchemaManager",
						"recordId": { "bindTo": "WorkflowSchemaUId" },
						"accessRightsType": "schema",
						"accessRightsValue": { "bindTo": "AccessRightsValue" },
						"visible": { "bindTo": "_getIsAccessRightsVisible" }
					}
				},
				{
					"operation": "insert",
					"name": "AccessRightsPlaceholder",
					"parentName": "AccessRightsContainer",
					"propertyName": "items",
					"values": {
						"itemType": Terrasoft.ViewItemType.LABEL,
						"caption": resources.localizableStrings.AccessRightsPlaceholder,
						"visible": { "bindTo": "_getIsAccessRightsHidden" }
					}
				}
			]/**SCHEMA_DIFF*/
		};
	});
