define("AIAgentsBase_FormPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
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
				"operation": "merge",
				"name": "SaveButton",
				"values": {
					"clicked": [
						{
							"request": "crt.SaveRecordRequest"
						},
						{
							"request": "crt.SaveAccessRightsChangesRequest",
							"params": {
								"recordId": "$Id",
								"rightsSchemaName": "$RightsSchemaName",
								"accessRightsType": "$AccessRightsType",
								"accessRightsValues": "$AccessRightsValue"
							}
						}
					]
				}
			},
			{
				"operation": "merge",
				"name": "CancelButton",
				"values": {
					"clicked": [
						{
							"request": "crt.CancelRecordChangesRequest"
						},
						{
							"request": "crt.CancelAccessRightsChangesRequest",
							"params": {
								"recordId": "$Id",
								"rightsSchemaName": "$RightsSchemaName",
								"accessRightsType": "$AccessRightsType"
							}
						}
					]
				}
			},
			{
				"operation": "merge",
				"name": "MainHeaderBottom",
				"values": {
					"justifyContent": "end",
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch"
				}
			},
			{
				"operation": "remove",
				"name": "CardToolsContainer"
			},
			{
				"operation": "remove",
				"name": "TagSelect"
			},
			{
				"operation": "remove",
				"name": "CardToggleContainer"
			},
			{
				"operation": "remove",
				"name": "CardButtonToggleGroup"
			},
			{
				"operation": "merge",
				"name": "CardContentWrapper",
				"values": {
					"padding": {
						"left": "small",
						"right": "small",
						"top": "none",
						"bottom": "none"
					},
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"alignItems": "stretch"
				}
			},
			{
				"operation": "move",
				"name": "SideContainer",
				"parentName": "GeneralInfoTabContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "SideAreaProfileContainer",
				"values": {
					"columns": [
						"minmax(64px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"borderRadius": "none",
					"visible": true,
					"alignItems": "stretch"
				}
			},
			{
				"operation": "merge",
				"name": "CenterContainer",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					}
				}
			},
			{
				"operation": "merge",
				"name": "Tabs",
				"values": {
					"styleType": "default",
					"mode": "tab",
					"bodyBackgroundColor": "primary-contrast-500",
					"selectedTabTitleColor": "auto",
					"tabTitleColor": "auto",
					"underlineSelectedTabColor": "auto",
					"headerBackgroundColor": "auto"
				}
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTabContainer",
				"values": {
					"columns": [
						"298px",
						"minmax(64px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "small",
						"right": "large",
						"bottom": "none",
						"left": "extra-small"
					},
					"alignItems": "stretch"
				}
			},
			{
				"operation": "remove",
				"name": "CardToggleTabPanel"
			},
			{
				"operation": "remove",
				"name": "FeedTabContainer"
			},
			{
				"operation": "remove",
				"name": "Feed"
			},
			{
				"operation": "remove",
				"name": "FeedTabContainerHeaderContainer"
			},
			{
				"operation": "remove",
				"name": "FeedTabContainerHeaderLabel"
			},
			{
				"operation": "remove",
				"name": "AttachmentsTabContainer"
			},
			{
				"operation": "remove",
				"name": "AttachmentList"
			},
			{
				"operation": "remove",
				"name": "AttachmentsTabContainerHeaderContainer"
			},
			{
				"operation": "remove",
				"name": "AttachmentsTabContainerHeaderLabel"
			},
			{
				"operation": "remove",
				"name": "AttachmentAddButton"
			},
			{
				"operation": "remove",
				"name": "AttachmentRefreshButton"
			},
			{
				"operation": "insert",
				"name": "Title",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.PDS_Name",
					"labelPosition": "auto",
					"control": "$PDS_Name",
					"visible": true,
					"readonly": false,
					"placeholder": "#ResourceString(Title_placeholder)#",
					"tooltip": "#ResourceString(Title_tooltip)#"
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Code",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "#ResourceString(Code_label)#",
					"labelPosition": "auto",
					"control": "$PDS_Code"
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Status",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 3,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_Status",
					"labelPosition": "auto",
					"control": "$PDS_Status",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": []
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "LlmModel",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 4,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "#ResourceString(LlmModel_label)#",
					"tooltip": "#ResourceString(LlmModel_tooltip)#",
					"labelPosition": "auto",
					"showList": {
						"request": "crt.ComboboxLoadDataRequest",
						"params": {
							"dataSourceName": "CopilotLlmModelDS",
							"parameters": [],
							"primaryDisplayFilterValue": "@event.filterValue",
							"additionalFilteredColumnPaths": [],
							"config": {
								"loadType": "reload"
							}
						},
						"useRelativeContext": true
					},
					"value": "$PDS_LlmModel",
					"items": "$LlmModelList",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": [],
					"visible": "$EnableMultiLlmSupport",
					"readonly": false,
					"placeholder": "#ResourceString(LlmModel_placeholder)#"
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "RightSideContainer",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"padding": {
						"top": "13px",
						"bottom": "none",
						"left": "none",
						"right": "none"
					},
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": true
				},
				"parentName": "GeneralInfoTabContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Description",
				"values": {
					"type": "crt.Input",
					"multiline": true,
					"label": "#ResourceString(Label_Description_caption)#",
					"labelPosition": "above",
					"control": "$PDS_Description",
					"placeholder": "#ResourceString(Description_placeholder)#",
					"tooltip": "#ResourceString(Description_tooltip)#"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AccessRightsExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(AccessRightsExpansionPanel_title)#",
					"toggleType": "default",
					"togglePosition": "before",
					"expanded": true,
					"labelColor": "auto",
					"fullWidthHeader": false,
					"titleWidth": 20,
					"padding": {
						"top": "small",
						"bottom": "small",
						"left": "none",
						"right": "none"
					},
					"fitContent": true,
					"visible": "$EnableRightsManagementUI",
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "SchemaAccessRightBindingInstruction",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(SchemaAccessRightBindingInstruction_caption)#)#",
					"labelType": "caption",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": "$CardState | crt.IsEqual : 'edit'",
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					}
				},
				"parentName": "AccessRightsExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SchemaAccessRightPlaceholder",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(SchemaAccessRightPlaceholder_caption)#)#",
					"labelType": "placeholder-large",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "center",
					"visible": "$CardState | crt.IsEqual : 'add'"
				},
				"parentName": "AccessRightsExpansionPanel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AccessRightsOutletContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"visible": "$CardState | crt.IsEqual : 'edit'"
				},
				"parentName": "AccessRightsExpansionPanel",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "AccessRightsOutlet",
				"values": {
					"type": "crt.SchemaOutlet",
					"schemaName": "AccessRightsDetail",
					"features": {
						"tools": {
							"canExpand": false,
							"canDesign": false
						}
					},
					"RightsSchemaName": "$RightsSchemaName",
					"RecordId": "$Id",
					"AccessRightsType": "$AccessRightsType",
					"AccessRightsValue": "$AccessRightsValue",
					"ShowExpands": "$ShowExpands"
				},
				"parentName": "AccessRightsOutletContainer",
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
					"LlmModelList": {
						"isCollection": true,
						"modelConfig": {
							"path": "CopilotLlmModelDS",
							"sortingConfig": {
								"default": []
							},
							"filterAttributes": []
						},
						"viewModelConfig": {
							"attributes": {
								"value": {
									"modelConfig": {
										"path": "CopilotLlmModelDS.Id"
									}
								},
								"displayValue": {
									"modelConfig": {
										"path": "CopilotLlmModelDS.Name"
									}
								}
							}
						}
					},
					"PDS_Id": {
						"modelConfig": {
							"path": "PDS.Id"
						}
					},
					"PDS_Name": {
						"modelConfig": {
							"path": "PDS.Name"
						},
						"change": {
							"request": "crt.GenerateAgentCodeValueRequest",
							"params": {
								"valueAttributeName": "PDS_Name",
								"codeAttributeName": "PDS_Code"
							}
						}
					},
					"PDS_Code": {
						"modelConfig": {
							"path": "PDS.Code"
						},
						"validators": {
							"CodeMaxLength": {
								"type": "crt.MaxLength",
								"params": {
									"maxLength": 41
								}
							},
							"CodePrefixValidator": {
								"type": "crt.SchemaNamePrefix"
							},
							"CodeAllowedSymbolsValidator": {
								"type": "crt.SchemaNameAllowedSymbols"
							}
						}
					},
					"PDS_Description": {
						"modelConfig": {
							"path": "PDS.Description"
						}
					},
					"PDS_Status": {
						"modelConfig": {
							"path": "PDS.Status"
						}
					},
					"PDS_LlmModel": {
						"modelConfig": {
							"path": "PDS.LlmModel"
						}
					},
					"PDS_PackageUId": {
						"modelConfig": {
							"path": "PDS.PackageUId"
						}
					},
					"PDS_ResponseFormatJsonSchema": {
						"modelConfig": {
							"path": "PDS.ResponseFormatJsonSchema"
						}
					},
					"PDS_Mode": {
						"modelConfig": {
							"path": "PDS.Mode"
						}
					},
					"PDS_Type": {
						"modelConfig": {
							"path": "PDS.Type"
						}
					},
					"EnableRightsManagementUI": {
						"value": false
					},
					"RightsSchemaName": {
						"value": "CopilotIntentSchemaManager"
					},
					"AccessRightsType": {
						"value": "schema"
					},
					"AccessRightsValue": {
						"modelConfig": {
							"path": "PageParameters.AccessRightsValue"
						}
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"CardState"
				],
				"values": {
					"modelConfig": {}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"Id",
					"modelConfig"
				],
				"values": {
					"path": "PDS.Id"
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"primaryDataSourceName": "PDS",
					"dependencies": {
						"CopilotLlmModelDS": []
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"dataSources"
				],
				"values": {
					"PDS": {
						"type": "crt.CopilotIntentDataSource",
						"scope": "page",
						"config": {
							"entitySchemaName": "CopilotAgent",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"Code": {
									"path": "Code"
								},
								"Name": {
									"path": "Name"
								},
								"Description": {
									"path": "Description"
								},
								"Status": {
									"path": "Status"
								},
								"LlmModel": {
									"path": "LlmModel"
								},
								"PackageUId": {
									"path": "PackageUId"
								},
								"Type": {
									"path": "Type"
								},
								"Mode": {
									"path": "Mode"
								},
								"ResponseFormatJsonSchema": {
									"path": "ResponseFormatJsonSchema"
								}
							}
						}
					},
					"CopilotLlmModelDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "LlmModel",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"Name": {
									"path": "Name"
								},
								"Code": {
									"path": "Code"
								}
							}
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