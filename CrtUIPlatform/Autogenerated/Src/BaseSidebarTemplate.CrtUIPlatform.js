define("BaseSidebarTemplate", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "insert",
				"name": "Main",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"stretch": true,
					"fitContent": false,
					"items": [],
					"color": "primary",
					"justifyContent": "start",
					"alignItems": "stretch",
					"wrap": "nowrap",
					"visible": true,
					"borderRadius": "medium",
					"gap": "0px",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"layoutConfig": {
						"width": 420
					},
					"styles": {
						"border": "none"
					}
				},
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MainHeader",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"justifyContent": "space-between",
					"wrap": "nowrap",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "medium",
						"right": "medium",
						"bottom": "none",
						"left": "large"
					},
					"alignItems": "stretch",
					"gap": "small"
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TitleContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"alignItems": "center",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"justifyContent": "start",
					"gap": "small",
					"wrap": "wrap"
				},
				"parentName": "MainHeader",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SidebarTitleLabel",
				"values": {
					"type": "crt.Label",
					"caption": "$SidebarTitleCaption",
					"labelType": "headline-3",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "#121212",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "TitleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ActionButtonsContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"items": [],
					"fitContent": true,
					"wrap": "nowrap",
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "small",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small"
				},
				"parentName": "MainHeader",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ResizeButton",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(ResizeButton_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "only-icon",
					"visible": "$AllowSizeAdjustment",
					"icon": "resize-width-icon",
					"menuItems": [],
					"clickMode": "menu"
				},
				"parentName": "ActionButtonsContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MenuItem_title",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(MenuItem_title_caption)#",
					"visible": true,
					"disabled": true
				},
				"parentName": "ResizeButton",
				"propertyName": "menuItems",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MenuItem_StandardMode",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(MenuItem_StandardMode_caption)#",
					"visible": true,
					"icon": "standard-width-size-icon",
					"clicked": {
						"request": "crt.ChangeSidebarWidthRequest",
						"params": {
							"sidebarCode": "$SidebarCode",
							"width": "narrow",
							"containerName": "$ContainerName"
						}
					}
				},
				"parentName": "ResizeButton",
				"propertyName": "menuItems",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "MenuItem_ExpandedMode",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(MenuItem_ExpandedMode_caption)#",
					"visible": true,
					"icon": "large-width-size-icon",
					"clicked": {
						"request": "crt.ChangeSidebarWidthRequest",
						"params": {
							"sidebarCode": "$SidebarCode",
							"width": "expanded",
							"containerName": "$ContainerName"
						}
					}
				},
				"parentName": "ResizeButton",
				"propertyName": "menuItems",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "MenuItem_FullscreenMode",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(MenuItem_FullscreenMode_caption)#",
					"visible": true,
					"icon": "fullscreen-width-size-icon",
					"clicked": {
						"request": "crt.ChangeSidebarWidthRequest",
						"params": {
							"sidebarCode": "$SidebarCode",
							"width": "fullscreen",
							"containerName": "$ContainerName"
						}
					}
				},
				"parentName": "ResizeButton",
				"propertyName": "menuItems",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "CloseButton",
				"values": {
					"type": "crt.Button",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "only-icon",
					"visible": true,
					"icon": "close-button-icon",
					"clicked": [
						{
							"request": "crt.CloseSidebarRequest",
							"params": {
								"containerName": "$ContainerName"
							}
						}
					],
					"clickMode": "default",
					"caption": "#ResourceString(CloseButton_caption)#"
				},
				"parentName": "ActionButtonsContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "MainContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"stretch": true,
					"fitContent": false,
					"padding": {
						"top": "14px",
						"bottom": "large",
						"right": "none",
						"left": "none"
					},
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap"
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 1
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"attributes": {
						"SidebarTitleCaption": {
							"modelConfig": {
								"path": "PageParameters.SidebarTitle"
							},
							"value": "#ResourceString(DefaultSidebarTitle)#"
						},
						"AllowSizeAdjustment": {
							"modelConfig": {
								"path": "PageParameters.AllowSizeAdjustment"
							},
							"value": false
						},
						"SidebarCode": {
							"modelConfig": {
								"path": "PageParameters.SidebarCode"
							},
							"value": ""
						},
						"ContainerName": {
							"modelConfig": {
								"path": "PageParameters.ContainerName"
							},
							"value": ""
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