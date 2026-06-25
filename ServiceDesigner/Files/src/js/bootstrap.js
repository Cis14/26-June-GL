(function() {
	require.config({
		paths: {
			"web-service-proxy-component-mf": Terrasoft.getFileContentUrl("ServiceDesigner", "src/js/web-service-proxy-component/web-service.js"),
			"web-service-proxy-component-styles": Terrasoft.getFileContentUrl("ServiceDesigner", "src/js/web-service-proxy-component/styles.css"),
		},
		shim: {
			"web-service-proxy-component-mf": {
				deps: ["css-ltr!web-service-proxy-component-styles"]
			}
		}
	});
	define("web-service-proxy-component", ["mf!web-service-proxy-component-mf", "css-ltr!web-service-proxy-component-styles"], function(module) {
		return module;
	});
}());
