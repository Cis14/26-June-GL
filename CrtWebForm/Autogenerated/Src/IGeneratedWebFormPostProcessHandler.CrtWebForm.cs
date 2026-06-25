namespace Terrasoft.Configuration {
	using System;
	using System.Collections.Generic;
	using Core;
	using GeneratedWebFormService;

	#region Interface: IGeneratedWebFormPostProcessHandler

	/// <summary>
	/// Provides method for post processing of the created entity from landing page.
	/// </summary>
	public interface IGeneratedWebFormPostProcessHandler: IGeneratedWebFormProcessHandler {

		#region Methods: Public

		/// <summary>
		/// Executes the post processing for specified landing page.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="landingId">The landing identifier.</param>
		/// <param name="formData">The form data from POST-request.</param>
		/// <param name="entityId">The entity identifier.</param>
		void Execute(UserConnection userConnection, Guid landingId, FormFieldsData[] formData, Guid entityId);

		#endregion

	}

	#endregion

}

