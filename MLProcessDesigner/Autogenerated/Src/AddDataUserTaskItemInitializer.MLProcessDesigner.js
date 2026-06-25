define("AddDataUserTaskItemInitializer", ["BaseProcessFlowElementSchemaItemInitializer"],
	function () {
		/**
		 * @class Terrasoft.configuration.ReadDataUserTaskItemInitializer
		 */
		Terrasoft.AddDataUserTaskItemInitializer = class AddDataUserTaskItemInitializer extends Terrasoft.BaseProcessFlowElementSchemaItemInitializer {

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