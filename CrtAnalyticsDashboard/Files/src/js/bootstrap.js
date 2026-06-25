(function () {
	require.config({
		paths: {
			"AnalyticsDashboard": Terrasoft.getFileContentUrl("CrtAnalyticsDashboard", "src/js/analytics-dashboard/mf/analytics-dashboard.js"),
			"AnalyticsDashboardStyles": Terrasoft.getFileContentUrl("CrtAnalyticsDashboard", "src/js/analytics-dashboard/mf/styles.css")
		},
		shim: {
		}
	});
})();
