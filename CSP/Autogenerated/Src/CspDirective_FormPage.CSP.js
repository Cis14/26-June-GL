define("CspDirective_FormPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "MainHeader",
				"values": {
					"visible": true,
					"alignItems": "stretch",
					"justifyContent": "start",
					"wrap": "nowrap",
					"stretch": false
				}
			},
			{
				"operation": "merge",
				"name": "MainHeaderTop",
				"values": {
					"wrap": "wrap",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"stretch": false,
					"alignItems": "stretch",
					"direction": "row",
					"gap": "small"
				}
			},
			{
				"operation": "remove",
				"name": "ActionButtonsContainer"
			},
			{
				"operation": "remove",
				"name": "SaveButton"
			},
			{
				"operation": "remove",
				"name": "CancelButton"
			},
			{
				"operation": "remove",
				"name": "CloseButton"
			},
			{
				"operation": "remove",
				"name": "SetRecordRightsButton"
			},
			{
				"operation": "remove",
				"name": "MainHeaderBottom"
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
				"name": "MainContainer"
			},
			{
				"operation": "remove",
				"name": "CardContentWrapper"
			},
			{
				"operation": "remove",
				"name": "TopAreaProfileContainer"
			},
			{
				"operation": "insert",
				"name": "FlexContainer_1960d88",
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
				"name": "GridContainer_hqxjr0b",
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
					"fitContent": false,
					"padding": {
						"top": "medium",
						"right": "none",
						"bottom": "medium",
						"left": "none"
					},
					"color": "primary",
					"borderRadius": "medium",
					"visible": true,
					"alignItems": "stretch",
					"layoutConfig": {},
					"selected": true,
					"stretch": true
				},
				"parentName": "FlexContainer_1960d88",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_4tglu7g",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.SysCspDirectiveNameDS_Name_2xcrjkk",
					"labelPosition": "auto",
					"control": "$SysCspDirectiveNameDS_Name_2xcrjkk",
					"visible": true,
					"readonly": true,
					"placeholder": "",
					"tooltip": "",
					"selected": false,
					"dragging": false
				},
				"parentName": "GridContainer_hqxjr0b",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_61e90ni",
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
						"right": "small",
						"bottom": "none",
						"left": "small"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap",
					"stretch": true,
					"layoutConfig": {},
					"selected": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_shus1if",
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
				"parentName": "FlexContainer_61e90ni",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_nqvla0f",
				"values": {
					"layoutConfig": {},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_nqvla0f_caption)#)#",
					"labelType": "headline-4",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"selected": true
				},
				"parentName": "FlexContainer_shus1if",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_dpiuuf5",
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
					"wrap": "nowrap"
				},
				"parentName": "FlexContainer_shus1if",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_da5wcsc",
				"values": {
					"layoutConfig": {},
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": {
								"enable": true,
								"multiple": true
							}
						}
					},
					"items": "$DataGrid_da5wcsc",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_da5wcscDS_Id",
					"columns": [
						{
							"id": "1737f9f6-bd06-aadc-7fe4-c710fcb1539e",
							"code": "DataGrid_da5wcscDS_CspUserTrustedSource",
							"path": "CspUserTrustedSource",
							"caption": "#ResourceString(DataGrid_da5wcscDS_CspUserTrustedSource)#",
							"dataValueType": 10,
							"referenceSchemaName": "SysCspUserTrustedSrc",
							"width": 596
						}
					],
					"stretch": true,
					"selected": false,
					"dragging": false
				},
				"parentName": "FlexContainer_dpiuuf5",
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
					"SysCspDirectiveNameDS_Name_2xcrjkk": {
						"modelConfig": {
							"path": "SysCspDirectiveNameDS.Name"
						}
					},
					"DataGrid_da5wcsc": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_da5wcscDS",
							"sortingConfig": {
								"default": [
									{
										"direction": "asc",
										"columnName": "CspUserTrustedSource"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_da5wcscDS_CspUserTrustedSource": {
									"modelConfig": {
										"path": "DataGrid_da5wcscDS.CspUserTrustedSource"
									}
								},
								"DataGrid_da5wcscDS_Id": {
									"modelConfig": {
										"path": "DataGrid_da5wcscDS.Id"
									}
								}
							}
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
					"dataSources": {
						"SysCspDirectiveNameDS": {
							"type": "crt.EntityDataSource",
							"scope": "page",
							"config": {
								"entitySchemaName": "SysCspDirectiveName"
							}
						},
						"DataGrid_da5wcscDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspUserTrustedSource": {
										"path": "CspUserTrustedSource"
									}
								}
							}
						}
					},
					"primaryDataSourceName": "SysCspDirectiveNameDS",
					"dependencies": {
						"DataGrid_da5wcscDS": [
							{
								"attributePath": "CspDirectiveName",
								"relationPath": "SysCspDirectiveNameDS.Id"
							}
						]
					}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});