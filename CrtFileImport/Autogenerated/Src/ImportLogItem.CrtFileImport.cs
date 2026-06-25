namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region Class: ImportLogItem

	/// <summary>
	/// An instance of this class is responsible for storing import log information.
	/// </summary>
	public class ImportLogItem : IImportLogItem
	{

		#region Constructor: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportLogItem"/>.
		/// </summary>
		public ImportLogItem() {
			StackTrace = string.Empty;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Log message.
		/// </summary>
		public string Message {
			get;
			set;
		}

		/// <summary>
		/// Exception stack trace.
		/// </summary>
		public string StackTrace {
			get;
			set;
		}

		#endregion

	}

	#endregion
}

