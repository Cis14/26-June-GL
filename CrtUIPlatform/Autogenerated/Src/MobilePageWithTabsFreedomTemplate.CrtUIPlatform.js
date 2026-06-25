{
	"viewConfigDiff": [
		{
			"operation": "merge",
			"name": "MainContainer",
			"values": {
				"padding": {
					"left": "small",
					"top": "none",
					"bottom": "none",
					"right": "small"
				}
			}
		},
		{
			"operation": "merge",
			"name": "FloatingActionButton",
			"values": {
				"visible": "$PrimaryModelMode | crt.IsEqual : 'update' | crt.AndBooleanValue : $IsMainTabSelected"
			}
		},
		{
			"operation": "move",
			"name": "AreaProfileContainer",
			"parentName": "GeneralTabContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "Tabs",
			"values": {
				"type": "crt.TabPanel",
				"items": [],
				"mode": "tab",
				"scrollable": true,
				"bodyBackgroundColor": "transparent"
			},
			"parentName": "MainContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "GeneralInfoTab",
			"values": {
				"type": "crt.TabContainer",
				"items": [],
				"caption": "#ResourceString(GeneralInfoTab_caption)#",
				"iconPosition": "only-text"
			},
			"parentName": "Tabs",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "GeneralTabContainer",
			"values": {
				"type": "crt.GridContainer",
				"items": [],
				"padding": {
					"top": "none",
					"bottom": "medium",
					"left": "none",
					"right": "none"
				},
				"gap": "small"
			},
			"parentName": "GeneralInfoTab",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "FeedTab",
			"values": {
				"type": "crt.TabContainer",
				"items": [],
				"caption": "#ResourceString(FeedTab_caption)#",
				"iconPosition": "only-text",
				"visible": "$PrimaryModelMode | crt.IsEqual : 'update'"
			},
			"parentName": "Tabs",
			"propertyName": "items",
			"index": 1
		},
		{
			"operation": "insert",
			"name": "FeedTabContainer",
			"values": {
				"type": "crt.GridContainer",
				"gap": {
					"rowGap": "small"
				},
				"alignItems": "stretch",
				"items": []
			},
			"parentName": "FeedTab",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "FeedContainer",
			"values": {
				"type": "crt.GridContainer",
				"padding": {
					"top": "small"
				},
				"color": "primary",
				"borderRadius": "medium",
				"gap": {
					"rowGap": "small"
				},
				"alignItems": "stretch",
				"items": [],
				"layoutConfig": {
					"column": 1,
					"colSpan": 1,
					"row": 1,
					"rowSpan": 1
				},
				"visible": true
			},
			"parentName": "FeedTabContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "Feed",
			"values": {
				"type": "crt.Feed",
				"primaryColumnValue": "$Id",
				"dataSourceName": null,
				"entitySchemaName": "#DataSourceEntityName()#"
			},
			"parentName": "FeedContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "AttachmentsTab",
			"values": {
				"type": "crt.TabContainer",
				"items": [],
				"caption": "#ResourceString(AttachmentsTab_caption)#",
				"iconPosition": "only-text",
				"visible": "$PrimaryModelMode | crt.IsEqual : 'update'"
			},
			"parentName": "Tabs",
			"propertyName": "items",
			"index": 2
		},
		{
			"operation": "insert",
			"name": "AttachmentsTabContainer",
			"values": {
				"type": "crt.GridContainer",
				"gap": "small",
				"items": [],
				"borderRadius": "none",
				"padding": {
					"top": "none",
					"right": "none",
					"bottom": "none",
					"left": "none"
				}
			},
			"parentName": "AttachmentsTab",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "AttachmentsContainer",
			"values": {
				"type": "crt.GridContainer",
				"color": "primary",
				"borderRadius": "medium",
				"items": [],
				"padding": {
					"top": "medium",
					"bottom": "medium",
					"left": "medium",
					"right": "medium"
				},
				"gap": "small"
			},
			"parentName": "AttachmentsTabContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "AttachmentsHeaderContainer",
			"values": {
				"type": "crt.FlexContainer",
				"direction": "row",
				"wrap": "nowrap",
				"alignItems": "center",
				"justifyContent": "space-between",
				"gap": "none",
				"borderRadius": "none",
				"padding": {
					"top": "none",
					"bottom": "small",
					"left": "none",
					"right": "none"
				},
				"items": [],
				"fitContent": true
			},
			"parentName": "AttachmentsContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "AttachmentsContainerLabel",
			"values": {
				"type": "crt.Label",
				"caption": "#ResourceString(AttachmentsContainerLabel_caption)#",
				"labelType": "headline-3",
				"labelThickness": "default",
				"labelEllipsis": false,
				"labelColor": "#0D2E4E"
			},
			"parentName": "AttachmentsHeaderContainer",
			"propertyName": "items",
			"index": 0
		},
		{
			"operation": "insert",
			"name": "UploadAttachmentButton",
			"values": {
				"type": "crt.Button",
				"caption": "#ResourceString(UploadAttachmentButton_caption)#",
				"icon": "upload-button-icon",
				"iconPosition": "only-icon",
				"color": "default",
				"size": "small",
				"clicked": {
					"request": "crt.UploadFileRequest",
					"params": {
						"viewElementName": "AttachmentFileList"
					}
				}
			},
			"parentName": "AttachmentsHeaderContainer",
			"propertyName": "items",
			"index": 1
		},
		{
			"operation": "insert",
			"name": "AttachmentFileList",
			"values": {
				"type": "crt.FileList",
				"masterRecordColumnValue": "$Id",
				"recordColumnName": "RecordId",
				"scrollable": true,
				"columns": [
					{
						"id": "57795b02-652b-9922-7e7b-234e164d7588",
						"code": "AttachmentListDS_Name",
						"caption": "#ResourceString(AttachmentListDS_Name)#",
						"dataValueType": 28
					},
					{
						"id": "dfb8dfb5-523f-aba2-58f9-55ed37e77101",
						"code": "AttachmentListDS_CreatedOn",
						"caption": "#ResourceString(AttachmentListDS_CreatedOn)#",
						"dataValueType": 7
					},
					{
						"id": "57a03c4f-d9f6-19ea-4982-f5e5db3f6574",
						"code": "AttachmentListDS_Size",
						"caption": "#ResourceString(AttachmentListDS_Size)#",
						"dataValueType": 4
					},
					{
						"id": "234ad108-20d0-433a-d615-baee46fcd8aa",
						"code": "AttachmentListDS_CreatedBy",
						"caption": "#ResourceString(AttachmentListDS_CreatedBy)#",
						"dataValueType": 10
					}
				],
				"items": "$AttachmentList",
				"primaryColumnName": "AttachmentListDS_Id"
			},
			"parentName": "AttachmentsContainer",
			"propertyName": "items",
			"index": 1
		}
	],
	"viewModelConfigDiff": [
		{
			"operation": "merge",
			"path": [
				"attributes"
			],
			"values": {
				"IsMainTabSelected": {
					"from": "Tabs_SelectedTabName",
					"converter": "crt.IsEqual : 'GeneralInfoTab'"
				},
				"AttachmentList": {
					"isCollection": true,
					"modelConfig": {
						"path": "AttachmentListDS",
						"sortingConfig": {
							"default": [
								{
									"columnName": "CreatedOn",
									"direction": "desc"
								}
							]
						}
					},
					"viewModelConfig": {
						"attributes": {
							"AttachmentListDS_Name": {
								"modelConfig": {
									"path": "AttachmentListDS.Name"
								}
							},
							"AttachmentListDS_CreatedOn": {
								"modelConfig": {
									"path": "AttachmentListDS.CreatedOn"
								}
							},
							"AttachmentListDS_Size": {
								"modelConfig": {
									"path": "AttachmentListDS.Size"
								}
							},
							"AttachmentListDS_CreatedBy": {
								"modelConfig": {
									"path": "AttachmentListDS.CreatedBy"
								}
							},
							"AttachmentListDS_Id": {
								"modelConfig": {
									"path": "AttachmentListDS.Id"
								}
							}
						}
					}
				}
			}
		}
	],
	"modelConfigDiff": [
		{
			"operation": "merge",
			"path": [
				"dataSources"
			],
			"values": {
				"AttachmentListDS": {
					"type": "crt.EntityDataSource",
					"scope": "viewElement",
					"config": {
						"entitySchemaName": "SysFile",
						"attributes": {
							"Name": {
								"path": "Name"
							},
							"CreatedOn": {
								"path": "CreatedOn"
							},
							"Size": {
								"path": "Size"
							},
							"CreatedBy": {
								"path": "CreatedBy"
							}
						}
					}
				}
			}
		}
	]
}