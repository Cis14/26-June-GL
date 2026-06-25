namespace Terrasoft.Configuration.FileImport
{
	using System;
	using global::Common.Logging;
	using Terrasoft.Core.Factories;

	#region Class: FileImportInfoLogger

	[DefaultBinding(typeof(IFileImportInfoLogger))]
	public class FileImportInfoLogger : IFileImportInfoLogger
	{

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger("FileImportAppender"));

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IFileImportInfoLogger.WriteSessionActionStartMessage"/>
		public void WriteSessionActionStartMessage(Guid importSessionId, string message) {
			Logger.Info($"{importSessionId} start: {message}");
		}

		/// <inheritdoc cref="IFileImportInfoLogger.WriteSessionActionMessage"/>
		public void WriteSessionActionMessage(Guid importSessionId, string message) {
			Logger.Info($"{importSessionId} {message}");
		}

		/// <inheritdoc cref="IFileImportInfoLogger.WriteActionMessage"/>
		public void WriteActionMessage(string message) {
			Logger.Info(message);
		}

		/// <inheritdoc cref="IFileImportInfoLogger.WriteSessionActionEndMessage"/>
		public void WriteSessionActionEndMessage(Guid importSessionId, string message) {
			Logger.Info($"{importSessionId} end: {message}");
		}

		#endregion

	}

	#endregion

}

