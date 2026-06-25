namespace Creatio.Copilot
{
	using System;
	using Common.Logging;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CopilotAgentSubSkillQueryExecutor

	[DefaultBinding(typeof(IEntityQueryExecutor), Name = nameof(CopilotAgentSubSkillQueryExecutor))]
	public class CopilotAgentSubSkillQueryExecutor : BaseCopilotSubSkillQueryExecutor
	{

		#region Constructors: Public

		public CopilotAgentSubSkillQueryExecutor(UserConnection userConnection)
			: base(userConnection, "CopilotAgentSubSkill", "CopilotAgentSubSkillQueryExecutorCache",
				"CopilotIntent") { }

		#endregion

	}

	#endregion

}

