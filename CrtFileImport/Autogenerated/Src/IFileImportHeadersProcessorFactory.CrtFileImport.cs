namespace Terrasoft.Configuration.FileImport {
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	public interface IFileImportHeadersProcessorFactory {

		#region Methods: Public

		/// <summary>
		/// Create header processor <see cref="IFileImportHeadersCreator"/> for source schema 
		/// </summary>
		/// <param name="userConnection">User connection<see cref="UserConnection"/></param>
		/// <param name="rootSchema">Source schema<see cref="EntitySchema"/></param>
		/// <returns>Instance of <see cref="IFileImportHeadersCreator"/></returns>
		IFileImportHeadersCreator GetProcessor(UserConnection userConnection, EntitySchema rootSchema);

		#endregion

	}
}

