define("ConfActivityLog_ListPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "MainHeader",
				"values": {
					"visible": true,
					"borderRadius": "none",
					"gap": "small"
				}
			},
			{
				"operation": "move",
				"name": "TitleContainer",
				"parentName": "SubsidiaryHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "TitleContainer",
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
					"gap": "small",
					"wrap": "wrap"
				}
			},
			{
				"operation": "merge",
				"name": "PageTitle",
				"values": {
					"labelThickness": "semibold",
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
				"operation": "merge",
				"name": "ActionButton",
				"values": {
					"iconPosition": "only-text",
					"icon": null
				}
			},
			{
				"operation": "merge",
				"name": "MainFilterContainer",
				"values": {
					"alignItems": "stretch"
				}
			},
			{
				"operation": "merge",
				"name": "LeftFilterContainer",
				"values": {
					"fitContent": true
				}
			},
			{
				"operation": "merge",
				"name": "LeftFilterContainerInner",
				"values": {
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "LookupQuickFilterByTag"
			},
			{
				"operation": "merge",
				"name": "DataTable_Summaries",
				"values": {
					"expanded": "$DataTable_Summaries_Expanded"
				}
			},
			{
				"operation": "remove",
				"name": "MainButtonToggleGroup"
			},
			{
				"operation": "merge",
				"name": "FolderTree",
				"values": {
					"rootSchemaName": "ConfActivityLog"
				}
			},
			{
				"operation": "remove",
				"name": "MainTabPanel"
			},
			{
				"operation": "remove",
				"name": "ListTabContainer"
			},
			{
				"operation": "merge",
				"name": "ListContainer",
				"values": {
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "medium"
					}
				}
			},
			{
				"operation": "move",
				"name": "ListContainer",
				"parentName": "SectionContentWrapper",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "DataTable",
				"values": {
					"columns": [
						{
							"id": "5dfa3dce-79f0-fe31-264c-4bb82bfe12ed",
							"code": "PDS_Number",
							"caption": "#ResourceString(PDS_Number)#",
							"dataValueType": 27,
							"width": 118
						},
						{
							"id": "8864e428-cb7d-4fc4-2887-b80ed4ac16a8",
							"code": "PDS_Parent",
							"caption": "#ResourceString(PDS_Parent)#",
							"dataValueType": 10,
							"width": 164
						},
						{
							"id": "c8689d78-80ba-4e71-8cf2-fa478e3be5bc",
							"code": "PDS_CreatedOn",
							"caption": "#ResourceString(PDS_CreatedOn)#",
							"dataValueType": 7,
							"width": 169
						},
						{
							"id": "69e7c7c8-7849-9406-5281-2623ac3295e6",
							"code": "PDS_Operation",
							"caption": "#ResourceString(PDS_Operation)#",
							"dataValueType": 10,
							"width": 182
						},
						{
							"id": "8aadb02b-fb56-8069-0722-3d960714a272",
							"code": "PDS_Element",
							"caption": "#ResourceString(PDS_Element)#",
							"dataValueType": 28,
							"width": 229
						},
						{
							"id": "948d65df-b91d-59e3-5900-6746f20ee7f6",
							"code": "PDS_Type",
							"caption": "#ResourceString(PDS_Type)#",
							"dataValueType": 10,
							"width": 126
						},
						{
							"id": "3727af46-b278-ac55-21e2-72f168040bf0",
							"code": "PDS_CreatedBy",
							"caption": "#ResourceString(PDS_CreatedBy)#",
							"dataValueType": 10,
							"width": 167
						},
						{
							"id": "99a2f234-81fd-a316-a0fd-3ea0b5b1d727",
							"code": "PDS_ApplicationCode",
							"caption": "#ResourceString(PDS_ApplicationCode)#",
							"dataValueType": 28,
							"width": 164
						},
						{
							"id": "5f05fcaa-8d67-8484-ce69-20bc584cc6bf",
							"code": "PDS_PackageName",
							"caption": "#ResourceString(PDS_PackageName)#",
							"dataValueType": 28,
							"width": 129
						},
						{
							"id": "d09da5fe-af71-0f76-9230-9f93ea71e8da",
							"code": "PDS_Status",
							"caption": "#ResourceString(PDS_Status)#",
							"dataValueType": 10,
							"width": 114
						}
					],
					"features": {
						"rows": {
							"selection": false,
							"numeration": false,
							"toolbar": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false,
							"floatingEditPanel": false
						}
					},
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "DashboardsTabContainer"
			},
			{
				"operation": "remove",
				"name": "DashboardsContainer"
			},
			{
				"operation": "remove",
				"name": "Dashboards"
			},
			{
				"operation": "insert",
				"name": "SubsidiaryHeader",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true
				},
				"parentName": "MainHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ClearActivityLogMenuItem",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(ClearActivityLogMenuItem_caption)#",
					"visible": true,
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.RunBusinessProcessRequest",
						"params": {
							"processName": "CleanConfigurationActivityLog",
							"processRunType": "RegardlessOfThePage",
							"saveAtProcessStart": false
						}
					}
				},
				"parentName": "ActionButton",
				"propertyName": "menuItems",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ConfigureLogRetentionMenuItem",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(ConfigureLogRetentionMenuItem_caption)#",
					"visible": true,
					"icon": "date-time",
					"clicked": {
						"request": "crt.RunBusinessProcessRequest",
						"params": {
							"processName": "UpdateConfigurationActivityLogRetentionPeriod",
							"processRunType": "RegardlessOfThePage",
							"saveAtProcessStart": false
						}
					}
				},
				"parentName": "ActionButton",
				"propertyName": "menuItems",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "SelectPeriodQuickFilter",
				"values": {
					"type": "crt.QuickFilter",
					"config": {
						"caption": "#ResourceString(SelectPeriodQuickFilter_config_caption)#",
						"hint": "",
						"icon": "date",
						"iconPosition": "left-icon",
						"defaultValue": "[#currentWeek#]"
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "SelectPeriodQuickFilter_Items",
								"converters": [
									{
										"converter": "crt.QuickFilterAttributeConverter",
										"args": [
											{
												"target": {
													"viewAttributeName": "Items",
													"filterColumnStart": "CreatedOn",
													"filterColumnEnd": "CreatedOn"
												},
												"quickFilterType": "date-range"
											}
										]
									}
								]
							}
						],
						"from": "SelectPeriodQuickFilter_Value"
					},
					"filterType": "date-range"
				},
				"parentName": "LeftFilterContainerInner",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ChangeMadeByQuickFilter",
				"values": {
					"type": "crt.QuickFilter",
					"config": {
						"caption": "#ResourceString(ChangeMadeByQuickFilter_config_caption)#",
						"hint": "",
						"icon": "person-button-icon",
						"iconPosition": "left-icon",
						"defaultValue": [],
						"entitySchemaName": "Contact",
						"recordsFilter": null
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "ChangeMadeByQuickFilter_Items",
								"converters": [
									{
										"converter": "crt.QuickFilterAttributeConverter",
										"args": [
											{
												"target": {
													"viewAttributeName": "Items",
													"filterColumn": "CreatedBy"
												},
												"quickFilterType": "lookup"
											}
										]
									}
								]
							}
						],
						"from": "ChangeMadeByQuickFilter_Value"
					},
					"filterType": "lookup"
				},
				"parentName": "LeftFilterContainerInner",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "OperationQuickFilter",
				"values": {
					"type": "crt.QuickFilter",
					"config": {
						"caption": "#ResourceString(OperationQuickFilter_config_caption)#",
						"hint": "",
						"icon": "pen-icon",
						"iconPosition": "left-icon",
						"defaultValue": [],
						"entitySchemaName": "ConfOperation",
						"recordsFilter": null
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "OperationQuickFilter_Items",
								"converters": [
									{
										"converter": "crt.QuickFilterAttributeConverter",
										"args": [
											{
												"target": {
													"viewAttributeName": "Items",
													"filterColumn": "Operation"
												},
												"quickFilterType": "lookup"
											}
										]
									}
								]
							}
						],
						"from": "OperationQuickFilter_Value"
					},
					"filterType": "lookup"
				},
				"parentName": "LeftFilterContainerInner",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ParentOperationQuickFilter",
				"values": {
					"type": "crt.QuickFilter",
					"config": {
						"caption": "#ResourceString(ParentOperationQuickFilter_config_caption)#",
						"hint": "",
						"icon": "organizational-structure-icon",
						"iconPosition": "left-icon",
						"defaultValue": [],
						"entitySchemaName": "ConfActivityLog",
						"recordsFilter": null
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "ParentOperationQuickFilter_Items",
								"converters": [
									{
										"converter": "crt.QuickFilterAttributeConverter",
										"args": [
											{
												"target": {
													"viewAttributeName": "Items",
													"filterColumn": "Parent"
												},
												"quickFilterType": "lookup"
											}
										]
									}
								]
							}
						],
						"from": "ParentOperationQuickFilter_Value"
					},
					"filterType": "lookup"
				},
				"parentName": "LeftFilterContainerInner",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "ShowChildOperationsQuickFilter",
				"values": {
					"type": "crt.QuickFilter",
					"config": {
						"caption": "#ResourceString(ShowChildOperationsQuickFilter_config_caption)#",
						"hint": "",
						"defaultValue": false,
						"approachState": false,
						"icon": "settings-button-icon",
						"iconPosition": "left-icon"
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "ShowChildOperationsQuickFilter_Items",
								"converters": [
									{
										"converter": "crt.QuickFilterAttributeConverter",
										"args": [
											{
												"target": {
													"viewAttributeName": "Items",
													"customFilter": {
														"items": {
															"5d9b0c5d-1e0f-4fff-919b-df67cffd4420": {
																"filterType": 2,
																"comparisonType": 1,
																"isEnabled": true,
																"trimDateTimeParameterToDate": false,
																"leftExpression": {
																	"expressionType": 0,
																	"columnPath": "Parent"
																},
																"isAggregative": false,
																"dataValueType": 10,
																"referenceSchemaName": "ConfActivityLog",
																"isNull": true
															}
														},
														"logicalOperation": 0,
														"isEnabled": true,
														"filterType": 6,
														"rootSchemaName": "ConfActivityLog"
													},
													"dependencyFilters": null
												},
												"quickFilterType": "custom",
												"config": {
													"approachState": false
												}
											}
										]
									}
								]
							}
						],
						"from": [
							"ShowChildOperationsQuickFilter_Value"
						]
					},
					"filterType": "custom"
				},
				"parentName": "LeftFilterContainerInner",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "SummaryItem_5j6h23h",
				"values": {
					"type": "crt.SummaryItem",
					"label": "#ResourceString(SummaryItem_5j6h23h_label)#",
					"_designOptions": {
						"value": {
							"attribute": "SummaryItem_5j6h23h_Value",
							"modelName": "PDS",
							"expression": {
								"function": "count",
								"columns": [
									{
										"type": "Column",
										"path": "Id"
									}
								],
								"resultDataValueType": 4
							}
						}
					}
				},
				"parentName": "DataTable_Summaries",
				"propertyName": "items",
				"index": 0
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"DataTable_Summaries_Expanded": {
						"value": true
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
					"PDS_Number": {
						"modelConfig": {
							"path": "PDS.Number"
						}
					},
					"PDS_Parent": {
						"modelConfig": {
							"path": "PDS.Parent"
						}
					},
					"PDS_CreatedOn": {
						"modelConfig": {
							"path": "PDS.CreatedOn"
						}
					},
					"PDS_Operation": {
						"modelConfig": {
							"path": "PDS.Operation"
						}
					},
					"PDS_Element": {
						"modelConfig": {
							"path": "PDS.Element"
						}
					},
					"PDS_Type": {
						"modelConfig": {
							"path": "PDS.Type"
						}
					},
					"PDS_CreatedBy": {
						"modelConfig": {
							"path": "PDS.CreatedBy"
						}
					},
					"PDS_ApplicationCode": {
						"modelConfig": {
							"path": "PDS.ApplicationCode"
						}
					},
					"PDS_PackageName": {
						"modelConfig": {
							"path": "PDS.PackageName"
						}
					},
					"PDS_Status": {
						"modelConfig": {
							"path": "PDS.Status"
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
							"loadOnChange": true,
							"name": "FolderTree_active_folder_filter"
						},
						{
							"name": "Items_PredefinedFilter",
							"loadOnChange": true
						},
						{
							"name": "SearchFilter_Items",
							"loadOnChange": true
						},
						{
							"name": "SelectPeriodQuickFilter_Items",
							"loadOnChange": true
						},
						{
							"name": "ChangeMadeByQuickFilter_Items",
							"loadOnChange": true
						},
						{
							"name": "OperationQuickFilter_Items",
							"loadOnChange": true
						},
						{
							"name": "ParentOperationQuickFilter_Items",
							"loadOnChange": true
						},
						{
							"name": "ShowChildOperationsQuickFilter_Items",
							"loadOnChange": true
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
							"direction": "desc",
							"columnName": "CreatedOn"
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
					"entitySchemaName": "ConfActivityLog",
					"attributes": {
						"Number": {
							"path": "Number"
						},
						"Parent": {
							"path": "Parent"
						},
						"CreatedOn": {
							"path": "CreatedOn"
						},
						"Operation": {
							"path": "Operation"
						},
						"Element": {
							"path": "Element"
						},
						"Type": {
							"path": "Type"
						},
						"CreatedBy": {
							"path": "CreatedBy"
						},
						"ApplicationCode": {
							"path": "ApplicationCode"
						},
						"PackageName": {
							"path": "PackageName"
						},
						"Status": {
							"path": "Status"
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