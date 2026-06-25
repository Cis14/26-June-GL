namespace Terrasoft.Configuration.Translation
{
	using System;
	using Terrasoft.Core;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: TranslationParallelOptions

	public static class TranslationParallelOptions
	{

		#region Methods: Public

		/// <summary>
		/// Get the maximum number of parallel tasks for execution parallel translation operations.
		/// </summary>
		/// <param name="userConnection">User connection</param>
		/// <param name="absoluteLimitSetting">Name of the setting that can have fixed number of parallel tasks.</param>
		/// <returns></returns>
		public static int GetMaxDegreeOfParallelism(UserConnection userConnection, string absoluteLimitSetting = null) {
			if (!string.IsNullOrEmpty(absoluteLimitSetting)) {
				if (CoreSysSettings.TryGetValue(userConnection, absoluteLimitSetting, out var result)) {
					var absoluteLimitOfParallelism = (int)result;
					if (absoluteLimitOfParallelism != 0) {
						return absoluteLimitOfParallelism;
					}
				}
			}
			const int defaultLimitPercentage = 50;
			var limitSettingExist = CoreSysSettings.TryGetValue(userConnection,
				"TranslationsConcurrencyLimitPercentage",
				out var settingLimitPercentage);
			var processorUsageLimitPercentage = limitSettingExist ? (int)settingLimitPercentage : defaultLimitPercentage;
			var processorUsageLimitRate = (double)processorUsageLimitPercentage / 100;
			var maxDegreeOfParallelism = (int)Math.Ceiling(Environment.ProcessorCount * processorUsageLimitRate);
			return Math.Max(1, maxDegreeOfParallelism);
		}

		#endregion
		
	}

	#endregion

}
