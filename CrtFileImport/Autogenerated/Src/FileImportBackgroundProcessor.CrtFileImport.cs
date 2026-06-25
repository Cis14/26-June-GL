namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Process;
	using global::Common.Logging;

	#region  Class: FileImportBackGroundProcessor

	public class FileImportBackgroundProcessor : IJobExecutor {

		#region Field: Private

		private readonly Guid _fileImportPersistentTask = Guid.Parse("2C488A31-9F43-43E5-AC28-0A3A3EA2489C");
		private static readonly ILog _logger = LogManager.GetLogger("FileImportAppender");

		#endregion

		#region Methods: Private

		private static void WriteRestartingImportErrorLog(KeyValuePair<Guid, ImportParameters> processImportParameter, Exception e) {
			_logger.Error($"An error occured while restarting import with ImportSessionId {processImportParameter.Value.ImportSessionId}", e);
		}

		private void RestartUncompletedImport(UserConnection userConnection, ImportParameters importParameter) {
			var processEngine = userConnection.ProcessEngine;
			var processId = importParameter.FileImportProcessId;
			Process process = processEngine.FindProcessByUId(processId.ToString(), true);
			if (process.Status == ProcessStatus.Error || process.Status == ProcessStatus.Inactive) {
				return;
			}
			var element = process.FindFlowElementBySchemaElementUId(_fileImportPersistentTask) as ProcessActivity;
			if (element?.Status != ProcessStatus.Running ) {
				return;
			}
			WriteRerunLog(importParameter);
			processEngine.CompleteExecuting(element.UId, importParameter);
		}

		private void WriteRerunLog(ImportParameters importParameter) {
			var message = FileImportLogMessageExtensions.CreateFileImportLogMessage(FileImportLogMessageType.PlanedReRunFileImport, importParameter);
			_logger.Info(message);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IJobExecutor"/>
		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			var repository = new ImportParametersRepository(userConnection);
			var processesImportParameters = repository.GetWithProcessIncomplete();
			foreach (var processImportParameter in processesImportParameters) {
				try {
					RestartUncompletedImport(userConnection, processImportParameter.Value);
				} catch (Exception e) {
					WriteRestartingImportErrorLog(processImportParameter, e);
				}
			}
		}

		#endregion

	}

	#endregion

}

