define("ApplyTranslationsSettingsMiniPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
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
				"operation": "merge",
				"name": "ContinueInOtherPageButton",
				"values": {
					"clicked": {},
					"color": "default",
					"clickMode": "default"
				}
			},
			{
				"operation": "merge",
				"name": "CloseButton",
				"values": {
					"clicked": {},
					"caption": "#ResourceString(CloseButton_caption)#"
				}
			},
			{
				"operation": "merge",
				"name": "MainContainer",
				"values": {
					"gap": {
						"columnGap": "large",
						"rowGap": "extra-small"
					},
					"alignItems": "stretch"
				}
			},
			{
				"operation": "merge",
				"name": "CancelButton",
				"values": {
					"clicked": {},
					"caption": "#ResourceString(CancelButton_caption)#",
					"color": "default",
					"size": "large",
					"iconPosition": "only-text",
					"clickMode": "default"
				}
			},
			{
				"operation": "merge",
				"name": "SaveButton",
				"values": {
					"clicked": {},
					"caption": "#ResourceString(SaveButton_caption)#"
				}
			},
			{
				"operation": "insert",
				"name": "WindowDescriptionLabel",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(WindowDescriptionLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ExpansionPanel_AdvancedSettings",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					},
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(ExpansionPanel_AdvancedSettings_title)#",
					"toggleType": "material",
					"togglePosition": "before",
					"expanded": false,
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
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_AdvancedSettings",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": true
				},
				"parentName": "ExpansionPanel_AdvancedSettings",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SkipTranslationsValidationCheckbox",
				"values": {
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.PageParameters_BooleanParameter1_jlrysey",
					"labelPosition": "right",
					"control": "$PageParameters_BooleanParameter1_jlrysey",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": "#ResourceString(SkipTranslationsValidationCheckbox_tooltip)#"
				},
				"parentName": "FlexContainer_AdvancedSettings",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ApplySpecificLanguageCheckbox",
				"values": {
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.PageParameters_BooleanParameter1_1a69gvr",
					"labelPosition": "right",
					"control": "$PageParameters_BooleanParameter1_1a69gvr",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": "#ResourceString(ApplySpecificLanguageCheckbox_tooltip)#"
				},
				"parentName": "FlexContainer_AdvancedSettings",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "LanguageComboBox",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PageParameters_LookupParameter1_dojrswl",
					"labelPosition": "above",
					"control": "$PageParameters_LookupParameter1_dojrswl",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": [],
					"visible": false,
					"readonly": false,
					"placeholder": "",
					"tooltip": "",
					"isSimpleLookup": null,
					"valueDetails": null,
					"secondaryDisplayValue": null,
					"mode": "List"
				},
				"parentName": "FlexContainer_AdvancedSettings",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "ListAction_4rngtym",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "ComboBox.AddNewRecord",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "LanguageComboBox",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ApplyAllTranslationsForcedlyCheckbox",
				"values": {
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.PageParameters_BooleanParameter1_ydqnq2i",
					"labelPosition": "right",
					"control": "$PageParameters_BooleanParameter1_ydqnq2i",
					"visible": false,
					"readonly": false,
					"placeholder": "",
					"tooltip": "#ResourceString(ApplyAllTranslationsForcedlyCheckbox_tooltip)#"
				},
				"parentName": "FlexContainer_AdvancedSettings",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ComboBox_PackageToSaveTranslations",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PageParameters_LocalizationPackage_lw89yji",
					"ariaLabel": "",
					"isAddAllowed": true,
					"showValueAsLink": false,
					"labelPosition": "above",
					"controlActions": [],
					"listActions": [],
					"tooltip": "",
					"control": "$PageParameters_LocalizationPackage_lw89yji",
					"mode": "List",
					"visible": "$ShowLocalizationPackage",
					"readonly": false,
					"placeholder": ""
				},
				"parentName": "FlexContainer_AdvancedSettings",
				"propertyName": "items",
				"index": 4
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"PageParameters_BooleanParameter1_1a69gvr": {
						"modelConfig": {
							"path": "PageParameters.UseSpecifiedLanguageOnly"
						}
					},
					"PageParameters_LookupParameter1_dojrswl": {
						"modelConfig": {
							"path": "PageParameters.Language"
						}
					},
					"PageParameters_BooleanParameter1_ydqnq2i": {
						"modelConfig": {
							"path": "PageParameters.ApplyAllTranslationsForcedly"
						}
					},
					"PageParameters_BooleanParameter1_jlrysey": {
						"modelConfig": {
							"path": "PageParameters.SkipTranslationsValidationAfterApply"
						}
					},
					"PageParameters_LocalizationPackage_lw89yji": {
						"modelConfig": {
							"path": "PageParameters.LocalizationPackage"
						}
					},
					"PageParameters_LocalizationPackage_lw89yji_List": {
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
					"PageParameters_LookupParameter1_dojrswl_List": {
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
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"dataSources": {},
					"ShowLocalizationPackage": {
						"value": false
					}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[{
				request: "crt.LoadDataRequest",
				handler: async (request, next) => {
					var languageDataSourceName = "PageParameters_LookupParameter1_dojrswl_List_DS";
					if(request.dataSourceName !== languageDataSourceName) {
						return await next?.handle(request);
					}
					request.parameters.push({
						type: "filter",
						value: {
							"items": {
								"8533d26b-cedc-4a3f-b701-c58775182a8d": {
									"filterType": 2,
									"comparisonType": 2,
									"isEnabled": true,
									"isNull": false,
									"trimDateTimeParameterToDate": false,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "[SysCulture:Language].Id"
									},
									"isAggregative": false,
									"dataValueType": 1
								}
							},
							"logicalOperation": 0,
							"isEnabled": true,
							"filterType": 6,
							"rootSchemaName": "SysLanguage"
						}
					});
					return await next?.handle(request);
				}
			},]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});