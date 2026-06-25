namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	#region Enum: FileImportLogMessageType

	public enum FileImportLogMessageType : byte {
		PlanedStartFileImportJob,
		StartFileImport,
		StartStage,
		CancelFileImport,
		EndStage,
		CompleteFileImport,
		InsertParametersError,
		PlanedReRunFileImport,
		StartReRunFileImport
	}

	#endregion

	#region Class: FileImportLogMessageExtensions

	/// <summary>
	/// Class contains methods for create log's messages for FileImport
	/// </summary>
	public static class FileImportLogMessageExtensions {
		private static readonly Dictionary<FileImportLogMessageType, Func<Guid, ImportParameters, string>>
			_messageCreators;

		#region Constructors: Private

		static FileImportLogMessageExtensions() =>
			_messageCreators = new Dictionary<FileImportLogMessageType, Func<Guid, ImportParameters, string>> {
				{ FileImportLogMessageType.StartStage, CreateStartStageLogMessage },
				{ FileImportLogMessageType.StartFileImport, CreateStartFileImportLogMessage },
				{ FileImportLogMessageType.CancelFileImport, CreateCancelLogMessage },
				{ FileImportLogMessageType.EndStage, CreateEndStageLogMessage },
				{ FileImportLogMessageType.InsertParametersError, CreateInsertParametersErrorLogMessage },
				{ FileImportLogMessageType.CompleteFileImport, CreateCompleteFileImportLogMessage },
				{ FileImportLogMessageType.PlanedStartFileImportJob, CreatePlanedStartFileImportLogMessage },
				{ FileImportLogMessageType.PlanedReRunFileImport, CreatePlanedReRunLogMessage },
				{ FileImportLogMessageType.StartReRunFileImport, CreateStartReRunImportInfoLogMessage }
			};

		#endregion

		#region Methods: Private

		private static void AddInfoFromImportParameters(ImportParameters parameters, StringBuilder sb) {
			if (parameters == null) {
				return;
			}
			sb.Append($"Parameters: Object - {parameters.ImportObject.UId}");
			sb.Append($"File name - {parameters.FileName}, ");
			int entitiesCount = parameters.Entities?.Count ?? 0;
			sb.Append($"rows count - {entitiesCount} ");
			sb.Append($"from stage - {parameters.ImportStage}");
		}

		private static string CreateCancelLogMessage(Guid importSessionId, ImportParameters parameters) =>
			$"{importSessionId} was canceled";

		private static string CreateCompleteFileImportLogMessage(Guid importSessionId, ImportParameters parameters) =>
			$"{importSessionId} complete";

		private static string CreateEndStageLogMessage(Guid importSessionId, ImportParameters parameters) =>
			parameters == null
				? string.Empty
				: CreateStageExecutionLogMessage(importSessionId, parameters.ImportStage, "complete");

		private static string CreateInsertParametersErrorLogMessage(Guid importSessionId,
			ImportParameters importParameters) {
			var sb = new StringBuilder(5);
			sb.AppendLine($"Error - {importSessionId}");
			AddInfoFromImportParameters(importParameters, sb);
			return sb.ToString();
		}

		private static string CreateMessage(FileImportLogMessageType messageType, Guid importSessionId,
			ImportParameters parameters) {
			if (!_messageCreators.TryGetValue(messageType, out var createMessageFn)) {
				return string.Empty;
			}
			return createMessageFn.Invoke(importSessionId, parameters) ?? string.Empty;
		}

		private static string CreatePlanedReRunLogMessage(Guid importSessionId, ImportParameters parameters) {
			var sb = new StringBuilder(5);
			Guid processId = parameters.FileImportProcessId;
			sb.Append($"Restart process {processId} with {importSessionId}. ");
			AddInfoFromImportParameters(parameters, sb);
			return sb.ToString();
		}

		private static string
			CreatePlanedStartFileImportLogMessage(Guid importSessionId, ImportParameters parameters) =>
			$"{importSessionId} complete";

		private static string CreateStageExecutionLogMessage(Guid importSessionId,
			FileImportStagesEnum currentImportStage, string stageAction) {
			string stageName = currentImportStage.ToString();
			return$"{importSessionId} {stageAction} stage {stageName}";
		}

		private static string CreateStartFileImportLogMessage(Guid importSessionId, ImportParameters parameters) {
			var sb = new StringBuilder(5);
			sb.Append($"Started import with {importSessionId}");
			AddInfoFromImportParameters(parameters, sb);
			return sb.ToString();
		}

		private static string CreateStartReRunImportInfoLogMessage(Guid importSessionId, ImportParameters parameters) {
			var sb = new StringBuilder(5);
			sb.Append($"Restarted import with {importSessionId}");
			AddInfoFromImportParameters(parameters, sb);
			return sb.ToString();
		}

		private static string CreateStartStageLogMessage(Guid importSessionId, ImportParameters parameters) =>
			parameters == null
				? string.Empty
				: CreateStageExecutionLogMessage(importSessionId, parameters.ImportStage, "begin");

		#endregion

		#region Methods: Public

		/// <summary>
		/// Create message from ImportSessionId
		/// </summary>
		/// <param name="messageType"><see cref="FileImportLogMessageType"/></param>
		/// <param name="importSessionId">Import session id</param>
		/// <returns></returns>
		public static string CreateFileImportLogMessage(FileImportLogMessageType messageType, Guid importSessionId) =>
			CreateMessage(messageType, importSessionId, null);

		/// <summary>
		/// Create message from <see cref="ImportParameters"/>
		/// </summary>
		/// <param name="messageType"><see cref="FileImportLogMessageType"/></param>
		/// <param name="parameters"><see cref="ImportParameters"/></param>
		/// <returns></returns>
		public static string CreateFileImportLogMessage(FileImportLogMessageType messageType,
			ImportParameters parameters) =>
			CreateMessage(messageType, parameters.ImportSessionId, parameters);

		#endregion

	}

	#endregion

}

