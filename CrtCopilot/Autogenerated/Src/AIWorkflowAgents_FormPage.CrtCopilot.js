define("AIWorkflowAgents_FormPage", /**SCHEMA_DEPS*/["InplaceProcessSchemaDesignerComponent", "css!AIProcessActions_FormPage"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "Tabs",
				"values": {
					"classes": [
						"tab-panel-fixed-height"
					]
				}
			},
			{
				"operation": "merge",
				"name": "SaveButton",
				"values": {
					"visible": "$AnySchemaHasUnsavedData | crt.OrBooleanValue : $WorkflowSchemaIsNew"
				}
			},
			{
				"operation": "merge",
				"name": "CancelButton",
				"values": {
					"visible": "$AnySchemaHasUnsavedData | crt.OrBooleanValue : $WorkflowSchemaIsNew"
				}
			},
			{
				"operation": "merge",
				"name": "CloseButton",
				"values": {
					"visible": "$AnySchemaHasUnsavedData | crt.InvertBooleanValue"
				}
			},
			{
				"operation": "insert",
				"name": "InplaceProcessDesignerContainer",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.FlexContainer",
					"direction": "column",
					"stretch": true,
					"fitContent": false,
					"items": [],
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "none"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "InplaceProcessSchemaDesigner",
				"values": {
					"type": "crt.InplaceProcessSchemaDesigner",
					"processName": "$WorkflowProcessName",
					"processCode": "$WorkflowProcessCode",
					"processSchemaUId": "$WorkflowSchemaUId",
					"processDescription": "$PDS_Description",
					"packageUId": "$PDS_PackageUId",
					"designerInstanceId": "$ProcessDesignerInstanceId",
					"schemaIsLoaded": "$WorkflowSchemaIsLoaded",
					"schemaIsChanged": "$WorkflowSchemaIsChanged",
					"schemaIsNew": "$WorkflowSchemaIsNew",
					"preCreateParameters": [{
						"name": "CopilotWorkflowRootSessionId",
						"direction": Terrasoft.ProcessSchemaParameterDirection.IN,
						"dataValueType": Terrasoft.DataValueType.GUID,
						"caption": {
							"en-US": "Copilot workflow root session id"
						},
						"description": {
							"en-US": "Creatio.ai Copilot chat session ID used to link workflow execution to a specific user conversation"
						},
						'isRequired': true
					}]
				},
				"parentName": "InplaceProcessDesignerContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "remove",
				"name": "LlmModel"
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTabContainer",
				"values": {
					"stretch": true,
					"rows": []
				}
			},
			{
				"operation": "merge",
				"name": "RightSideContainer",
				"values": {
					"stretch": true
				}
			},
			{
				"operation": "merge",
				"name": "CardContentWrapper",
				"values": {
					"fitContent": false
				}
			},
			{
				"operation": "move",
				"name": "Description",
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "move",
				"name": "AccessRightsExpansionPanel",
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "merge",
				"name": "SchemaAccessRightPlaceholder",
				"values": {
					"labelTextAlign": "start",
					"labelType": "caption"
				}
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"ProcessDesignerInstanceId": {},
					"WorkflowSchemaUId": {},
					"WorkflowProcessName": {},
					"WorkflowProcessCode": {},
					"WorkflowSchemaIsLoaded": {},
					"WorkflowSchemaIsChanged": {},
					"WorkflowHasUnsavedData": {},
					"WorkflowSchemaIsNew": {},
					"PDS_Params": {
						"modelConfig": {
							"path": "PDS.Params"
						}
					},
					"AnySchemaHasUnsavedData": {
						"from": [
							"HasUnsavedData",
							"WorkflowHasUnsavedData"
						],
						"converter": "crt.OrBooleanValue"
					},
					"WorkflowProcessNameTemplate": {
						"value": "#ResourceString(WorkflowProcessNameTemplate)#"
					},
					"WorkflowProcessCodeTemplate": {
						"value": "{0}Process"
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"PDS_Name"
				],
				"values": {
					"change": {
						"request": "crt.GenerateWorkflowAgentCodeValueRequest",
						"params": {
							"valueAttributeName": "PDS_Name",
							"codeAttributeName": "PDS_Code"
						}
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"dataSources",
					"PDS",
					"config",
					"attributes"
				],
				"values": {
					"Params": {
						"path": "Params"
					}
				}
			},
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});