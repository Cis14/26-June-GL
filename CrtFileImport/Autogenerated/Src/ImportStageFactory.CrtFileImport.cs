namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: ImportStageFactory

	/// <inheritdoc cref="IImportStageFactory"/>
	/// <summary>
	/// Implements interface <see cref="IImportStageFactory"/>
	/// </summary>
	[DefaultBinding(typeof(IImportStageFactory), Name = nameof(ImportStageFactory))]
	public class ImportStageFactory : IImportStageFactory
	{

		#region Fields: Private

		private readonly Dictionary<FileImportStagesEnum, string> _stages;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create instance of <see cref="ImportStageFactory"/>
		/// </summary>
		public ImportStageFactory() => _stages = new Dictionary<FileImportStagesEnum, string> {
				{ FileImportStagesEnum.PrepareFileImportStage, nameof(PrepareFileImportStage) },
				{ FileImportStagesEnum.ProcessColumnsFileImportStage, nameof(ProcessColumnsFileImportStage) },
				{ FileImportStagesEnum.ProcessEntitiesFileImportStage, nameof(ProcessEntitiesFileImportStage)},
				{ FileImportStagesEnum.CompleteFileImportStage, nameof(CompleteFileImportStage) }
		};

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IImportStageFactory"/>
		public IBaseFileImportStage CreateImportStage(FileImportStagesEnum importStage, UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) {
			if (!_stages.ContainsKey(importStage)) {
				throw new KeyNotFoundException(importStage.ToString());
			}
			var stageTypeName = _stages[importStage];
			var userConnectionArg = new ConstructorArgument("userConnection", userConnection);
			var columnsProcessorArg = new ConstructorArgument("columnsProcessor", columnsProcessor);
			return ClassFactory.Get<IBaseFileImportStage>(stageTypeName, userConnectionArg, columnsProcessorArg);
		}

		#endregion
	}

	#endregion

}

