namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotMessageButtonSchema

	/// <exclude/>
	public class CopilotMessageButtonSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotMessageButtonSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotMessageButtonSchema(CopilotMessageButtonSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ddcf6e29-750e-446a-8e27-49806aa3db31");
			Name = "CopilotMessageButton";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,205,110,194,48,16,132,207,65,226,29,44,122,1,9,229,1,26,245,208,186,151,30,168,16,28,43,14,142,179,68,150,28,59,181,215,135,54,202,187,215,127,105,33,162,18,151,160,29,102,191,204,108,20,235,192,246,140,3,161,6,24,10,93,82,221,11,169,113,185,24,150,139,194,89,161,90,114,252,178,8,93,53,155,203,131,83,40,58,40,143,96,4,147,226,59,172,43,239,242,190,7,3,173,31,8,149,204,218,71,146,153,59,176,150,181,64,181,58,11,211,69,251,139,67,212,42,238,124,188,50,100,254,63,52,140,227,41,8,191,224,90,66,16,122,87,75,193,9,15,208,25,115,226,20,67,100,253,5,208,202,162,113,28,181,241,57,246,17,144,28,25,118,11,179,246,43,161,39,103,125,200,184,37,121,246,185,46,71,174,27,216,144,112,166,162,160,201,74,158,166,165,42,202,207,60,171,105,53,137,212,239,5,163,255,137,194,152,35,131,106,82,234,235,10,123,163,123,48,40,96,86,32,222,107,7,93,13,102,253,238,63,163,71,174,242,203,87,91,242,102,15,240,233,132,129,198,235,103,38,45,108,78,23,181,115,133,41,246,64,90,192,138,216,240,24,255,199,167,22,119,211,115,251,59,225,225,30,247,7,15,55,188,1,158,29,49,169,215,226,248,3,92,168,53,189,244,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ddcf6e29-750e-446a-8e27-49806aa3db31"));
		}

		#endregion

	}

	#endregion

}

