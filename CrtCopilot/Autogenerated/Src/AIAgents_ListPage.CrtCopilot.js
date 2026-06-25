define("AIAgents_ListPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "PageTitle",
				"values": {
					"caption": "#MacrosTemplateString(#ResourceString(PageTitle_caption)#)#",
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "AddButton"
			},
			{
				"operation": "remove",
				"name": "DataImportButton"
			},
			{
				"operation": "remove",
				"name": "MenuItem_ImportFromExcel"
			},
			{
				"operation": "remove",
				"name": "OpenLandingDesigner"
			},
			{
				"operation": "remove",
				"name": "ActionButton"
			},
			{
				"operation": "remove",
				"name": "MenuItem_ExportToExcel"
			},
			{
				"operation": "merge",
				"name": "MainFilterContainer",
				"values": {
					"visible": true,
					"alignItems": "stretch"
				}
			},
			{
				"operation": "merge",
				"name": "LeftFilterContainer",
				"values": {
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "FolderTreeActions"
			},
			{
				"operation": "remove",
				"name": "LookupQuickFilterByTag"
			},
			{
				"operation": "merge",
				"name": "RightFilterContainer",
				"values": {
					"padding": {
						"top": "small",
						"right": "large",
						"bottom": "none",
						"left": "none"
					},
					"gap": "small",
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "DataTable_Summaries"
			},
			{
				"operation": "merge",
				"name": "RefreshButton",
				"values": {
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "PDS"
						}
					},
					"visible": true
				}
			},
			{
				"operation": "merge",
				"name": "ContentContainer",
				"values": {
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
					"justifyContent": "start"
				}
			},
			{
				"operation": "remove",
				"name": "FolderTree"
			},
			{
				"operation": "merge",
				"name": "DataTable",
				"values": {
					"columns": [
						{
							"id": "3770e176-9d2f-bb25-3ea5-3b0de11e7ad3",
							"code": "PDS_Name",
							"caption": "#ResourceString(PDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "02d1e3d3-d5bc-c92f-a521-af9b7bcd5442",
							"code": "PDS_Status",
							"caption": "#ResourceString(PDS_Status)#",
							"dataValueType": 10,
							"width": 200
						},
						{
							"id": "9354d3f4-b15e-4462-a12d-7b690f4cf903",
							"code": "PDS_Type",
							"caption": "#ResourceString(PDS_Type)#",
							"dataValueType": 28,
							"width": 200
						},
						{
							"id": "87927053-2480-61b4-8dad-095198fb097f",
							"code": "PDS_Description",
							"caption": "#ResourceString(PDS_Description)#",
							"dataValueType": 28,
							"width": 700
						},
						{
							"id": "96347187-81f6-afe4-887b-439b174f9682",
							"code": "PDS_CreatedBy",
							"caption": "#ResourceString(PDS_CreatedBy)#",
							"dataValueType": 10
						},
						{
							"id": "55c75b2e-4487-de6b-7b60-f4be07b29116",
							"code": "PDS_ModifiedOn",
							"caption": "#ResourceString(PDS_ModifiedOn)#",
							"dataValueType": 7
						}
					],
					"placeholder": false,
					"features": {
						"rows": {
							"toolbar": false,
							"selection": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						},
						"columns": {
							"sorting": true,
							"adding": false
						}
					},
					"bulkActions": [],
					"_designOptions": {
						"columns": {
							"cellViews": {
								"PDS_Name": {
									"type": "crt.Link",
									"caption": "$Items.PDS_Name",
									"href": "$Items.PDS_Link",
									"mode": "$Items.PDS_LinkMode",
									"underlining": "hover",
									"target": "$Items.PDS_LinkTarget",
									"clicked": {
										"request": "crt.UpdateRecordRequest",
										"params": {
											"entityName": "CopilotAgent",
											"recordId": '$Items.PDS_Id'
										}
									}
								}
							}
						}
					},
					"visible": true,
					"fitContent": true
				}
			},
			{
				"operation": "insert",
				"name": "AddButtonForNotAgenticProcesses",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AddButton_caption)#",
					"color": "primary",
					"disabled": false,
					"size": "large",
					"iconPosition": "left-icon",
					"visible": "$UseAgenticProcessesFeatureValue | crt.InvertBooleanValue",
					"clicked": {
						"request": "crt.CreateRecordRequest",
						"params": {
							"entityName": "CopilotAgent",
							"entityPageName": "AIAgents_FormPage",
							"defaultValues": [
								{
									"attributeName": "Type", 
									"value":"a8fbe253-be1b-4ec4-bc58-65b8047013da"
								}
							]
						}
					},
					"clickMode": "default",
					"menuItems": [],
					"icon": "add-button-icon"
				},
				"parentName": "ActionButtonsContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AddButtonForAgenticProcesses",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AddButton_caption)#",
					"color": "primary",
					"disabled": false,
					"size": "large",
					"iconPosition": "left-icon",
					"clickMode": "menu",
					"visible": "$UseAgenticProcessesFeatureValue",
					"menuItems": [
							{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(WorkflowAgentIntentTypeCaption)#",
							"color": "default",
							"disabled": false,
							"size": "medium",
							"clicked": {
								"request": "crt.OpenAgenticDesignerRequest"
							},
							"clickMode": "default"
						},
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(AgentIntentTypeCaption)#",
							"color": "default",
							"disabled": false,
							"size": "medium",
							"clicked": {
								"request": "crt.CreateRecordRequest",
								"params": {
									"entityName": "CopilotAgent"
								}
							},
							"clickMode": "default"
						}],
					"icon": "add-button-icon"
				},
				"parentName": "ActionButtonsContainer",
				"propertyName": "items",
				"index": 2
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"Items_PredefinedFilter": {
						"value": null
					},
					"UseAgenticProcessesFeatureName": {
						"value": "GenAIFeatures.UseAgenticProcesses"
					},
					"UseAgenticProcessesFeatureValue": {
						"from": "UseAgenticProcessesFeatureName",
						"converter": "crt.GetFeatureState"
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"Items",
					"viewModelConfig",
					"attributes"
				],
				"values": {
					"PDS_Link": {
						"from": "PDS_Id",
						"converter": "crt.ToCopilotAgentLink : $PDS_Type"
					},
					"PDS_LinkTarget": {
						"from": "PDS_Type",
						"converter": "crt.ToObjectProp: 'value' |crt.IsEqual: '196ba912-742c-40fc-92a9-3eebe9021762'|crt.IfElse: '_blank' : '_self'"
					},
					"PDS_LinkMode": {
						"from": "PDS_Type",
						"converter": "crt.ToObjectProp: 'value' |crt.IsEqual: '196ba912-742c-40fc-92a9-3eebe9021762'|crt.IfElse: 'native' : 'preventDefault'"
					},
					"PDS_Name": {
						"modelConfig": {
							"path": "PDS.Name"
						}
					},
					"PDS_Type": {
						"modelConfig": {
							"path": "PDS.Type"
						}
					},
					"PDS_Status": {
						"modelConfig": {
							"path": "PDS.Status"
						}
					},
					"PDS_Description": {
						"modelConfig": {
							"path": "PDS.Description"
						}
					},
					"PDS_CreatedBy": {
						"modelConfig": {
							"path": "PDS.CreatedBy"
						}
					},
					"PDS_ModifiedOn": {
						"modelConfig": {
							"path": "PDS.ModifiedOn"
						}
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"Items",
					"modelConfig"
				],
				"values": {
					"filterAttributes": [
						{
							"name": "SearchFilter_Items",
							"loadOnChange": true
						},
						{
							"loadOnChange": true,
							"name": "Items_PredefinedFilter"
						}
					]
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"Items",
					"modelConfig",
					"sortingConfig"
				],
				"values": {
					"default": [
						{
							"direction": "asc",
							"columnName": "Name"
						}
					]
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
					"entitySchemaName": "CopilotAgent",
					"attributes": {
						"Name": {
							"path": "Name"
						},
						"Status": {
							"path": "Status"
						},
						"Description": {
							"path": "Description"
						},
						"CreatedBy": {
							"path": "CreatedBy"
						},
						"ModifiedOn": {
							"path": "ModifiedOn"
						},
						"Type": {
							"path": "Type"
						}
					}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});