define("CspTrustedSource_FormPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
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
				"name": "FlexContainer_0edonen",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"layoutConfig": {},
					"selected": true,
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
				"name": "GridContainer_sxtokt1",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)",
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
					"stretch": true,
					"dragging": false
				},
				"parentName": "FlexContainer_0edonen",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Input_zqir2tx",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.SysCspUserTrustedSrcDS_Source_1q1vpzc",
					"labelPosition": "left",
					"control": "$SysCspUserTrustedSrcDS_Source_1q1vpzc",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "GridContainer_sxtokt1",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Checkbox_anxgs6x",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.SysCspUserTrustedSrcDS_Active_z36bn9n",
					"labelPosition": "right",
					"control": "$SysCspUserTrustedSrcDS_Active_z36bn9n",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "GridContainer_sxtokt1",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Checkbox_d8dja20",
				"values": {
					"layoutConfig": {
						"column": 3,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.SysCspUserTrustedSrcDS_Verified_lzpzx3i",
					"labelPosition": "right",
					"control": "$SysCspUserTrustedSrcDS_Verified_lzpzx3i",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "GridContainer_sxtokt1",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Input_b3lonrw",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 3,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"multiline": false,
					"label": "$Resources.Strings.SysCspUserTrustedSrcDS_Description_tzig0si",
					"labelPosition": "auto",
					"control": "$SysCspUserTrustedSrcDS_Description_tzig0si"
				},
				"parentName": "GridContainer_sxtokt1",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "FlexContainer_md2rzvq",
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
					"stretch": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_zs1delm",
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
					"stretch": true,
					"layoutConfig": {},
					"selected": true,
					"dragging": false,
					"currentLayoutConfig": {
						"column": 1,
						"row": 5,
						"rowSpan": 1,
						"colSpan": 1
					}
				},
				"parentName": "FlexContainer_md2rzvq",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_piv3rhu",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_piv3rhu_caption)#)#",
					"labelType": "headline-4",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"layoutConfig": {},
					"selected": false,
					"dragging": false
				},
				"parentName": "FlexContainer_zs1delm",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_1lga0f6",
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
					"stretch": false
				},
				"parentName": "FlexContainer_zs1delm",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_sl1qb9e",
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
							"lookupItemsCreation": false
						}
					},
					"items": "$DataGrid_sl1qb9e",
					"visible": true,
					"stretch": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_sl1qb9eDS_Id",
					"columns": [
						{
							"id": "c56f0046-8085-2266-c921-dc80eec94ecb",
							"code": "DataGrid_sl1qb9eDS_CspDirectiveName",
							"path": "CspDirectiveName",
							"caption": "#ResourceString(DataGrid_sl1qb9eDS_CspDirectiveName)#",
							"dataValueType": 10,
							"referenceSchemaName": "SysCspDirectiveName"
						}
					]
				},
				"parentName": "FlexContainer_1lga0f6",
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
					"SysCspUserTrustedSrcDS_Source_1q1vpzc": {
						"modelConfig": {
							"path": "SysCspUserTrustedSrcDS.Source"
						}
					},
					"SysCspUserTrustedSrcDS_Active_z36bn9n": {
						"modelConfig": {
							"path": "SysCspUserTrustedSrcDS.Active"
						}
					},
					"DataGrid_235fxln": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_235fxlnDS"
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_235fxlnDS_CspDirectiveName": {
									"modelConfig": {
										"path": "DataGrid_235fxlnDS.CspDirectiveName"
									}
								},
								"DataGrid_235fxlnDS_Id": {
									"modelConfig": {
										"path": "DataGrid_235fxlnDS.Id"
									}
								}
							}
						}
					},
					"GridDetail_ty4ebhf": {
						"isCollection": true,
						"modelConfig": {
							"path": "GridDetail_ty4ebhfDS"
						},
						"viewModelConfig": {
							"attributes": {
								"GridDetail_ty4ebhfDS_CspDirectiveName": {
									"modelConfig": {
										"path": "GridDetail_ty4ebhfDS.CspDirectiveName"
									}
								},
								"GridDetail_ty4ebhfDS_Id": {
									"modelConfig": {
										"path": "GridDetail_ty4ebhfDS.Id"
									}
								}
							}
						}
					},
					"GridDetail_rdler5m": {
						"isCollection": true,
						"modelConfig": {
							"path": "GridDetail_rdler5mDS"
						},
						"viewModelConfig": {
							"attributes": {
								"GridDetail_rdler5mDS_CspDirectiveName": {
									"modelConfig": {
										"path": "GridDetail_rdler5mDS.CspDirectiveName"
									}
								},
								"GridDetail_rdler5mDS_Id": {
									"modelConfig": {
										"path": "GridDetail_rdler5mDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_zyxosey": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_zyxoseyDS"
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_zyxoseyDS_CspDirectiveName": {
									"modelConfig": {
										"path": "DataGrid_zyxoseyDS.CspDirectiveName"
									}
								},
								"DataGrid_zyxoseyDS_CreatedBy": {
									"modelConfig": {
										"path": "DataGrid_zyxoseyDS.CreatedBy"
									}
								},
								"DataGrid_zyxoseyDS_Id": {
									"modelConfig": {
										"path": "DataGrid_zyxoseyDS.Id"
									}
								}
							}
						}
					},
					"DataGrid_3ho99l8": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_3ho99l8DS"
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_3ho99l8DS_CreatedBy": {
									"modelConfig": {
										"path": "DataGrid_3ho99l8DS.CreatedBy"
									}
								},
								"DataGrid_3ho99l8DS_Id": {
									"modelConfig": {
										"path": "DataGrid_3ho99l8DS.Id"
									}
								}
							}
						}
					},
					"DataGrid_sl1qb9e": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_sl1qb9eDS"
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_sl1qb9eDS_CspDirectiveName": {
									"modelConfig": {
										"path": "DataGrid_sl1qb9eDS.CspDirectiveName"
									}
								},
								"DataGrid_sl1qb9eDS_Id": {
									"modelConfig": {
										"path": "DataGrid_sl1qb9eDS.Id"
									}
								}
							}
						}
					},
					"SysCspUserTrustedSrcDS_Verified_lzpzx3i": {
						"modelConfig": {
							"path": "SysCspUserTrustedSrcDS.Verified"
						}
					},
					"SysCspUserTrustedSrcDS_Description_tzig0si": {
						"modelConfig": {
							"path": "SysCspUserTrustedSrcDS.Description"
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
						"SysCspUserTrustedSrcDS": {
							"type": "crt.EntityDataSource",
							"scope": "page",
							"config": {
								"entitySchemaName": "SysCspUserTrustedSrc"
							}
						},
						"DataGrid_235fxlnDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspDirectiveName": {
										"path": "CspDirectiveName"
									}
								}
							}
						},
						"GridDetail_ty4ebhfDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspDirectiveName": {
										"path": "CspDirectiveName"
									}
								}
							}
						},
						"GridDetail_rdler5mDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspDirectiveName": {
										"path": "CspDirectiveName"
									}
								}
							}
						},
						"DataGrid_zyxoseyDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspDirectiveName": {
										"path": "CspDirectiveName"
									},
									"CreatedBy": {
										"path": "CreatedBy"
									}
								}
							}
						},
						"DataGrid_3ho99l8DS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CreatedBy": {
										"path": "CreatedBy"
									}
								}
							}
						},
						"DataGrid_sl1qb9eDS": {
							"type": "crt.EntityDataSource",
							"scope": "viewElement",
							"config": {
								"entitySchemaName": "SysCspUsrSrcInDirectv",
								"attributes": {
									"CspDirectiveName": {
										"path": "CspDirectiveName"
									}
								}
							}
						}
					},
					"primaryDataSourceName": "SysCspUserTrustedSrcDS",
					"dependencies": {
						"DataGrid_sl1qb9eDS": [
							{
								"attributePath": "CspUserTrustedSource",
								"relationPath": "SysCspUserTrustedSrcDS.Id"
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