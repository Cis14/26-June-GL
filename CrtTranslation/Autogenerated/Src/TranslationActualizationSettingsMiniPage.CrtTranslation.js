define("TranslationActualizationSettingsMiniPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "PageTitle",
				"values": {
					"caption": "#MacrosTemplateString(#ResourceString(PageTitle_caption)#)#",
					"visible": true,
					"headingLevel": "label"
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
						"rowGap": "medium"
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
					"clicked": {
						"request": "crt.SaveRecordRequest",
						"params": {
							"showSuccessMessage": false
						}
					},
					"caption": "#ResourceString(SaveButton_caption)#"
				}
			},
			{
				"operation": "insert",
				"name": "TopContentFlexContainer",
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
					"gap": "extra-small",
					"wrap": "nowrap"
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_uf3ltxt",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_uf3ltxt_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "TopContentFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Checkbox_eias582",
				"values": {
					"type": "crt.Checkbox",
					"label": "$Resources.Strings.PageParameters_RunActualizationFromScratch_u5qc189",
					"labelPosition": "right",
					"control": "$PageParameters_RunActualizationFromScratch_u5qc189",
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": "#ResourceString(Checkbox_eias582_tooltip)#"
				},
				"parentName": "TopContentFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridContainer_jd5pn2d",
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
					"padding": {
						"top": "medium",
						"right": "large",
						"bottom": "medium",
						"left": "large"
					},
					"color": "primary",
					"borderRadius": "medium",
					"visible": false,
					"alignItems": "stretch",
					"styles": {
						"border-color": "#FFE7CC"
					}
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "TabDescriptionTitleContainer",
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
					"alignItems": "center",
					"gap": "none"
				},
				"parentName": "GridContainer_jd5pn2d",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabDescriptionTitleIconContainer",
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
						"right": "4px",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"justifyContent": "center",
					"gap": "none",
					"wrap": "wrap"
				},
				"parentName": "TabDescriptionTitleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabDescriptionTitleIcon",
				"values": {
					"type": "crt.ImageInput",
					"label": "#ResourceString(ImageLabel)#",
					"value": "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNiIgaGVpZ2h0PSIxNiIgdmlld0JveD0iMCAwIDE2IDE2IiBmaWxsPSJub25lIj4KICA8cGF0aCBkPSJNNy4xNjE4MyA1LjM5NTkxQzcuMTYxODMgNC45MzQ3NiA3LjUzNTY3IDQuNTYwOTEgNy45OTY4MyA0LjU2MDkxSDguMDAzNDlDOC40NjQ2NSA0LjU2MDkxIDguODM4NSA0LjkzNDc2IDguODM4NSA1LjM5NTkxQzguODM4NSA1Ljg1NzA3IDguNDY0NjUgNi4yMzA5MSA4LjAwMzQ5IDYuMjMwOTFINy45OTY4M0M3LjUzNTY3IDYuMjMwOTEgNy4xNjE4MyA1Ljg1NzA3IDcuMTYxODMgNS4zOTU5MVoiIGZpbGw9IiNGRjg4MDAiLz4KICA8cGF0aCBkPSJNNy45OTk5NSA2Ljk1MzU3QzguNDYxMTEgNi45NTM5NyA4LjgzNDYzIDcuMzI4MTQgOC44MzQyMyA3Ljc4OTNMOC44MzE3OSAxMC42MDU1QzguODMxMzkgMTEuMDY2NiA4LjQ1NzIyIDExLjQ0MDIgNy45OTYwNyAxMS40Mzk4QzcuNTM0OTEgMTEuNDM5NCA3LjE2MTM5IDExLjA2NTIgNy4xNjE3OSAxMC42MDRMNy4xNjQyMyA3Ljc4Nzg1QzcuMTY0NjMgNy4zMjY2OSA3LjUzODggNi45NTMxNyA3Ljk5OTk1IDYuOTUzNTdaIiBmaWxsPSIjRkY4ODAwIi8+CiAgPHBhdGggZmlsbC1ydWxlPSJldmVub2RkIiBjbGlwLXJ1bGU9ImV2ZW5vZGQiIGQ9Ik04LjAwMDE2IDEuNUMxMS41ODk2IDEuNSAxNC40OTkzIDQuNDA5NzggMTQuNDk5MyA3Ljk5OTE4QzE0LjQ5OTMgMTEuNTg4NiAxMS41ODk2IDE0LjQ5ODQgOC4wMDAxNiAxNC40OTg0QzQuNDEwNzYgMTQuNDk4NCAxLjUwMDk4IDExLjU4ODYgMS41MDA5OCA3Ljk5OTE4QzEuNTAwOTggNC40MDk3OCA0LjQxMDc2IDEuNSA4LjAwMDE2IDEuNVpNMTMuMTU5MyA3Ljk5OTE4QzEzLjE1OTMgNS4xNDk4NCAxMC44NDk1IDIuODQgOC4wMDAxNiAyLjg0QzUuMTUwODIgMi44NCAyLjg0MDk4IDUuMTQ5ODQgMi44NDA5OCA3Ljk5OTE4QzIuODQwOTggMTAuODQ4NSA1LjE1MDgyIDEzLjE1ODQgOC4wMDAxNiAxMy4xNTg0QzEwLjg0OTUgMTMuMTU4NCAxMy4xNTkzIDEwLjg0ODUgMTMuMTU5MyA3Ljk5OTE4WiIgZmlsbD0iI0ZGODgwMCIvPgo8L3N2Zz4=",
					"readonly": true,
					"placeholder": "",
					"labelPosition": "auto",
					"customWidth": "100%",
					"customHeight": "100%",
					"borderRadius": "none",
					"positioning": "cover",
					"visible": true,
					"tooltip": ""
				},
				"parentName": "TabDescriptionTitleIconContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_3xw1kaw",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(WarningTitleCaption)#)#",
					"labelType": "headline-4",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "#181818",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "TabDescriptionTitleContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Label_jncep89",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_jncep89_caption)#)#",
					"labelType": "body",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true,
					"headingLevel": "label"
				},
				"parentName": "GridContainer_jd5pn2d",
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
					"PageParameters_RunActualizationFromScratch_u5qc189": {
						"modelConfig": {
							"path": "PageParameters.RunActualizationFromScratch"
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
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});