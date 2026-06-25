 define("SystemDesigner", ["SystemDesignerResources"], function (resources, NetworkUtilities) {
	return {
		methods: {
			/**
			 * Opens Configuration Activity section.
			 * @private
			 */
			_navigateToActivityLog: function () {
				this.sandbox.publish("PushHistoryState", {
					hash: "Section/ConfActivityLog_ListPage"
				});
			},



			/**
			 * @return {Boolean} True if CanUseConfActivityLog is enabled.
			 * @private
			 */
			_isConfAcitivtyLogEnabled: function () {
				return this.getIsFeatureEnabled("UseConfActivityLog");
			}

		},
		diff: [
			{
				"operation": "insert",
				"propertyName": "items",
				"parentName": "SystemSettingsTile",
				"name": "ConfActivityLog",
				"values": {
					"itemType": Terrasoft.ViewItemType.LINK,
					"caption": {"bindTo": "Resources.Strings.ConfActivityLog"},
					"tag": "_navigateToActivityLog",
					"click": {"bindTo": "invokeOperation"},
					"visible": {"bindTo": "_isConfAcitivtyLogEnabled"},
					"isNew": true
				}
			}
		]
	};
});