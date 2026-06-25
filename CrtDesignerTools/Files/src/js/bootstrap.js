(function() {
	require.config({
		paths: {
			"page-wizard-component-mf": Terrasoft.getFileContentUrl("CrtDesignerTools", "src/js/page-wizard-component/page-wizard.js"),
			"page-wizard-component-styles": Terrasoft.getFileContentUrl("CrtDesignerTools", "src/js/page-wizard-component/styles.css"),
		},
		shim: {
			"page-wizard-component-mf": {
				deps: ["css-ltr!page-wizard-component-styles"]
			}
		}
	});
	define("page-wizard-component", ["mf!page-wizard-component-mf"], function(module) {
		return module;
	});
}());
