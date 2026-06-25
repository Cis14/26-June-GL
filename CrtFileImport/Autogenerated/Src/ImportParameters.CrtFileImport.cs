namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Threading;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Store;

	#region Class: ImportParameters

	/// <inheritdoc />
	/// <summary>
	/// An instance of this class represents file import parameters.
	/// </summary>
	[DataContract]
	[Serializable]
	public class ImportParameters : EntityData {

		#region Constants: Private

		private const string ImportEntitiesKeySuffix = "ImportEntities";

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportParameters"/>.
		/// </summary>
		public ImportParameters() { }

		/// <summary>
		/// Creates instance of type <see cref="ImportParameters"/>.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		public ImportParameters(Guid importSessionId) => ImportSessionId = importSessionId;

		public ImportParameters(ImportParameters source) {
			RootSchemaUId = source.RootSchemaUId;
			NotImportedRowsCount = source.NotImportedRowsCount;
			ImportedRowsCount = source.ImportedRowsCount;
			ProcessedRowsCount = source.ProcessedRowsCount;
			TotalRowsCount = source.TotalRowsCount;
			ChunkSize = source.ChunkSize;
			ImportSessionId = source.ImportSessionId;
			AuthorId = source.AuthorId;
			AuthorTimeZone = source.AuthorTimeZone;
			Columns = source.Columns;
			ImportObject = source.ImportObject;
			CanUseImportEntitiesStorage = source.CanUseImportEntitiesStorage;
			FileName = source.FileName;
			HeaderColumnsValues = source.HeaderColumnsValues;
			ImportTags = source.ImportTags;
			ImportStage = source.ImportStage;
			ImportCancellationToken = source.ImportCancellationToken;
			LoadOnlyPrimaryColumnValuesInDuplicateSearch = source.LoadOnlyPrimaryColumnValuesInDuplicateSearch;
			HasLookupProcessingErrors = source.HasLookupProcessingErrors;
		}

		#endregion

		#region Fields: Public

		/// <summary>
		/// Flag for send notify.
		/// </summary>
		[DataMember(Name = "NeedSendNotify")]
		public bool NeedSendNotify;

		/// <summary>
		/// Root schema unique identifier.
		/// </summary>
		[DataMember(Name = "rootSchemaUId")]
		public Guid RootSchemaUId;

		/// <summary>
		/// Not imported rows count.
		/// </summary>
		[DataMember(Name = "notImportedRowsCount")]
		public uint NotImportedRowsCount;

		/// <summary>
		/// Imported rows count.
		/// </summary>
		[DataMember(Name = "importedRowsCount")]
		public uint ImportedRowsCount;

		/// <summary>
		/// Processed rows count.
		/// </summary>
		public uint ProcessedRowsCount;

		/// <summary>
		/// Total rows count.
		/// </summary>
		[DataMember(Name = "totalRowsCount")]
		public uint TotalRowsCount;

		/// <summary>
		/// Import chunk size.
		/// </summary>
		[DataMember(Name = "chunkSize")]
		public int ChunkSize;

		/// <summary>
		/// Import session identifier.
		/// </summary>
		public Guid ImportSessionId {
			get => Id;
			set => Id = value;
		}

		[NonSerialized]
		public Guid ChunkId;

		/// <summary>
		/// Id of contact who started import
		/// </summary>.
		[DataMember(Name = "authorId")]
		public Guid AuthorId;

		/// <summary>
		/// Time zone of contact who started import, need for Newtonsoft.Json serialization
		/// </summary>
		[DataMember(Name = "authorTimeZoneInfo")]
		public string AuthorTimeZoneInfo;

		#endregion

		#region Field: Private

		[NonSerialized]
		private IImportParametersRepository _importParametersRepository;

		#endregion

		#region Properties: Protected

		[NonSerialized]
		private ICacheStore _cacheStore;

		/// <summary>
		/// Cache store.
		/// </summary>
		protected ICacheStore CacheStore => _cacheStore ?? (_cacheStore = Store.Cache[CacheLevel.Application]);

		#endregion

		#region Properties: Public

		[NonSerialized]
		private bool _entitiesChanged;

		/// <summary>
		/// Import entities.
		/// </summary>
		[NonSerialized]
		private List<ImportEntity> _entities;

		public List<ImportEntity> Entities {
			get {
				if (_entities != null) {
					return _entities;
				}
				string importEntitiesKey = string.Concat(ImportSessionId, ImportEntitiesKeySuffix);
				_entities = CacheStore.GetValue<List<ImportEntity>>(importEntitiesKey);
				_entitiesChanged = true;
				return _entities;
			}
			set {
				_entities = value;
				_entitiesChanged = true;
			}
		}

		/// <summary>
		/// Import columns.
		/// </summary>
		[DataMember(Name = "columns")]
		public IEnumerable<ImportColumn> Columns { get; set; }

		/// <summary>
		/// Import object.
		/// </summary>
		private ImportObject _importObject;

		[DataMember(Name = "importObject")]
		public ImportObject ImportObject {
			get => _importObject ?? (_importObject = new ImportObject());
			set {
				_importObject = value;
				RootSchemaUId = _importObject == null
					? Guid.Empty
					: _importObject.UId;
			}
		}

		[DataMember(Name = "canUseImportEntitiesStorage")]
		public bool CanUseImportEntitiesStorage { get; set; }

		/// <summary>
		/// File name.
		/// </summary>
		[DataMember(Name = "fileName")]
		public string FileName { get; set; }

		/// <summary>
		/// Header columns values.
		/// </summary>
		[DataMember(Name = "headerColumnsValues")]
		public IEnumerable<ImportColumnValue> HeaderColumnsValues { get; set; }

		/// <summary>
		/// Tag names.
		/// </summary>
		[DataMember(Name = "importTags")]
		public List<ImportTag> ImportTags { get; set; }

		public FileImportStagesEnum ImportStage { get; set; }

		public Guid FileImportProcessId { get; private set; }

		/// <summary>
		/// Time zone of contact who started import
		/// </summary>
		public TimeZoneInfo AuthorTimeZone {
			get {
				return string.IsNullOrEmpty(AuthorTimeZoneInfo) ?
					default(TimeZoneInfo) : JsonConvert.DeserializeObject<TimeZoneInfo>(AuthorTimeZoneInfo);
			}
			set {
				if (value != default(TimeZoneInfo)) {
					AuthorTimeZoneInfo = JsonConvert.SerializeObject(value);
				} else {
					AuthorTimeZoneInfo = null;
				}
			}
		}

		[NonSerialized]
		private CancellationToken _importCancellationToken;

		/// <summary>
		/// Cancel import process
		/// </summary>
		public CancellationToken ImportCancellationToken {
			get => _importCancellationToken;
			set => _importCancellationToken = value;
		}

		public bool LoadOnlyPrimaryColumnValuesInDuplicateSearch { get; set; }

		public bool UseAdminRights { get; set; } = true;

		[DataMember(Name = "hasLookupProcessingErrors")]
		public bool HasLookupProcessingErrors { get; set; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Saves import entities.
		/// </summary>
		private void SaveImportEntities() {
			if (!_entitiesChanged) {
				return;
			}
			string importEntitiesKey = string.Concat(ImportSessionId, ImportEntitiesKeySuffix);
			if (_entities == null) {
				CacheStore.Remove(importEntitiesKey);
				return;
			}
			CacheStore[importEntitiesKey] = _entities;
		}

		private IImportParametersRepository GetImportParametersRepository(UserConnection userConnection) {
			return _importParametersRepository ??
				(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(new ConstructorArgument("userConnection", userConnection)));
		}

		#endregion

		#region Methods: Public

		public bool GetIsImportCancelled(UserConnection userConnection) {
			userConnection.CheckArgumentNull(nameof(userConnection));
			return ImportCancellationToken.IsCancellationRequested ||
			 	GetImportParametersRepository(userConnection).GetIsImportSessionCanceled(ImportSessionId);
		}

		/// <summary>
		/// Saves import parameters.
		/// </summary>
		public void Save() {
			CacheStore[ImportSessionId.ToString()] = this;
			SaveImportEntities();
		}

		/// <summary>
		/// Resets file data.
		/// </summary>
		public void ResetFileData() {
			FileName = null;
			Entities = null;
			Columns = null;
			HeaderColumnsValues = null;
		}

		/// <summary>
		/// Sets entities.
		/// </summary>
		public void SetEntities(List<ImportEntity> entities) {
			Entities = entities;
			TotalRowsCount = (uint)entities.Count;
		}

		/// <summary>
		/// Returns collection of key columns
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ImportColumn> GetKeyColumns() {
			return Columns.Where(column => column.Destinations.Any(destination => destination.IsKey)).ToList();
		}

		/// <summary>
		/// Returns collection of lookup columns.
		/// </summary>
		public IEnumerable<ImportColumn> GetLookupColumns() {
			return Columns.Where(c => c.Destinations.Any(d => d.FindColumnTypeUId() == DataValueType.LookupDataValueTypeUId));
		}

		/// <summary>
		/// Determines whether a import parameters contains any lookup columns.
		/// </summary>
		/// <returns>True if the import parameters contains any lookup columns; otherwise, false.</returns>
		public bool AnyLookupColumns() {
			return GetLookupColumns().Any();
		}

		/// <summary>
		/// Create copy of instance
		/// </summary>
		/// <returns></returns>
		public ImportParameters Clone() => new ImportParameters(this);

		public void SetProcessId(Guid fileImportProcessId) => FileImportProcessId = fileImportProcessId;

		#endregion

	}

	#endregion

	#region Class: ImportTag

	[DataContract]
	[Serializable]
	public class ImportTag {
		#region Fields: Public

		/// <summary>
		/// Tag unique identifier.
		/// </summary>
		[DataMember(Name = "value")]
		public Guid Value;

		/// <summary>
		/// Tag name.
		/// </summary>
		[DataMember(Name = "displayValue")]
		public string DisplayValue;

		/// <summary>
		/// Tag type.
		/// </summary>
		[DataMember(Name = "type")]
		public ImportTagType Type;

		#endregion
	}

	#endregion

	#region Class: ImportTagType

	[DataContract]
	[Serializable]
	public class ImportTagType {
		#region Fields: Public

		/// <summary>
		/// Tag type unique identifier.
		/// </summary>
		[DataMember(Name = "value")]
		public Guid Value;

		/// <summary>
		/// Tag type name.
		/// </summary>
		[DataMember(Name = "displayValue")]
		public string DisplayValue;

		#endregion
	}

	#endregion
}

