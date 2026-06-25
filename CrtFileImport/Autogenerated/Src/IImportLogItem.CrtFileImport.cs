namespace Terrasoft.Configuration.FileImport
{

	#region Interface: IImportLogItem

	public interface IImportLogItem
	{

		#region Properties: Public

		/// <summary>
		/// Log message.
		/// </summary>
		string Message {
			get;
			set;
		}

		/// <summary>
		/// Exception stack trace.
		/// </summary>
		string StackTrace {
			get;
			set;
		}

		#endregion

	}

	#endregion
}

