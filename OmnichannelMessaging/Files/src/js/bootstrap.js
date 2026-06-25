(function() {
	require.config({
		paths: {
			"OmnichannelMessagingComponent": Terrasoft.getFileContentUrl("OmnichannelMessaging", "src/js/omnichannel-messaging-component/mf/omnichannel-messaging-component.js"),
			"OmnichannelMessagingComponentStyles": Terrasoft.getFileContentUrl("OmnichannelMessaging", "src/js/omnichannel-messaging-component/mf/styles.css"),
		},
		shim: {
		}
	});
})();