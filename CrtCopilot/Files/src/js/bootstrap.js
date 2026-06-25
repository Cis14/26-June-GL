(function() {
	const useV2 = Terrasoft.Features.getIsEnabled("UseExecuteIntentUserTaskPropertiesPageV2");

	if (useV2) {
		require.config({
			map: {
				'*': {
					ExecuteIntentUserTaskPropertiesPage: 'ExecuteIntentUserTaskPropertiesPageV2',
				},
			},
		});
	}
})();
