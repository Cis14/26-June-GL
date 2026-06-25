define("AIAgents_FormPage", /**SCHEMA_DEPS*/["css!AIAgents_FormPage"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
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
				"operation": "merge",
				"name": "CancelButton",
				"values": {
					"size": "large",
					"iconPosition": "only-text"
				}
			},
			{
				"operation": "insert",
				"name": "Restrictions",
				"values": {
					"type": "crt.Input",
					"label": "#ResourceString(Label_Restrictions_caption)#",
					"labelPosition": "above",
					"control": "$PDS_Restrictions",
					"multiline": true,
					"visible": true,
					"readonly": false,
					"placeholder": "#ResourceString(Restrictions_placeholder)#",
					"tooltip": "#ResourceString(Restrictions_tooltip)#"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Prompt",
				"values": {
					"type": "crt.Input",
					"label": "#ResourceString(Label_Prompt_caption)#",
					"labelPosition": "above",
					"control": "$PDS_Prompt",
					"visible": true,
					"readonly": false,
					"placeholder": "#ResourceString(Prompt_placeholder)#",
					"tooltip": "#ResourceString(Prompt_tooltip)#",
					"multiline": true
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "RoleDescription",
				"values": {
					"type": "crt.Input",
					"label": "#ResourceString(Label_Role_caption)#",
					"labelPosition": "above",
					"control": "$PDS_RoleDescription",
					"multiline": true,
					"visible": true,
					"readonly": false,
					"placeholder": "#ResourceString(RoleDescription_placeholder)#",
					"tooltip": "#ResourceString(RoleDescription_tooltip)#"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 3
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
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 4
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
				"index": 0
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
					"clicked": {},
					"visible": true,
					"clickMode": "menu",
					"menuItems": []
				},
				"parentName": "SubSkillsFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "CreateNewSubSkills",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(CreateNewSubSkills_caption)#",
					"clicked": {
						"request": "crt.CreateRecordRequest",
						"params": {
							"entityName": "CopilotAgentSubSkill",
							"defaultValues": [
								{
									"attributeName": "ParentIntentId",
									"value": "$Id"
								},
								{
									"attributeName": "PackageUId",
									"value": "$PDS_PackageUId"
								}
							]
						}
					}
				},
				"parentName": "GridDetailAddSubSkillsBtn",
				"propertyName": "menuItems",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "SelectExistingSubSkill",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(SelectExistingSubSkill_caption)#",
					"clicked": {
						"request": "crt.CopilotSelectExistingSkillRequest",
						"params": {
							"caption": "#ResourceString(SelectExistingSubSkill_caption)#",
							"agentId": "$Id",
							"packageUId": "$PDS_PackageUId"
						}
					}
				},
				"parentName": "GridDetailAddSubSkillsBtn",
				"propertyName": "menuItems",
				"index": 1
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
				"index": 0
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
							"id": "ab3b73d7-2ec5-b54e-a5af-6fb20347f95c",
							"code": "SubSkillsDetailDS_Name",
							"caption": "#ResourceString(SubSkillsDetailDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "934f65bb-4ac7-76c0-5231-c4afc3b29de0",
							"code": "SubSkillsDetailDS_Description",
							"caption": "#ResourceString(SubSkillsDetailDS_Description)#",
							"dataValueType": 28,
							"width": 369
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
									"parentIntentId": "$PDS_Id"
								}
							}
						}
					],
					"placeholder": false
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
					"visible": "$IsApiMode | crt.InvertBooleanValue",
					"title": "#ResourceString(ToolsExpansionPanel_title)#",
					"toggleType": "default",
					"togglePosition": "before",
					"expanded": true,
					"labelColor": "auto",
					"fullWidthHeader": false,
					"titleWidth": 20,
					"padding": {
						"top": "medium",
						"bottom": "small",
						"left": "none",
						"right": "none"
					},
					"fitContent": true,
					"alignItems": "stretch"
				},
				"parentName": "RightSideContainer",
				"propertyName": "items",
				"index": 5
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
							"id": "ab3b73d7-2ec5-b54e-a5af-6fb20347f95c",
							"code": "ActionsDetailDS_Name",
							"caption": "#ResourceString(ActionsDetailDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "934f65bb-4ac7-76c0-5231-c4afc3b29de0",
							"code": "ActionsDetailDS_Description",
							"caption": "#ResourceString(ActionsDetailDS_Description)#",
							"dataValueType": 28,
							"width": 369
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
					],
					"placeholder": false
				},
				"parentName": "DataGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFilesExpansionPanel",
				"values": {
					"type": "crt.ExpansionPanel",
					"tools": [],
					"items": [],
					"title": "#ResourceString(AgentFilesExpansionPanel_title)#",
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
				"index": 6
			},
			{
				"operation": "insert",
				"name": "AgentFileActionGridContainer",
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
				"parentName": "AgentFilesExpansionPanel",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFileActionFlexContainer",
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
				"parentName": "AgentFileActionGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFileAddBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AgentFileAddBtn_caption)#",
					"icon": "upload-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clicked": {
						"request": "crt.UploadFileRequest",
						"params": {
							"viewElementName": "AgentFileList"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "AgentFileActionFlexContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFileRefreshBtn",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(AgentFileRefreshBtn_caption)#",
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
							"dataSourceName": "AgentFileListDS"
						}
					},
					"visible": true,
					"clickMode": "default"
				},
				"parentName": "AgentFileActionFlexContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "AgentFileGridContainer",
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
				"parentName": "AgentFilesExpansionPanel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFileList",
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
							"code": "AgentFileListDS_Name",
							"caption": "#ResourceString(AgentFileListDS_Name)#",
							"dataValueType": 28
						},
						{
							"id": "3b87b6c0-adeb-0580-761b-d43469db9782",
							"code": "AgentFileListDS_CreatedOn",
							"caption": "#ResourceString(AgentFileListDS_CreatedOn)#",
							"dataValueType": 7
						},
						{
							"id": "26f8c27b-c269-ff1a-272a-c3affd27b65a",
							"code": "AgentFileListDS_CreatedBy",
							"caption": "#ResourceString(AgentFileListDS_CreatedBy)#",
							"dataValueType": 10
						},
						{
							"id": "88b39285-116f-a431-5a3d-6540a834dd17",
							"code": "AgentFileListDS_Size",
							"caption": "#ResourceString(AgentFileListDS_Size)#",
							"dataValueType": 4
						}
					],
					"items": "$AgentFileList",
					"primaryColumnName": "AgentFileListDS_Id",
					"visible": "$CardState | crt.IsEqual : 'edit'"
				},
				"parentName": "AgentFileGridContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentFileListPlaceholder",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(AgentFileListPlaceholder_caption)#)#",
					"labelType": "placeholder-large",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "auto",
					"labelBackgroundColor": "transparent",
					"labelTextAlign": "center",
					"visible": "$CardState | crt.IsEqual : 'add'"
				},
				"parentName": "AgentFilesExpansionPanel",
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
					"PDS_Prompt": {
						"modelConfig": {
							"path": "PDS.Prompt"
						},
						"validators": {
							"required": {
								"type": "crt.Required"
							}
						}
					},
					"PDS_Restrictions": {
						"modelConfig": {
							"path": "PDS.Restrictions"
						}
					},
					"PDS_RoleDescription": {
						"modelConfig": {
							"path": "PDS.RoleDescription"
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
								"ActionsDetailDS_Code": {
									"modelConfig": {
										"path": "ActionsDetailDS.Code"
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
					"AgentFileList": {
						"isCollection": true,
						"modelConfig": {
							"path": "AgentFileListDS",
							"sortingConfig": {
								"default": [
									{
										"direction": "asc",
										"columnName": "Name"
									}
								]
							},
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "AgentFileList_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"AgentFileListDS_Name": {
									"modelConfig": {
										"path": "AgentFileListDS.Name"
									}
								},
								"AgentFileListDS_CreatedOn": {
									"modelConfig": {
										"path": "AgentFileListDS.CreatedOn"
									}
								},
								"AgentFileListDS_CreatedBy": {
									"modelConfig": {
										"path": "AgentFileListDS.CreatedBy"
									}
								},
								"AgentFileListDS_Size": {
									"modelConfig": {
										"path": "AgentFileListDS.Size"
									}
								},
								"AgentFileListDS_Id": {
									"modelConfig": {
										"path": "AgentFileListDS.Id"
									}
								}
							}
						}
					},
					"AgentFileList_PredefinedFilter": {
						"value": {
							"items": {
								"IntentUIdFilter": {
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
					"ShowExpands": {
						"value": false
					},
					"undefined_List": {
						"isCollection": true,
						"modelConfig": {}
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"dataSources"
				],
				"values": {
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
								},
								"Code": {
									"path": "Code"
								}
							}
						}
					},
					"SubSkillsDetailDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotAgentSubSkill",
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
					"AgentFileListDS": {
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
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"dataSources",
					"PDS",
					"config",
					"attributes"
				],
				"values": {
					"Prompt": {
						"path": "Prompt"
					},
					"RoleDescription": {
						"path": "RoleDescription"
					},
					"Restrictions": {
						"path": "Restrictions"
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"dependencies"
				],
				"values": {
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
					],
					"AgentFileListDS": []
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});