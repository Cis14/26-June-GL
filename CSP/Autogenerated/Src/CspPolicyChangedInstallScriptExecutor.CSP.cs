namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using global::Common.Logging;

	#region Class: CspPolicyChangedInstallScriptExecutor

	public class CspPolicyChangedInstallScriptExecutor : IInstallScriptExecutor
	{

		private static readonly ILog _log = LogManager.GetLogger("CSP");

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			IEnumerable<Guid> guids = new List<Guid> {
				new Guid("{d999ca9c-6c71-46e0-9e67-3f7fa8987ee5}"),
				new Guid("{595dcd7e-437f-462b-9f1c-c178274db271}"),
				new Guid("{6182f2de-7ef9-4570-9d63-fc00f2e64cbb}"),
				new Guid("{8ba51e55-ad35-4e85-9961-cf1e6e39bc1d}"),
				new Guid("{ec876769-41d5-4021-809b-455a7fa7cf1d}")
			};
			IEnumerable<QueryParameter> selectedIds = guids.Select(b => new QueryParameter(b));
			var cnt = (new Delete(userConnection)
					.From("SysCspDefSrcInDirectv")
					.Where("Id").In(selectedIds) as Delete)
				.Execute();
			_log.Info($"Deleted {cnt} records from CSP");
		}

		#endregion

	}

	#endregion

}

