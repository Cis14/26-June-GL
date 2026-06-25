namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Configuration;

	#region Class: PrepareFileImportStage

	/// <inheritdoc cref="BaseFileImportStage"/>
	/// <summary>
	/// Execute prepare file import
	/// </summary>
	[DefaultBinding(typeof(IBaseFileImportStage), Name = nameof(PrepareFileImportStage))]
	public class PrepareFileImportStage : BaseFileImportStage {

		#region Fields: Private

		private bool _uploadLargeFileByChunk;

		#endregion

		#region Properties: Private

		private IImportParametersRepository _importParametersRepository;
		private IImportParametersRepository ImportParametersRepository => _importParametersRepository ??
			(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(new ConstructorArgument("userConnection", UserConnection)));

		private IFileImportFindExistsRowColumnProviderFactory _columnProviderFactory;
		private IFileImportFindExistsRowColumnProviderFactory ColumnProviderFactory =>
			_columnProviderFactory ?? (_columnProviderFactory =
				ClassFactory.Get<IFileImportFindExistsRowColumnProviderFactory>());

		private IFileImportFindExistsRowColumnProvider _columnProvider;
		private IFileImportFindExistsRowColumnProvider ColumnProvider =>
			_columnProvider ?? (_columnProvider = ColumnProviderFactory.GetColumnProvider());

		#endregion

		#region Constructors: Public

		public PrepareFileImportStage(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor)
				: base(userConnection, columnsProcessor) {
			_uploadLargeFileByChunk = UserConnection.GetIsFeatureEnabled("UploadLargeFileByChunk");
		}

		public PrepareFileImportStage(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor,
				IFileImportFindExistsRowColumnProviderFactory columnProviderFactory)
				: this(userConnection, columnsProcessor) {
			_columnProviderFactory = columnProviderFactory;
		}

		#endregion

		#region Properties: Public

		/// <inheritdoc cref="BaseFileImportStage"/>
		public override FileImportStagesEnum StageId => FileImportStagesEnum.PrepareFileImportStage;

		#endregion

		#region Methods: Private

		private List<ImportEntity> GetMergedImportEntities(IEnumerable<ImportEntity> entities,
				IEnumerable<ImportColumn> keyColumns) {
			return entities.GroupBy(importEntity => importEntity.GetRawKey(keyColumns))
				.Select(entityGroup => entityGroup.First()).ToList();
		}

		private string GetMergedImportEntitiesInfoMessage(uint mergedRowsCount) {
			string messageTemplate = new LocalizableString(UserConnection.Workspace.ResourceStorage, "FileImporter",
				"LocalizableStrings.MergedImportEntitiesInfoMessageTemplate.Value");
			return string.Format(messageTemplate, mergedRowsCount);
		}

		private void MergeImportEntities(ImportParameters parameters) {
			if (_uploadLargeFileByChunk) {
				return;
			}
			var keyColumns = GetKeyColumns(parameters).ToList();
			if (keyColumns.Any()) {
				parameters.Entities = GetMergedImportEntities(parameters.Entities, keyColumns);
				var mergedRowsCount = (uint)(parameters.TotalRowsCount - parameters.Entities.Count);
				if (mergedRowsCount > 0) {
					parameters.NotImportedRowsCount += mergedRowsCount;
					var eventArgs = new InfoMessageEventArgs {
						Message = GetMergedImportEntitiesInfoMessage(mergedRowsCount)
					};
					ImportLogger.HandleInfoMessage(this, eventArgs);
				}
			}
		}

		private void ValidateColumnsMapping(ImportParameters parameters) {
			Guid rootSchemaUId = parameters.RootSchemaUId;
			var columns = parameters.Columns;
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(rootSchemaUId);
			var schemaColumns = entitySchema.Columns.Where(column =>
				column.RequirementType.Equals(EntitySchemaColumnRequirementType.ApplicationLevel) &&
				column.UsageType.Equals(EntitySchemaColumnUsageType.General) && !column.HasDefValue &&
				!column.HasConstDefValue);
			var destinations = columns.SelectMany(parameterColumn => parameterColumn.Destinations)
				.Where(destination => destination.SchemaUId.Equals(rootSchemaUId));
			var notFilledColumns = schemaColumns.Where(schemaColumn =>
				!destinations.Any(destination => destination.ColumnName.Equals(schemaColumn.Name)));
			var entitySchemaColumns = notFilledColumns.ToList();
			if (entitySchemaColumns.Any()) {
				throw new RequiredColumnsEmptyValuesException(entitySchemaColumns);
			}
		}

		private void CreateDataChunks(ImportParameters parameters) {
			var chunkDataProvider = ClassFactory.Get<IImportEntitiesChunksDataProvider>(
				new ConstructorArgument("userConnection", UserConnection));
			chunkDataProvider.CreateDataChunks(parameters);
		}

		private void InitializationOfAuxiliaryData(ImportParameters parameters) {
			SetImportTags(parameters);
			SetChunkSize(parameters);
			SetCurrentUser(parameters);
			SaveImportParameters(parameters);
		}

		private void SetImportTags(ImportParameters parameters) {
			if (parameters.ImportTags.IsNullOrEmpty()) {
				parameters.ImportTags = CreateTags();
			}
		}

		private void SetChunkSize(ImportParameters parameters) {
			parameters.ChunkSize = SysSettings.GetValue(UserConnection, "FileImportEntitySaveChunksCount", 500);
		}

		private void SetCurrentUser(ImportParameters parameters) {
			if (parameters.AuthorId == default(Guid)) {
				var currentUser = UserConnection.CurrentUser;
				parameters.AuthorTimeZone = currentUser.TimeZone;
				parameters.AuthorId = currentUser.ContactId;
			}
		}

		private void SaveImportParameters(ImportParameters parameters) {
			ImportParametersRepository.Update(parameters);
		}

		private List<ImportTag> CreateTags() {
			var dataValueType = UserConnection.DataValueTypeManager.GetDataValueTypeByValueType(typeof(DateTime));
			var currentDate = dataValueType.GetColumnDisplayValue(null, UserConnection.CurrentUser.GetCurrentDateTime());
			var tagTemplate = new LocalizableString(UserConnection.Workspace.ResourceStorage, "FileImportTagManager",
				"LocalizableStrings.TagTemplate.Value");
			return new List<ImportTag> {
				new ImportTag {
					DisplayValue = string.Format(tagTemplate, currentDate)
				}
			};
		}

		private bool TryInitDbColumnsNamesForKeyColumns(ImportParameters parameters) {
			try {
				ColumnProvider.InitDbColumnNameForKeyColumns(parameters);
				return true;
			} catch (FileImportExceededAvailableKeyColumnsLimitException columnsLimitException) {
				var eventArgs = new ImportEntitySaveErrorEventArgs {
					Exception = columnsLimitException,
					ImportEntity = new ImportEntity { RowIndex = 0},
					ImportSessionId = parameters.ImportSessionId
				};
				ImportLogger.HandleException(this, eventArgs);
				return false;
			}
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFileImportStage"/>
		protected override FileImportStagesEnum? InternalProcess(ImportParameters parameters) {
			InitializationOfAuxiliaryData(parameters);
			ValidateColumnsMapping(parameters);
			if (!TryInitDbColumnsNamesForKeyColumns(parameters)) {
				return FileImportStagesEnum.CompleteFileImportStage;;
			}
			MergeImportEntities(parameters);
			CreateDataChunks(parameters);
			return FileImportStagesEnum.ProcessColumnsFileImportStage;
		}

		#endregion

	}

	#endregion

}

