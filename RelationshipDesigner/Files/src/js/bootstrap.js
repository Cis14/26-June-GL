(function() {
	require.config({
		paths: {
			"RelationshipDiagramComponent": Terrasoft.getFileContentUrl("RelationshipDesigner", "src/js/relationship-diagram-component/mf/relationship-diagram-component.js"),
			"RelationshipDiagramComponentStyles": Terrasoft.getFileContentUrl("RelationshipDesigner", "src/js/relationship-diagram-component/mf/styles.css"),
		},
		shim: {
		}
	});
}());
