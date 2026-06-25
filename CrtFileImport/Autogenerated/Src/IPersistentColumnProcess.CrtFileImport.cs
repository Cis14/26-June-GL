namespace Terrasoft.Configuration.FileImport
{		
	public interface IPersistentColumnProcess
	{
		#region Methods: Public

		/// <summary>
		/// Execute values processing. 
		/// </summary>
		/// <param name="importParameters"> <see cref="ImportParameters"/></param>
		void Process(ImportParameters importParameters);

		#endregion
	}
}
