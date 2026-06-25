namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotSysCapabilityQueryExecutorSchema

	/// <exclude/>
	public class CopilotSysCapabilityQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotSysCapabilityQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotSysCapabilityQueryExecutorSchema(CopilotSysCapabilityQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f8e6c76a-d774-438c-ad85-e7d0ca20047a");
			Name = "CopilotSysCapabilityQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,83,77,75,195,64,16,61,183,208,255,48,212,75,10,146,31,208,175,131,177,150,128,21,165,21,15,226,97,179,153,182,11,201,110,220,15,49,20,255,187,147,108,180,38,180,20,188,101,223,188,121,239,237,204,70,178,28,77,193,56,66,164,145,89,161,194,72,21,34,83,118,208,63,12,250,61,103,132,220,193,186,52,22,115,170,100,25,114,226,72,19,46,81,162,22,124,210,229,220,11,249,126,4,55,168,53,51,106,107,169,87,227,57,60,92,72,43,172,64,115,150,112,199,184,85,218,51,136,115,165,113,71,41,32,202,152,49,99,104,18,83,130,136,21,44,17,153,176,229,147,67,93,46,62,145,59,106,172,155,94,111,113,203,92,102,111,132,76,201,34,176,101,129,106,27,196,181,121,155,63,186,134,7,154,11,204,96,120,81,123,56,122,35,241,194,37,153,224,192,171,64,151,243,192,111,230,88,90,148,182,19,182,119,168,3,31,175,73,3,183,218,85,35,160,219,62,214,86,158,209,216,94,52,12,158,13,106,146,145,126,125,224,90,199,81,37,213,27,67,194,12,6,157,18,28,224,171,73,131,50,245,129,218,233,86,104,247,42,173,130,105,101,169,11,211,38,219,207,17,212,7,109,83,164,8,52,107,151,163,102,73,134,211,120,205,247,152,179,21,147,108,135,58,166,183,51,109,205,196,151,231,115,88,98,131,252,161,154,128,130,213,169,255,37,41,42,9,218,238,137,106,211,28,86,174,222,104,82,251,104,180,78,75,223,25,190,236,81,99,80,125,195,204,171,133,49,173,136,73,142,225,134,158,21,204,58,218,21,24,250,63,196,235,157,158,169,71,219,32,97,223,163,208,66,228,164,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f8e6c76a-d774-438c-ad85-e7d0ca20047a"));
		}

		#endregion

	}

	#endregion

}

