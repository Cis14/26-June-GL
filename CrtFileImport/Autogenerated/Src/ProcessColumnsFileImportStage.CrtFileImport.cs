namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: ProcessColumnsFileImportStage

	/// <inheritdoc cref="BaseFileImportStage"/>
	/// <summary>
	/// Execute process columns
	/// </summary>
	[DefaultBinding(typeof(IBaseFileImportStage), Name = nameof(ProcessColumnsFileImportStage))]
	public class ProcessColumnsFileImportStage : BaseFileImportStage
	{

		#region Constructors: Public

		public ProcessColumnsFileImportStage(UserConnection userConnection,
				IColumnsAggregatorAdapter columnsProcessor)
				: base(userConnection, columnsProcessor) { }

		#endregion

		#region Properties: Public

		/// <inheritdoc cref="BaseFileImportStage"/>
		public override FileImportStagesEnum StageId => FileImportStagesEnum.ProcessColumnsFileImportStage;

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFileImportStage"/>
		protected override FileImportStagesEnum? InternalProcess(ImportParameters parameters) {
			ColumnsProcessor.ProcessError += ImportLogger.HandleError;
			ColumnsProcessor.Process(parameters);
			ColumnsProcessor.ProcessError -= ImportLogger.HandleError;
			return FileImportStagesEnum.ProcessEntitiesFileImportStage;
		}

		#endregion

	}

	#endregion

}

