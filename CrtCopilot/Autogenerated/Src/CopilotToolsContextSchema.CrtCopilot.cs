namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotToolsContextSchema

	/// <exclude/>
	public class CopilotToolsContextSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotToolsContextSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotToolsContextSchema(CopilotToolsContextSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("42e41678-3e68-47c4-bb06-274ca465f6bb");
			Name = "CopilotToolsContext";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,81,219,138,194,48,16,125,86,240,31,6,124,81,144,126,128,186,250,80,203,34,184,32,232,15,196,236,104,3,185,116,147,41,40,197,127,223,164,137,116,93,202,186,111,201,201,185,76,206,104,166,208,85,140,35,228,22,25,9,147,229,166,18,210,208,104,216,140,134,131,218,9,125,129,195,205,17,42,255,34,37,114,207,209,46,123,71,141,86,240,197,111,206,78,232,175,14,60,162,181,204,153,51,101,133,246,236,82,161,166,108,171,9,237,217,71,186,44,47,25,229,70,85,18,131,171,215,121,229,216,226,197,95,32,151,204,185,57,164,113,142,198,200,220,120,229,149,90,150,8,38,154,73,224,129,214,203,26,52,45,179,51,244,115,147,173,57,25,235,125,247,245,73,10,30,25,85,123,238,49,153,132,215,193,96,187,19,142,150,1,223,224,89,104,17,134,93,1,249,187,155,69,198,70,180,189,48,123,91,250,12,255,245,25,108,3,191,184,34,175,125,224,10,20,171,170,128,39,199,66,215,10,45,59,73,92,166,216,80,139,166,3,47,81,177,21,136,246,230,166,208,180,130,224,229,224,45,102,46,90,232,35,26,122,48,89,71,56,218,4,110,178,128,245,26,186,180,172,80,21,221,122,51,39,211,214,225,254,170,147,191,218,128,127,23,49,133,216,196,28,168,20,110,146,228,143,150,64,215,82,166,207,167,129,198,168,63,227,38,159,215,186,183,166,66,75,2,251,151,218,63,109,236,179,129,11,210,2,238,63,232,175,231,127,244,222,35,126,185,213,199,114,58,109,207,207,34,250,12,222,191,1,176,218,165,6,168,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("42e41678-3e68-47c4-bb06-274ca465f6bb"));
		}

		#endregion

	}

	#endregion

}

