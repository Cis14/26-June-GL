namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotSessionProgressStatesSchema

	/// <exclude/>
	public class CopilotSessionProgressStatesSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotSessionProgressStatesSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotSessionProgressStatesSchema(CopilotSessionProgressStatesSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c97cec0c-44eb-481a-82a1-0e11ea2c192d");
			Name = "CopilotSessionProgressStates";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,85,143,205,78,195,48,16,132,207,141,148,119,240,3,160,230,5,42,164,168,130,27,18,138,169,56,27,103,8,43,28,219,120,55,136,31,245,221,113,140,35,181,199,157,153,111,118,215,155,25,28,141,133,58,38,24,161,176,63,134,72,46,72,219,252,182,205,174,235,58,117,224,101,158,77,250,190,173,243,128,152,192,240,194,74,222,160,88,140,64,133,215,50,84,88,49,152,41,120,21,83,152,114,152,247,91,87,119,81,22,151,23,71,86,193,47,243,6,234,127,238,177,98,122,237,230,28,93,111,217,61,27,18,242,211,125,72,39,70,122,200,190,153,112,115,237,244,153,207,23,121,185,180,239,190,96,151,53,208,219,252,161,47,154,126,39,231,52,28,172,96,44,74,63,229,159,174,148,39,18,135,83,28,205,38,20,168,54,235,242,8,253,84,107,192,199,2,206,188,31,243,162,42,113,12,158,49,192,130,62,49,102,237,220,54,103,245,7,246,0,204,166,116,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c97cec0c-44eb-481a-82a1-0e11ea2c192d"));
		}

		#endregion

	}

	#endregion

}

