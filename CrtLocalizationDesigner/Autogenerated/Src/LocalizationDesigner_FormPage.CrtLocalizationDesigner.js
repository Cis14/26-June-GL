define("LocalizationDesigner_FormPage", /**SCHEMA_DEPS*/["@creatio-devkit/common"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/(sdk)/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "SaveButton",
				"values": {
					"size": "large",
					"iconPosition": "only-text"
				}
			},
			{
				"operation": "remove",
				"name": "SetRecordRightsButton"
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
				"operation": "merge",
				"name": "CardContentWrapper",
				"values": {
					"padding": {
						"left": "small",
						"right": "small",
						"top": "none",
						"bottom": "none"
					},
					"fitContent": false,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"alignItems": "stretch"
				}
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
				"operation": "merge",
				"name": "CenterContainer",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					},
					"stretch": false,
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
					"gap": "small"
				}
			},
			{
				"operation": "merge",
				"name": "CardContentContainer",
				"values": {
					"stretch": false,
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
					"gap": "small",
					"wrap": "nowrap"
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
					"headerBackgroundColor": "auto",
					"allowToggleClose": true,
					"visible": true,
					"stretch": true
				}
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTab",
				"values": {
					"iconPosition": "only-text",
					"visible": true
				}
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTabContainer",
				"values": {
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"visible": true,
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
				"operation": "merge",
				"name": "CardToggleTabPanel",
				"values": {
					"styleType": "default",
					"bodyBackgroundColor": "primary-contrast-500",
					"selectedTabTitleColor": "auto",
					"tabTitleColor": "auto",
					"underlineSelectedTabColor": "auto",
					"headerBackgroundColor": "auto",
					"allowToggleClose": true,
					"visible": true,
					"selectedTab": {
						"value": "TabContainer_PropertiesToggleTab"
					}
				}
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
				"name": "FlexContainer_ConfigurationElementFilters",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 1,
						"rowSpan": 1
					},
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "center",
					"justifyContent": "space-between",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GeneralInfoTabContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SearchFilter_ConfigurationElemLcz",
				"values": {
					"type": "crt.SearchFilter",
					"placeholder": "#ResourceString(SearchFilter_ConfigurationElemLcz_placeholder)#",
					"_filterOptions": {
						"expose": [
							{
								"attribute": "SearchFilter_ConfigurationElemLcz_DataGrid_x3umv3r",
								"converters": [
									{
										"converter": "crt.SearchFilterAttributeConverter",
										"args": [
											"DataGrid_x3umv3r"
										]
									}
								]
							}
						],
						"from": [
							"SearchFilter_ConfigurationElemLcz_SearchValue",
							"SearchFilter_ConfigurationElemLcz_FilteredColumnsGroups"
						]
					}
				},
				"parentName": "FlexContainer_ConfigurationElementFilters",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_GridButtonsContainerConfElemetns",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "center",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "FlexContainer_ConfigurationElementFilters",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Button_RefereshConfigurationElementsGrid",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_RefereshConfigurationElementsGrid_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "only-icon",
					"visible": true,
					"icon": "reload-icon",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload"
							},
							"refreshDataConfig": {
								"mode": "RefreshSpecific",
								"targetDataSourceNames": [
									"DataGrid_x3umv3rDS"
								]
							}
						}
					},
					"clickMode": "default"
				},
				"parentName": "FlexContainer_GridButtonsContainerConfElemetns",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Summaries_ConfigurationElements",
				"values": {
					"type": "crt.Summaries",
					"items": [],
					"visible": true,
					"_designOptions": {
						"modelName": "DataGrid_x3umv3rDS"
					},
					"expanded": "$Summaries_ConfigurationElements_Expanded"
				},
				"parentName": "FlexContainer_GridButtonsContainerConfElemetns",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SummaryItem_ggeaykf",
				"values": {
					"type": "crt.SummaryItem",
					"label": "#ResourceString(SummaryItem_ggeaykf_label)#",
					"_designOptions": {
						"value": {
							"attribute": "SummaryItem_ggeaykf_Value",
							"modelName": "DataGrid_x3umv3rDS",
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
				"parentName": "Summaries_ConfigurationElements",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_ConfigurationElementsLcz",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 2,
						"rowSpan": 1
					},
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": false,
							"numeration": true,
							"toolbar": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false,
							"floatingEditPanel": false
						}
					},
					"items": "$DataGrid_x3umv3r",
					"activeRow": "$DataGrid_x3umv3r_ActiveRow",
					"selectionState": "$DataGrid_x3umv3r_SelectionState",
					"_selectionOptions": {
						"attribute": "DataGrid_x3umv3r_SelectionState"
					},
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_x3umv3rDS_Id",
					"columns": [
						{
							"id": "878cad52-5089-51d6-540c-ab9aab189fb0",
							"code": "DataGrid_x3umv3rDS_SysSchema",
							"caption": "#ResourceString(DataGrid_ConfigurationElementsLcz_caption)#",
							"dataValueType": 10,
							"width": 395
						},
						{
							"id": "5e1752b3-3fc9-3ae2-f714-ed607defb2a1",
							"code": "DataGrid_x3umv3rDS_Key",
							"caption": "#ResourceString(DataGrid_x3umv3rDS_Key)#",
							"dataValueType": 28,
							"width": 648
						},
						{
							"id": "8b6af8be-9450-be30-ec72-2792b9d10b85",
							"code": "DataGrid_x3umv3rDS_Value",
							"caption": "#ResourceString(DataGrid_x3umv3rDS_Value)#",
							"dataValueType": 29,
							"width": 378
						}
					],
					"placeholder": false,
					"bulkActions": []
				},
				"parentName": "GeneralInfoTabContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_x3umv3r_AddTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Add tag",
					"icon": "tag-icon",
					"clicked": {
						"request": "crt.AddTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_x3umv3rDS",
							"filters": "$DataGrid_x3umv3r | crt.ToCollectionFilters : 'DataGrid_x3umv3r' : $DataGrid_x3umv3r_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_x3umv3r_SelectionState"
						}
					},
					"items": []
				},
				"parentName": "DataGrid_ConfigurationElementsLcz",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_x3umv3r_RemoveTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Remove tag",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.RemoveTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_x3umv3rDS",
							"filters": "$DataGrid_x3umv3r | crt.ToCollectionFilters : 'DataGrid_x3umv3r' : $DataGrid_x3umv3r_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_x3umv3r_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_x3umv3r_AddTagsBulkAction",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_x3umv3r_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_ConfigurationElementsLcz",
							"filters": "$DataGrid_x3umv3r | crt.ToCollectionFilters : 'DataGrid_x3umv3r' : $DataGrid_x3umv3r_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_x3umv3r_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_ConfigurationElementsLcz",
				"propertyName": "bulkActions",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_x3umv3r_MergeBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Merge",
					"icon": "merge-icon",
					"clicked": {
						"request": "crt.MergeRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_x3umv3rDS",
							"selectionState": "$DataGrid_x3umv3r_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_ConfigurationElementsLcz",
				"propertyName": "bulkActions",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "DataGrid_x3umv3r_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_x3umv3rDS",
							"filters": "$DataGrid_x3umv3r | crt.ToCollectionFilters : 'DataGrid_x3umv3r' : $DataGrid_x3umv3r_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_x3umv3r_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_ConfigurationElementsLcz",
				"propertyName": "bulkActions",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "TabContainer_LocalizableData",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_LocalizableData_caption)#",
					"iconPosition": "only-text",
					"visible": "$isLocalizableDataManagementEnabled"
				},
				"parentName": "Tabs",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_LocalizableDataFilters",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "center",
					"justifyContent": "space-between",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "TabContainer_LocalizableData",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SearchFilter_a9emdwx",
				"values": {
					"type": "crt.SearchFilter",
					"placeholder": "#ResourceString(SearchFilter_a9emdwx_placeholder)#",
					"_filterOptions": {
						"expose": [],
						"from": [
							"SearchFilter_a9emdwx_SearchValue",
							"SearchFilter_a9emdwx_FilteredColumnsGroups"
						]
					}
				},
				"parentName": "FlexContainer_LocalizableDataFilters",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_GridButtonsContainerLocData",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "center",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "FlexContainer_LocalizableDataFilters",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Button_RefereshLocalizableDataGrid",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_RefereshLocalizableDataGrid_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "only-icon",
					"visible": true,
					"icon": "reload-icon",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload"
							},
							"refreshDataConfig": {
								"mode": "RefreshSpecific",
								"targetDataSourceNames": [
									"DataGrid_xm741rqDS"
								]
							}
						}
					},
					"clickMode": "default"
				},
				"parentName": "FlexContainer_GridButtonsContainerLocData",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Summaries_LocalizableData",
				"values": {
					"type": "crt.Summaries",
					"items": [],
					"visible": true,
					"_designOptions": {
						"modelName": "DataGrid_xm741rqDS"
					},
					"expanded": "$Summaries_LocalizableData_Expanded"
				},
				"parentName": "FlexContainer_GridButtonsContainerLocData",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SummaryItem_nh76qbr",
				"values": {
					"type": "crt.SummaryItem",
					"label": "#ResourceString(SummaryItem_nh76qbr_label)#",
					"_designOptions": {
						"value": {
							"attribute": "SummaryItem_nh76qbr_Value",
							"modelName": "DataGrid_xm741rqDS",
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
				"parentName": "Summaries_LocalizableData",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_LocalizableData",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": false,
							"numeration": true,
							"toolbar": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false,
							"floatingEditPanel": false
						}
					},
					"items": "$DataGrid_xm741rq",
					"activeRow": "$DataGrid_xm741rq_ActiveRow",
					"selectionState": "$DataGrid_xm741rq_SelectionState",
					"_selectionOptions": {
						"attribute": "DataGrid_xm741rq_SelectionState"
					},
					"primaryColumnName": "DataGrid_xm741rqDS_Id",
					"columns": [
						{
							"id": "0e9d5964-2187-0955-389c-d38f8fc6a554",
							"code": "DataGrid_xm741rqDS_DataSchemaName",
							"caption": "#ResourceString(DataGrid_xm741rqDS_DataSchemaName)#",
							"dataValueType": 28
						},
						{
							"id": "98a9758e-2050-2a60-a39c-e4902b1c2f5d",
							"code": "DataGrid_xm741rqDS_ColumnName",
							"caption": "#ResourceString(DataGrid_xm741rqDS_ColumnName)#",
							"dataValueType": 28
						},
						{
							"id": "40a5ee6c-4382-13fd-11f9-38538a0b6f12",
							"code": "DataGrid_xm741rqDS_RecordId",
							"caption": "#ResourceString(DataGrid_xm741rqDS_RecordId)#",
							"dataValueType": 0
						},
						{
							"id": "7f7ccabc-d872-1e66-2456-84d5d1607be5",
							"code": "DataGrid_xm741rqDS_Value",
							"caption": "#ResourceString(DataGrid_xm741rqDS_Value)#",
							"dataValueType": 29
						}
					],
					"placeholder": false,
					"bulkActions": [],
					"visible": true,
					"fitContent": true,
					"stretch": true
				},
				"parentName": "TabContainer_LocalizableData",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_xm741rq_AddTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Add tag",
					"icon": "tag-icon",
					"clicked": {
						"request": "crt.AddTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_xm741rqDS",
							"filters": "$DataGrid_xm741rq | crt.ToCollectionFilters : 'DataGrid_xm741rq' : $DataGrid_xm741rq_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_xm741rq_SelectionState"
						}
					},
					"items": []
				},
				"parentName": "DataGrid_LocalizableData",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_xm741rq_RemoveTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Remove tag",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.RemoveTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_xm741rqDS",
							"filters": "$DataGrid_xm741rq | crt.ToCollectionFilters : 'DataGrid_xm741rq' : $DataGrid_xm741rq_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_xm741rq_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_xm741rq_AddTagsBulkAction",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_xm741rq_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_LocalizableData",
							"filters": "$DataGrid_xm741rq | crt.ToCollectionFilters : 'DataGrid_xm741rq' : $DataGrid_xm741rq_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_xm741rq_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_LocalizableData",
				"propertyName": "bulkActions",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_xm741rq_MergeBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Merge",
					"icon": "merge-icon",
					"clicked": {
						"request": "crt.MergeRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_xm741rqDS",
							"selectionState": "$DataGrid_xm741rq_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_LocalizableData",
				"propertyName": "bulkActions",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "DataGrid_xm741rq_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_xm741rqDS",
							"filters": "$DataGrid_xm741rq | crt.ToCollectionFilters : 'DataGrid_xm741rq' : $DataGrid_xm741rq_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_xm741rq_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_LocalizableData",
				"propertyName": "bulkActions",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "TabContainer_HelpToggleButton",
				"values": {
					"type": "crt.TabContainer",
					"tools": [],
					"items": [],
					"caption": "#ResourceString(TabContainer_HelpToggleButton_caption)#",
					"iconPosition": "left-icon",
					"visible": true,
					"icon": "book-open-icon"
				},
				"parentName": "CardToggleTabPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_HelpTogglePanelHeader",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"alignItems": "center",
					"items": []
				},
				"parentName": "TabContainer_HelpToggleButton",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_455poas",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_455poas_caption)#)#",
					"labelType": "headline-3",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "#0D2E4E",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"headingLevel": "label"
				},
				"parentName": "FlexContainer_HelpTogglePanelHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_HelpTogglePanel",
				"values": {
					"type": "crt.FlexContainer",
					"items": [],
					"direction": "column"
				},
				"parentName": "TabContainer_HelpToggleButton",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_HelpTabTextContent",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": true
				},
				"parentName": "FlexContainer_HelpTogglePanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_HelpPageDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpPageDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_HelpPropertiesPanelDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpPropertiesPanelDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Label_HelpConfElementsTabDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpConfElementsTabDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_HelpTabColumnsDescriptionList",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": true,
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
					"gap": "none",
					"wrap": "nowrap"
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "Label_HelpColumnsListTitle",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpColumnsListTitle_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabColumnsDescriptionList",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_HelpConfElementColumnDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpConfElementColumnDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabColumnsDescriptionList",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Label_HelpKeyColumnDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpKeyColumnDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabColumnsDescriptionList",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Label_HelpValueColumnDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpValueColumnDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabColumnsDescriptionList",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "Label_HelpLocalizableValueSourcesDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpLocalizableValueSourcesDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "Label_HelpUseCasesDescription",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_HelpUseCasesDescription_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "FlexContainer_HelpTabTextContent",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "TabContainer_PropertiesToggleTab",
				"values": {
					"type": "crt.TabContainer",
					"tools": [],
					"items": [],
					"caption": "#ResourceString(TabContainer_PropertiesToggleTab_caption)#",
					"iconPosition": "left-icon",
					"visible": true,
					"icon": "settings"
				},
				"parentName": "CardToggleTabPanel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_PropertiesToggleTabHeader",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"alignItems": "center",
					"items": []
				},
				"parentName": "TabContainer_PropertiesToggleTab",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_PropertiesToggleTabTitle",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_PropertiesToggleTabTitle_caption)#)#",
					"labelType": "headline-3",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "#0D2E4E",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"headingLevel": "label"
				},
				"parentName": "FlexContainer_PropertiesToggleTabHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_PropertiesToggleTabBody",
				"values": {
					"type": "crt.FlexContainer",
					"items": [],
					"direction": "column"
				},
				"parentName": "TabContainer_PropertiesToggleTab",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridContainer_ngb8and",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch"
				},
				"parentName": "FlexContainer_PropertiesToggleTabBody",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_SchemaTitle",
				"values": {
					"type": "crt.Input",
					"label": "$Resources.Strings.PageParameters_SchemaTitle",
					"control": "$PageParameters_SchemaTitle",
					"placeholder": "",
					"tooltip": "",
					"readonly": true,
					"multiline": true,
					"labelPosition": "auto",
					"visible": true,
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "GridContainer_ngb8and",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_SchemaCode",
				"values": {
					"type": "crt.Input",
					"label": "$Resources.Strings.PageParameters_SchemaCode",
					"control": "$PageParameters_SchemaCode",
					"placeholder": "",
					"tooltip": "",
					"readonly": true,
					"multiline": true,
					"labelPosition": "auto",
					"visible": true,
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					}
				},
				"parentName": "GridContainer_ngb8and",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ComboBox_PackageLookup",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PageParameters_SchemaPackage",
					"ariaLabel": "",
					"isAddAllowed": true,
					"showValueAsLink": true,
					"labelPosition": "auto",
					"controlActions": [],
					"listActions": [],
					"tooltip": "",
					"control": "$PageParameters_SchemaPackage",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"valueDetails": null,
					"mode": "List",
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 3,
						"rowSpan": 1
					}
				},
				"parentName": "GridContainer_ngb8and",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "addRecord_iaculf4",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_iaculf4_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_PackageLookup",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_LanguageLookup",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 4,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PageParameters_SchemaLanguage",
					"ariaLabel": "",
					"isAddAllowed": true,
					"showValueAsLink": true,
					"labelPosition": "auto",
					"controlActions": [],
					"listActions": [],
					"tooltip": "",
					"control": "$PageParameters_SchemaLanguage",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"valueDetails": null,
					"mode": "List"
				},
				"parentName": "GridContainer_ngb8and",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "addRecord_yiex6dt",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_yiex6dt_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_LanguageLookup",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_Culture",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 5,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PageParameters_Culture",
					"ariaLabel": "",
					"isAddAllowed": true,
					"showValueAsLink": true,
					"labelPosition": "auto",
					"controlActions": [],
					"listActions": [],
					"tooltip": "",
					"control": "$PageParameters_Culture",
					"mode": "List",
					"visible": true,
					"readonly": true,
					"placeholder": ""
				},
				"parentName": "GridContainer_ngb8and",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "addRecord_g3ckx5b",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_g3ckx5b_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_Culture",
				"propertyName": "listActions",
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
					"DataGrid_x3umv3r": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_x3umv3rDS",
							"filterAttributes": [
								{
									"name": "SearchFilter_ConfigurationElemLcz_DataGrid_x3umv3r",
									"loadOnChange": true
								},
								{
									"loadOnChange": true,
									"name": "DataGrid_x3umv3r_PredefinedFilter"
								}
							],
							"sortingConfig": {
								"default": [
									{
										"direction": "asc",
										"columnName": "SysSchema"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_x3umv3rDS_SysSchema": {
									"modelConfig": {
										"path": "DataGrid_x3umv3rDS.SysSchema"
									}
								},
								"DataGrid_x3umv3rDS_Key": {
									"modelConfig": {
										"path": "DataGrid_x3umv3rDS.Key"
									}
								},
								"DataGrid_x3umv3rDS_Value": {
									"modelConfig": {
										"path": "DataGrid_x3umv3rDS.Value"
									}
								},
								"DataGrid_x3umv3rDS_Id": {
									"modelConfig": {
										"path": "DataGrid_x3umv3rDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_x3umv3r_PredefinedFilter": {
						"value": {
							"items": {
								"5196312a-f1ba-4bea-ab91-3af2fe7bde0f": {
									"filterType": 1,
									"comparisonType": 3,
									"isEnabled": true,
									"trimDateTimeParameterToDate": false,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "ResourceType"
									},
									"isAggregative": false,
									"dataValueType": 4,
									"rightExpression": {
										"expressionType": 2,
										"parameter": {
											"dataValueType": 4,
											"value": 0
										}
									}
								}
							},
							"logicalOperation": 0,
							"isEnabled": true,
							"filterType": 6,
							"rootSchemaName": "SysLocalizableValue"
						}
					},
					"PageParameters_SchemaCode": {
						"modelConfig": {
							"path": "PageParameters.SchemaCode"
						}
					},
					"PageParameters_SchemaTitle": {
						"modelConfig": {
							"path": "PageParameters.SchemaTitle"
						}
					},
					"PageParameters_SchemaPackage": {
						"modelConfig": {
							"path": "PageParameters.SchemaPackage"
						}
					},
					"PageParameters_SchemaPackage_List": {
						"isCollection": true,
						"modelConfig": {
							"sortingConfig": {
								"default": [
									{
										"columnName": "Name",
										"direction": "asc"
									}
								]
							}
						}
					},
					"PageParameters_SchemaLanguage": {
						"modelConfig": {
							"path": "PageParameters.SchemaLanguage"
						}
					},
					"PageParameters_SchemaLanguage_List": {
						"isCollection": true,
						"modelConfig": {
							"sortingConfig": {
								"default": [
									{
										"columnName": "Name",
										"direction": "asc"
									}
								]
							}
						}
					},
					"PageParameters_Culture": {
						"modelConfig": {
							"path": "PageParameters.Culture"
						}
					},
					"PageParameters_Culture_List": {
						"isCollection": true,
						"modelConfig": {
							"sortingConfig": {
								"default": [
									{
										"columnName": "Name",
										"direction": "asc"
									}
								]
							}
						}
					},
					"DataGrid_xm741rq": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_xm741rqDS",
							"sortingConfig": {
								"default": [
									{
										"direction": "asc",
										"columnName": "DataSchemaName"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_xm741rqDS_DataSchemaName": {
									"modelConfig": {
										"path": "DataGrid_xm741rqDS.DataSchemaName"
									}
								},
								"DataGrid_xm741rqDS_ColumnName": {
									"modelConfig": {
										"path": "DataGrid_xm741rqDS.ColumnName"
									}
								},
								"DataGrid_xm741rqDS_RecordId": {
									"modelConfig": {
										"path": "DataGrid_xm741rqDS.RecordId"
									}
								},
								"DataGrid_xm741rqDS_Value": {
									"modelConfig": {
										"path": "DataGrid_xm741rqDS.Value"
									}
								},
								"DataGrid_xm741rqDS_Id": {
									"modelConfig": {
										"path": "DataGrid_xm741rqDS.Id"
									}
								}
							}
						}
					},
					"Summaries_LocalizableData_Expanded": {
						"value": true
					},
					"Summaries_ConfigurationElements_Expanded": {
						"value": true
					},
					"isLocalizableDataManagementEnabled": {
						"value": false
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"dependencies": {
						"DataGrid_x3umv3rDS": [
							{
								"attributePath": "SysPackage",
								"relationPath": "PageParameters.SchemaPackage"
							},
							{
								"attributePath": "SysCulture",
								"relationPath": "PageParameters.Culture"
							}
						],
						"DataGrid_xm741rqDS": [
							{
								"attributePath": "SysPackage",
								"relationPath": "PageParameters.SchemaPackage"
							},
							{
								"attributePath": "SysCulture",
								"relationPath": "PageParameters.Culture"
							}
						]
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"dataSources"
				],
				"values": {
					"DataGrid_x3umv3rDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "SysLocalizableValue",
							"attributes": {
								"SysSchema": {
									"path": "SysSchema"
								},
								"Key": {
									"path": "Key"
								},
								"Value": {
									"path": "Value"
								}
							}
						}
					},
					"DataGrid_xm741rqDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "SysDataLocalizableValue",
							"attributes": {
								"DataSchemaName": {
									"path": "DataSchemaName"
								},
								"ColumnName": {
									"path": "ColumnName"
								},
								"RecordId": {
									"path": "RecordId"
								},
								"Value": {
									"path": "Value"
								}
							}
						}
					}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[
			{
                request: "crt.HandleViewModelInitRequest",
                handler: async (request, next) => {
                    await next?.handle(request);
                    const featureService = new sdk.FeatureService();
					request.$context.isLocalizableDataManagementEnabled = await featureService.getFeatureState("PackageDataLocalizationManagement");
                }
            }
		]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});