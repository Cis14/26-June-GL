define("ReadDataUserTaskItemInitializer", ["BaseProcessFlowElementSchemaItemInitializer"],
	function () {
		/**
		 * @class Terrasoft.configuration.ReadDataUserTaskItemInitializer
		 */
		Terrasoft.ReadDataUserTaskItemInitializer = class ReadDataUserTaskItemInitializer extends Terrasoft.BaseProcessFlowElementSchemaItemInitializer {

			//region Methods: Protected

			/**
			 * @inheritdoc Terrasoft.BaseProcessFlowElementSchemaItemInitializer#getElementEntityParameterName
			 * @override
			 */
			getElementEntityParameterName() {
				return 'ResultEntity';
			}

			/**
			 * @inheritdoc Terrasoft.BaseProcessFlowElementSchemaItemInitializer#getElementEntityParameterValue
			 * @override
			 */
			getElementEntityParameterValue(defaultParamValues) {
				const referenceSchemaUId = defaultParamValues["referenceSchemaUId"];
				return {
					source: Terrasoft.ProcessSchemaParameterValueSource.ConstValue,
					referenceSchemaUId: referenceSchemaUId
				};
			}

			//endregion
		}
	});