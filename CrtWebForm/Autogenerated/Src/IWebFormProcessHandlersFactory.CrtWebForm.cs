namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;

	#region Interface: IWebFormProcessHandlersFactory

	/// <summary>
	/// Factory for getting process handlers depending on landing page entity.
	/// </summary>
	public interface IWebFormProcessHandlersFactory
	{
		#region Methods: Public

		/// <summary>
		/// Gets the pre process handlers.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="webFormId">The web form identifier.</param>
		/// <returns></returns>
		IEnumerable<IGeneratedWebFormPreProcessHandler> GetPreProcessHandlers(UserConnection userConnection,
			Guid webFormId);

		/// <summary>
		/// Gets the post process handlers.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="webFormId">The web form identifier.</param>
		/// <returns></returns>
		IEnumerable<IGeneratedWebFormPostProcessHandler> GetPostProcessHandlers(UserConnection userConnection,
			Guid webFormId);

		#endregion

	}

	#endregion

}

