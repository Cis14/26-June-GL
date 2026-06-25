namespace Creatio.Copilot
{
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CopilotIntCapabilityQueryExecutor

	[DefaultBinding(typeof(IEntityQueryExecutor), Name = nameof(CopilotIntCapabilityQueryExecutor))]
	public class CopilotIntCapabilityQueryExecutor : BaseCopilotSubSkillQueryExecutor
	{

		#region Constructors: Public

		public CopilotIntCapabilityQueryExecutor(UserConnection userConnection)
			: base(userConnection, "CopilotIntCapability", "CopilotIntCapabilityQueryExecutorCache",
				"CopilotSysCapability") { }

		#endregion

	}

	#endregion

}

