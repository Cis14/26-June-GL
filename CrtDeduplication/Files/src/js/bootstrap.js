(function() {
    require.config({
        paths: {
            "DuplicatesWidgetComponent": Terrasoft.getFileContentUrl("CrtDeduplication", "src/js/duplicates-widget/mf/duplicates-widget.js"),
            "DuplicatesWidgetComponentStyles": Terrasoft.getFileContentUrl("CrtDeduplication", "src/js/duplicates-widget/mf/styles.css"),
        },
        shim: {
        }
    });
})();
