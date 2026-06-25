 namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using global::Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Hooks;

	#region Interface: ICopilotProcessUtilities

	/// <summary>
	/// Provides utilities for executing Copilot-related processes.
	/// </summary>
	public interface ICopilotProcessExecutor
	{

		#region Methods: Public

		/// <summary>
		/// Starts a process with the specified configuration.
		/// </summary>
		/// <param name="processSchemaUId">The unique identifier of the process schema.</param>
		/// <param name="parameterValues">The parameter values to pass to the process.</param>
		/// <param name="registerHook">Function to register the process hook.</param>
		/// <param name="hookArgs">The hook arguments for process completion.</param>
		/// <param name="hookEvents">The process execution hook events.</param>
		/// <param name="inlineExecutionContext">The inline execution context for synchronous results (optional).</param>
		/// <returns>The process descriptor result.</returns>
		ProcessDescriptor StartProcess(Guid processSchemaUId, Dictionary<string, string> parameterValues,
			Func<ExecuteProcessOptions, object, ProcessExecutionHookEvents, object, ExecuteProcessOptions> registerHook,
			object hookArgs, ProcessExecutionHookEvents hookEvents, object inlineExecutionContext = null);
		
		/// <summary>
		/// Completes the process with the specified process element identifier.
		/// </summary>
		/// <param name="processElementId">Process element identifier.</param>
		/// <param name="userMessage">User message</param>
		/// <returns></returns>
		bool CompleteProcess(Guid processElementId, string userMessage);

		/// <summary>
		/// Cancels the process with the specified process element identifier.
		/// </summary>
		/// <param name="processElementId">Process element identifier.</param>
		void CancelProcess(Guid processElementId);

		#endregion

	}

	#endregion

	#region Class: CopilotProcessExecutor

	/// <summary>
	/// Implementation of utilities for executing Copilot-related processes.
	/// </summary>
	[DefaultBinding(typeof(ICopilotProcessExecutor))]
	internal class CopilotProcessExecutor : ICopilotProcessExecutor
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private static ILog _log;

		#endregion

		#region Properties: Private

		private static ILog Log => _log ?? (_log = LogManager.GetLogger(nameof(CopilotProcessExecutor)));

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotProcessExecutor"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public CopilotProcessExecutor(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public ProcessDescriptor StartProcess(Guid processSchemaUId, Dictionary<string, string> parameterValues,
				Func<ExecuteProcessOptions, object, ProcessExecutionHookEvents, object, ExecuteProcessOptions> registerHook,
				object hookArgs, ProcessExecutionHookEvents hookEvents, object inlineExecutionContext = null) {
			try {
				ExecuteProcessOptions executeProcessOptions = ExecuteProcessOptions
					.RunBySchemaUId(processSchemaUId)
					.WithParseSerializableObjectAsJson();
				executeProcessOptions = registerHook(executeProcessOptions, hookArgs, hookEvents, inlineExecutionContext);
				executeProcessOptions.ParameterValues = parameterValues;
				return _userConnection.ProcessEngine.ProcessExecutor.Execute(executeProcessOptions);
			} catch (Exception e) {
				Log.Error($"Error starting process {processSchemaUId}: {e.Message}", e);
				throw;
			}
		}

		/// <inheritdoc />
		public bool CompleteProcess(Guid processElementId, string userMessage) {
			return _userConnection.ProcessEngine.CompleteExecuting(processElementId, userMessage);
		}

		/// <inheritdoc />
		public void CancelProcess(Guid processElementId) {
			try {
				Process process = _userConnection.ProcessEngine.FindProcessByProcessElementUId(processElementId);
				if (process != null) {
					_userConnection.ProcessEngine.ProcessExecutor.CancelExecutionAsync(process.UId);
				}
			} catch (Exception e) {
				Log.Error($"Error cancelling process with element id {processElementId}: {e.Message}", e);
				throw;
			}
		}

		#endregion

	}

	#endregion

}

