{
	"viewConfigDiff": [
		{
			"operation": "insert",
			"name": "Scaffold",
			"values": {
				"type": "crt.Scaffold",
				"title": "$PageTitle",
				"leading": [
					{
						"type": "crt.Button",
						"clicked": {
							"request": "crt.ClosePageRequest"
						},
						"icon": "back-button-icon",
						"name": "CloseButton"
					}
				],
				"actions": [],
				"items": []
			},
			"index": 0
		},
		{
			"operation": "insert",
			"name": "MainContainer",
			"values": {
				"type": "crt.GridContainer",
				"items": [],
				"gap": "small",
				"stretch": true
			},
			"parentName": "Scaffold",
			"propertyName": "items",
			"index": 0
		}
	],
	"viewModelConfigDiff": [
		{
			"operation": "merge",
			"path": [],
			"values": {
				"attributes": {
					"PageTitle": {
						"value": "#ResourceString(DefaultPageTitle)#"
					}
				}
			}
		}
	],
	"modelConfigDiff": []
}