define("SystemDesigner", ["NavigationHelper", "SystemDesignerResources"], function () {
	return {
		methods: {
			/**
			 * Opens Creatio.ai skills section.
			 * @private
			 */
			_navigateToCopilotIntents: function () {
				this.sandbox.publish("PushHistoryState", {
					hash: "Section/AISkills_ListPage"
				});
			},

			/**
			 * Opens Creatio.ai agent section.
			 * @private
			 */
			_navigateToCopilotAgents: function () {
				this.sandbox.publish("PushHistoryState", {
					hash: "Section/AIAgents_ListPage"
				});
			},

			/**
			 * @return {Boolean} True if Copilot intent is enabled.
			 * @private
			 */
			_isGenAICopilotEnabled: function () {
				return this.getIsFeatureEnabled("GenAIFeatures.Copilot");
			},

			/**
			 * @return {Boolean} True if Copilot agent is enabled.
			 * @private
			 */
			_isGenAIAgentEnabled: function () {
				return true;
			},

			/**
			 * Opens Agent Builder in a new browser tab.
			 * @public
			 */
			navigateToAgentBuilder: function () {
				Terrasoft.SysSettings.querySysSettingsItem("AgentBuilderSiteUrl", (url) => {
					const navigationHelper = this.Ext.create("Terrasoft.NavigationHelper", {
						Ext: this.Ext,
						sandbox: this.sandbox
					});
					navigationHelper.navigateTo({
						target: "Url",
						options: {
							newTab: true,
							url
						}
					});
				}, this);
			},

			/**
			 * Checks whether Agent Builder link is available in System Designer.
			 * @return {Boolean} True when the Agent Builder link should be shown.
			 * @public
			 */
			isAgentBuilderEnabled: function () {
				return this.getIsFeatureEnabled("GenAIFeatures.ShowAgentBuilderLink");
			},

		},
		diff: [
			{
				"operation": "insert",
				"propertyName": "items",
				"parentName": "SystemSettingsTile",
				"name": "AISkills",
				"values": {
					"itemType": Terrasoft.ViewItemType.LINK,
					"caption": {"bindTo": "Resources.Strings.CopilotIntentsPage"},
					"tag": "_navigateToCopilotIntents",
					"click": {"bindTo": "invokeOperation"},
					"visible": {"bindTo": "_isGenAICopilotEnabled"},
					"isNew": true
				}
			},
			{
				"operation": "insert",
				"propertyName": "items",
				"parentName": "SystemSettingsTile",
				"name": "AIAgents",
				"values": {
					"itemType": Terrasoft.ViewItemType.LINK,
					"caption": {"bindTo": "Resources.Strings.CopilotAgentsPage"},
					"tag": "_navigateToCopilotAgents",
					"click": {"bindTo": "invokeOperation"},
					"visible": {"bindTo": "_isGenAIAgentEnabled"},
					"isNew": true
				}
			},
			{
				"operation": "insert",
				"propertyName": "items",
				"parentName": "SystemSettingsTile",
				"name": "AgentBuilder",
				"values": {
					"itemType": Terrasoft.ViewItemType.LINK,
					"caption": {"bindTo": "Resources.Strings.AgentBuilderCaption"},
					"tag": "navigateToAgentBuilder",
					"click": {"bindTo": "invokeOperation"},
					"visible": {"bindTo": "isAgentBuilderEnabled"},
					"isNew": true
				}
			}
		]
	};
});