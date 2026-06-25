namespace Terrasoft.Configuration.FileImport
{

	#region  Interface: IBaseFileImportStage

	public interface IBaseFileImportStage
	{
		#region Properties: Public

		/// <summary>
		/// Stage id
		/// </summary>
		FileImportStagesEnum StageId { get; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Execute process stage in derived classes
		/// </summary>
		/// <param name="parameters">
		/// <see cref="ImportParameters"/>
		/// </param>
		IBaseFileImportStage ProcessStage(ImportParameters parameters);

		#endregion
	}

	#endregion

}

