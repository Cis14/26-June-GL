namespace Terrasoft.Configuration.FileUpload
{
	using System;
	using Terrasoft.Core;

	#region Interface: IFileUploadRightsChecker

	/// <summary>
	/// Provides methods to validate access rights for file upload operations.
	/// </summary>
	public interface IFileUploadRightsChecker
	{
		/// <summary>
		/// Checks whether current user can upload a file identified by <paramref name="fileId"/>.
		/// </summary>
		/// <param name="userConnection">Current user connection.</param>
		/// <param name="fileId">File record identifier.</param>
		/// <returns><c>true</c> if upload can be performed; otherwise <c>false</c>.</returns>
		bool CheckRights(UserConnection userConnection, Guid fileId);
	}

	#endregion
}

