define("AISkillsDashboards", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "remove",
				"name": "ActionButtonsContainer"
			},
			{
				"operation": "remove",
				"name": "AddButton"
			},
			{
				"operation": "remove",
				"name": "DataImportButton"
			},
			{
				"operation": "remove",
				"name": "MenuItem_ImportFromExcel"
			},
			{
				"operation": "remove",
				"name": "OpenLandingDesigner"
			},
			{
				"operation": "remove",
				"name": "ActionButton"
			},
			{
				"operation": "remove",
				"name": "MenuItem_ExportToExcel"
			},
			{
				"operation": "merge",
				"name": "MainFilterContainer",
				"values": {
					"visible": true,
					"alignItems": "stretch"
				}
			},
			{
				"operation": "remove",
				"name": "LeftFilterContainer"
			},
			{
				"operation": "remove",
				"name": "LeftFilterContainerInner"
			},
			{
				"operation": "remove",
				"name": "FolderTreeActions"
			},
			{
				"operation": "remove",
				"name": "LookupQuickFilterByTag"
			},
			{
				"operation": "remove",
				"name": "SearchFilter"
			},
			{
				"operation": "remove",
				"name": "RightFilterContainer"
			},
			{
				"operation": "remove",
				"name": "DataTable_Summaries"
			},
			{
				"operation": "remove",
				"name": "RefreshButton"
			},
			{
				"operation": "remove",
				"name": "FolderTree"
			},
			{
				"operation": "merge",
				"name": "SectionContentWrapper",
				"values": {
					"visible": true,
					"borderRadius": "none",
					"padding": {
						"top": "none",
						"right": "none",
						"bottom": "none",
						"left": "medium"
					},
					"alignItems": "stretch",
					"justifyContent": "start",
					"gap": "small",
					"wrap": "nowrap"
				}
			},
			{
				"operation": "remove",
				"name": "DataTable"
			},
			{
				"operation": "insert",
				"name": "GridContainerDashboars",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "none",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"visible": "$IsTokensFeatureEnabled",
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
				"parentName": "SectionContentWrapper",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetTokensAvailable",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 4,
						"rowSpan": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetTokensAvailable_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidgetTokensAvailable_Data",
								"schemaName": "CopilotClientQuota",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "CopilotClientQuota"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "TokensAvailable"
											},
											"functionType": 2,
											"aggregationType": 5,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetTokensAvailable_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "violet",
							"icon": {
								"iconName": "diagram-icon"
							}
						},
						"theme": "full-fill"
					},
					"visible": true
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetAverageTokensIntent",
				"values": {
					"layoutConfig": {
						"column": 5,
						"row": 1,
						"colSpan": 4,
						"rowSpan": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetAverageTokensIntent_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidgetAverageTokensIntent_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"fc7ad2df-79bf-431f-baa0-3275a1877dd3": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "TotalTokens"
											},
											"functionType": 2,
											"aggregationType": 3,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetAverageTokensIntent_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "navy-blue",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetIntentsLaunchedLastMonth",
				"values": {
					"layoutConfig": {
						"column": 9,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetIntentsLaunchedLastMonth_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidgetIntentsLaunchedLastMonth_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"55f3cadb-c13b-407c-8253-8ff2b7a7b42b": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											},
											"55cf6d83-477f-454f-98a4-fa2a6e4b759c": {
												"filterType": 1,
												"comparisonType": 3,
												"isEnabled": true,
												"trimDateTimeParameterToDate": true,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "CopilotSession.StartDate"
												},
												"isAggregative": false,
												"dataValueType": 7,
												"rightExpression": {
													"expressionType": 1,
													"functionType": 1,
													"macrosType": 9
												}
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "Id"
											},
											"functionType": 2,
											"aggregationType": 1,
											"aggregationEvalType": 2
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetIntentsLaunchedLastMonth_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "dark-green",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetIntentsLaunchedThisMonth",
				"values": {
					"layoutConfig": {
						"column": 11,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetIntentsLaunchedThisMonth_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_9pvixsw_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"db442ec4-c3fa-4b88-ba2e-b75f0e45fbeb": {
												"filterType": 1,
												"comparisonType": 3,
												"isEnabled": true,
												"trimDateTimeParameterToDate": true,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "CopilotSession.CreatedOn"
												},
												"isAggregative": false,
												"dataValueType": 7,
												"rightExpression": {
													"expressionType": 1,
													"functionType": 1,
													"macrosType": 10
												}
											},
											"fec215bc-c122-47d1-a661-9498b46d3122": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "Id"
											},
											"functionType": 2,
											"aggregationType": 1,
											"aggregationEvalType": 2
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetIntentsLaunchedThisMonth_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "dark-green",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ChartWidgetTokensSpentMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 12,
						"rowSpan": 14
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetTokensSpentMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "bar",
								"label": "#ResourceString(ChartWidgetTokensSpentMonth_series_0)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidgetTokensSpentMonth_SeriesData_0",
										"schemaName": "CopilotRequestEnt",
										"filters": {
											"filter": {
												"items": {
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "CopilotRequestEnt"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalTokens"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-date-part",
											"column": [
												{
													"orderDirection": 0,
													"orderPosition": -1,
													"isVisible": true,
													"expression": {
														"expressionType": 1,
														"functionArgument": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"functionType": 3,
														"datePartType": 3
													}
												},
												{
													"orderDirection": 0,
													"orderPosition": -1,
													"isVisible": true,
													"expression": {
														"expressionType": 1,
														"functionArgument": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"functionType": 3,
														"datePartType": 4
													}
												}
											]
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "ChartWidgetTokensSpentIntentsCurrentMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 6,
						"rowSpan": 20,
						"row": 16
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetTokensSpentIntentsCurrentMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "horizontal-bar",
								"label": "#ResourceString(ChartWidgetTokensSpentIntentsCurrentMonth_series_0)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_xbouiyr_SeriesData_0",
										"schemaName": "VwCopilotSessionIntent",
										"filters": {
											"filter": {
												"items": {
													"986cdad5-df79-4357-ab4b-645596b11c76": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CopilotSession.StartDate"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "Intent"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotSessionIntent"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalTokens"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Intent"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "ChartWidgetIntentsLaunchesCurrentMonth",
				"values": {
					"layoutConfig": {
						"column": 7,
						"colSpan": 6,
						"rowSpan": 20,
						"row": 16
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetIntentsLaunchesCurrentMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "horizontal-bar",
								"label": "#ResourceString(ChartWidgetIntentsLaunchesCurrentMonth_series_0)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_rwi3fa8_SeriesData_0",
										"schemaName": "VwCopilotSessionIntent",
										"filters": {
											"filter": {
												"items": {
													"986cdad5-df79-4357-ab4b-645596b11c76": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CopilotSession.StartDate"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "Intent"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotSessionIntent"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "Id"
													},
													"functionType": 2,
													"aggregationType": 1,
													"aggregationEvalType": 2
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Intent"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "ChartWidgetTokenSpentUserMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 36,
						"colSpan": 12,
						"rowSpan": 20
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetTokenSpentUserMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "bar",
								"label": "#ResourceString(ChartWidgetTokenSpentUserMonth_series_0)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidgetTokenSpentUserMonth_SeriesData_0",
										"schemaName": "VwCopilotRequest",
										"filters": {
											"filter": {
												"items": {
													"f8787b91-09f4-4bca-80be-e01bd082d3b9": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedBy"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotRequest"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalTokens"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "CreatedBy"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainerDashboars",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "GridContainer_WithActions",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "none",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"visible": "$IsActionsFeatureEnabled",
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
				"parentName": "SectionContentWrapper",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetTokensAvailableWithActions",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 4,
						"rowSpan": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetTokensAvailableWithActions_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_cr6mdn5_Data",
								"schemaName": "CopilotClientQuota",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "CopilotClientQuota"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "ActionsAvailable"
											},
											"functionType": 2,
											"aggregationType": 5,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetTokensAvailableWithActions_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "violet",
							"icon": {
								"iconName": "diagram-icon"
							}
						},
						"theme": "full-fill"
					},
					"visible": true
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetAverageActionsIntent",
				"values": {
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetAverageActionsIntent_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_c49xe7g_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"fc7ad2df-79bf-431f-baa0-3275a1877dd3": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "TotalActions"
											},
											"functionType": 2,
											"aggregationType": 3,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetAverageActionsIntent_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "navy-blue",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill",
						"hideTitle": false
					},
					"visible": true,
					"layoutConfig": {
						"column": 5,
						"colSpan": 4,
						"row": 1,
						"rowSpan": 1
					}
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetIntentsLaunchedLastMonthWithActions",
				"values": {
					"layoutConfig": {
						"column": 9,
						"row": 1,
						"colSpan": 2,
						"rowSpan": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetIntentsLaunchedLastMonthWithActions_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_1y2019p_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"55f3cadb-c13b-407c-8253-8ff2b7a7b42b": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											},
											"55cf6d83-477f-454f-98a4-fa2a6e4b759c": {
												"filterType": 1,
												"comparisonType": 3,
												"isEnabled": true,
												"trimDateTimeParameterToDate": true,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "CopilotSession.StartDate"
												},
												"isAggregative": false,
												"dataValueType": 7,
												"rightExpression": {
													"expressionType": 1,
													"functionType": 1,
													"macrosType": 9
												}
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "Id"
											},
											"functionType": 2,
											"aggregationType": 1,
											"aggregationEvalType": 2
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetIntentsLaunchedLastMonthWithActions_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "dark-green",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "IndicatorWidgetIntentsLaunchedThisMonthWithActions",
				"values": {
					"layoutConfig": {
						"column": 11,
						"colSpan": 2,
						"rowSpan": 1,
						"row": 1
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(IndicatorWidgetIntentsLaunchedThisMonthWithActions_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_5s1hwmv_Data",
								"schemaName": "VwCopilotSessionIntent",
								"filters": {
									"filter": {
										"items": {
											"db442ec4-c3fa-4b88-ba2e-b75f0e45fbeb": {
												"filterType": 1,
												"comparisonType": 3,
												"isEnabled": true,
												"trimDateTimeParameterToDate": true,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "CopilotSession.CreatedOn"
												},
												"isAggregative": false,
												"dataValueType": 7,
												"rightExpression": {
													"expressionType": 1,
													"functionType": 1,
													"macrosType": 10
												}
											},
											"fec215bc-c122-47d1-a661-9498b46d3122": {
												"filterType": 2,
												"comparisonType": 2,
												"isEnabled": true,
												"trimDateTimeParameterToDate": false,
												"leftExpression": {
													"expressionType": 0,
													"columnPath": "Intent"
												},
												"isAggregative": false,
												"dataValueType": 10,
												"referenceSchemaName": "VwSysSchemaInfo",
												"isNull": false
											}
										},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "VwCopilotSessionIntent"
									},
									"filterAttributes": []
								},
								"aggregation": {
									"column": {
										"orderDirection": 0,
										"orderPosition": -1,
										"isVisible": true,
										"expression": {
											"expressionType": 1,
											"functionArgument": {
												"expressionType": 0,
												"columnPath": "Id"
											},
											"functionType": 2,
											"aggregationType": 1,
											"aggregationEvalType": 2
										}
									}
								},
								"dependencies": []
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(IndicatorWidgetIntentsLaunchedThisMonthWithActions_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "large",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "dark-green",
							"border": {
								"hidden": true
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ChartWidgetActionsSpentMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 12,
						"rowSpan": 14
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetActionsSpentMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "bar",
								"label": "#ResourceString(ChartWidgetActionsSpentMonth_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_nxn9jcv_SeriesData_u6qppa2",
										"schemaName": "CopilotRequestEnt",
										"filters": {
											"filter": {
												"items": {
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "CopilotRequestEnt"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalActions"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-date-part",
											"column": [
												{
													"orderDirection": 0,
													"orderPosition": -1,
													"isVisible": true,
													"expression": {
														"expressionType": 1,
														"functionArgument": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"functionType": 3,
														"datePartType": 3
													}
												},
												{
													"orderDirection": 0,
													"orderPosition": -1,
													"isVisible": true,
													"expression": {
														"expressionType": 1,
														"functionArgument": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"functionType": 3,
														"datePartType": 4
													}
												}
											]
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "ChartWidgetActionsSpentIntentsCurrentMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 6,
						"rowSpan": 20,
						"row": 16
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetActionsSpentIntentsCurrentMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "horizontal-bar",
								"label": "#ResourceString(ChartWidgetActionsSpentIntentsCurrentMonth_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_d8cwa2v_SeriesData_fyfloe3",
										"schemaName": "VwCopilotSessionIntent",
										"filters": {
											"filter": {
												"items": {
													"986cdad5-df79-4357-ab4b-645596b11c76": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CopilotSession.StartDate"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "Intent"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotSessionIntent"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalActions"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Intent"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "ChartWidgetIntentsLaunchesCurrentMonthWithActions",
				"values": {
					"layoutConfig": {
						"column": 7,
						"colSpan": 6,
						"rowSpan": 20,
						"row": 16
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetIntentsLaunchesCurrentMonthWithActions_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "horizontal-bar",
								"label": "#ResourceString(ChartWidgetIntentsLaunchesCurrentMonthWithActions_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_ihnmv5g_SeriesData_xzaj07t",
										"schemaName": "VwCopilotSessionIntent",
										"filters": {
											"filter": {
												"items": {
													"986cdad5-df79-4357-ab4b-645596b11c76": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CopilotSession.StartDate"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "Intent"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotSessionIntent"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "Id"
													},
													"functionType": 2,
													"aggregationType": 1,
													"aggregationEvalType": 2
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Intent"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "ChartWidgetActionsSpentUserMonth",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 36,
						"colSpan": 12,
						"rowSpan": 20
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(ChartWidgetActionsSpentUserMonth_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "",
								"formatting": {
									"type": "number",
									"thousandAbbreviation": {
										"enabled": true
									}
								}
							}
						},
						"series": [
							{
								"color": "dark-green",
								"type": "bar",
								"label": "#ResourceString(ChartWidgetActionsSpentUserMonth_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_mnfld9b_SeriesData_1ybypsv",
										"schemaName": "VwCopilotRequest",
										"filters": {
											"filter": {
												"items": {
													"f8787b91-09f4-4bca-80be-e01bd082d3b9": {
														"filterType": 1,
														"comparisonType": 3,
														"isEnabled": true,
														"trimDateTimeParameterToDate": true,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedOn"
														},
														"isAggregative": false,
														"dataValueType": 7,
														"rightExpression": {
															"expressionType": 1,
															"functionType": 1,
															"macrosType": 10
														}
													},
													"columnIsNotNullFilter": {
														"comparisonType": 2,
														"filterType": 2,
														"isEnabled": true,
														"isNull": false,
														"trimDateTimeParameterToDate": false,
														"leftExpression": {
															"expressionType": 0,
															"columnPath": "CreatedBy"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "VwCopilotRequest"
											},
											"filterAttributes": []
										},
										"aggregation": {
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 1,
													"functionArgument": {
														"expressionType": 0,
														"columnPath": "TotalActions"
													},
													"functionType": 2,
													"aggregationType": 2,
													"aggregationEvalType": 0
												}
											}
										},
										"dependencies": [],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "CreatedBy"
												}
											}
										}
									},
									"formatting": {
										"type": "number",
										"decimalSeparator": ".",
										"decimalPrecision": 0,
										"thousandSeparator": ","
									}
								},
								"dataLabel": {
									"display": true
								}
							}
						],
						"seriesOrder": {
							"type": "by-aggregation-value",
							"direction": 2,
							"seriesIndex": 0
						},
						"layout": {
							"border": {
								"hidden": true
							}
						}
					},
					"visible": true,
					"sectionBindingColumnRecordId": "$Id"
				},
				"parentName": "GridContainer_WithActions",
				"propertyName": "items",
				"index": 7
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"IsActionsFeatureEnabled": {
						"value": false
					},
					"IsTokensFeatureEnabled": {
						"value": true
					}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "remove",
				"path": [
					"dataSources"
				],
				"properties": [
					"PDS"
				]
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[
			{
				request: "crt.HandleViewModelInitRequest",
				handler: async (request, next) => {
					await next?.handle(request);
					const isFeatureEnabled = Terrasoft?.Features?.getIsEnabled?.("GenAIFeatures.UseActionsForBilling");
					if (isFeatureEnabled === true) {
						// When feature is enabled: show GridContainer_WithActions, hide GridContainerDashboars
						request.$context.IsActionsFeatureEnabled = true;
						request.$context.IsTokensFeatureEnabled = false;
					} else {
						// When feature is disabled: show GridContainerDashboars, hide GridContainer_WithActions
						request.$context.IsActionsFeatureEnabled = false;
						request.$context.IsTokensFeatureEnabled = true;
					}
				}
			}
		]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});