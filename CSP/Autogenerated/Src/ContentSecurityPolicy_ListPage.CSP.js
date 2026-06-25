define("ContentSecurityPolicy_ListPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "MainHeader",
				"values": {
					"visible": true,
					"borderRadius": "none",
					"gap": "small",
					"stretch": false
				}
			},
			{
				"operation": "remove",
				"name": "PageTitle"
			},
			{
				"operation": "remove",
				"name": "ActionButtonsContainer"
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
				"operation": "remove",
				"name": "SectionContentWrapper"
			},
			{
				"operation": "remove",
				"name": "DataTable"
			},
			{
				"operation": "insert",
				"name": "GridContainer_5y1huba",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": false,
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
					"stretch": true
				},
				"parentName": "MainHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "PageTitle",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(PageTitle_caption)#)#",
					"labelType": "headline-1",
					"labelThickness": "default",
					"labelColor": "#0D2E4E",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"selected": false
				},
				"parentName": "GridContainer_5y1huba",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_9ypke69",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
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
					"alignItems": "stretch",
					"justifyContent": "end",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_5y1huba",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Button_jr8l5ej",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_jr8l5ej_caption)#",
					"color": "default",
					"disabled": false,
					"size": "large",
					"iconPosition": "only-text",
					"visible": false,
					"clicked": {},
					"clickMode": "default"
				},
				"parentName": "FlexContainer_9ypke69",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabPanel_nzntbs8",
				"values": {
					"type": "crt.TabPanel",
					"items": [],
					"mode": "tab",
					"styleType": "default",
					"bodyBackgroundColor": "primary-contrast-500",
					"tabTitleColor": "auto",
					"selectedTabTitleColor": "auto",
					"headerBackgroundColor": "auto",
					"underlineSelectedTabColor": "auto",
					"fitContent": false,
					"visible": true,
					"stretch": true
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabContainer_ryopf9z",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_ryopf9z_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "TabPanel_nzntbs8",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridContainer_72otm7r",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
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
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"selected": false,
					"dragging": false,
					"currentLayoutConfig": {
						"column": 1,
						"row": 3,
						"rowSpan": 1,
						"colSpan": 1
					}
				},
				"parentName": "TabContainer_ryopf9z",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_4fyhgb4",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
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
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_72otm7r",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_4kb9jwe",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_4kb9jwe_caption)#",
					"color": "primary",
					"disabled": false,
					"size": "large",
					"iconPosition": "left-icon",
					"visible": true,
					"icon": "add-button-icon",
					"clicked": {
						"request": "crt.CreateRecordRequest",
						"params": {
							"entityName": "SysCspUserTrustedSrc"
						}
					},
					"clickMode": "default",
					"layoutConfig": {}
				},
				"parentName": "FlexContainer_4fyhgb4",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_rgewqxx",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
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
					"alignItems": "stretch",
					"justifyContent": "end",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_72otm7r",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SearchFilter_cpbsmsl",
				"values": {
					"type": "crt.SearchFilter",
					"placeholder": "#ResourceString(SearchFilter_cpbsmsl_placeholder)#",
					"_filterOptions": {
						"expose": [
							{
								"attribute": "SearchFilter_cpbsmsl_DataGrid_jh58umy",
								"converters": [
									{
										"converter": "crt.SearchFilterAttributeConverter",
										"args": [
											"DataGrid_jh58umy"
										]
									}
								]
							}
						],
						"from": [
							"SearchFilter_cpbsmsl_SearchValue",
							"SearchFilter_cpbsmsl_FilteredColumnsGroups"
						]
					},
				},
				"parentName": "FlexContainer_rgewqxx",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_omzqfha",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_omzqfha_caption)#",
					"color": "default",
					"disabled": false,
					"size": "large",
					"iconPosition": "only-icon",
					"visible": true,
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "DataGrid_jh58umyDS"
						}
					},
					"clickMode": "default",
					"icon": "reload-button-icon"
				},
				"parentName": "FlexContainer_rgewqxx",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_o65bf91",
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
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap",
					"stretch": true,
					"layoutConfig": {
						"row": 1,
						"rowSpan": 1,
						"column": 1,
						"colSpan": 2
					},
					"selected": true,
					"dragging": false,
					"currentLayoutConfig": {
						"column": 1,
						"row": 1,
						"rowSpan": 1,
						"colSpan": 2
					}
				},
				"parentName": "TabContainer_ryopf9z",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_jh58umy",
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
							"enable": true,
							"itemsCreation": false
						}
					},
					"items": "$DataGrid_jh58umy",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_jh58umyDS_Id",
					"columns": [
						{
							"id": "297c5893-9d8c-73b2-93f8-df38a02253ba",
							"code": "DataGrid_jh58umyDS_Source",
							"caption": "#ResourceString(DataGrid_jh58umyDS_Source)#",
							"dataValueType": 30
						},
						{
							"id": "05a36c94-6b37-dfdc-f1eb-78abe004e459",
							"code": "DataGrid_jh58umyDS_Active",
							"path": "Active",
							"caption": "#ResourceString(DataGrid_jh58umyDS_Active)#",
							"dataValueType": 12
						},
						{
							"id": "6cca3848-80cc-2bc2-419b-f96e1e2612ac",
							"code": "DataGrid_jh58umyDS_Verified",
							"path": "Verified",
							"caption": "#ResourceString(DataGrid_jh58umyDS_Verified)#",
							"dataValueType": 12
						},
						{
							"id": "8aa07392-b79a-d431-e3b8-c37b0a280b17",
							"code": "DataGrid_jh58umyDS_SysCspUsrSrcInDirectvCspUserTrustedSourceIdCount587afb69",
							"path": "[SysCspUsrSrcInDirectv:CspUserTrustedSource].Id",
							"caption": "#ResourceString(DataGrid_jh58umyDS_SysCspUsrSrcInDirectvCspUserTrustedSourceIdCount587afb69)#",
							"dataValueType": 4,
							"referenceSchemaName": "SysCspUsrSrcInDirectv",
							"width": 244
						},
						{
							"id": "2c986bcf-9ddd-f518-3d43-ae552c9d6d7d",
							"code": "DataGrid_jh58umyDS_Description",
							"path": "Description",
							"caption": "#ResourceString(DataGrid_jh58umyDS_Description)#",
							"dataValueType": 30,
							"width": 880
						}
					],
					"layoutConfig": {},
					"selected": true,
					"stretch": true,
					"selectionState": "$DataGrid_jh58umy_SelectionState",
					"_selectionOptions": {
						"attribute": "DataGrid_jh58umy_SelectionState"
					},
					"bulkActions": []
				},
				"parentName": "FlexContainer_o65bf91",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_jh58umy_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_jh58umy",
							"filters": "$DataGrid_jh58umy | crt.ToCollectionFilters : 'DataGrid_jh58umy' : $DataGrid_jh58umy_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_jh58umy_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_jh58umy",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_jh58umy_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_jh58umyDS",
							"filters": "$DataGrid_jh58umy | crt.ToCollectionFilters : 'DataGrid_jh58umy' : $DataGrid_jh58umy_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_jh58umy_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_jh58umy",
				"propertyName": "bulkActions",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "TabContainer_pquua7b",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_pquua7b_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "TabPanel_nzntbs8",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridContainer_roxu06x",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
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
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch"
				},
				"parentName": "TabContainer_pquua7b",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_v5snota",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": false,
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
					"justifyContent": "end",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_roxu06x",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SearchFilter_tz99hcm",
				"values": {
					"type": "crt.SearchFilter",
					"placeholder": "#ResourceString(SearchFilter_tz99hcm_placeholder)#",
					"_filterOptions": {
						"expose": [
							{
								"attribute": "SearchFilter_tz99hcm_DataGrid_de8h30n",
								"converters": [
									{
										"converter": "crt.SearchFilterAttributeConverter",
										"args": [
											"DataGrid_de8h30n"
										]
									}
								]
							}
						],
						"from": [
							"SearchFilter_tz99hcm_SearchValue",
							"SearchFilter_tz99hcm_FilteredColumnsGroups"
						]
					},
				},
				"parentName": "FlexContainer_v5snota",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_dns3zu8",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_dns3zu8_caption)#",
					"color": "default",
					"disabled": false,
					"size": "large",
					"iconPosition": "only-icon",
					"visible": true,
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "DataGrid_de8h30nDS"
						}
					},
					"adding": false,
					"clickMode": "default",
					"icon": "reload-button-icon"
				},
				"parentName": "FlexContainer_v5snota",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_cw39fyr",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": false,
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
					"wrap": "nowrap",
					"stretch": true
				},
				"parentName": "TabContainer_pquua7b",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_de8h30n",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": false,
							"toolbar": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"items": "$DataGrid_de8h30n",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_de8h30nDS_Id",
					"columns": [
						{
							"id": "157831b3-3d60-5a60-c151-0de3d6a8ffb8",
							"code": "DataGrid_de8h30nDS_Name",
							"caption": "#ResourceString(DataGrid_de8h30nDS_Name)#",
							"dataValueType": 28,
							"width": 352
						},
						{
							"id": "4fd88f7d-39a4-c431-045c-f425cb38d1a6",
							"code": "DataGrid_de8h30nDS_SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b",
							"path": "[SysCspUsrSrcInDirectv:CspDirectiveName].Id",
							"caption": "#ResourceString(DataGrid_de8h30nDS_SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b)#",
							"dataValueType": 4,
							"referenceSchemaName": "SysCspUsrSrcInDirectv",
							"width": 286
						}
					],
					"layoutConfig": {},
					"selected": true,
					"dragging": true,
					"stretch": true
				},
				"parentName": "FlexContainer_cw39fyr",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabContainer_ab8lfbc",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_ab8lfbc_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "TabPanel_nzntbs8",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "GridContainer_rhdbkaw",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
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
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch"
				},
				"parentName": "TabContainer_ab8lfbc",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_2okyw4s",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
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
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_rhdbkaw",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FolderTreeActions_fbg82l0",
				"values": {
					"type": "crt.FolderTreeActions",
					"caption": "#ResourceString(FolderTreeActions_fbg82l0_caption)#",
					"folderTree": "FolderTree_embu9fm"
				},
				"parentName": "FlexContainer_2okyw4s",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_zrr3fwn",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
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
					"justifyContent": "end",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "GridContainer_rhdbkaw",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SearchFilter_qebzpyy",
				"values": {
					"type": "crt.SearchFilter",
					"placeholder": "#ResourceString(SearchFilter_qebzpyy_placeholder)#",
					"_filterOptions": {
						"expose": [
							{
								"attribute": "SearchFilter_qebzpyy_DataGrid_k19tzea",
								"converters": [
									{
										"converter": "crt.SearchFilterAttributeConverter",
										"args": [
											"DataGrid_k19tzea"
										]
									}
								]
							}
						],
						"from": [
							"SearchFilter_qebzpyy_SearchValue",
							"SearchFilter_qebzpyy_FilteredColumnsGroups"
						]
					},
				},
				"parentName": "FlexContainer_zrr3fwn",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_g7ygaqv",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_g7ygaqv_caption)#",
					"color": "default",
					"disabled": false,
					"size": "large",
					"iconPosition": "only-icon",
					"visible": true,
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "DataGrid_k19tzeaDS"
						}
					},
					"clickMode": "default",
					"icon": "reload-button-icon"
				},
				"parentName": "FlexContainer_zrr3fwn",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_vv7vlkl",
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
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap",
					"stretch": true
				},
				"parentName": "TabContainer_ab8lfbc",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FolderTree_embu9fm",
				"values": {
					"type": "crt.FolderTree",
					"caption": "#ResourceString(FolderTree_embu9fm_caption)#",
					"sourceSchemaName": "FolderTree",
					"borderRadius": "none",
					"rootSchemaName": "VwSysCspViolation",
					"layoutConfig": {
						"width": 321.09375
					},
					"_filterOptions": {
						"expose": [
							{
								"attribute": "FolderTree_embu9fm_active_folder_filter",
								"converters": [
									{
										"converter": "crt.FolderTreeActiveFilterAttributeConverter",
										"args": ["VwSysCspViolation"]
									}
								]
							}
						],
						"from": [
							"FolderTree_embu9fm_items",
							"FolderTree_embu9fm_favoriteItems",
							"FolderTree_embu9fm_active_folder_id"
						]
					}
				},
				"parentName": "FlexContainer_vv7vlkl",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_k19tzea",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": {
								"enable": true,
								"multiple": true
							},
							"editable": {
								"itemsCreation": false
							}
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"items": "$DataGrid_k19tzea",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_k19tzeaDS_Id",
					"columns": [
						{
							"id": "eb87cd9b-104b-b8c9-4ce9-785b03fcd9c6",
							"code": "DataGrid_k19tzeaDS_BlockedHost",
							"path": "BlockedHost",
							"caption": "#ResourceString(DataGrid_k19tzeaDS_BlockedHost)#",
							"dataValueType": 28
						},
						{
							"id": "569aa4d0-678c-8345-bc88-6cf748b80bd7",
							"code": "DataGrid_k19tzeaDS_ViolatedDirective",
							"path": "ViolatedDirective",
							"caption": "#ResourceString(DataGrid_k19tzeaDS_ViolatedDirective)#",
							"dataValueType": 27
						},
						{
							"id": "05f4819a-910d-88ae-7d70-ae76bac15dab",
							"code": "DataGrid_k19tzeaDS_LastViolationDate",
							"path": "LastViolationDate",
							"caption": "#ResourceString(DataGrid_k19tzeaDS_LastViolationDate)#",
							"dataValueType": 7
						}
					],
					"rowToolbarItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(RowToolbar_OpenItem_caption)#",
							"icon": "edit-row-action",
							"disabled": "$DataGrid_k19tzea.PrimaryModelMode | crt.IsEqual : 'create'",
							"clicked": {
								"request": "crt.UpdateRecordRequest",
								"params": {
									"itemsAttributeName": "Items",
									"recordId": "$DataGrid_k19tzea.DataGrid_k19tzeaDS_Id"
								},
								"useRelativeContext": true
							}
						},
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(AddToTrustedButtonCaption)#",
							"icon": "add-button-icon",
							"clicked": {
								"request": "crt.AddToTrustedSourceRequest",
								"params": {
									"blockedSource": "$DataGrid_k19tzea.DataGrid_k19tzeaDS_BlockedHost",
									"directive": "$DataGrid_k19tzea.DataGrid_k19tzeaDS_ViolatedDirective"
								},
								"useRelativeContext": true
							}
						}
					],
					"stretch": true,
					"selectionState": "$DataGrid_k19tzea_SelectionState",
					"_selectionOptions": {
						"attribute": "DataGrid_k19tzea_SelectionState"
					},
					"bulkActions": []
				},
				"parentName": "FlexContainer_vv7vlkl",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_k19tzea_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_k19tzea",
							"filters": "$DataGrid_k19tzea | crt.ToCollectionFilters : 'DataGrid_k19tzea' : $DataGrid_k19tzea_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_k19tzea_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_k19tzea",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_k19tzea_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_k19tzeaDS",
							"filters": "$DataGrid_k19tzea | crt.ToCollectionFilters : 'DataGrid_k19tzea' : $DataGrid_k19tzea_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_k19tzea_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_k19tzea",
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
					"DataGrid_jh58umy": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_jh58umyDS",
							"sortingConfig": {
								"default": [
									{
										"direction": "asc",
										"columnName": "Active"
									}
								]
							},
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "DataGrid_jh58umy_PredefinedFilter"
								},
								{
									"name": "SearchFilter_cpbsmsl_DataGrid_jh58umy",
									"loadOnChange": true
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_jh58umyDS_Source": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.Source"
									}
								},
								"DataGrid_jh58umyDS_Active": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.Active"
									}
								},
								"DataGrid_jh58umyDS_Verified": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.Verified"
									}
								},
								"DataGrid_jh58umyDS_SysCspUsrSrcInDirectvCspUserTrustedSourceIdCount587afb69": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.SysCspUsrSrcInDirectvCspUserTrustedSourceIdCount587afb69"
									}
								},
								"DataGrid_jh58umyDS_Description": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.Description"
									}
								},
								"DataGrid_jh58umyDS_Id": {
									"modelConfig": {
										"path": "DataGrid_jh58umyDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_de8h30n": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_de8h30nDS",
							"sortingConfig": {
								"default": [
									{
										"direction": "desc",
										"columnName": "SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b"
									}
								]
							},
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "DataGrid_de8h30n_PredefinedFilter"
								},
								{
									"name": "SearchFilter_tz99hcm_DataGrid_de8h30n",
									"loadOnChange": true
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_de8h30nDS_Name": {
									"modelConfig": {
										"path": "DataGrid_de8h30nDS.Name"
									}
								},
								"DataGrid_de8h30nDS_SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b": {
									"modelConfig": {
										"path": "DataGrid_de8h30nDS.SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b"
									}
								},
								"DataGrid_de8h30nDS_Id": {
									"modelConfig": {
										"path": "DataGrid_de8h30nDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_k19tzea": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_k19tzeaDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "DataGrid_k19tzea_PredefinedFilter"
								},
								{
									"name": "FolderTree_embu9fm_active_folder_filter",
									"loadOnChange": true
								},
								{
									"name": "SearchFilter_qebzpyy_DataGrid_k19tzea",
									"loadOnChange": true
								}
							],
							"sortingConfig": {
								"default": [
									{
										"direction": "desc",
										"columnName": "LastViolationDate"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_k19tzeaDS_BlockedHost": {
									"modelConfig": {
										"path": "DataGrid_k19tzeaDS.BlockedHost"
									}
								},
								"DataGrid_k19tzeaDS_ViolatedDirective": {
									"modelConfig": {
										"path": "DataGrid_k19tzeaDS.ViolatedDirective"
									}
								},
								"DataGrid_k19tzeaDS_LastViolationDate": {
									"modelConfig": {
										"path": "DataGrid_k19tzeaDS.LastViolationDate"
									}
								},
								"DataGrid_k19tzeaDS_Id": {
									"modelConfig": {
										"path": "DataGrid_k19tzeaDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_jh58umy_PredefinedFilter": {
						"value": null
					},
					"DataGrid_de8h30n_PredefinedFilter": {
						"value": null
					},
					"DataGrid_k19tzea_PredefinedFilter": {
						"value": null
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"dataSources": {
						"DataGrid_jh58umyDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUserTrustedSrc",
								"attributes": {
									"Source": {
										"path": "Source"
									},
									"Active": {
										"path": "Active"
									},
									"Verified": {
										"path": "Verified"
									},
									"SysCspUsrSrcInDirectvCspUserTrustedSourceIdCount587afb69": {
										"type": "Aggregation",
										"path": "[SysCspUsrSrcInDirectv:CspUserTrustedSource].Id",
										"aggregationConfig": {
											"aggregationFunction": "Count",
											"filter": null
										}
									},
									"Description": {
										"path": "Description"
									}
								}
							}
						},
						"DataGrid_de8h30nDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspDirectiveName",
								"attributes": {
									"Name": {
										"path": "Name"
									},
									"SysCspUsrSrcInDirectvCspDirectiveNameIdCountd4851e9b": {
										"type": "Aggregation",
										"path": "[SysCspUsrSrcInDirectv:CspDirectiveName].Id",
										"aggregationConfig": {
											"aggregationFunction": "Count",
											"filter": null
										}
									}
								}
							}
						},
						"DataGrid_k19tzeaDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "VwSysCspViolation",
								"attributes": {
									"BlockedHost": {
										"path": "BlockedHost"
									},
									"ViolatedDirective": {
										"path": "ViolatedDirective"
									},
									"LastViolationDate": {
										"path": "LastViolationDate"
									}
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