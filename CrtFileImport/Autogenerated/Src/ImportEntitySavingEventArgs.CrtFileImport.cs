namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region Class: ImportEntitySavingEventArgs

	public class ImportEntitySavingEventArgs : InfoMessageEventArgs
	{

		#region Properties: Public

		public uint TotalRowsCount {
			get;
			set;
		}

		public uint ProcessedRowsCount {
			get;
			set;
		}

		#endregion

	}

	#endregion

}

