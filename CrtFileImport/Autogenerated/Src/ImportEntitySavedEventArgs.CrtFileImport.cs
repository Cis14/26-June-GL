namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core.Entities;

	#region Class: ImportEntitySavedEventArgs

	public class ImportEntitySavedEventArgs : InfoMessageEventArgs
	{

		#region Properties: Public

		public Guid ImportSessionId { get; set; }

		public Guid RootSchemaUId { get; set; }

		public Entity PrimaryEntity { get; set; }
		
		public Guid ChunkId { get; set; }

		public uint RowIndex { get; set; }

		#endregion

	}

	#endregion

}

