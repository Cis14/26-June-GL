define("VwSysCspViolation_FormPage", /**SCHEMA_DEPS*/["@creatio-devkit/common"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/(sdk)/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "remove",
				"name": "SaveButton"
			},
			{
				"operation": "remove",
				"name": "CancelButton"
			},
			{
				"operation": "merge",
				"name": "CloseButton",
				"values": {
					"size": "large",
					"iconPosition": "only-text"
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
					"alignItems": "flex-end"
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
				"name": "CardButtonToggleGroup"
			},
			{
				"operation": "remove",
				"name": "MainContainer"
			},
			{
				"operation": "remove",
				"name": "CardContentWrapper"
			},
			{
				"operation": "remove",
				"name": "SideContainer"
			},
			{
				"operation": "remove",
				"name": "SideAreaProfileContainer"
			},
			{
				"operation": "remove",
				"name": "CenterContainer"
			},
			{
				"operation": "remove",
				"name": "CardContentContainer"
			},
			{
				"operation": "remove",
				"name": "Tabs"
			},
			{
				"operation": "remove",
				"name": "GeneralInfoTab"
			},
			{
				"operation": "remove",
				"name": "GeneralInfoTabContainer"
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
				"name": "Button_AddToTrusted",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_AddToTrusted_caption)#",
					"color": "primary",
					"disabled": false,
					"size": "large",
					"iconPosition": "only-text",
					"visible": true,
					"clicked": {
						"request": "crt.AddToTrustedSourceRequest",
						"params": {
							"blockedSource": "$BlockedHost",
							"directive": "$ViolatedDirective"
						}
					},
					"clickMode": "default"
				},
				"parentName": "CardToggleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "CardContentContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"stretch": false,
					"fitContent": true,
					"items": [],
					"layoutConfig": {},
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "small",
						"bottom": "none",
						"left": "small"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SideAreaProfileContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(64px, 1fr)",
						"minmax(64px, 1fr)",
						"minmax(64px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"padding": {
						"top": "large",
						"right": "large",
						"bottom": "medium",
						"left": "large"
					},
					"layoutConfig": {},
					"color": "primary",
					"borderRadius": "medium",
					"items": [],
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "CardContentContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_rck7w9b",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.VwSysCspViolationDS_BlockedHost_eu9hmb3",
					"labelPosition": "auto",
					"control": "$VwSysCspViolationDS_BlockedHost_eu9hmb3",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_hb1ozmo",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.VwSysCspViolationDS_ViolatedDirective_c8qdwr9",
					"labelPosition": "auto",
					"control": "$VwSysCspViolationDS_ViolatedDirective_c8qdwr9",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DateTimePicker_6w70ub5",
				"values": {
					"layoutConfig": {
						"column": 3,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.DateTimePicker",
					"pickerType": "date",
					"label": "$Resources.Strings.VwSysCspViolationDS_LastViolationDate_9yxmlbt",
					"labelPosition": "auto",
					"control": "$VwSysCspViolationDS_LastViolationDate_9yxmlbt",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_w768hju",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": false,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "small",
						"bottom": "none",
						"left": "small"
					},
					"stretch": true,
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_85aya0v",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": false,
					"visible": true,
					"color": "primary",
					"borderRadius": "medium",
					"padding": {
						"top": "large",
						"right": "large",
						"bottom": "medium",
						"left": "large"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap",
					"stretch": true
				},
				"parentName": "FlexContainer_w768hju",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_32y8elt",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_32y8elt_caption)#)#",
					"labelType": "headline-4",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_85aya0v",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_m8t96vi",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": {
								"enable": true,
								"multiple": true
							}
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"items": "$DataGrid_m8t96vi",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "ViolationsByHostDS_Id",
					"columns": [
						{
							"id": "c623dbf0-d5cd-9a53-9e93-c3db7281b365",
							"code": "ViolationsByHostDS_BlockedUri",
							"path": "BlockedUri",
							"caption": "#ResourceString(ViolationsByHostDS_BlockedUri)#",
							"dataValueType": 28,
							"width": 339
						},
						{
							"id": "4a187146-cb86-3fb2-7efa-a4e75bd508c9",
							"code": "ViolationsByHostDS_DocumentUri",
							"caption": "#ResourceString(ViolationsByHostDS_DocumentUri)#",
							"dataValueType": 28
						},
						{
							"id": "651cba8f-e392-1b83-fcfa-2fe4f76e26dd",
							"code": "ViolationsByHostDS_Source",
							"path": "Source",
							"caption": "#ResourceString(ViolationsByHostDS_Source)#",
							"dataValueType": 29,
							"width": 267
						},
						{
							"id": "3cee5a6b-d9ee-4c20-c009-6e29d7d6d9c0",
							"code": "ViolationsByHostDS_LastModifiedDate",
							"path": "LastModifiedDate",
							"caption": "#ResourceString(ViolationsByHostDS_LastModifiedDate)#",
							"dataValueType": 8,
							"width": 191
						}
					],
					"rowToolbarItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(AddToTrustedButtonCaption)#",
							"icon": "add-button-icon",
							"clicked": {
								"request": "crt.AddToTrustedSourceRequest",
								"params": {
									"blockedSource": "$DataGrid_m8t96vi.ViolationsByHostDS_BlockedUri",
									"directive": "$ViolatedDirective"
								},
								"useRelativeContext": true
							}
						}
					],
					"referenceSchema": "VwSysCspViolation",
					"layoutConfig": {},
					"selected": false,
					"selectionState": "$DataGrid_m8t96vi_SelectionState",
					"_selectionOptions": {
						"attribute": "$DataGrid_m8t96vi_SelectionState"
					},
					"bulkActions": []
				},
				"parentName": "FlexContainer_85aya0v",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_m8t96vi_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_m8t96vi",
							"filters": "$DataGrid_m8t96vi | crt.ToCollectionFilters : 'DataGrid_m8t96vi' : $DataGrid_m8t96vi_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_m8t96vi_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_m8t96vi",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_m8t96vi_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "ViolationsByHostDS",
							"filters": "$DataGrid_m8t96vi | crt.ToCollectionFilters : 'DataGrid_m8t96vi' : $DataGrid_m8t96vi_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_m8t96vi_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_m8t96vi",
				"propertyName": "bulkActions",
				"index": 1
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"ViolatedDirective": {
						"change": {
							"request": "crt.SetCspViolationFilterRequest",
							"params": {
								"filterAttributeName": "DataGrid_m8t96vi_Filter"
							}
						},
						"modelConfig": {
							"path": "VwSysCspViolationDS.ViolatedDirective"
						}
					},
					"BlockedHost": {
						"change": {
							"request": "crt.SetCspViolationFilterRequest",
							"params": {
								"filterAttributeName": "DataGrid_m8t96vi_Filter"
							}
						},
						"modelConfig": {
							"path": "VwSysCspViolationDS.BlockedHost"
						}
					},
					"DataGrid_m8t96vi": {
						"isCollection": true,
						"modelConfig": {
							"path": "ViolationsByHostDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "DataGrid_m8t96vi_Filter"
								}
							],
							"sortingConfig": {
								"default": [
									{
										"direction": "desc",
										"columnName": "LastModifiedDate"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"ViolationsByHostDS_BlockedUri": {
									"modelConfig": {
										"path": "ViolationsByHostDS.BlockedUri"
									}
								},
								"ViolationsByHostDS_DocumentUri": {
									"modelConfig": {
										"path": "ViolationsByHostDS.DocumentUri"
									}
								},
								"ViolationsByHostDS_Source": {
									"modelConfig": {
										"path": "ViolationsByHostDS.Source"
									}
								},
								"ViolationsByHostDS_LastModifiedDate": {
									"modelConfig": {
										"path": "ViolationsByHostDS.LastModifiedDate"
									}
								},
								"ViolationsByHostDS_Id": {
									"modelConfig": {
										"path": "ViolationsByHostDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_m8t96vi_Filter": {
						"value": {}
					},
					"VwSysCspViolationDS_BlockedHost_eu9hmb3": {
						"modelConfig": {
							"path": "VwSysCspViolationDS.BlockedHost"
						}
					},
					"VwSysCspViolationDS_ViolatedDirective_c8qdwr9": {
						"modelConfig": {
							"path": "VwSysCspViolationDS.ViolatedDirective"
						}
					},
					"VwSysCspViolationDS_LastViolationDate_9yxmlbt": {
						"modelConfig": {
							"path": "VwSysCspViolationDS.LastViolationDate"
						}
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"primaryDataSourceName": "VwSysCspViolationDS"
				}
			},
			{
				"operation": "merge",
				"path": [
					"dataSources"
				],
				"values": {
					"VwSysCspViolationDS": {
						"type": "crt.EntityDataSource",
						"scope": "page",
						"config": {
							"entitySchemaName": "VwSysCspViolation",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"BlockedHost": {
									"path": "BlockedHost"
								}
							}
						}
					},
					"ViolationsByHostDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "SysCspViolation",
							"attributes": {
								"BlockedUri": {
									"path": "BlockedUri"
								},
								"DocumentUri": {
									"path": "DocumentUri"
								},
								"Source": {
									"path": "Source"
								},
								"LastModifiedDate": {
									"path": "LastModifiedDate"
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