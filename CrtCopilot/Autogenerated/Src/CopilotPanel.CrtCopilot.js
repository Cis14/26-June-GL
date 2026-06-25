define("CopilotPanel", /**SCHEMA_DEPS*/["@creatio-devkit/common", "css!CopilotPanel"]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/(sdk)/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "Main",
				"values": {
					"classes": [
						"copilot-panel-main-wrapper-container"
					]
				}
			},
			{
				"operation": "remove",
				"name": "SidebarTitleLabel"
			},
						{
				"operation": "merge",
				"name": "MainHeader",
				"values": {
					"padding": {
						"top": "medium",
						"right": "medium",
						"bottom": "none",
						"left": "medium"
					}
				}
			},
			{
				"operation": "merge",
				"name": "MainContainer",
				"values": {
					"padding": {
						"top": "small",
						"bottom": "medium",
						"right": "medium",
						"left": "medium"
					},
					"classes": [
						"copilot-panel-main-container"
					]
				}
			},
			{
				"operation": "insert",
				"name": "AILogoImage",
				"values": {
					"type": "crt.ImageInput",
					"value": "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiB2aWV3Qm94PSIwIDAgODAgODEiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgZml0PSIiIHByZXNlcnZlQXNwZWN0UmF0aW89InhNaWRZTWlkIG1lZXQiIGZvY3VzYWJsZT0iZmFsc2UiPg0KPHBhdGggZD0iTTQ2Ljk4NzggMzEuOTA2NkM0NS4xNjcyIDMwLjE2NyA0My4wMDE0IDI4Ljc4NTcgNDAuNjE0OSAyNy44NDUyQzM4LjIyODUgMjYuOTA0NiAzNS42Njg5IDI2LjQyMjUgMzMuMDg0NiAyNi40MjZDMzAuNDUxNyAyNi40MTUgMjcuODQ0NSAyNi45MTQzIDI1LjQyMzMgMjcuODk1M0MyMC41OTk1IDI5LjgxNjUgMTYuNzc3NSAzMy40NzgzIDE0Ljc5OSAzOC4wNzE4QzEyLjgyMDQgNDIuNjY1MyAxMi44NDcgNDcuODE0MSAxNC44NzI3IDUyLjM4OTFDMTYuODk4NCA1Ni45NjQxIDIwLjc1ODIgNjAuNTg4OSAyNS42MDE1IDYyLjQ2NTVDMzAuNDQ0OSA2NC4zNDIgMzUuODc0NyA2NC4zMTk5IDQwLjY5ODUgNjIuMzk4N0M0My4wNjk4IDYxLjQ2NSA0NS4yMTk2IDYwLjA4NiA0Ny4wMTk3IDU4LjM0OThWNjMuNTIxNUg1My4wODMzVjI2LjgwMTZINDcuMDE5N1YzMS45MDY2SDQ2Ljk4NzhaTTMzLjA0NjEgNTguMTExOEMzMC4zMjQgNTguMTEyMyAyNy42NjM2IDU3LjM0NjEgMjUuNDAwMiA1NS45MTIxQzIzLjEzNjcgNTQuNDc4IDIxLjM3MyA1Mi40NDA4IDIwLjMzMTIgNTAuMDU1N0MxOS4yODk0IDQ3LjY3MDcgMTkuMDE2NyA0NS4wNDUxIDE5LjU0NzggNDIuNTEzMUMyMC4wNzg5IDM5Ljk4MTEgMjEuMzg5MSAzNy42NTU2IDIzLjMxNCAzNS44MzAzQzI1LjIzOSAzNC4wMDQ5IDI3LjY5MTYgMzIuNzY0NyAzMC4zNjE0IDMyLjI2MTRDMzMuMDMxMiAzMS43NTgxIDM1Ljc5ODUgMzIuMDE2IDM4LjMxMzEgMzMuMDA0NEM0MC44Mjc3IDMzLjk5MjggNDIuOTc3MyAzNS42NjYxIDQ0LjQ4OSAzNy44MTNDNDYuMDAwNyAzOS45NTk5IDQ2LjgwNzEgNDIuNDgxOCA0Ni44MDYyIDQ1LjA2MzVDNDYuODAwOCA0OC41MjI3IDQ1LjM0ODUgNTEuODM4MyA0Mi43NjkzIDU0LjI4NDFDNDAuMTkgNTYuNzMgMzYuNjkzNCA1OC4xMDczIDMzLjA0NjEgNTguMTExOFoiIGZpbGw9IiNGRjQwMTMiPjwvcGF0aD4NCjxwYXRoIGQ9Ik03OS40MTcxIDE1LjgyNTlMNTIuMjY4MSAyMS43ODgzTDYwLjM1NzIgMTUuODI1OUwyNy4yNDYxIDAuOTE5OTIyTDc5LjQxNzEgMTUuODI1OVoiIGZpbGw9IiMwRDJFNEUiPjwvcGF0aD4NCjxwYXRoIGQ9Ik02Ni40OTkxIDI2LjkyMzhINTkuNTQzVjYzLjUyNjNINjYuNDk5MVYyNi45MjM4WiIgZmlsbD0iI0ZGNDAxMyI+PC9wYXRoPg0KPGcgZmlsdGVyPSJ1cmwoJy8wL1NoZWxsLyNmaWx0ZXIwX2RfMTg0OTRfMTcwMzY4JykiPg0KPHBhdGggZmlsbC1ydWxlPSJldmVub2RkIiBjbGlwLXJ1bGU9ImV2ZW5vZGQiIGQ9Ik00Ny40OTI2IDU3Ljg3NTFINDYuOTkxOVY0NS41OTkxQzQ3LjAwOSA0NS4yMjU0IDQ3LjAwOTEgNDQuODUwOCA0Ni45OTE5IDQ0LjQ3NjNWMzEuODkwNkM0OC44OTA4IDMzLjY0NTMgNTAuNDEwNiAzNS43NjYgNTEuNDMyNSAzOC4xMzgzQzUzLjM4NiA0Mi42NzM4IDUzLjM1OTcgNDcuNzU3NSA1MS4zNTk3IDUyLjI3NDdDNTAuNDMyMiA1NC4zNjk0IDQ5LjExNTIgNTYuMjYyNCA0Ny40OTI2IDU3Ljg3NTFaIiBmaWxsPSJ1cmwoJy8wL1NoZWxsLyNwYWludDBfcmFkaWFsXzE4NDk0XzE3MDM2OCcpIiBzaGFwZS1yZW5kZXJpbmc9ImNyaXNwRWRnZXMiPjwvcGF0aD4NCjwvZz4NCjxwYXRoIGQ9Ik01My4wODMxIDI2Ljc1NTlINDYuOTU1MVY1NC43NDZINTMuMDgzMVYyNi43NTU5WiIgZmlsbD0idXJsKCcvMC9TaGVsbC8jcGFpbnQxX2xpbmVhcl8xODQ5NF8xNzAzNjgnKSI+PC9wYXRoPg0KPGRlZnM+DQo8ZmlsdGVyIGlkPSJmaWx0ZXIwX2RfMTg0OTRfMTcwMzY4IiB4PSI0NS45OTIyIiB5PSIzMS44OTA2IiB3aWR0aD0iNy44ODY3MiIgaGVpZ2h0PSIyNy45ODQ0IiBmaWx0ZXJVbml0cz0idXNlclNwYWNlT25Vc2UiIGNvbG9yLWludGVycG9sYXRpb24tZmlsdGVycz0ic1JHQiI+DQo8ZmVGbG9vZCBmbG9vZC1vcGFjaXR5PSIwIiByZXN1bHQ9IkJhY2tncm91bmRJbWFnZUZpeCI+PC9mZUZsb29kPg0KPGZlQ29sb3JNYXRyaXggaW49IlNvdXJjZUFscGhhIiB0eXBlPSJtYXRyaXgiIHZhbHVlcz0iMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMTI3IDAiIHJlc3VsdD0iaGFyZEFscGhhIj48L2ZlQ29sb3JNYXRyaXg+DQo8ZmVPZmZzZXQgZHk9IjEiPjwvZmVPZmZzZXQ+DQo8ZmVHYXVzc2lhbkJsdXIgc3RkRGV2aWF0aW9uPSIwLjUiPjwvZmVHYXVzc2lhbkJsdXI+DQo8ZmVDb21wb3NpdGUgaW4yPSJoYXJkQWxwaGEiIG9wZXJhdG9yPSJvdXQiPjwvZmVDb21wb3NpdGU+DQo8ZmVDb2xvck1hdHJpeCB0eXBlPSJtYXRyaXgiIHZhbHVlcz0iMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMC4wNSAwIj48L2ZlQ29sb3JNYXRyaXg+DQo8ZmVCbGVuZCBtb2RlPSJub3JtYWwiIGluMj0iQmFja2dyb3VuZEltYWdlRml4IiByZXN1bHQ9ImVmZmVjdDFfZHJvcFNoYWRvd18xODQ5NF8xNzAzNjgiPjwvZmVCbGVuZD4NCjxmZUJsZW5kIG1vZGU9Im5vcm1hbCIgaW49IlNvdXJjZUdyYXBoaWMiIGluMj0iZWZmZWN0MV9kcm9wU2hhZG93XzE4NDk0XzE3MDM2OCIgcmVzdWx0PSJzaGFwZSI+PC9mZUJsZW5kPg0KPC9maWx0ZXI+DQo8cmFkaWFsR3JhZGllbnQgaWQ9InBhaW50MF9yYWRpYWxfMTg0OTRfMTcwMzY4IiBjeD0iMCIgY3k9IjAiIHI9IjEiIGdyYWRpZW50VW5pdHM9InVzZXJTcGFjZU9uVXNlIiBncmFkaWVudFRyYW5zZm9ybT0idHJhbnNsYXRlKDQ3LjM2ODEgNTEuODQ5Mykgcm90YXRlKDcuMDc2NzcpIHNjYWxlKDEyLjA5OTggMTQ3LjIxNSkiPg0KPHN0b3Agb2Zmc2V0PSIwLjAwMDMwNDIxMSIgc3RvcC1jb2xvcj0iI0ZGNDAxMyI+PC9zdG9wPg0KPHN0b3Agb2Zmc2V0PSIwLjY0NDM0MiIgc3RvcC1jb2xvcj0iI0RDMkMwMyI+PC9zdG9wPg0KPHN0b3Agb2Zmc2V0PSIwLjk5IiBzdG9wLWNvbG9yPSIjRkY0MDEzIiBzdG9wLW9wYWNpdHk9IjAiPjwvc3RvcD4NCjwvcmFkaWFsR3JhZGllbnQ+DQo8bGluZWFyR3JhZGllbnQgaWQ9InBhaW50MV9saW5lYXJfMTg0OTRfMTcwMzY4IiB4MT0iNTAuMDE5MSIgeTE9IjI2Ljc1NTkiIHgyPSI1MC4wMTkxIiB5Mj0iNTQuNzQ2IiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI+DQo8c3RvcCBzdG9wLWNvbG9yPSIjRTgyQzAwIj48L3N0b3A+DQo8c3RvcCBvZmZzZXQ9IjAuNzE1IiBzdG9wLWNvbG9yPSIjRkY0MDEzIj48L3N0b3A+DQo8c3RvcCBvZmZzZXQ9IjAuOTkiIHN0b3AtY29sb3I9IiNGRjQwMTMiIHN0b3Atb3BhY2l0eT0iMCI+PC9zdG9wPg0KPC9saW5lYXJHcmFkaWVudD4NCjwvZGVmcz4NCjwvc3ZnPg",
					"readonly": true,
					"placeholder": "",
					"labelPosition": "auto",
					"customWidth": "28px",
					"customHeight": "28px",
					"borderRadius": "none",
					"positioning": "cover",
					"visible": true,
					"tooltip": ""
				},
				"parentName": "TitleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NewChatButton",
				"values": {
					"type": "crt.Button",
					"icon": "edit-button-icon",
					"size": "medium",
					"iconPosition": "only-icon",
					"title": "#ResourceString(NewChatButton_caption)#",
					"clicked": {
						"request": "crt.CopilotNewChatRequest",
						"params": {
							"chatMessagesAttributeName": "ChatMessages",
							"sessionIdAttributeName": "CurrentCopilotSessionId",
							"sessionTitleAttributeName": "CurrentCopilotSessionTitle",
							"conversationEventAttributeName": "ConversationEvent",
							"boundedIntentIdAttributeName": "BoundedIntentId"
						}
					},
					"color": "default",
					"disabled": "$IsCopilotDisclaimerVisible",
					"visible": true,
					"caption": "#ResourceString(NewChatButton_caption)#",
					"clickMode": "default"
				},
				"parentName": "TitleContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "CombinedModeChatButton",
				"values": {
					"type": "crt.Button",
					"icon": "unordered-list-icon",
					"size": "medium",
					"iconPosition": "only-icon",
					"title": "#ResourceString(CombinedModeChatButton)#",
					"color": "default",
					"visible": "$CombinedModeButtonVisible",
					"caption": "#ResourceString(CombinedModeChatButton)#",
					"clickMode": "default",
					"pressed": "$CombinedModeEnabled",
					"clicked": {
						"request": "crt.CombinedChatViewRequest",
						"params": {}
					},
				},
				"parentName": "TitleContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "CopilotActionButtonsContainer",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(CopilotChatActionsButtonCaption)#",
					"icon": "more-button-icon",
					"iconPosition": "only-icon",
					"color": "default",
					"size": "medium",
					"clickMode": "menu",
					"menuItems": [],
					"visible": "$CopilotActionButtonsVisible",
				},
				"parentName": "ActionButtonsContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "RenameChat_Button",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(RenameButton_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "left-icon",
					"icon": "pencil-button-icon",
					"visible": "$CanRenameChat",
					"clicked": {
						"request": "crt.CopilotRenameSessionRequest",
						"params": {
							"chatId": "$CurrentCopilotSessionId",
						}
					},
					"clickMode": "default"
				},
				"parentName": "CopilotActionButtonsContainer",
				"propertyName": "menuItems",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AgentBuilder_Button",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(AgentBuilderButton_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "left-icon",
					"visible": "$ShowAgentBuilderLink",
					"icon": "agent-builder-icon",
					"clicked": {
						"request": "crt.OpenAgentBuilderRequest"
					},
					"clickMode": "default"
				},
				"parentName": "CopilotActionButtonsContainer",
				"propertyName": "menuItems",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DebugInfo_Button",
				"values": {
					"type": "crt.MenuItem",
					"caption": "#ResourceString(DebugInfo_Button_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "left-icon",
					"visible": "$CanDebugSkills",
					"icon": "message-warn-button-icon",
					"clicked": {
						"request": "crt.CopilotShowDebugInfoRequest",
						"params": {
							"sessionId": "$CurrentCopilotSessionId"
						}
					},
					"clickMode": "default"
				},
				"parentName": "CopilotActionButtonsContainer",
				"propertyName": "menuItems",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Chat",
				"values": {
					"type": "crt.Chat",
					"isCombinedMode": "$IsCombinedMode",
					"combinedModeEnabled": "$CombinedModeEnabled",
					"items": [],
					"openChatSession": {
						"request": "crt.ChatSessionOpenRequest",
						"params": {
							"session": { "id": "@event" }
						}
					},
					"openChatSessionWithMessage": {
						"request": "crt.CopilotNewChatWithMessageRequest",
						"params": {
							"sessionId": "@event.sessionId", 
							"message": "@event.message" 
						}
					},
					"changeCombinedMode": {
						"request": "crt.CopilotChatModeRequest",
						"params": {
							"isCombined": "@event",
						}
					},
					"combinedModeButtonVisible": "$CombinedModeButtonVisible",
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "FlexContainer_Chat",
				"values": {
					"type": "crt.FlexContainer",
					"direction": "row",
					"gap": "none",
					"alignItems": "stretch",
					"wrap": "nowrap",
					"stretch": true,
					"fitContent": true,
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "none"
					},
					"items": []
				},
				"parentName": "Chat",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ChatUploadWrapper",
				"values": {
					"type": "crt.FileDrop",
					"visible": "$IsConversationVisible",
					"multiple": false,
					"showOverlay": true,
					"disabled": "$EnableChatFileHandling | crt.InvertBooleanValue",
					"items": [],
					"fileDropped": {
						"request": "crt.UploadFileRequest",
						"params": {
							"fileEntitySchemaName": "CreatioAISessionFile",
							"recordColumnName": "SessionId",
							"recordId": "$CurrentCopilotSessionId",
							"files": "@event.files"
						}
					}
				},
				"parentName": "FlexContainer_Chat",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Conversation",
				"values": {
					"type": "crt.Conversation",
					"actions": [
						{
							"type": "crt.FlexContainer",
							"direction": "row",
							"gap": "10",
							"alignItems": "center",
							"items": [
								{
									"type": "crt.Button",
									"icon": "back-button-icon",
									"iconPosition": "only-icon",
									"visible": "$IsBackButtonVisible",
									"labelType": "body-1",
									"title": "#ResourceString(GoBackToListButton_title)#",
									"color": "default",
									"size": "medium",
									"clicked": {
										"request": "crt.ChatListOpenRequest",
										"params": {}
									}
								},
								{
									"type": "crt.Label",
									"caption": "$CurrentCopilotSessionTitle",
									"labelType": "body-large",
									"labelThickness": "default",
									"labelTextAlign": "start",
									"labelEllipsis": true,
								}
							]
						}
					],
					"information": [],
					"placeholder": [
						{
							"type": "crt.Placeholder",
							"image": {
								"type": "icon",
								"icon": "copilot-logo",
								"width": "80px",
								"height": "80px",
								"padding": "0px"
							},
							"title": null,
							"subhead": null
						},
						{
							"type": "crt.ChatDisclaimer",
							"visible": "$IsCopilotDisclaimerVisible",
							"caption": "$CopilotDisclaimer",
							"consentCode": "CopilotLegalNotice",
							"acceptClicked": {
								"request": "crt.CopilotDisclaimerAcceptRequest",
								"params": {
									"consentCode": "CopilotLegalNotice"
								}
							}
						},
						{
							"type": "crt.FlexContainer",
							"items": [
								{
									"type": "crt.Label",
									"caption": "#MacrosTemplateString(#ResourceString(CopilotTitle_text)#)#",
									"labelType": "body",
									"labelThickness": "default",
									"labelTextAlign": "center"
								},
								{
									"type": "crt.TemplateList",
									"items": "$CopilotQuickActions",
									"direction": "row",
									"gap": 8,
									"template": [
										{
											"type": "crt.Button",
											"name": "$CopilotQuickActions.Code",
											"caption": "$CopilotQuickActions.Name",
											"size": "large",
											"color": "outline",
											"clicked": {
												"request": "crt.CopilotActionRequest",
												"params": {
													"prompt": "$CopilotQuickActions.Name",
													"promptCode": "$CopilotQuickActions.Code",
													"useCurrentSession": true
												}
											},
											"disabled": "$NavigationStateIsLoading"
										}
									],
									"classes": [
										"copilot-actions-list"
									]
								}
							],
							"direction": "column",
							"visible": "$IsCopilotChatVisible",
							"color": "transparent",
							"borderRadius": "none",
							"padding": {
								"top": "none",
								"right": "none",
								"bottom": "none",
								"left": "none"
							},
							"stretch": true,
							"alignItems": "stretch",
							"justifyContent": "start",
							"gap": "small",
							"wrap": "nowrap"
						}
					],
					"conversationEvent": "$ConversationEvent",
					"messages": "$ChatMessages",
					"previewMessageId": "$PreviewMessageId",
					"searchFilter": "$ChatsSearchFilter",
					"isTyping": "$IsTyping",
					"tools": [],
					"typing": [
						{
							"type": "crt.ChatTyping",
							"author": "$CopilotContact",
							"message": "#ResourceString(CopilotTyping_text)#"
						}
					],
					"loading": "$ConversationLoading",
					"conversationId": "$CurrentCopilotSessionId",
					"paginationPageSize": 15,
					"paginationChange": {
						"request": "crt.ChatSessionPaginationRequest",
						"params": {
							"sessionId": "@event.id",
							"offset": "@event.offset"
						}
					}
				},
				"parentName": "ChatUploadWrapper",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "FlexContainer_MessageEditor",
				"values": {
					"type": "crt.FlexContainer",
					"visible": "$IsCopilotChatVisible",
					"direction": "row",
					"justifyContent": "start",
					"gap": "none",
					"alignItems": "center",
					"items": []
				},
				"parentName": "Conversation",
				"propertyName": "tools",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Conversation_MessageEditor",
				"values": {
					"type": "crt.MessageEditor",
					"items": []
				},
				"parentName": "FlexContainer_MessageEditor",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MessageEditorBody",
				"values": {
					"type": "crt.MessageEditorBody",
					"toolbarItems": [
						{
							"type": "crt.Button",
							"displayType": "text",
							"name": "MessageAttachFileButton",
							"icon": "clip-button-icon",
							"size": "small",
							"iconPosition": "only-icon",
							"title": "#ResourceString(AttachButton_caption)#",
							"color": "default",
							"clicked": {
								"request": "crt.UploadFileRequest",
								"params": {
									"fileEntitySchemaName": "CreatioAISessionFile",
									"recordColumnName": "SessionId",
									"recordId": "$CurrentCopilotSessionId"
								}
							},
							"visible": "$EnableChatFileHandling"
						},
						{
							"type": "crt.Button",
							"displayType": "text",
							"name": "MentionButton",
							"visible": "$MentionButtonVisible",
							"icon": "at-icon",
							"size": "small",
							"iconPosition": "only-icon",
							"title": "#ResourceString(MentionButton_caption)#",
							"color": "default",
							"clicked": {
								"request": "crt.TriggerMentionRequest",
								"params": {
									"actionTarget": "MessageEditorInputAction"
								}
							}
						},
						{
							"type": "crt.ChipList",
							"name": "SelectedMentionsChipList",
							"visible": "$SelectedMentionsChipListVisible",
							"items": "$SelectedMentionsChipList",
							"itemRemove": {
								"request": "crt.CopilotChatRemoveBoundedSessionRequest",
								"params": {
									"attributesMapping": "$MessageEditorAttributesMapping"
								}
							}
						}
					],
					"inputs": [],
					"chatInput": "$ChatInput",
					"sendMessage": {
						"request": "crt.MessageEditorSendRequest",
						"params": {
							"attributesMapping": "$MessageEditorAttributesMapping"
						}
					}
				},
				"parentName": "Conversation_MessageEditor",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MessageEditorCrtInput",
				"values": {
					"type": "crt.MessageEditorInput",
					"chatInput": "$ChatInput",
					"action": "$MessageEditorInputAction",
					"inputMode": "html",
					"getMentionService": {
						"request": "crt.CopilotMentionInitRequest",
						"params": {
							"initService": "@event"
						}
					},
					"sendMessage": {
						"request": "crt.MessageEditorSendRequest",
						"params": {
							"attributesMapping": "$MessageEditorAttributesMapping",
						}
					},
					"chatInputChange": {
						"request": "crt.MessageEditorInputChangeRequest",
						"params": {
							"attributesMapping": "$MessageEditorAttributesMapping",
							"newValue": "@event"
						}
					}
				},
				"parentName": "MessageEditorBody",
				"propertyName": "inputs",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ChatList",
				"values": {
					"type": "crt.ChatList",
					"visible": "$IsChatListVisible",
					"chats": "$ActiveChatsList",
					"isCombinedMode": "$IsCombinedMode",
					"loading": "$IsActiveChatsLoading",
					"selectedChatId": "$CurrentCopilotSessionId",
					"currentPage": "$ChatsListPage",
					"searchFilter": "$ChatsSearchFilter",
					"searchDisableFeatureName": "DisableSearchForCreatioAI",
					"chatsListChange": {
						"request": "crt.ChatListChangeRequest",
						"params": {
							"chats": "@event",
						}
					},
					"loadData": {
						"request": "crt.CopilotChatListLoadRequest",
						"params": {
							"searchFilter": "@event.search",
							"rowsToLoadCount": "@event.count"
						}
					},
					"focusedChatId": "$FocusedChatId",
					"chatClicked": {
						"request": "crt.ChatSessionOpenRequest",
						"params": {
							"session": "@event"
						}
					},
					"createChatClicked": {
						"request": "crt.CopilotNewChatRequest",
						"params": {
							"chatMessagesAttributeName": "ChatMessages",
							"sessionIdAttributeName": "CurrentCopilotSessionId",
							"sessionTitleAttributeName": "CurrentCopilotSessionTitle",
							"conversationEventAttributeName": "ConversationEvent",
							"boundedIntentIdAttributeName": "BoundedIntentId"
						}
					},
					"menuItems": [
						{
							"type": "crt.MenuItem",
							"caption": "#ResourceString(RenameButton_caption)#",
							"color": "default",
							"disabled": false,
							"size": "medium",
							"iconPosition": "left-icon",
							"icon": "pencil-button-icon",
							"clicked": {
								"request": "crt.CopilotRenameSessionRequest",
								"params": {
									"chatId": "$FocusedChatId",
								}
							},
							"clickMode": "default"
						},
					]
				},
				"parentName": "FlexContainer_Chat",
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
					"CanDebugSkills": {
						"value": false
					},
					"ShowAgentBuilderLink": {
						"value": false
					},
					"CanRenameChat": {
						"value": false
					},
					"CopilotActionButtonsVisible": {
						"value": false
					},
					"ChatMessages": {},
					"ActiveChats": {},
					"CopilotContact": {},
					"ChatsListVisible": {
						"value": true
					},
					"IsCombinedMode": {
						"value": false
					},
					"ChatsListPage": {
						"value": 0
					},
					"PreviewMessageId": {
						"value": null
					},
					"ChatsSearchFilter": {},
					"ActiveChatsList": {
						"from": "ActiveChats",
						"converter": "crt.CopilotChatListConverter | crt.ChatPreviewTrimConverter"
					},
					"IsChatListVisible": {
						"from": [
							"IsCombinedMode",
							"ChatsListVisible"
						],
						"converter": "crt.CopilotChatMode : false"
					},
					"IsConversationVisible": {
						"from": [
							"IsCombinedMode",
							"ChatsListVisible"
						],
						"converter": "crt.CopilotChatMode : true"
					},
					"IsBackButtonVisible": {
						"from": [
							"IsCombinedMode",
							"IsCopilotDisclaimerVisible"
						],
						"converter": "crt.IsEveryEqualTo : false"
					},
					"NavigationStateIsLoading": {
						"value": true
					},
					"IsCopilotDisclaimerVisible": {
						"value": false
					},
					"IsActiveChatsLoading": {},
					"IsCopilotChatVisible": {
						"value": false
					},
					"CopilotDisclaimer": {
						"value": ""
					},
					"CombinedModeEnabled": {
						"value": true
					},
					"CombinedModeButtonVisible": {
						"value": false
					},
					"CopilotQuickActions": {
						"isCollection": true,
						"viewModelConfig": {
							"attributes": {
								"Name": {
									"modelConfig": {
										"path": "CopilotIntentDS.Name"
									}
								},
								"Code": {
									"modelConfig": {
										"path": "CopilotIntentDS.Code"
									}
								},
							}
						}
					},
					"CopilotIntentPageQuickLinks": {
						"isCollection": true,
						"modelConfig": {
							"path": "CopilotIntentPageQuickLinksDS"
						},
						"viewModelConfig": {
							"attributes": {
								"PageName": {
									"modelConfig": {
										"path": "CopilotIntentPageQuickLinksDS.PageName"
									}
								},
								"IntentCode": {
									"modelConfig": {
										"path": "CopilotIntentPageQuickLinksDS.IntentCode"
									}
								},
								"Name": {
									"modelConfig": {
										"path": "CopilotIntentPageQuickLinksDS.Name"
									}
								},
							}
						}
					},
					"CopilotIntents": {
						"isCollection": true,
						"modelConfig": {
							"path": "CopilotIntentDS",
							"filterAttributes": [
								{
									"loadOnChange": true,
									"name": "ActiveIntents_r8uqh52_PredefinedFilter"
								}
							]
						},
						"viewModelConfig": {
							"attributes": {
								"Id": {
									"modelConfig": {
										"path": "CopilotIntentDS.Id"
									}
								},
								"Code": {
									"modelConfig": {
										"path": "CopilotIntentDS.Code"
									}
								},
								"Name": {
									"modelConfig": {
										"path": "CopilotIntentDS.Name"
									}
								}
							}
						}
					},
					"ActiveIntents_r8uqh52_PredefinedFilter": {
						"value": {
							"isEnabled": true,
							"trimDateTimeParameterToDate": false,
							"filterType": 6,
							"logicalOperation": 0,
							"items": {
								"ActiveIntents": {
									"isEnabled": true,
									"trimDateTimeParameterToDate": false,
									"filterType": 1,
									"comparisonType": 3,
									"leftExpression": {
										"expressionType": 0,
										"columnPath": "Status"
									},
									"rightExpression": {
										"expressionType": 2,
										"parameter": {
											"dataValueType": 1,
											"value": "1D73B111-07A9-49E2-AA15-C9415CCE7470"
										}
									}
								}
							}
						}
					},
					"CurrentPageSchemaName": {
						"value": null
					},
					"IsTyping": {},
					"ChatInput": {
						"value": ""
					},
					"MessageEditorInputAction": {
						"value": ""
					},
					"ConversationEvent": {
						"value": []
					},
					"CurrentCopilotSessionId": {
						"value": null
					},
					"CurrentCopilotSessionTitle": {
						"value": ""
					},
					"EnableChatFileHandling": {
						"value": false
					},
					"ConversationLoading": {
						"value": false
					},
					"EnableGuidedSessionFeatureValue": {
						value: false
					},
					"MentionButtonVisible": {
						"from": ["EnableGuidedSessionFeatureValue", "BoundedIntentId"],
						"converter": "crt.MentionButtonVisible"
					},
					"SelectedMentionsChipListVisible": {
						"from": "MentionButtonVisible",
						"converter": "crt.InvertBooleanValue"
					},
					"BoundedIntentId": {
						value: null,
					},
					"SelectedMentionsChipList": {
						"from": ["BoundedIntentId"],
						"converter": "crt.IntentsToMentionsChipList"
					},
					"MessageEditorAttributesMapping": {
						"value": {
							"chatInput": "ChatInput",
							"chatId": "CurrentCopilotSessionId",
							"chatMessages": "ChatMessages",
							"boundedIntentId": "BoundedIntentId",
							"selectedMentions": "SelectedMentionsChipList"
						}
					},
					"FocusedChatId": {
						"value": null
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
					"CopilotIntentDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotIntent",
							"attributes": {
								"Id": {
									"path": "Id"
								},
								"Name": {
									"path": "Name"
								}
							}
						}
					},
					"CopilotIntentPageQuickLinksDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "CopilotIntentPageQuickLinks",
							"attributes": {
								"IntentCode": {
									"path": "IntentCode"
								},
								"PageName": {
									"path": "PageName"
								},
								"Name": {
									"path": "Name"
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
					const rightsService = new sdk.RightsService();
					const canDebugSkills = await rightsService.getCanExecuteOperation('CanDebugSkills');
					request.$context.CanDebugSkills = canDebugSkills;
					const featureService = new sdk.FeatureService();
					const features = await featureService.getFeaturesState([
						'GenAIFeatures.EnableGuidedSession',
						'GenAIFeatures.ShowAgentBuilderLink'
					]);
					request.$context.EnableGuidedSessionFeatureValue = features[0];
					request.$context.ShowAgentBuilderLink = features[1];
					return next?.handle(request);
				}
			},
			{
				request: "crt.HandleViewModelAttributeChangeRequest",
				handler: async (request, next) => {
					const allowedAttributes = [
						'CurrentCopilotSessionId',
						'IsChatListVisible',
						'CanDebugSkills',
						];
					if (allowedAttributes.includes(request.attributeName)) {
						const context = request.$context;
						const currentSessionId = await context['CurrentCopilotSessionId'];
						const isCombinedMode = await context['IsCombinedMode'];
						const isConversationVisible = await context['IsConversationVisible'];
						const canDebugSkills = await context['CanDebugSkills'];
						const canRename = currentSessionId && (isCombinedMode || isConversationVisible);
						context['CanRenameChat'] = canRename;
						context['CopilotActionButtonsVisible'] = canRename || canDebugSkills;
					}
					return next?.handle(request);
				}
			},
			{
				request: "crt.OpenAgentBuilderRequest",
				handler: async (request, next) => {
					const sysSettingsService = new sdk.SysSettingsService();
					const agentBuilderSiteUrl = (await sysSettingsService.getByCode('AgentBuilderSiteUrl'))?.value;
					if (agentBuilderSiteUrl) {
						window.open(agentBuilderSiteUrl, '_blank');
					}
					return next?.handle(request);
				}
			}
		]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{
			"crt.MentionButtonVisible": ([enableGuidedSessionFeatureValue, boundedIntentId]) =>
				!enableGuidedSessionFeatureValue || boundedIntentId == null
		}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});