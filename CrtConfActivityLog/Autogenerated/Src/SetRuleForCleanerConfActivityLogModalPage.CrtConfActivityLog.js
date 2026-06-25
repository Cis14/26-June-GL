define("SetRuleForCleanerConfActivityLogModalPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "TitleContainer",
				"values": {
					"direction": "column",
					"alignItems": "flex-start",
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
					"wrap": "nowrap"
				}
			},
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
				"name": "ContinueInOtherPageButton"
			},
			{
				"operation": "merge",
				"name": "MainContainer",
				"values": {
					"gap": {
						"columnGap": "medium",
						"rowGap": "none"
					},
					"padding": {
						"top": "medium",
						"right": "none",
						"bottom": "extra-small",
						"left": "none"
					},
					"alignItems": "stretch"
				}
			},
			{
				"operation": "merge",
				"name": "FooterContainer",
				"values": {
					"color": "transparent",
					"borderRadius": "none",
					"gap": "small"
				}
			},
			{
				"operation": "merge",
				"name": "CancelButton",
				"values": {
					"caption": "#ResourceString(CancelButton_caption)#",
					"color": "default",
					"size": "large",
					"iconPosition": "only-text",
					"clickMode": "default"
				}
			},
			{
				"operation": "move",
				"name": "CancelButton",
				"parentName": "InnerRightFooterContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "remove",
				"name": "SaveButton"
			},
			{
				"operation": "insert",
				"name": "MainSubtitleContainer",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					},
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
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SubtitleContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"gap": "none",
					"alignItems": "stretch",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "small",
						"right": "none",
						"bottom": "small",
						"left": "none"
					},
					"justifyContent": "start",
					"wrap": "wrap"
				},
				"parentName": "MainSubtitleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "RecordsOlderLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(RecordsOlderLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DaysWillBeLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(DaysWillBeLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AutomaticallyDeletedLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(AutomaticallyDeletedLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "ByScheduledLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(ByScheduledLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "HelpsLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(HelpsLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainer",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "SubtitleContainerTurnOnDataRetention",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"gap": "none",
					"alignItems": "stretch",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "small",
						"right": "none",
						"bottom": "small",
						"left": "none"
					},
					"justifyContent": "start",
					"wrap": "wrap"
				},
				"parentName": "MainSubtitleContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AutomaticDataCleanupLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(AutomaticDataCleanupLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ConfigurationLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(ConfigurationLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ActivityLogLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(ActivityLogLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "TurnedOffLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(TurnedOffLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "WithoutLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(WithoutLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "RecordsWillLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(RecordsWillLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "RecordsLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(RecordsLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "SignificantlyLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(SignificantlyLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "IncraseStorageLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(IncraseStorageLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "AmountLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(AmountLabel_caption)#)#",
					"labelType": "body",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "SubtitleContainerTurnOnDataRetention",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "insert",
				"name": "EnableCleanUpGridContainer",
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
				"parentName": "MainSubtitleContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "EnableCleanUpCheckbox",
				"values": {
					"type": "crt.Checkbox",
					"disabled": false,
					"inversed": false,
					"label": "$Resources.Strings.PageParameters_BooleanParameter1_x12txk8",
					"ariaLabel": "",
					"labelPosition": "right",
					"tooltip": "",
					"control": "$PageParameters_BooleanParameter1_x12txk8",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "EnableCleanUpGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SecondaryContainer",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "large",
						"rowGap": "extra-small"
					},
					"items": [],
					"fitContent": true,
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
				"parentName": "MainSubtitleContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "DeleteRecordsOlderThanDaysNumberInput",
				"values": {
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.PageParameters_IntegerParameter1_p4ly2wj",
					"control": "$PageParameters_IntegerParameter1_p4ly2wj",
					"readonly": false,
					"placeholder": "",
					"labelPosition": "above",
					"tooltip": "#ResourceString(DeleteRecordsOlderThanDaysNumberInput_tooltip)#",
					"visible": true,
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "SecondaryContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "RecordsNewerLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(RecordsNewerLabel_caption)#)#",
					"labelType": "button-small",
					"labelThickness": "light",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true,
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					}
				},
				"parentName": "SecondaryContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "InnerRightFooterContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"visible": true,
					"justifyContent": "end",
					"alignItems": "stretch",
					"wrap": "wrap",
					"padding": {
						"top": "12px",
						"right": "none",
						"bottom": "none",
						"left": "none"
					}
				},
				"parentName": "FooterContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SaveChangesButton",
				"values": {
					"type": "crt.Button",
					"clicked": {
						"request": "crt.SaveRecordRequest",
						"params": {
							"showSuccessMessage": "$PageParameters_BooleanParameter1_x12txk8",
							"messageTextAfterCompletion": "#ResourceString(SaveChangesButton_clicked_params_messageTextAfterCompletion)#"
						}
					},
					"color": "primary",
					"caption": "#ResourceString(SaveChangesButton_caption)#",
					"visible": true,
					"size": "large",
					"iconPosition": "only-text",
					"clickMode": "default",
					"disabled": "$HasUnsavedData | crt.InvertBooleanValue"
				},
				"parentName": "InnerRightFooterContainer",
				"propertyName": "items",
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
					"PageParameters_IntegerParameter1_p4ly2wj": {
						"modelConfig": {
							"path": "PageParameters.DeleteRecordsOlderThan"
						},
						"validators": {
							"Min_0": {
								"type": "crt.Min",
								"params": {
									"min": 0,
									"message": "#ResourceString(MinValidatorMessage)#"
								}
							},
							"required": {
								"type": "crt.Required"
							}
						}
					},
					"PageParameters_BooleanParameter1_x12txk8": {
						"modelConfig": {
							"path": "PageParameters.EnableCleanUp"
						},
						"DeleteRecordsOlderThanRequired": {
							"value": true
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
					"dataSources": {}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[
			  {
			    request: "crt.CanDiscardUnsavedDataRequest",
			    handler: async (request, next) => {
			      // turn off Required 
			      request.$context.disableAttributeValidator(
			        "PageParameters_IntegerParameter1_p4ly2wj",
			        "required"
			      );
			      return true; 
			    }
			  }
			]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});