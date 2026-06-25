define("LinkEntityToProcessUserTaskItemInitializer", ["BaseProcessFlowElementSchemaItemInitializer"],
	function () {
		/**
		 * @class Terrasoft.configuration.ReadDataUserTaskItemInitializer
		 */
		Terrasoft.LinkEntityToProcessUserTaskItemInitializer = class LinkEntityToProcessUserTaskItemInitializer extends Terrasoft.BaseProcessFlowElementSchemaItemInitializer {

			//region Methods: Protected

			/**
			 * @inheritdoc Terrasoft.BaseProcessFlowElementSchemaItemInitializer#getElementEntityParameterName
			 * @override
			 */
			getElementEntityParameterName() {
				return 'EntitySchemaId';
			}

			//endregion
		}
	});