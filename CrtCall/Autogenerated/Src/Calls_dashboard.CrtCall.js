define("Calls_dashboard", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "insert",
				"name": "CallDurationWidget",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 3,
						"row": 1,
						"rowSpan": 3
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(CallDurationWidget_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_sgxzfsd_Data",
								"schemaName": "Call",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "Call"
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
												"columnPath": "Duration"
											},
											"functionType": 2,
											"aggregationType": 5,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": [
									{
										"attributePath": "Id",
										"relationPath": "DashboardDS.Id"
									}
								]
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(CallDurationWidget_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "medium",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "steel-blue",
							"icon": {
								"iconName": "phone-icon"
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "AverageCallDurationWidget",
				"values": {
					"layoutConfig": {
						"column": 4,
						"colSpan": 3,
						"row": 1,
						"rowSpan": 3
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(AverageCallDurationWidget_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_6lgrhnr_Data",
								"schemaName": "Call",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "Call"
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
												"columnPath": "TalkTime"
											},
											"functionType": 2,
											"aggregationType": 3,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": [
									{
										"attributePath": "Id",
										"relationPath": "DashboardDS.Id"
									}
								]
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(AverageCallDurationWidget_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "medium",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "dark-turquoise",
							"icon": {
								"iconName": "diagram-icon"
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "CallConversationTimeWidget",
				"values": {
					"layoutConfig": {
						"column": 7,
						"colSpan": 3,
						"row": 1,
						"rowSpan": 3
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(CallConversationTimeWidget_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_hc4a6m3_Data",
								"schemaName": "Call",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "Call"
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
												"columnPath": "TalkTime"
											},
											"functionType": 2,
											"aggregationType": 5,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": [
									{
										"attributePath": "Id",
										"relationPath": "DashboardDS.Id"
									}
								]
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(CallConversationTimeWidget_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "medium",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "navy-blue",
							"icon": {
								"iconName": "contact-call-center-icon"
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "CallOnHoldTimeWidget",
				"values": {
					"layoutConfig": {
						"column": 10,
						"colSpan": 3,
						"row": 1,
						"rowSpan": 3
					},
					"type": "crt.IndicatorWidget",
					"config": {
						"title": "#ResourceString(CallOnHoldTimeWidget_title)#",
						"data": {
							"providing": {
								"attribute": "IndicatorWidget_ye64m4a_Data",
								"schemaName": "Call",
								"filters": {
									"filter": {
										"items": {},
										"logicalOperation": 0,
										"isEnabled": true,
										"filterType": 6,
										"rootSchemaName": "Call"
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
												"columnPath": "HoldTime"
											},
											"functionType": 2,
											"aggregationType": 5,
											"aggregationEvalType": 0
										}
									}
								},
								"dependencies": [
									{
										"attributePath": "Id",
										"relationPath": "DashboardDS.Id"
									}
								]
							},
							"formatting": {
								"type": "number",
								"decimalSeparator": ".",
								"decimalPrecision": 0,
								"thousandSeparator": ","
							}
						},
						"text": {
							"template": "#ResourceString(CallOnHoldTimeWidget_template)#",
							"metricMacros": "{0}",
							"fontSizeMode": "medium",
							"labelPosition": "above-under"
						},
						"layout": {
							"color": "blue",
							"icon": {
								"iconName": "hourglass-icon"
							}
						},
						"theme": "without-fill"
					},
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "CallsResultChart",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 4,
						"row": 4,
						"rowSpan": 8
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(CallsResultChart_title)#",
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
								"type": "doughnut",
								"label": "#ResourceString(CallsResultChart_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_v4jt504_SeriesData_vrgjo1h",
										"schemaName": "Call",
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
															"columnPath": "Result"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "Call"
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
										"dependencies": [
											{
												"attributePath": "Id",
												"relationPath": "DashboardDS.Id"
											}
										],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Result"
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
									"display": false
								}
							}
						],
						"seriesOrder": {
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {}
					},
					"sectionBindingColumnRecordId": "$Id",
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "CallsVolumeTrendChart",
				"values": {
					"layoutConfig": {
						"column": 5,
						"colSpan": 8,
						"row": 4,
						"rowSpan": 8
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(CallsVolumeTrendChart_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "#ResourceString(CallsVolumeTrendChart_xAxis)#",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "#ResourceString(CallsVolumeTrendChart_yAxis)#",
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
								"color": "dark-turquoise",
								"type": "line",
								"label": "#ResourceString(CallsVolumeTrendChart_series_0)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_q0yfja5_SeriesData_c3t8n61",
										"schemaName": "Call",
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
															"columnPath": "StartDate"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "Call"
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
										"dependencies": [
											{
												"attributePath": "Id",
												"relationPath": "DashboardDS.Id"
											}
										],
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
															"columnPath": "StartDate"
														},
														"functionType": 3,
														"datePartType": 1
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
															"columnPath": "StartDate"
														},
														"functionType": 3,
														"datePartType": 3
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
									"display": null
								}
							}
						],
						"seriesOrder": {
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {}
					},
					"sectionBindingColumnRecordId": "$Id",
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "CallsPerContactChart",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 12,
						"row": 12,
						"rowSpan": 9
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(CallsPerContactChart_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "#ResourceString(CallsPerContactChart_name)#",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "#ResourceString(CallsPerContactChart_name)#",
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
								"color": "navy-blue",
								"type": "bar",
								"label": "#ResourceString(CallsPerContactChart_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_pnde6q6_SeriesData_mdbw7dn",
										"schemaName": "Call",
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
															"columnPath": "Contact"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "Call"
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
										"dependencies": [
											{
												"attributePath": "Id",
												"relationPath": "DashboardDS.Id"
											}
										],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Contact"
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
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {}
					},
					"sectionBindingColumnRecordId": "$Id",
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "CallsPerAccountChart",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 12,
						"rowSpan": 9,
						"row": 21
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(CallsPerAccountChart_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "#ResourceString(CallsPerAccountChart_name)#",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "#ResourceString(CallsPerAccountChart_name)#",
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
								"color": "dark-turquoise",
								"type": "bar",
								"label": "#ResourceString(CallsPerAccountChart_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_13pwpzz_SeriesData_1sliz51",
										"schemaName": "Call",
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
															"columnPath": "Account"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "Call"
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
										"dependencies": [
											{
												"attributePath": "Id",
												"relationPath": "DashboardDS.Id"
											}
										],
										"rowCount": 50,
										"grouping": {
											"type": "by-value",
											"column": {
												"orderDirection": 0,
												"orderPosition": -1,
												"isVisible": true,
												"expression": {
													"expressionType": 0,
													"columnPath": "Account"
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
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {}
					},
					"sectionBindingColumnRecordId": "$Id",
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "CallsPerAgentChart",
				"values": {
					"layoutConfig": {
						"column": 1,
						"colSpan": 12,
						"rowSpan": 9,
						"row": 30
					},
					"type": "crt.ChartWidget",
					"config": {
						"title": "#ResourceString(CallsPerAgentChart_title)#",
						"color": "dark-blue",
						"theme": "without-fill",
						"scales": {
							"stacked": false,
							"xAxis": {
								"name": "#ResourceString(CallsPerAgentChart_name)#",
								"formatting": {
									"type": "string",
									"maxLinesCount": 2,
									"maxLineLength": 10
								}
							},
							"yAxis": {
								"name": "#ResourceString(CallsPerAgentChart_name)#",
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
								"color": "violet",
								"type": "bar",
								"label": "#ResourceString(CallsPerAgentChart_label)#",
								"legend": {
									"enabled": false
								},
								"data": {
									"providing": {
										"attribute": "ChartWidget_4gu3c4s_SeriesData_frrvunp",
										"schemaName": "Call",
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
															"columnPath": "CreatedBy"
														}
													}
												},
												"logicalOperation": 0,
												"isEnabled": true,
												"filterType": 6,
												"rootSchemaName": "Call"
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
										"dependencies": [
											{
												"attributePath": "Id",
												"relationPath": "DashboardDS.Id"
											}
										],
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
							"type": "by-grouping-value",
							"direction": 1
						},
						"layout": {}
					},
					"sectionBindingColumnRecordId": "$Id",
					"visible": true
				},
				"parentName": "Main",
				"propertyName": "items",
				"index": 8
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"attributes": {}
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"loadingConfig": {},
					"dataSources": {}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});