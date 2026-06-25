namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CheckedEmailResponseSchema

	/// <exclude/>
	public class CheckedEmailResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CheckedEmailResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CheckedEmailResponseSchema(CheckedEmailResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d3507f4f-1914-4114-8a7c-cc67545f380a");
			Name = "CheckedEmailResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,145,59,79,2,65,20,133,107,73,248,15,55,216,104,179,219,11,210,172,132,88,96,8,216,25,139,97,56,44,19,231,177,185,51,91,32,241,191,123,247,1,34,137,150,115,230,59,247,126,179,235,149,67,172,148,6,189,130,89,197,176,75,89,17,252,206,148,53,171,100,130,207,138,217,122,37,72,240,17,113,56,56,14,7,55,117,52,190,164,245,33,38,56,129,173,133,110,200,152,205,225,193,70,143,175,153,85,237,147,113,200,214,114,171,172,249,108,7,255,80,255,108,94,132,45,108,20,84,224,91,70,41,49,21,86,197,248,64,197,30,250,3,219,153,83,198,158,4,91,46,207,115,154,196,218,57,197,135,105,127,94,161,98,68,248,20,41,237,65,186,235,18,154,50,113,223,166,29,7,71,178,53,59,77,201,47,198,188,61,169,164,68,48,177,210,233,93,130,170,222,88,163,73,55,58,127,216,220,28,91,163,179,250,146,67,5,78,6,226,191,108,235,221,253,181,114,27,204,33,182,129,41,162,183,22,207,218,166,236,92,184,180,235,244,22,112,27,240,221,139,252,84,122,164,81,87,24,221,55,182,39,221,231,153,175,29,88,109,44,38,151,210,83,249,70,13,77,71,42,145,198,205,214,49,125,245,250,240,219,238,5,237,185,75,127,135,146,125,3,178,189,32,218,75,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d3507f4f-1914-4114-8a7c-cc67545f380a"));
		}

		#endregion

	}

	#endregion

}

