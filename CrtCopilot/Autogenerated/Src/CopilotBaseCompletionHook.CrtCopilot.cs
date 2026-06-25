namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Common.Logging;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Core;
	using Terrasoft.Core.Process;

	#region Class: CopilotBaseCompletionHook

	/// <summary>
	/// Base class for copilot process completion hooks with common logic.
	/// </summary>
	public abstract class CopilotBaseCompletionHook
	{

		#region Methods: Protected

		/// <summary>
		/// Gets the logger for the hook.
		/// </summary>
		protected static ILog Log => LogManager.GetLogger("Copilot");

		/// <summary>
		/// Creates the response from process output parameters.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="processInfo">The process information.</param>
		/// <param name="parameterMetaInfos">The parameter meta information list.</param>
		/// <param name="responseProvider">The parameterized response provider.</param>
		/// <returns>Response string.</returns>
		protected static string GetResponse(UserConnection userConnection, IProcessInfo processInfo,
				IReadOnlyList<ICopilotParameterMetaInfo> parameterMetaInfos,
				ICopilotParametrizedActionResponseProvider responseProvider) {
			object GetParameterValue(ICopilotParameterMetaInfo info) =>
				processInfo.GetParameterValue(info.Name).Value;
			return responseProvider.GetParameterizedResponse(userConnection, parameterMetaInfos, GetParameterValue);
		}

		#endregion

	}

	#endregion

}
