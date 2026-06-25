(function() {
	const config = {
		paths: {
			"SchemaViewComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/schema-view-component/mf/schema-view.js"),
			"SchemaViewComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/schema-view-component/mf/styles.css")
		},
		shim: {
		}
	};
	if (Terrasoft.isAngularHost) {
		define("StructureExplorerComponent",[],()=>{});
		define("StructureExplorerComponentStyles",[],()=>{});
	} else {
		define("StructureExplorerComponent", ["mf!StructureExplorerComponentMF", "css-ltr!StructureExplorerComponentStyles"], function(module) {
			return module;
		});
		config.paths = {
			...config.paths,
			"StructureExplorerComponentMF": Terrasoft.getFileContentUrl("CrtNUI", "src/js/structure-explorer-component/structure-explorer.js"),
			"StructureExplorerComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/structure-explorer-component/styles.css"),
			"ErrorListDialogComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/mf/error-list-dialog.js"),
			"ErrorListDialogComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/mf/styles.css"),
		}
	}
	if (Terrasoft.isAngularHost) {
		config.paths = {
			...config.paths,
			"ErrorListDialogComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/mf/error-list-dialog.js"),
			"ErrorListDialogComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/mf/styles.css"),
		}
	}
	require.config(config);
})();
