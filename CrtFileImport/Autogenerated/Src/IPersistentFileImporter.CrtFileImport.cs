namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.Threading;

	#region Interface: IPersistentFileImporter

		public interface IPersistentFileImporter : IBaseFileImporter {

			#region Methods

			/// <summary>
			/// Delete file from import parameters.
			/// </summary>
			/// <param name="fileImportSessionId">Import session id.</param>
			void DeleteFile(Guid fileImportSessionId);

			/// <summary>
			/// Validate uploaded file. 
			/// </summary>
			/// <param name="fileImportSessionId">Import session id.</param>
			/// <returns></returns>
			void CheckIsFileValid(Guid fileImportSessionId);
			
			/// <summary>
			/// Execute import by parameters.
			/// </summary>
			/// <param name="sessionId">Id session.</param>
			/// <param name="token">Token for cancel import.</param>
			void Import(Guid sessionId, CancellationToken token);
			
			#endregion
		}

		#endregion
	
}

