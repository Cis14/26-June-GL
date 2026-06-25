define("ChangeDataUserTaskItemInitializer", ["BaseProcessFlowElementSchemaItemInitializer"],
	function () {
		/**
		 * @class Terrasoft.configuration.ReadDataUserTaskItemInitializer
		 */
		Terrasoft.ChangeDataUserTaskItemInitializer = class ChangeDataUserTaskItemInitializer extends Terrasoft.BaseProcessFlowElementSchemaItemInitializer {

			//region Methods: Protected

			/**
			 * @inheritdoc Terrasoft.BaseProcessFlowElementSchemaItemInitializer#getElementEntityParameterName
			 * @override
			 */
			getElementEntityParameterName() {
				return 'EntitySchemaUId';
			}

			//endregion
		}
	});