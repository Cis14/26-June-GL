namespace Terrasoft.Configuration.DataForge
{
	using System;
	using System.Linq;
	using Core;
	using Core.Factories;
	using global::Common.Logging;

	#region Class: DataForgeJobStartupExecutor

	/// <summary>
	/// Executor for scheduling DataForge jobs using workspace console or update scripts.
	/// </summary>
	public class DataForgeJobStartupExecutor : IExecutor
	{
		#region Constants: Private

		private const string InitializeArgument = "--df-init";
		private const string SyncArgument = "--df-sync";

		#endregion

		#region Methods: Private

		private static bool HasArgument(string[] args, string argument) {
			return args.Any(arg => 
				string.Equals(arg, argument, StringComparison.OrdinalIgnoreCase) ||
				string.Equals(arg, argument.Replace("--", "/"), StringComparison.OrdinalIgnoreCase));
		}

		private static void ScheduleInitializationJobs(IAppSchedulerWraper scheduler, UserConnection userConnection) {
			Console.WriteLine("Scheduling DataForge initialization jobs");
			scheduler.ScheduleImmediateJob<DataForgeInitializeDataStructureJob>(
				nameof(DataForgeInitializeDataStructureJob),
				userConnection.Workspace.Name,
				userConnection.CurrentUser.Name,
				null,
				true);
			scheduler.ScheduleImmediateJob<DataForgeInitializeLookupsJob>(
				nameof(DataForgeInitializeLookupsJob),
				userConnection.Workspace.Name,
				userConnection.CurrentUser.Name,
				null,
				true);
		}

		private static void ScheduleSyncJobs(IAppSchedulerWraper scheduler, UserConnection userConnection) {
			Console.WriteLine("Scheduling DataForge sync jobs");
			scheduler.ScheduleImmediateJob<DataForgeUploadPendingDataStructuresJob>(
				nameof(DataForgeUploadPendingDataStructuresJob),
				userConnection.Workspace.Name,
				userConnection.CurrentUser.Name,
				null,
				true);
			scheduler.ScheduleImmediateJob<DataForgeUploadPendingLookupsJob>(
				nameof(DataForgeUploadPendingLookupsJob),
				userConnection.Workspace.Name,
				userConnection.CurrentUser.Name,
				null,
				true);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Executes DataForge job scheduling based on command-line arguments.
		/// Supported arguments: --df-init, --df-sync
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public void Execute(UserConnection userConnection) {
			try {
				IAppSchedulerWraper scheduler = ClassFactory.Get<IAppSchedulerWraper>();

				string[] args = Environment.GetCommandLineArgs();
				bool shouldInitialize = HasArgument(args, InitializeArgument);
				bool shouldSync = HasArgument(args, SyncArgument);

				if (shouldInitialize) {
					ScheduleInitializationJobs(scheduler, userConnection);
				}

				if (shouldSync) {
					ScheduleSyncJobs(scheduler, userConnection);
				}

				if (!shouldInitialize && !shouldSync) {
					Console.WriteLine("No DataForge command-line arguments found. Skipping job scheduling.");
				}

			} catch (Exception ex) {
				Console.WriteLine($"Error executing DataForge job startup: {ex.Message}", ex);
				throw;
			}
		}

		#endregion
	}

	#endregion
}
