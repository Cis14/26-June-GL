namespace Terrasoft.Configuration
{
	using Terrasoft.Configuration.GeneratedWebFormService;

	#region Interface: IWebFormHandler

	/// <summary>
	/// Provides interface 
	/// </summary>
	public interface IWebFormHandler: IWebFormResultInfo
	{
		#region Methods: Public

		/// <summary>
		/// Handles the form data.
		/// </summary>
		/// <param name="formData">The form data.</param>
		void HandleForm(FormData formData);

		#endregion

	}

	#endregion

}

