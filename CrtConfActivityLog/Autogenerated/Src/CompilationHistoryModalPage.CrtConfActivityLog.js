define("CompilationHistoryModalPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "MainHeader",
				"values": {
					"wrap": "wrap"
				}
			},
			{
				"operation": "merge",
				"name": "PageTitle",
				"values": {
					"labelType": "headline-3",
					"visible": true
				}
			},
			{
				"operation": "remove",
				"name": "ActionButtonsContainer"
			},
			{
				"operation": "remove",
				"name": "ContinueInOtherPageButton"
			},
			{
				"operation": "merge",
				"name": "CloseButton",
				"values": {
					"color": "primary",
					"caption": "#ResourceString(CloseButton_caption)#",
					"iconPosition": "only-text",
					"size": "large",
					"disabled": false
				}
			},
			{
				"operation": "remove",
				"name": "CloseButton",
				"properties": [
					"visible",
					"icon"
				]
			},
			{
				"operation": "move",
				"name": "CloseButton",
				"parentName": "FooterContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "merge",
				"name": "MainContainer",
				"values": {
					"alignItems": "stretch"
				}
			},
			{
				"operation": "remove",
				"name": "CancelButton"
			},
			{
				"operation": "remove",
				"name": "SaveButton"
			},
			{
				"operation": "insert",
				"name": "ProjectNameInput",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.CompilationHistoryDS_ProjectName_i3gfpo0",
					"control": "$CompilationHistoryDS_ProjectName_i3gfpo0",
					"placeholder": "",
					"tooltip": "",
					"readonly": true,
					"multiline": false,
					"labelPosition": "above",
					"visible": true
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "StartedByComboBox",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 2,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.CompilationHistoryDS_StartedBy_d6vpia3",
					"ariaLabel": "",
					"isAddAllowed": true,
					"showValueAsLink": true,
					"labelPosition": "above",
					"controlActions": [],
					"listActions": [],
					"tooltip": "",
					"control": "$CompilationHistoryDS_StartedBy_d6vpia3",
					"visible": true,
					"readonly": true,
					"placeholder": ""
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "addRecord_6yjif3x",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_6yjif3x_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "StartedByComboBox",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "CreatedOnDateTimePicker",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 3,
						"rowSpan": 1
					},
					"type": "crt.DateTimePicker",
					"label": "$Resources.Strings.CompilationHistoryDS_CreatedOn_n4bhvuw",
					"placeholder": "",
					"readonly": false,
					"labelPosition": "above",
					"tooltip": "",
					"pickerType": "datetime",
					"control": "$CompilationHistoryDS_CreatedOn_n4bhvuw"
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "DurationSecondsNumberInput",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 4,
						"rowSpan": 1
					},
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.CompilationHistoryDS_DurationInSeconds_zxgzbeu",
					"control": "$CompilationHistoryDS_DurationInSeconds_zxgzbeu",
					"readonly": true,
					"placeholder": "",
					"labelPosition": "above",
					"tooltip": "",
					"visible": true
				},
				"parentName": "MainContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ErrorsWarningsTextArea",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 1,
						"row": 5,
						"rowSpan": 2
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.CompilationHistoryDS_ErrorsWarnings_4aagot1",
					"control": "$CompilationHistoryDS_ErrorsWarnings_4aagot1",
					"placeholder": "",
					"tooltip": "",
					"readonly": false,
					"multiline": true,
					"labelPosition": "above",
					"visible": true
				},
				"parentName": "MainContainer",
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
					"CompilationHistoryDS_ProjectName_i3gfpo0": {
						"modelConfig": {
							"path": "CompilationHistoryDS.ProjectName"
						}
					},
					"CompilationHistoryDS_CreatedOn_n4bhvuw": {
						"modelConfig": {
							"path": "CompilationHistoryDS.CreatedOn"
						}
					},
					"CompilationHistoryDS_DurationInSeconds_zxgzbeu": {
						"modelConfig": {
							"path": "CompilationHistoryDS.DurationInSeconds"
						}
					},
					"CompilationHistoryDS_ErrorsWarnings_4aagot1": {
						"modelConfig": {
							"path": "CompilationHistoryDS.ErrorsWarnings"
						}
					},
					"CompilationHistoryDS_StartedBy_d6vpia3": {
						"modelConfig": {
							"path": "CompilationHistoryDS.StartedBy"
						}
					},
					"CompilationHistoryDS_StartedBy_d6vpia3_List": {
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
					"dataSources": {
						"CompilationHistoryDS": {
							"type": "crt.EntityDataSource",
							"scope": "page",
							"config": {
								"entitySchemaName": "CompilationHistory",
								"loadParameters": {
									"options": {
										"pagingConfig": {
											"rowCount": 1,
											"rowsOffset": -1
										},
										"sortingConfig": {
											"columns": []
										}
									}
								},
								"allowCopyingRecords": false
							}
						}
					},
					"primaryDataSourceName": "CompilationHistoryDS"
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});