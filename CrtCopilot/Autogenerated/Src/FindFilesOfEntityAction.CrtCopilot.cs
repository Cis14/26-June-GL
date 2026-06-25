namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Common.Logging;
	using Creatio.Copilot.Actions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: FindFilesOfEntityAction

	/// <summary>
	/// Represents an action that finds files associated with given entity.
	/// </summary>
	public class FindFilesOfEntityAction : BaseExecutableCodeAction, IUserConnectionRequired
	{

		#region Constants: Private

		private const string SysFileSchemaName = "SysFile";
		private const string SysFileEntityIdColumnName = "RecordId";
		private const string SysFileRecordSchemaNameColumnName = "RecordSchemaName";

		#endregion

		#region Fields: Private

		private UserConnection _userConnection;

		#endregion

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger(nameof(Copilot)));

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create a new instance of the <see cref="FindFilesOfEntityAction"/> class.
		/// </summary>
		public FindFilesOfEntityAction() {
			Parameters = new List<SourceCodeActionParameter> {
				new SourceCodeActionParameter {
					Name = "schemaName",
					Caption = new LocalizableString("Entity schema name"),
					Description = new LocalizableString(
						"Schema of the entity for which files would be searched. " +
						"Should not contain 'Files' suffix or prefix in its name"),
					IsRequired = true,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId
				},
				new SourceCodeActionParameter {
					Name = "entityId",
					Caption = new LocalizableString("Entity identifier"),
					Description = new LocalizableString("Entity identifier (GUID) to search files for."),
					IsRequired = true,
					DataValueTypeUId = DataValueType.GuidDataValueTypeUId
				}
			};
		}

		#endregion

		#region Methods: Private

		private static string ResolveFileSchemaName(string schemaName) {
			return string.Equals(schemaName, "Lead",
				StringComparison.InvariantCultureIgnoreCase) ? "FileLead" : schemaName + "File";
		}

		private static (string schemaName, Guid entityIdGuid) GetParameters(Dictionary<string, string> parameterValues,
				out string failureReason) {
			failureReason = null;
			if (parameterValues == null || !parameterValues.TryGetValue("schemaName", out string schemaName) ||
				string.IsNullOrWhiteSpace(schemaName)) {
				failureReason = "SchemaName is required.";
				return (null, Guid.Empty);
			}
			if (!parameterValues.TryGetValue("entityId", out string entityId) || string.IsNullOrWhiteSpace(entityId)) {
				failureReason = "EntityId is required.";
				return (null, Guid.Empty);
			}
			if (!Guid.TryParse(entityId, out Guid entityIdGuid)) {
				failureReason = "EntityId has invalid entity identifier format.";
				return (null, Guid.Empty);
			}
			return (schemaName, entityIdGuid);
		}

		private static void FillDocuments(EntityCollection files, string fileSchemaName,
				List<CreatioAIDocument> documents) {
			foreach (Entity file in files) {
				Guid id = file.GetTypedColumnValue<Guid>("Id");
				string fileName = file.GetTypedColumnValue<string>("Name");
				var document = new CreatioAIDocument {
					FileName = fileName,
					FileId = id,
					FileSchemaName = fileSchemaName
				};
				documents.Add(document);
			}
		}

		private EntitySchemaQuery GetBaseQuery(string fileSchemaName, string entityIdColumnName,
				Guid entityIdColumnValue) {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, fileSchemaName) {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				},
				UseAdminRights = true
			};
			esq.AddColumn("Name");
			esq.AddColumn(entityIdColumnName);
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				entityIdColumnName, entityIdColumnValue));
			return esq;
		}

		private EntityCollection GetFiles(Guid entityId, string schemaName, string fileSchemaName) {
			EntitySchemaQuery fileSchemaQuery = GetBaseQuery(fileSchemaName, schemaName, entityId);
			EntityCollection files = fileSchemaQuery.GetEntityCollection(_userConnection);
			return files;
		}

		private EntityCollection GetSysFiles(Guid entityId, string schemaName) {
			EntitySchemaQuery sysFileSchemaQuery = GetBaseQuery(SysFileSchemaName, SysFileEntityIdColumnName, entityId);
			sysFileSchemaQuery.Filters.Add(sysFileSchemaQuery.CreateFilterWithParameters(FilterComparisonType.Equal,
				SysFileRecordSchemaNameColumnName, schemaName));
			EntityCollection sysFiles = sysFileSchemaQuery.GetEntityCollection(_userConnection);
			return sysFiles;
		}

		private List<CreatioAIDocument> GetDocumentsByEntityId(Guid entityId, string schemaName) {
			string fileSchemaName = ResolveFileSchemaName(schemaName);
			EntityCollection files = null;
			EntityCollection sysFiles = null;
			try {
				files = GetFiles(entityId, schemaName, fileSchemaName);
				sysFiles = GetSysFiles(entityId, schemaName);
			} catch (Exception e) {
				Logger.Error($"Failed to retrieve files for entity {entityId} with schema name: " + schemaName, e);
			}
			List<CreatioAIDocument> documents = new List<CreatioAIDocument>(files?.Count ?? 0 + sysFiles?.Count ?? 0);
			if (files?.Count > 0) {
				FillDocuments(files, fileSchemaName, documents);
			}
			if (sysFiles?.Count > 0) {
				FillDocuments(sysFiles, SysFileSchemaName, documents);
			}
			return documents;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public override LocalizableString GetCaption() {
			return new LocalizableString("Find files of entity");
		}

		/// <inheritdoc/>
		public override LocalizableString GetDescription() {
			return new LocalizableString("Finds files/documents of the specified entity.");
		}

		/// <inheritdoc/>
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
			(string schemaName, Guid entityIdGuid) = GetParameters(options?.ParameterValues, out string failReason);
			if (!string.IsNullOrWhiteSpace(failReason)) {
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Failed,
					ErrorMessage = failReason
				};;
			}
			List<CreatioAIDocument> documents = GetDocumentsByEntityId(entityIdGuid, schemaName);
			if (documents.Count == 0) {
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Completed,
					Response = "Entity has no files."
				};
			}
			var entityFilesResponseBuilder = new StringBuilder();
			entityFilesResponseBuilder.AppendLine($"Following documents were found for the {schemaName} with " +
				$"Id {entityIdGuid}:");
			foreach (CreatioAIDocument document in documents) {
				entityFilesResponseBuilder.Append(Environment.NewLine + $"Id: {document.FileId}; " +
					$"Name: {document.FileName}; FileSchema: {document.FileSchemaName}");
			}
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = entityFilesResponseBuilder.ToString()
			};
		}

		/// <inheritdoc />
		public void SetUserConnection(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

	}

	#endregion

}

