namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotIntCapabilityQueryExecutorSchema

	/// <exclude/>
	public class CopilotIntCapabilityQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotIntCapabilityQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotIntCapabilityQueryExecutorSchema(CopilotIntCapabilityQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ad068d13-c649-4c86-98ac-f384244483d6");
			Name = "CopilotIntCapabilityQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,205,74,196,48,20,133,215,29,152,119,184,212,77,11,101,30,160,226,102,58,35,204,70,148,234,74,92,164,153,219,49,24,147,146,220,128,165,248,238,222,254,136,86,44,93,149,156,156,156,239,156,26,241,142,190,17,18,161,112,40,72,217,93,97,27,165,45,109,55,221,118,19,5,175,204,5,30,209,57,225,109,77,124,233,240,122,65,223,29,13,41,82,232,23,13,183,66,146,117,163,131,61,87,14,47,202,26,40,180,240,62,135,9,124,50,84,136,70,84,74,43,106,31,2,186,246,248,129,50,240,195,225,209,243,1,107,17,52,237,149,57,51,34,161,182,65,91,39,167,1,62,247,167,25,220,241,60,184,1,195,31,54,173,18,210,244,133,17,77,168,180,146,32,251,90,235,173,32,135,189,240,56,249,202,80,149,111,74,235,63,197,163,110,40,255,51,217,26,79,46,244,191,131,151,223,15,192,209,49,193,87,177,201,147,71,199,49,6,37,245,137,97,118,76,251,168,40,135,138,155,37,243,171,12,226,255,194,227,5,125,6,45,132,124,197,56,27,194,163,111,123,217,250,95,49,41,116,240,57,141,69,115,30,247,14,231,81,157,139,172,125,1,166,175,61,67,130,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ad068d13-c649-4c86-98ac-f384244483d6"));
		}

		#endregion

	}

	#endregion

}

