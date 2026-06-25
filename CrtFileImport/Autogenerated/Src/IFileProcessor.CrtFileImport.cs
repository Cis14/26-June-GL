namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Web.Http.Abstractions;

	#region Interface: IFileProcessor

	public interface IFileProcessor
	{

		#region Methods: Public

		/// <summary>
		/// Processes given file.
		/// </summary>
		/// <param name="file">File.</param>
		void ProcessFile(HttpPostedFile file);

		/// <summary>
		/// Processes given object, considering it contains data of entity schema
		/// with unique identifier <paramref name="rootSchemaUId"/>.
		/// </summary>
		/// <param name="rootSchemaUId">Root schema UId.</param>
		void ProcessObject(Guid rootSchemaUId);

		#endregion

	}

	#endregion

}

