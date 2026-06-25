define("DeleteDataUserTaskItemInitializer", ["BaseProcessFlowElementSchemaItemInitializer"],
	function () {
		/**
		 * @class Terrasoft.configuration.ReadDataUserTaskItemInitializer
		 */
		Terrasoft.DeleteDataUserTaskItemInitializer = class DeleteDataUserTaskItemInitializer extends Terrasoft.BaseProcessFlowElementSchemaItemInitializer {

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