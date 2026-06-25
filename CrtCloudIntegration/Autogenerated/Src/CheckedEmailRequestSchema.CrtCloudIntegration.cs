namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CheckedEmailRequestSchema

	/// <exclude/>
	public class CheckedEmailRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CheckedEmailRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CheckedEmailRequestSchema(CheckedEmailRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("942945d7-ac66-48ca-b114-0dbc0d994415");
			Name = "CheckedEmailRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,146,65,143,218,48,16,133,207,93,137,255,48,162,151,246,146,220,129,229,80,22,173,122,160,90,1,183,170,7,227,188,128,181,142,157,206,56,72,20,245,191,119,156,176,116,151,173,212,75,162,25,191,249,198,239,201,193,52,144,214,88,208,22,204,70,98,157,138,69,12,181,219,119,108,146,139,161,88,44,55,171,88,193,203,232,238,60,186,251,208,137,11,123,218,156,36,161,81,165,247,176,89,38,197,35,2,216,217,233,173,102,221,133,228,26,20,27,61,53,222,253,234,169,170,82,221,71,198,94,11,90,120,35,50,161,197,1,246,25,213,178,49,206,175,241,179,131,164,94,86,150,37,205,164,107,26,195,167,249,165,94,163,101,8,66,18,226,65,74,41,146,205,4,66,6,72,174,119,160,163,174,173,29,42,210,61,45,199,163,171,192,36,250,45,94,200,229,13,122,38,128,241,162,52,70,125,63,254,111,46,197,23,35,80,119,71,103,113,185,246,152,202,76,251,254,96,146,209,169,196,198,166,31,218,104,187,157,119,150,108,246,251,47,187,52,161,247,48,157,59,247,57,92,243,122,226,216,130,147,131,134,246,212,35,135,243,219,160,250,198,35,52,163,168,158,243,63,29,64,222,233,162,88,191,138,169,143,173,184,18,202,91,196,236,104,124,135,107,185,85,200,48,252,106,230,175,164,119,189,66,179,3,127,250,166,207,139,238,105,60,200,199,159,115,8,47,41,124,93,134,174,1,155,157,199,76,18,235,147,153,211,114,184,211,153,246,72,211,124,229,41,253,190,120,71,168,6,251,125,61,116,223,54,181,247,7,4,57,173,176,207,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("942945d7-ac66-48ca-b114-0dbc0d994415"));
		}

		#endregion

	}

	#endregion

}

