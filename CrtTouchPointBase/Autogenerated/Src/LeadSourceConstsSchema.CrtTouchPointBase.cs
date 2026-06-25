namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LeadSourceConstsSchema

	/// <exclude/>
	public class LeadSourceConstsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LeadSourceConstsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LeadSourceConstsSchema(LeadSourceConstsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1f02ccde-e202-4fc1-ac0f-1d4417da8cfe");
			Name = "LeadSourceConsts";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c695e3ed-eb31-41e8-baf6-8b1758bb9790");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,145,91,75,195,48,24,134,175,87,216,127,248,216,110,244,34,235,97,105,235,156,10,109,87,135,32,120,49,193,235,216,102,53,216,38,35,7,165,140,253,119,211,110,131,177,137,55,134,144,143,239,244,62,47,132,147,134,170,13,41,40,188,82,41,137,18,107,61,201,4,95,179,202,72,162,153,224,67,103,59,116,6,70,49,94,193,170,85,154,54,115,155,219,59,150,180,178,125,200,106,162,212,45,60,83,82,174,132,145,5,181,235,74,171,161,99,103,92,215,133,59,101,154,134,200,246,225,144,219,182,38,140,171,147,13,168,133,248,52,27,40,186,77,194,53,124,145,218,80,53,57,42,184,39,18,27,243,94,179,2,236,156,182,161,232,224,191,176,7,219,158,127,97,160,47,44,133,168,106,10,73,249,38,100,217,83,46,49,103,28,105,9,130,215,45,44,13,43,79,120,123,169,131,210,83,9,247,192,233,119,63,116,53,138,252,56,206,252,48,69,33,158,206,16,206,34,15,165,9,142,16,94,4,121,26,120,126,16,123,249,232,122,254,135,209,23,253,65,37,40,166,233,191,76,246,50,103,230,30,195,60,158,166,1,70,233,34,186,65,56,76,19,52,203,179,8,45,242,28,123,201,52,204,34,63,60,154,219,117,47,216,51,166,188,220,255,122,87,217,253,0,179,201,95,59,60,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1f02ccde-e202-4fc1-ac0f-1d4417da8cfe"));
		}

		#endregion

	}

	#endregion

}

