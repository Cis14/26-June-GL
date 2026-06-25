namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;

	/// <summary>
	/// Represents interface for create file import's stages
	/// </summary>
	public interface IImportStageFactory
	{
		/// <summary>
		/// Create import stage by id
		/// </summary>
		/// <param name="importStage"></param>
		/// <param name="userConnection"></param>
		/// <param name="columnsProcessor"></param>
		/// <returns><see cref="BaseFileImportStage"/></returns>
		IBaseFileImportStage CreateImportStage(FileImportStagesEnum importStage, UserConnection userConnection,
				IColumnsAggregatorAdapter columnsProcessor);
	}
}
