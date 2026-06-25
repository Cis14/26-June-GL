define("AISkills_FormPage", /**SCHEMA_DEPS*/["@creatio-devkit/common", "css!AISkills_FormPage"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/(devkit)/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "SaveButton",
				"values": {
					"clicked": [
						{
							"request": "crt.SaveRecordRequest",
							"params": {
								"showSuccessMessage": true,
								"preventCardClose": true,
								"messageTextAfterCompletion": "#ResourceString(SaveButton_clicked_params_messageTextAfterCompletion)#"
							}
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
					],
					"caption": "#ResourceString(SaveButton_caption)#",
					"size": "large",
					"iconPosition": "only-text",
					"clickMode": "default",
					"icon": null
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
					],
					"size": "large",
					"iconPosition": "only-text",
					"color": "default"
				}
			},
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
					"gap": {
						"columnGap": "medium",
						"rowGap": "small"
					},
					"padding": {
						"left": "none",
						"right": "none",
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
				"name": "CardContentWrapper",
				"parentName": "GeneralInfoTab",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "SideContainer",
				"values": {
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "large",
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
						"top": "11px",
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
				"operation": "remove",
				"name": "CenterContainer",
				"properties": [
					"layoutConfig"
				]
			},
			{
				"operation": "move",
				"name": "CenterContainer",
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "CardContentContainer",
				"values": {
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "small"
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
					"headerBackgroundColor": "auto"
				}
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTab",
				"values": {
					"iconPosition": "only-text"
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
					"headerBackgroundColor": "auto"
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
					"label": "#ResourceString(Title_label)#",
					"labelPosition": "auto",
					"control": "$PDS_Name",
					"visible": true,
					"readonly": false,
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
						"row": 2,
						"colSpan": 1,
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
					"label": "#ResourceString(Status_label)#",
					"ariaLabel": "#ResourceString(Status_ariaLabel)#",
					"isAddAllowed": true,
					"showValueAsLink": true,
					"readonly": "$PDS_Type | crt.ToObjectProp : 'value' | crt.IsEqual : '35f3b644-4fa3-4d1e-8e62-5c3fdc4d3e52'",
					"control": "$PDS_Status",
					"labelPosition": "auto",
					"controlActions": [],
					"listActions": []
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Mode",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 4,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "#ResourceString(Mode_label)#",
					"tooltip": "#ResourceString(Mode_tooltip)#",
					"labelPosition": "auto",
					"control": "$PDS_Mode",
					"readonly": "$PDS_Type | crt.ToObjectProp : 'value' | crt.IsEqual : '35f3b644-4fa3-4d1e-8e62-5c3fdc4d3e52'",
					"showValueAsLink": true,
					"isAddAllowed": true,
					"controlActions": [],
					"listActions": []
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "LlmModel",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 5,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "#ResourceString(LlmModel_label)#",
					"tooltip": "",
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
				"index": 4
			},
			{
				"operation": "insert",
				"name": "UsePageContext",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 6,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"disabled": false,
					"inversed": false,
					"label": "#ResourceString(UsePageContext_label)#",
					"ariaLabel": "#ResourceString(UsePageContext_ariaLabel)#",
					"labelPosition": "right",
					"control": "$PDS_UsePageContext",
					"tooltip": "#ResourceString(UsePageContext_tooltip)#",
					"visible": "$UseAgenticProcesses",
					"readonly": false,
					"placeholder": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "UseChatHistory",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 6,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"disabled": false,
					"inversed": false,
					"label": "#ResourceString(UseChatHistory_label)#",
					"ariaLabel": "#ResourceString(UseChatHistory_ariaLabel)#",
					"labelPosition": "right",
					"control": "$PDS_UseChatHistory",
					"tooltip": "#ResourceString(UseChatHistory_tooltip)#",
					"visible": "$UseAgenticProcesses",
					"readonly": false,
					"placeholder": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "UseDefaultSystemPrompt",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 6,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"disabled": false,
					"inversed": false,
					"label": "#ResourceString(UseDefaultSystemPrompt_label)#",
					"ariaLabel": "#ResourceString(UseDefaultSystemPrompt_ariaLabel)#",
					"labelPosition": "right",
					"control": "$PDS_UseDefaultSystemPrompt",
					"tooltip": "#ResourceString(UseDefaultSystemPrompt_tooltip)#",
					"visible": "$UseAgenticProcesses",
					"readonly": false,
					"placeholder": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "WorkflowOnly",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 6,
						"rowSpan": 1
					},
					"type": "crt.Checkbox",
					"disabled": false,
					"inversed": false,
					"label": "#ResourceString(WorkflowOnly_label)#",
					"ariaLabel": "#ResourceString(WorkflowOnly_ariaLabel)#",
					"labelPosition": "right",
					"control": "$PDS_WorkflowOnly",
					"visible": "$IsChatMode | crt.IfElse : $ShowWorkflowOnlyCheckbox : false",
					"readonly": false,
					"placeholder": ""
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 9
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
				"parentName": "CardContentWrapper",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Description",
				"values": {
					"type": "crt.Input",
					"multiline": true,
					"label": "#ResourceString(Description_label)#",
					"labelPosition": "above",
					"control": "$PDS_Description",
					"tooltip": "#ResourceString(Description_placeholder)#",
					"placeholder": "#ResourceString(Description_placeholder)#"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ImportantNotesAndGuidesPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(ImportantNotesAndGuidesPanel_title)#",
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
					"fitContent": false,
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "NotesAndGuidesGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": 0
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": []
				},
				"parentName": "ImportantNotesAndGuidesPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NotesAndGuidesFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "NotesAndGuidesGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NotesAndGuidesMainGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 16px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "small",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
					"visible": true,
					"color": "transparent",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "large"
					},
					"alignItems": "stretch"
				},
				"parentName": "ImportantNotesAndGuidesPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridContainer_AgentsWarning",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "small",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "extra-small",
						"left": "none"
					},
					"color": "primary",
					"borderRadius": "none",
					"visible": "$IsChatMode",
					"alignItems": "stretch",
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_AgentsWarning",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_AgentsWarning_caption)#)#",
					"labelType": "headline-3",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "#0D2E4E",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"headingLevel": "label",
					"visible": true
				},
				"parentName": "GridContainer_AgentsWarning",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentListPageLink",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Link",
					"caption": "#MacrosTemplateString(#ResourceString(AgentListPageLinkCaption)#)#",
					"href": "#Section/AIAgents_ListPage",
					"target": "_blank",
					"linkType": "body",
					"underlining": "never"
				},
				"parentName": "GridContainer_AgentsWarning",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_1",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 2,
						"rowSpan": 1
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_1_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_2",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 3
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_2_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_3",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 4
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_3_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_4",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 5
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_4_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_5",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 6
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_5_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_6",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 7
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_6_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "InstructionLabel_7",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 8,
						"colSpan": 2,
						"rowSpan": 1
					},
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(InstructionLabel_7_caption)#)#",
					"labelType": "caption",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "AISkillDocLink",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 9,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Link",
					"caption": "#MacrosTemplateString(#ResourceString(AISkillDocLinkCaption)#)#",
					"href": "https://academy.creatio.com/documents?id=2535",
					"target": "_blank",
					"visible": true,
					"linkType": "body",
					"underlining": "never"
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "AISkillDevRecLink",
				"values": {
					"layoutConfig": {
						"column": 2,
						"row": 9,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Link",
					"caption": "#MacrosTemplateString(#ResourceString(AISkillDevRecLinkCaption)#)#",
					"href": "https://academy.creatio.com/documents?id=2536",
					"target": "_blank",
					"visible": true,
					"linkType": "body",
					"underlining": "never"
				},
				"parentName": "NotesAndGuidesMainGridContainer",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "insert",
				"name": "PromptExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(PromptExpansionPanel_title)#",
					"toggleType": "default",
					"togglePosition": "before",
					"expanded": true,
					"labelColor": "auto",
					"fullWidthHeader": false,
					"titleWidth": 20,
					"padding": {
						"top": "none",
						"bottom": "none",
						"left": "none",
						"right": "none"
					},
					"fitContent": true,
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "PromptContainer",
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
					"color": "primary",
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "32px"
					},
					"alignItems": "stretch"
				},
				"parentName": "PromptExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Prompt",
				"values": {
					"type": "crt.Input",
					"label": "#ResourceString(Prompt_label)#",
					"control": "$PDS_Prompt",
					"placeholder": "#ResourceString(Prompt_placeholder)#",
					"readonly": false,
					"multiline": true,
					"labelPosition": "hidden",
					"visible": true,
					"tooltip": "$Prompt_tooltip",
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					}
				},
				"parentName": "PromptContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SubSkillsExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(SubSkillsExpansionPanel_title)#",
					"toggleType": "default",
					"togglePosition": "before",
					"expanded": true,
					"labelColor": "auto",
					"fullWidthHeader": false,
					"titleWidth": 20,
					"padding": {
						"top": "medium",
						"bottom": "none",
						"left": "none",
						"right": "none"
					},
					"fitContent": true,
					"visible": "$UseAgenticProcesses",
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "SubSkillsInstruction",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(SubSkillsInstruction_caption)#)#",
					"labelType": "caption",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					}
				},
				"parentName": "SubSkillsExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SubSkillsGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "SubSkillsExpansionPanel",
				"propertyName": "tools",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SubSkillsFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "SubSkillsGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridDetailAddSubSkillsBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailAddSubSkillsBtn_caption)#",
					"icon": "add-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.CopilotSelectExistingIntentRequest",
						"params": {
							"sourceIntentId": "$Id",
							"selectionWindow": {
								"caption": "#ResourceString(SelectExistingSubSkill_caption)#",
								"entitySchemaName":  "CopilotSysCapability",
								"schemaName": "CopilotSelectExistingCapabilityForSkillPage",
								"features": {
									"disableLinks": false,
								}
							},
							"reloadDataSources": ['SubSkillsDetailDS'],
							"selectedValueIsChild": true,
						}
					},
					"visible": true,
					"clickMode": "default",
					"menuItems": []
				},
				"parentName": "SubSkillsFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridDetailRefreshSubSkillsBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailRefreshSubSkillsBtn_caption)#",
					"icon": "reload-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "SubSkillsDetailDS"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "SubSkillsFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AddUserMessaging",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AddUserMessaging_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "left-icon",
					"visible": true,
					"icon": "chat-icon",
					"clicked": {
						"request": "crt.CopilotAddUserMessagingProcessRequest",
						"params": {
							"intentId": "$PDS_Id",
							"subIntentId": "51c693e3-2c94-b0ee-6b1a-0fdcc63c82ec",
							"reloadDataSources": [
								"SubSkillsDetailDS"
							]
						}
					},
					"clickMode": "default",
					"menuItems": []
				},
				"parentName": "SubSkillsFlexContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "SubSkillsDataGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "SubSkillsExpansionPanel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "SubSkillsDetail",
				"values": {
					"type": "crt.DataGrid",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 12
					},
					"features": {
						"rows": {
							"selection": false,
							"numeration": false,
							"toolbar": true
						},
						"columns": {
							"sorting": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"items": "$SubSkillsDetail",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "SubSkillsDetailDS_Id",
					"columns": [
						{
							"id": "e2f07270-435b-4351-897c-3f9d3b07973c",
							"code": "SubSkillsDetailDS_Name",
							"caption": "#ResourceString(SubSkillsDetailDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "d0498b5e-4903-455b-b9d9-052a926a809b",
							"code": "SubSkillsDetailDS_Description",
							"caption": "#ResourceString(SubSkillsDetailDS_Description)#",
							"dataValueType": 28
						}
					],
					"rowToolbarItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(DeleteButton)#",
							"icon": "delete-row-action",
							"clicked": {
								"request": "crt.DeleteSubIntentRequest",
								"params": {
									"recordId": "$SubSkillsDetail.SubSkillsDetailDS_Id",
									"parentIntentId": "$PDS_Id",
									"reloadDataSourceName": "SubSkillsDetailDS"
								}
							}
						}
					]
				},
				"parentName": "SubSkillsDataGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ToolsExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"visible": "$IsApiMode | crt.IfElse : $UseAgenticProcessesFeatureValue : true",
					"title": "#ResourceString(ToolsExpansionPanel_title)#",
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
					"fitContent": true
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "ActionsGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "ToolsExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ActionsFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "ActionsGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridDetailAddActionBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailAddActionBtn_caption)#",
					"icon": "add-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {},
					"visible": true,
					"clickMode": "menu",
					"menuItems": []
				},
				"parentName": "ActionsFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "CreateNewAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(CreateNewAction_caption)#",
					"clicked": {
						"request": "crt.CreateRecordRequest",
						"params": {
							"entityName": "CopilotAction",
							"defaultValues": [
								{
									"attributeName": "Intent",
									"value": "$Id"
								},
								{
									"attributeName": "Code",
									"value": "$ActionsDetail | crt.GenerateCopilotActionCode : 'ActionsDetailDS_Code'"
								},
								{
									"attributeName": "PackageUId",
									"value": "$PDS_PackageUId"
								}
							]
						}
					}
				},
				"parentName": "GridDetailAddActionBtn",
				"propertyName": "menuItems",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SelectExistingProcessForAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(SelectExistingProcessForAction_caption)#",
					"clicked": {
						"request": "crt.CopilotSelectExistingProcessRequest",
						"params": {
							"caption": "#ResourceString(SelectExistingProcessForAction_caption)#",
							"intentId": "$Id",
							"actionCode": "$ActionsDetail | crt.GenerateCopilotActionCode : 'ActionsDetailDS_Code'",
							"packageUId": "$PDS_PackageUId"
						}
					}
				},
				"parentName": "GridDetailAddActionBtn",
				"propertyName": "menuItems",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridDetailRefreshActionsBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailRefreshActionsBtn_caption)#",
					"icon": "reload-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "ActionsDetailDS"
						}
					},
					"visible": false,
					"clickMode": "default"
				},
				"parentName": "ActionsFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "ToolsExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ActionsDetail",
				"values": {
					"type": "crt.DataGrid",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 12
					},
					"features": {
						"rows": {
							"selection": false,
							"numeration": false,
							"toolbar": true
						},
						"columns": {
							"sorting": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"items": "$ActionsDetail",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "ActionsDetailDS_Id",
					"columns": [
						{
							"id": "1894d380-0081-4203-99b0-466d6cc6e355",
							"code": "ActionsDetailDS_Name",
							"caption": "#ResourceString(ActionsDetailDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "7683416e-4456-4c40-9f15-7798782d8c36",
							"code": "ActionsDetailDS_Description",
							"caption": "#ResourceString(ActionsDetailDS_Description)#",
							"dataValueType": 28
						}
					],
					"_designOptions": {
						"columns": {
							"cellViews": {
								"ActionsDetailDS_Name": {
									"type": "crt.Link",
                					"caption": "$ActionsDetail.ActionsDetailDS_Name",
                					"value": "$ActionsDetail.ActionsDetailDS_Name",
                					"mode": "$ActionsDetail.ActionsDetailDS_IsSourceCodeActionType | crt.IfElse: 'native' : 'preventDefault'",
                					"underlining": "$ActionsDetail.ActionsDetailDS_IsSourceCodeActionType | crt.IfElse: 'never' : 'hover'",
									"linkType": "$ActionsDetail.ActionsDetailDS_IsSourceCodeActionType | crt.IfElse: 'plain' : ''",
                					"clicked": {
                    					"request": "crt.UpdateRecordRequest",
                    					"params": {
                        					"entityName": "CopilotAction",
                        					"recordId": "$ActionsDetail.ActionsDetailDS_Id"
                    					}
                					}
								}
							}
						}
					},
					"rowToolbarItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(DeleteButton)#",
							"icon": "delete-row-action",
							"clicked": {
								"request": "crt.DeleteActionFromIntentRequest",
								"params": {
									"recordId": "$ActionsDetail.ActionsDetailDS_Id",
									"intentId": "$PDS_Id"
								}
							}
						}
					]
				},
				"parentName": "DataGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFilesExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(IntentFilesExpansionPanel_title)#",
					"toggleType": "default",
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
					"visible": "$EnableAttachmentsUI"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "IntentFileActionGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "IntentFilesExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFileActionFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "IntentFileActionGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFileAddBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(IntentFileAddBtn_caption)#",
					"icon": "upload-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.UploadFileRequest",
						"params": {
							"viewElementName": "IntentFileList"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "IntentFileActionFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFileRefreshBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(IntentFileRefreshBtn_caption)#",
					"icon": "reload-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "IntentFileListDS"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "IntentFileActionFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "IntentFileGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "IntentFilesExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFileList",
				"values": {
					"type": "crt.FileList",
					"masterRecordColumnValue": "$Id",
					"recordColumnName": "IntentUId",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 10
					},
					"columns": [
						{
							"id": "72c8d0fc-f4e2-b292-81b4-a5bde5f8d291",
							"code": "IntentFileListDS_Name",
							"caption": "#ResourceString(IntentFileListDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "3b87b6c0-adeb-0580-761b-d43469db9782",
							"code": "IntentFileListDS_CreatedOn",
							"caption": "#ResourceString(IntentFileListDS_CreatedOn)#",
							"dataValueType": 7
						},
						{
							"id": "26f8c27b-c269-ff1a-272a-c3affd27b65a",
							"code": "IntentFileListDS_CreatedBy",
							"caption": "#ResourceString(IntentFileListDS_CreatedBy)#",
							"dataValueType": 10
						},
						{
							"id": "88b39285-116f-a431-5a3d-6540a834dd17",
							"code": "IntentFileListDS_Size",
							"caption": "#ResourceString(IntentFileListDS_Size)#",
							"dataValueType": 4
						}
					],
					"items": "$IntentFileList",
					"primaryColumnName": "IntentFileListDS_Id",
					"visible": "$CardState | crt.IsEqual : 'edit'"
				},
				"parentName": "IntentFileGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentFileListPlaceholder",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(IntentFileListPlaceholder_caption)#)#",
					"labelType": "placeholder-large",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "center",
					"visible": "$CardState | crt.IsEqual : 'add'"
				},
				"parentName": "IntentFilesExpansionPanel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "InputParametersExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(InputParametersExpansionPanel_title)#",
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
					"visible": "$IsApiMode | crt.IfElse : $EnableParametersUI : false",
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "ParameterActionsGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "InputParametersExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ParameterActionsFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "ParameterActionsGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridDetailRefreshInputParametersBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailRefreshInputParametersBtn_caption)#",
					"color": "default",
					"disabled": false,
					"size": "small",
					"iconPosition": "only-icon",
					"icon": "reload-icon",
					"visible": true,
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "InputParametersDS"
						}
					},
					"clickMode": "default"
				},
				"parentName": "ParameterActionsFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "InputParametersGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "InputParametersExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "InputParametersDetail",
				"values": {
					"type": "crt.DataGrid",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 11
					},
					"features": {
						"rows": {
							"selection": false
						},
						"columns": {
							"sorting": false
						},
						"editable": {
							"enable": true,
							"floatingEditPanel": false
						}
					},
					"items": "$InputParametersDetail",
					"primaryColumnName": "InputParametersDS_Id",
					"_designOptions": {
						"columns": {
							"cellViews": {
								"InputParametersDS_DataValueTypeUId": {
									"type": "crt.TableTextCell",
									"value": "$InputParametersDetail.InputParametersDS_DataValueTypeUId | crt.ToDataValueTypeDisplayValue"
								}
							},
							"editingCellViews": {
								"InputParametersDS_DataValueTypeUId": {
									"type": "crt.DataTableEditLookupCell",
									"value": "$InputParametersDetail.InputParametersDS_DataValueTypeUId | crt.ToDataValueTypeLookupValue",
									"valueChange": {
										"request": "crt.CopilotIntentParameterDataValueTypeChangeRequest",
										"params": {
											"attributeName": "InputParametersDS_DataValueTypeUId",
											"attributeValue": "@event.value",
											"collectionName": "InputParametersDetail",
											"primaryColumnValue": "$InputParametersDetail.InputParametersDS_Id"
										}
									},
									"items": "$AvailableInputDataValueTypes"
								}
							}
						}
					},
					"columns": [
						{
							"id": "20edd68a-a71e-4969-9d2e-790620c10b61",
							"code": "InputParametersDS_Code",
							"caption": "#ResourceString(InputParametersDS_Code)#",
							"dataValueType": 28
						},
						{
							"id": "ada0e83e-91b8-4b93-9da0-9b03da9972df",
							"code": "InputParametersDS_Name",
							"caption": "#ResourceString(InputParametersDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "0a58f55b-8cda-43e0-b271-36c4342ee35e",
							"code": "InputParametersDS_Description",
							"caption": "#ResourceString(InputParametersDS_Description)#",
							"dataValueType": 28
						},
						{
							"id": "8b09c20e-588d-4316-8923-cd97609f253a",
							"code": "InputParametersDS_DataValueTypeUId",
							"caption": "#ResourceString(InputParametersDS_DataValueTypeUId)#",
							"dataValueType": 28
						}
					],
					"visible": true,
					"fitContent": true,
					"placeholder": false
				},
				"parentName": "InputParametersGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "OutputParametersContainer",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"visible": "$IsApiMode | crt.IfElse : $EnableParametersUI : false",
					"fitContent": true,
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "JsonStructureExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(JsonStructureExpansionPanel_caption)#",
					"toggleType": "default",
					"togglePosition": "before",
					"expanded": true,
					"labelColor": "auto",
					"fullWidthHeader": false,
					"titleWidth": 20,
					"padding": {
						"top": "none",
						"bottom": "none",
						"left": "none",
						"right": "none"
					},
					"fitContent": true,
					"alignItems": "stretch",
					"visible": "$IsJsonStructureMode",
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "OutputParametersContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "JsonSchemaSwitchBtnPanel",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "stretch",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "JsonStructureExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SwitchToOutputParametersBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(SwitchToOutputParametersBtn_caption)#",
					"icon": "list-icon",
					"iconPosition": "left-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.ChangeViewModelAttributeValueRequest",
						"params": {
							"attributeName": "IsJsonStructureMode",
							"attributeValue": false
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "JsonSchemaSwitchBtnPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "JsonOutputSchemaGridContainer",
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
				"parentName": "JsonStructureExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "InputResponseFormatJsonSchema",
				"values": {
					"type": "crt.Input",
					"label": "#ResourceString(JsonStructureExpansionPanel_label)#",
					"labelPosition": "hidden",
					"control": "$PDS_ResponseFormatJsonSchema",
					"visible": true,
					"placeholder": "#ResourceString(InputResponseFormatJsonSchema_placeholder)#",
					"classes": [
						"json-schema-text-editor"
					],
					"readonly": false,
					"multiline": true,
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					}
				},
				"parentName": "JsonOutputSchemaGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "OutputParametersExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(OutputParametersExpansionPanel_title)#",
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
					"visible": "$IsJsonStructureMode | crt.InvertBooleanValue",
					"alignItems": "stretch",
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					}
				},
				"parentName": "OutputParametersContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "OutputParameterBtnContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "OutputParametersExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "OutputParameterBtnPanel",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "OutputParameterBtnContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "GridDetailRefreshOutputParametersBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(GridDetailRefreshOutputParametersBtn_caption)#",
					"icon": "reload-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "OutputParametersDS"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "OutputParameterBtnPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SwitchToJsonStructureBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(SwitchToJsonStructureBtn_caption)#",
					"icon": "codeblock-icon",
					"iconPosition": "left-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.ChangeViewModelAttributeValueRequest",
						"params": {
							"attributeName": "IsJsonStructureMode",
							"attributeValue": true
						}
					},
					"visible": "$EnableResponseJsonSchemaUI",
					"clickMode": "default"
				},
				"parentName": "OutputParameterBtnPanel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "OutputParametersGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": [],
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
				"parentName": "OutputParametersExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "OutputParametersDetail",
				"values": {
					"type": "crt.DataGrid",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 11
					},
					"features": {
						"rows": {
							"selection": false
						},
						"columns": {
							"sorting": false
						},
						"editable": {
							"enable": true,
							"floatingEditPanel": false
						}
					},
					"items": "$OutputParametersDetail",
					"primaryColumnName": "OutputParametersDS_Id",
					"_designOptions": {
						"columns": {
							"cellViews": {
								"OutputParametersDS_DataValueTypeUId": {
									"type": "crt.TableTextCell",
									"value": "$OutputParametersDetail.OutputParametersDS_DataValueTypeUId | crt.ToDataValueTypeDisplayValue"
								}
							},
							"editingCellViews": {
								"OutputParametersDS_DataValueTypeUId": {
									"type": "crt.DataTableEditLookupCell",
									"value": "$OutputParametersDetail.OutputParametersDS_DataValueTypeUId | crt.ToDataValueTypeLookupValue",
									"valueChange": {
										"request": "crt.CopilotIntentParameterDataValueTypeChangeRequest",
										"params": {
											"attributeName": "OutputParametersDS_DataValueTypeUId",
											"attributeValue": "@event.value",
											"collectionName": "OutputParametersDetail",
											"primaryColumnValue": "$OutputParametersDetail.OutputParametersDS_Id"
										}
									},
									"items": "$AvailableOutputDataValueTypes"
								}
							}
						}
					},
					"columns": [
						{
							"id": "8488860c-d9d0-49f9-a187-5e81f6738329",
							"code": "OutputParametersDS_Code",
							"caption": "#ResourceString(OutputParametersDS_Code)#",
							"dataValueType": 28
						},
						{
							"id": "54cde841-6a3d-4a10-aee2-bf46a4d08a04",
							"code": "OutputParametersDS_Name",
							"caption": "#ResourceString(OutputParametersDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "aae636bc-8b70-47d1-8827-8c89d4f8aa32",
							"code": "OutputParametersDS_Description",
							"caption": "#ResourceString(OutputParametersDS_Description)#",
							"dataValueType": 28
						},
						{
							"id": "df355886-c600-4fd6-ae57-0f830d7b8077",
							"code": "OutputParametersDS_DataValueTypeUId",
							"caption": "#ResourceString(OutputParametersDS_DataValueTypeUId)#",
							"dataValueType": 28
						}
					],
					"visible": true,
					"fitContent": true,
					"placeholder": false
				},
				"parentName": "OutputParametersGridContainer",
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
				"index": 8
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
			},
			{
				"operation": "insert",
				"name": "AgentsTabContainer",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(AgentsTabContainer_caption)#",
					"iconPosition": "only-text",
					"visible": "$DisplayAgentsDetail"
				},
				"parentName": "Tabs",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridContainer_Agents",
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
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"alignItems": "stretch"
				},
				"parentName": "AgentsTabContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentsExpansionPanel",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 2,
						"row": 1,
						"rowSpan": 1
					},
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(AgentsExpansionPanel_title)#",
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
					"fitContent": true
				},
				"parentName": "GridContainer_Agents",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentsMainGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 24px)",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": "0"
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": []
				},
				"parentName": "AgentsExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentsFlexContainer",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "center",
					"items": [],
					"layoutConfig": {
						"colSpan": 1,
						"column": 1,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "AgentsMainGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentsGridDetailRefreshBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AgentsGridDetailRefreshBtn_caption)#",
					"icon": "reload-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload"
							},
							"dataSourceName": "CopilotSkillAgentsDS"
						}
					}
				},
				"parentName": "AgentsFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SelectExistingAgents",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(SelectExistingAgent_caption)#",
					"icon": "add-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.CopilotSelectExistingIntentRequest",
						"params": {
							"sourceIntentId": "$Id",
							"selectionWindow": {
								"caption": "#ResourceString(SelectExistingAgent_caption)#",
								"entitySchemaName": "CopilotAgent",
								"schemaName": "CopilotSelectExistingSkillForAgentPage",
								"filtersConfig": {
									"filterAttributes": [
										{
											"name": "SearchFilterMain_DataGridMain",
											"loadOnChange": true
										},
										{
											"loadOnChange": true,
											"name": "DataGridMain_PredefinedFilter"
										}
									],
									"attributesConfig": {
										"DataGridMain_PredefinedFilter": {
											"value": {}
										}
									}
								}
							},
							"reloadDataSources": [
								"CopilotSkillAgentsDS"
							],
							"selectedValueIsChild": false
						}
					}
				},
				"parentName": "AgentsFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AgentsInnerGridContainer",
				"values": {
					"type": "crt.GridContainer",
					"rows": "minmax(max-content, 32px)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": 0
					},
					"styles": {
						"overflow-x": "hidden"
					},
					"items": []
				},
				"parentName": "AgentsExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentsGridDetail",
				"values": {
					"type": "crt.DataGrid",
					"layoutConfig": {
						"colSpan": 2,
						"column": 1,
						"row": 1,
						"rowSpan": 12
					},
					"items": "$CopilotSkillAgents",
					"features": {
						"rows": {
							"selection": false,
							"numeration": false,
							"toolbar": true
						},
						"columns": {
							"sorting": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false
						}
					},
					"fitContent": true,
					"primaryColumnName": "CopilotSkillAgentsDS_Id",
					"columns": [
						{
							"id": "bb3b73d7-2ec5-b54e-a5af-6fb20347f95c",
							"code": "CopilotSkillAgentsDS_Name",
							"caption": "#ResourceString(CopilotSkillAgentsDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "e8f0f32a-7d40-6ed0-58e0-8c865f7d8678",
							"code": "CopilotSkillAgentsDS_Status",
							"caption": "#ResourceString(CopilotSkillAgentsDS_Status)#",
							"dataValueType": 10
						},
						{
							"id": "b3e62476-3798-8528-9cde-7555939d8b09",
							"code": "CopilotSkillAgentsDS_Description",
							"caption": "#ResourceString(CopilotSkillAgentsDS_Description)#",
							"dataValueType": 28
						}
					],
					"rowToolbarItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(DeleteButton)#",
							"icon": "delete-row-action",
							"clicked": {
								"request": "crt.DeleteSubIntentRequest",
								"params": {
									"recordId": "$Id",
									"parentIntentId": "$CopilotSkillAgents.CopilotSkillAgentsDS_Id",
									"reloadDataSourceName": "CopilotSkillAgentsDS"
								}
							}
						}
					],
					"placeholder": false
				},
				"parentName": "AgentsInnerGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabContainer_Helper",
				"values": {
					"type": "crt.TabContainer",
					"tools": [],
					"items": [],
					"caption": "#ResourceString(TabContainer_Helper_caption)#",
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
				"name": "FlexContainer_9tlbbq3",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"alignItems": "center",
					"items": []
				},
				"parentName": "TabContainer_Helper",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IntentHelpTabLabel",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(IntentHelpTabLabel_caption)#)#",
					"labelType": "headline-3",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "#0D2E4E",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_9tlbbq3",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_helper",
				"values": {
					"type": "crt.FlexContainer",
					"items": [],
					"direction": "column"
				},
				"parentName": "TabContainer_Helper",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_vjl127g",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_vjl127g_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_AcademyLink",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_tbdtke3_caption)#",
					"color": "default",
					"disabled": false,
					"size": "small",
					"iconPosition": "right-icon",
					"visible": true,
					"clicked": {
						"request": "crt.AcademyHelpLinkRequest",
						"params": {
							"contextHelpCode": "CopilotIntents"
						}
					},
					"clickMode": "default",
					"icon": "open-button-icon"
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Label_9k5w1jp",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_9k5w1jp_caption)#)#",
					"labelType": "body-large",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Label_099dh7q",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_099dh7q_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "Label_1v910ch",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_1v910ch_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "Label_b2fx4vr",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_b2fx4vr_caption)#)#",
					"labelType": "body-large",
					"labelThickness": "semibold",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "Label_aklt9zb",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_aklt9zb_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "Label_51mzska",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_51mzska_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "Label_d7websz",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_d7websz_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "Label_3rz059f",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_3rz059f_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "insert",
				"name": "Label_si5462l",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_si5462l_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 10
			},
			{
				"operation": "insert",
				"name": "Label_1s9n19j",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_1s9n19j_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 11
			},
			{
				"operation": "insert",
				"name": "Label_5t1rr63",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_5t1rr63_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 12
			},
			{
				"operation": "insert",
				"name": "Label_ca6jrmu",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_ca6jrmu_caption)#)#",
					"labelType": "body",
					"labelThickness": "normal",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "FlexContainer_helper",
				"propertyName": "items",
				"index": 13
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
							"request": "crt.GenerateSkillCodeValueRequest",
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
					"PDS_Mode": {
						"modelConfig": {
							"path": "PDS.Mode"
						}
					},
					"PDS_Prompt": {
						"modelConfig": {
							"path": "PDS.Prompt"
						}
					},
					"PDS_PackageUId": {
						"modelConfig": {
							"path": "PDS.PackageUId"
						}
					},
					"PDS_Type": {
						"modelConfig": {
							"path": "PDS.Type"
						}
					},
					"PDS_ParentIntentId": {
						"modelConfig": {
							"path": "PDS.ParentIntentId"
						}
					},
					"PDS_ResponseFormatJsonSchema": {
						"modelConfig": {
							"path": "PDS.ResponseFormatJsonSchema"
						}
					},
					"PDS_UsePageContext": {
						"modelConfig": {
							"path": "PDS.UsePageContext"
						}
					},
					"PDS_UseChatHistory": {
						"modelConfig": {
							"path": "PDS.UseChatHistory"
						}
					},
					"PDS_UseDefaultSystemPrompt": {
						"modelConfig": {
							"path": "PDS.UseDefaultSystemPrompt"
						}
					},
					"PDS_WorkflowOnly": {
						"modelConfig": {
							"path": "PDS.WorkflowOnly"
						}
					},
					"InputParametersDetail": {
						"isCollection": true,
						"modelConfig": {
							"path": "InputParametersDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "InputParametersDetail_PredefinedFilter"
								}
							]
						},
						"change": {
							"request": "crt.CodeChangeParameterListRequest"
						},
						"viewModelConfig": {
							"attributes": {
								"InputParametersDS_Name": {
									"modelConfig": {
										"path": "InputParametersDS.Name"
									}
								},
								"InputParametersDS_Code": {
									"modelConfig": {
										"path": "InputParametersDS.Code"
									},
									"validators": {
										"CodeMaxLength": {
											"type": "crt.MaxLength",
											"params": {
												"maxLength": 41
											}
										},
										"CodeAllowedSymbolsValidator": {
											"type": "crt.SchemaNameAllowedSymbols"
										},
										"UniqueCodeColumnValue": {
											"type": "crt.UniqueCodeColumnValue"
										}
									}
								},
								"InputParametersDS_Description": {
									"modelConfig": {
										"path": "InputParametersDS.Description"
									}
								},
								"InputParametersDS_Id": {
									"modelConfig": {
										"path": "InputParametersDS.Id"
									}
								},
								"InputParametersDS_Intent": {
									"modelConfig": {
										"path": "InputParametersDS.Intent"
									}
								},
								"InputParametersDS_SourceCollection": {
									"modelConfig": {
										"path": "InputParametersDS.SourceCollection"
									}
								},
								"InputParametersDS_DataValueTypeUId": {
									"modelConfig": {
										"path": "InputParametersDS.DataValueTypeUId"
									}
								}
							}
						}
					},
					"InputParametersDetail_PredefinedFilter": {
						"value": {
							"items": {
								"OutputParametersFilter": {
									"filterType": 1,
									"comparisonType": 3,
									"isEnabled": true,
									"trimDateTimeParameterToDate": false,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "SourceCollection"
									},
									"rightExpression": {
										"expressionType": 2,
										"parameter": {
											"dataValueType": 27,
											"value": "InputParameters"
										}
									}
								}
							},
							"logicalOperation": 0,
							"isEnabled": true,
							"filterType": 6,
							"rootSchemaName": "CopilotIntentParameter"
						}
					},
					"OutputParametersDetail": {
						"isCollection": true,
						"modelConfig": {
							"path": "OutputParametersDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "OutputParametersDetail_PredefinedFilter"
								}
							],
							"sortingConfig": {
								"default": [
									{
										"direction": "desc",
										"columnName": "Name"
									}
								]
							}
						},
						"change": {
							"request": "crt.CodeChangeParameterListRequest"
						},
						"viewModelConfig": {
							"attributes": {
								"OutputParametersDS_Name": {
									"modelConfig": {
										"path": "OutputParametersDS.Name"
									}
								},
								"OutputParametersDS_Code": {
									"modelConfig": {
										"path": "OutputParametersDS.Code"
									},
									"validators": {
										"CodeMaxLength": {
											"type": "crt.MaxLength",
											"params": {
												"maxLength": 41
											}
										},
										"CodeAllowedSymbolsValidator": {
											"type": "crt.SchemaNameAllowedSymbols"
										},
										"UniqueCodeColumnValue": {
											"type": "crt.UniqueCodeColumnValue"
										}
									}
								},
								"OutputParametersDS_Description": {
									"modelConfig": {
										"path": "OutputParametersDS.Description"
									},
									"validators": {
										"required": {
											"type": "crt.Required"
										}
									}
								},
								"OutputParametersDS_Id": {
									"modelConfig": {
										"path": "OutputParametersDS.Id"
									}
								},
								"OutputParametersDS_Intent": {
									"modelConfig": {
										"path": "OutputParametersDS.Intent"
									}
								},
								"OutputParametersDS_SourceCollection": {
									"modelConfig": {
										"path": "OutputParametersDS.SourceCollection"
									}
								},
								"OutputParametersDS_DataValueTypeUId": {
									"modelConfig": {
										"path": "OutputParametersDS.DataValueTypeUId"
									}
								}
							}
						}
					},
					"OutputParametersDetail_PredefinedFilter": {
						"value": {
							"items": {
								"OutputParametersFilter": {
									"filterType": 1,
									"comparisonType": 3,
									"isEnabled": true,
									"trimDateTimeParameterToDate": false,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "SourceCollection"
									},
									"rightExpression": {
										"expressionType": 2,
										"parameter": {
											"dataValueType": 27,
											"value": "OutputParameters"
										}
									}
								}
							},
							"logicalOperation": 0,
							"isEnabled": true,
							"filterType": 6,
							"rootSchemaName": "CopilotIntentParameter"
						}
					},
					"ActionsDetail": {
						"isCollection": true,
						"modelConfig": {
							"path": "ActionsDetailDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "ActionsDetail_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"ActionsDetailDS_Name": {
									"modelConfig": {
										"path": "ActionsDetailDS.Name"
									}
								},
								"ActionsDetailDS_Description": {
									"modelConfig": {
										"path": "ActionsDetailDS.Description"
									}
								},
								"ActionsDetailDS_Id": {
									"modelConfig": {
										"path": "ActionsDetailDS.Id"
									}
								},
								"ActionsDetailDS_Type": {
									"modelConfig": {
										"path": "ActionsDetailDS.ActionType"
									}
								},
								"ActionsDetailDS_IsSourceCodeActionType": {
									"from": "ActionsDetailDS_Type",
									"converter": "crt.ToObjectProp: 'value' |crt.IsEqual: '2fb3f2a9-974d-40b7-aa34-5da65192231c'"
								}
							}
						}
					},
					"ActionsDetail_PredefinedFilter": {
						"value": null
					},
					"SubSkillsDetail": {
						"isCollection": true,
						"modelConfig": {
							"path": "SubSkillsDetailDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "SubSkillsDetail_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"SubSkillsDetailDS_Name": {
									"modelConfig": {
										"path": "SubSkillsDetailDS.Name"
									}
								},
								"SubSkillsDetailDS_Description": {
									"modelConfig": {
										"path": "SubSkillsDetailDS.Description"
									}
								},
								"SubSkillsDetailDS_Id": {
									"modelConfig": {
										"path": "SubSkillsDetailDS.Id"
									}
								}
							}
						}
					},
					"SubSkillsDetail_PredefinedFilter": {
						"value": null
					},
					"IsApiMode": {
						"value": null
					},
					"IsJsonStructureMode": {
						"value": false
					},
					"Prompt_tooltip": {
						"value": null
					},
					"AvailableInputDataValueTypes": {
						"value": null,
						"isCollection": true
					},
					"AvailableOutputDataValueTypes": {
						"value": null,
						"isCollection": true
					},
					"EnableParametersUI": {
						"value": false
					},
					"EnableRightsManagementUI": {
						"value": false
					},
					"EnableMultiLlmSupport": {
						"value": false
					},
					"EnableResponseJsonSchemaUI": {
						"value": false
					},
					"IntentFileList": {
						"isCollection": true,
						"modelConfig": {
							"path": "IntentFileListDS",
							"sortingConfig": {
								"default": [
									{
										"columnName": "CreatedOn",
										"direction": "desc"
									}
								]
							},
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "IntentFileList_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"IntentFileListDS_Name": {
									"modelConfig": {
										"path": "IntentFileListDS.Name"
									}
								},
								"IntentFileListDS_CreatedOn": {
									"modelConfig": {
										"path": "IntentFileListDS.CreatedOn"
									}
								},
								"IntentFileListDS_CreatedBy": {
									"modelConfig": {
										"path": "IntentFileListDS.CreatedBy"
									}
								},
								"IntentFileListDS_Size": {
									"modelConfig": {
										"path": "IntentFileListDS.Size"
									}
								},
								"IntentFileListDS_Id": {
									"modelConfig": {
										"path": "IntentFileListDS.Id"
									}
								}
							}
						}
					},
					"IntentFileList_PredefinedFilter": {
						"value": {
							"items": {
								"SysSchemaUIdFilter": {
									"filterType": 1,
									"comparisonType": 3,
									"isEnabled": true,
									"trimDateTimeParameterToDate": false,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "IntentUId"
									},
									"rightExpression": {
										"expressionType": 2,
										"parameter": {
											"dataValueType": 0,
											"value": "00000000-0000-0000-0000-000000000000"
										}
									}
								}
							},
							"logicalOperation": 0,
							"isEnabled": true,
							"filterType": 6,
							"rootSchemaName": "CreatioAIIntentFile"
						}
					},
					"IsChatMode": {
						"from": "IsApiMode",
						"converter": "crt.IsEqual: false"
					},
					"DisplayAgentsDetail": {
						"from": [
							"IsChatMode"
						],
						"converter": "crt.IsEveryEqualTo: true"
					},
					"ShowToolCallsCheckbox": {
						"from": "UseAgenticProcessesFeatureValue",
						"converter": "crt.IsEqual: false"
					},
					"ShowWorkflowOnlyCheckbox": {
						"from": [
							"UseAgenticProcessesFeatureValue",
							"IsSystemType"
						],
						"converter": "crt.IsEveryEqualTo: true"
					},
					"UseAgenticProcessesFeatureName": {
						"value": "GenAIFeatures.UseAgenticProcesses"
					},
					"UseAgenticProcessesFeatureValue": {
						"from": "UseAgenticProcessesFeatureName",
						"converter": "crt.GetFeatureState"
					},
					"UseAgenticProcesses": {
						"from": [
							"UseAgenticProcessesFeatureValue",
							"IsApiMode"
						],
						"converter": "crt.IsEveryEqualTo: true"
					},
					"CopilotSkillAgents": {
						"isCollection": true,
						"modelConfig": {
							"path": "CopilotSkillAgentsDS",
							"sortingConfig": {
								"default": [
									{
										"columnName": "Name",
										"direction": "asc"
									}
								]
							},
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "CopilotSkillAgents_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"CopilotSkillAgentsDS_Id": {
									"modelConfig": {
										"path": "CopilotSkillAgentsDS.Id"
									}
								},
								"CopilotSkillAgentsDS_Name": {
									"modelConfig": {
										"path": "CopilotSkillAgentsDS.Name"
									}
								},
								"CopilotSkillAgentsDS_Status": {
									"modelConfig": {
										"path": "CopilotSkillAgentsDS.Status"
									}
								},
								"CopilotSkillAgentsDS_Description": {
									"modelConfig": {
										"path": "CopilotSkillAgentsDS.Description"
									}
								}
							}
						}
					},
					"CopilotSkillAgents_PredefinedFilter_Path": {
						"value": "[CopilotAgentSubSkill:Intent:Id].Id"
					},
					"CopilotSkillAgents_PredefinedFilter": {
						"from": "PDS_Id",
						"converter": "crt.ToEqualFilter: $CopilotSkillAgents_PredefinedFilter_Path"
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
					},
					"ShowExpands": {
						"value": false
					},
					"undefined_List": {
						"isCollection": true,
						"modelConfig": {}
					},
					"PDS_Status_List": {
						"isCollection": true,
						"modelConfig": {}
					}
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
						"CopilotLlmModelDS": [],
						"IntentFileListDS": [],
						"CopilotSkillAgentsDS": [],
						"InputParametersDS": [
							{
								"attributePath": "Intent",
								"relationPath": "PDS.Id"
							}
						],
						"OutputParametersDS": [
							{
								"attributePath": "Intent",
								"relationPath": "PDS.Id"
							}
						],
						"ActionsDetailDS": [
							{
								"attributePath": "Intent",
								"relationPath": "PDS.Id"
							}
						],
						"SubSkillsDetailDS": [
							{
								"attributePath": "Intent",
								"relationPath": "PDS.Id"
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
					"PDS": {
						"type": "crt.CopilotIntentDataSource",
						"scope": "page",
						"config": {
							"entitySchemaName": "CopilotIntent",
							"attributes": {
								"Id": {
									"path": "Id"
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
								"Prompt": {
									"path": "Prompt"
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
								"ParentIntentId": {
									"path": "ParentIntentId"
								},
								"ResponseFormatJsonSchema": {
									"path": "ResponseFormatJsonSchema"
								},
								"UsePageContext": {
									"path": "UsePageContext"
								},
								"UseChatHistory": {
									"path": "UseChatHistory"
								},
								"UseDefaultSystemPrompt": {
									"path": "UseDefaultSystemPrompt"
								},
								"WorkflowOnly": {
									"path": "WorkflowOnly"
								}
							}
						}
					},
					"InputParametersDS": {
						"type": "crt.CopilotIntentParameterDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotIntentParameter",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"Intent": {
									"path": "Intent"
								},
								"Name": {
									"path": "Name"
								},
								"Code": {
									"path": "Code"
								},
								"Description": {
									"path": "Description"
								},
								"SourceCollection": {
									"path": "SourceCollection"
								},
								"DataValueTypeUId": {
									"path": "DataValueTypeUId"
								}
							}
						}
					},
					"OutputParametersDS": {
						"type": "crt.CopilotIntentParameterDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotIntentParameter",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"Intent": {
									"path": "Intent"
								},
								"Name": {
									"path": "Name"
								},
								"Code": {
									"path": "Code"
								},
								"Description": {
									"path": "Description"
								},
								"SourceCollection": {
									"path": "SourceCollection"
								},
								"DataValueTypeUId": {
									"path": "DataValueTypeUId"
								}
							}
						}
					},
					"ActionsDetailDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotAction",
							"attributes": {
								"Name": {
									"path": "Name"
								},
								"Description": {
									"path": "Description"
								}
							}
						}
					},
					"SubSkillsDetailDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotIntCapability",
							"attributes": {
								"Name": {
									"path": "Name"
								},
								"Description": {
									"path": "Description"
								}
							}
						}
					},
					"IntentFileListDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CreatioAIIntentFile",
							"attributes": {
								"Name": {
									"path": "Name"
								},
								"CreatedOn": {
									"path": "CreatedOn"
								},
								"CreatedBy": {
									"path": "CreatedBy"
								},
								"Size": {
									"path": "Size"
								}
							}
						}
					},
					"CopilotSkillAgentsDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotAgent",
							"attributes": {
								"Name": {
									"path": "Name"
								},
								"Status": {
									"path": "Status"
								},
								"Description": {
									"path": "Description"
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