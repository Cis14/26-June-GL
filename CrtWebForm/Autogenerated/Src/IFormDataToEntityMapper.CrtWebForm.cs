namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using Terrasoft.Core.Entities;
	
	/// <summary>
	/// Transforms form data to entity. 
	/// </summary>
	public interface IFormDataToEntityMapper
	{
		/// <summary>
		/// Flag that indicates whether mapping performed successfully.
		/// </summary>
		bool Success {
			get;
		}

		/// <summary>
		/// Message that is occured during mapping.
		/// </summary>
		string Message {
			get; 
		}

		/// <summary>
		/// Creates entity from landing.
		/// </summary>
		/// <param name="formData">Data from landing.</param>
		/// /// <param name="entity">Entity onto which data from landing is mapped.</param>
		void Map(FormData formData, Entity entity);
	}
}

