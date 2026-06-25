namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: BaseFileImportStage

	/// <inheritdoc cref="IBaseFileImportStage"/>
	public abstract class BaseFileImportStage : IBaseFileImportStage
	{

		#region  Fields: Private

		private IImportStageFactory _importStageFactory;

		#endregion

		#region Constructors: Protected

		protected BaseFileImportStage(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) {
			UserConnection = userConnection;
			ColumnsProcessor = columnsProcessor;
		}

		#endregion

		#region Properties: Private

		private IImportStageFactory ImportStageFactory =>
				_importStageFactory ?? (_importStageFactory = ClassFactory.Get<IImportStageFactory>());

		#endregion

		#region Properties: Protected

		protected UserConnection UserConnection { get; }

		protected IImportLogger ImportLogger { get; private set; }

		protected IColumnsAggregatorAdapter ColumnsProcessor { get; }

		#endregion

		#region Properties: Public

		/// <inheritdoc cref="IBaseFileImportStage"/>
		public abstract FileImportStagesEnum StageId { get; }

		#endregion

		#region Methods: Private

		private void CreateImportLogger(ImportParameters parameters) {
			var userConnectionArg = new ConstructorArgument("userConnection", UserConnection);
			var importParametersArg = new ConstructorArgument("importParameters", parameters);
			ImportLogger = ClassFactory.Get<IImportLogger>(userConnectionArg, importParametersArg);
		}

		private IBaseFileImportStage CreateNextStage(FileImportStagesEnum nextStage) =>
				ImportStageFactory.CreateImportStage(nextStage, UserConnection, ColumnsProcessor);

		#endregion

		#region Methods: Protected

		protected IEnumerable<ImportColumn> GetKeyColumns(ImportParameters parameters) => parameters.GetKeyColumns();

		protected abstract FileImportStagesEnum? InternalProcess(ImportParameters parameters);

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IBaseFileImportStage"/>
		public IBaseFileImportStage ProcessStage(ImportParameters parameters) {
			CreateImportLogger(parameters);
			var nextStageId = InternalProcess(parameters);
			ImportLogger.SaveLog();
			return nextStageId.HasValue ? CreateNextStage(nextStageId.Value) : null;
		}

		#endregion

	}

	#endregion

}

