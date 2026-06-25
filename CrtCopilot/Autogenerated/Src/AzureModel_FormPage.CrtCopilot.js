define("AzureModel_FormPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "SetRecordRightsButton",
				"values": {
					"caption": "#ResourceString(SetRecordRightsButton_caption)#",
					"visible": false
				}
			},
			{
				"operation": "insert",
				"name": "ModelName",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.PDS_Model_tzd851i",
					"control": "$PDS_Model_tzd851i",
					"placeholder": "#ResourceString(ModelName_placeholder)#",
					"tooltip": "#ResourceString(ModelName_tooltip)#",
					"readonly": false,
					"multiline": false,
					"labelPosition": "auto",
					"visible": true
				},
				"parentName": "ConnectionSettingGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "APIKey",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 2,
						"rowSpan": 1
					},
					"type": "crt.PasswordInput",
					"label": "$Resources.Strings.PDS_ApiKey_jkd2o6f",
					"labelPosition": "auto",
					"control": "$PDS_ApiKey_jkd2o6f",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": "#ResourceString(APIKey_tooltip)#"
				},
				"parentName": "ConnectionSettingGridContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AzureResourceName",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 3,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.PDS_ApiBase_dz3yrii",
					"control": "$PDS_ApiBase_dz3yrii",
					"placeholder": "#ResourceString(AzureResourceName_placeholder)#",
					"tooltip": "#ResourceString(AzureResourceName_tooltip)#",
					"readonly": false,
					"multiline": false,
					"labelPosition": "auto",
					"visible": true
				},
				"parentName": "ConnectionSettingGridContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "APIVersion",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 4,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.PDS_ApiVersion_8sifdjq",
					"control": "$PDS_ApiVersion_8sifdjq",
					"placeholder": "#ResourceString(APIVersion_placeholder)#",
					"tooltip": "#ResourceString(APIVersion_tooltip)#",
					"readonly": false,
					"multiline": false,
					"labelPosition": "auto",
					"visible": true
				},
				"parentName": "ConnectionSettingGridContainer",
				"propertyName": "items",
				"index": 3
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"PDS_ApiKey_jkd2o6f": {
						"modelConfig": {
							"path": "PDS.ApiKey"
						}
					},
					"PDS_Model_tzd851i": {
						"modelConfig": {
							"path": "PDS.Model"
						}
					},
					"PDS_ApiBase_dz3yrii": {
						"modelConfig": {
							"path": "PDS.ResourceName"
						}
					},
					"PDS_ApiVersion_8sifdjq": {
						"modelConfig": {
							"path": "PDS.ApiVersion"
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
					"config"
				],
				"values": {
					"entitySchemaName": "AzureOpenAIModel"
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});
