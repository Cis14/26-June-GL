namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	#region Class: BeforeImportEntitiesSaveEventArgs

	public class BeforeImportEntitiesSaveEventArgs : InfoMessageEventArgs
	{
		#region Properties: Public

		public Guid RootSchemaUId { get; set; }

		public List<ImportTag> ImportTags { get; set; }

		#endregion
	}

	#endregion

}

