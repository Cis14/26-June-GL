 namespace Terrasoft.Configuration.FileLoad
{
	using System;

	#region Class: FileMismatchException

	/// <summary>
	/// Exception that is thrown when a file is not fully uploaded.
	/// </summary>
	public class FileMismatchException : Exception
	{

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="FileMismatchException"/> class.
		/// </summary>
		/// <param name="name">The name of the file that is not fully uploaded.</param>
		public FileMismatchException(string name)
			: base($"File \"{name}\" is not fully uploaded") {
		}

		#endregion

	}

	#endregion

}

