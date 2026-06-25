namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Common;

	#region Class: EmptyHeaderException

	/// <summary>
	/// The exception that is thrown when file does not contain a header.
	/// </summary>
	public class EmptyHeaderException : NullOrEmptyException
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="EmptyHeaderException"/>.
		/// </summary>
		/// <param name="storage">Resource storage.</param>
		public EmptyHeaderException(IResourceStorage storage)
			: base(new LocalizableString(storage, "FileImportExceptions", "LocalizableStrings.EmptyHeaderExceptionMessage.Value")) {
		}

		#endregion

	}

	#endregion

	#region Class: EmptyDataException

	/// <summary>
	/// The exception that is thrown when file does not contain data.
	/// </summary>
	public class EmptyDataException : NullOrEmptyException
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="EmptyDataException"/>.
		/// </summary>
		/// <param name="storage">Resource storage.</param>
		public EmptyDataException(IResourceStorage storage)
			: base(new LocalizableString(storage, "FileImportExceptions", "LocalizableStrings.EmptyDataExceptionMessage.Value")) {
		}

		#endregion

	}

	#endregion

}
