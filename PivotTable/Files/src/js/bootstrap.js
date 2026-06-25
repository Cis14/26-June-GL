(function() {
    require.config({
        paths: {
            "PivotTableComponent": Terrasoft.getFileContentUrl("PivotTable", "src/js/pivot-table-component/mf/pivot-table.js"),
            "PivotTableComponentStyles": Terrasoft.getFileContentUrl("PivotTable", "src/js/pivot-table-component/mf/styles.css"),
        },
        shim: {
        }
    });
})();
