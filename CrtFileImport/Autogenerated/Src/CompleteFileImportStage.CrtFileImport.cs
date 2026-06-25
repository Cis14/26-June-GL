namespace Terrasoft.Configuration.FileImport 
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: CompleteFileImportStage

	/// <inheritdoc cref="BaseFileImportStage"/>
	/// <summary>
	/// Execute complete file import
	/// </summary>
	[DefaultBinding(typeof(IBaseFileImportStage), Name = nameof(CompleteFileImportStage))]
	public class CompleteFileImportStage : BaseFileImportStage
	{
		#region Fields: Private

		private IImportParametersRepository _importParametersRepository;
		private IImportEntitiesChunksDataProvider _importEntitiesChunksDataProvider;
		private IImportLookupChunksDataProvider _importLookupChunksDataProvider;
		private ConstructorArgument UserConnectionConstructorArg => new ConstructorArgument("userConnection", UserConnection);

		#endregion

		#region Properties: Private 

		private IImportParametersRepository ImportParametersRepository => _importParametersRepository ??
				(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(UserConnectionConstructorArg));

		private IImportEntitiesChunksDataProvider ImportEntitiesChunksDataProvider => _importEntitiesChunksDataProvider ??
				(_importEntitiesChunksDataProvider = ClassFactory.Get<IImportEntitiesChunksDataProvider>(UserConnectionConstructorArg));

		private IImportLookupChunksDataProvider ImportLookupChunksDataProvider => _importLookupChunksDataProvider ??
				(_importLookupChunksDataProvider = ClassFactory.Get<IImportLookupChunksDataProvider>(UserConnectionConstructorArg));

		#endregion

		#region Constructors: Public

		public CompleteFileImportStage(UserConnection userConnection,
				IColumnsAggregatorAdapter columnsProcessor)
				: base(userConnection, columnsProcessor) { }

		#endregion

		#region Properties: Public

		/// <inheritdoc cref="BaseFileImportStage"/>
		public override FileImportStagesEnum StageId => FileImportStagesEnum.CompleteFileImportStage;

		#endregion

		#region Methods: Private

		private void CompleteImport(ImportParameters parameters) {
			NotifyOnComplete(parameters);
			DeleteImportOperationalData(parameters);
		}

		private void NotifyOnComplete(ImportParameters parameters) {
			if (!parameters.NeedSendNotify) {
				return;
			}
			var importNotifier = ClassFactory.Get<IImportNotifier>(
					new ConstructorArgument("userConnection", UserConnection),
					new ConstructorArgument("importParameters", parameters));
			importNotifier.NotifyEnd();

		}

		private void DeleteImportOperationalData(ImportParameters parameters) {
			parameters.Entities = null;
			ImportParametersRepository.Update(parameters);
			ImportParametersRepository.DeleteFile(parameters.ImportSessionId);
			ImportEntitiesChunksDataProvider.Delete(parameters.ImportSessionId);
			ImportLookupChunksDataProvider.Delete(parameters.ImportSessionId);
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFileImportStage"/>
		protected override FileImportStagesEnum? InternalProcess(ImportParameters parameters) {
			CompleteImport(parameters);
			return null;
		}

		#endregion
	}

	#endregion

}

