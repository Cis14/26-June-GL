namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: KnwSearchItemResponseSchema

	/// <exclude/>
	public class KnwSearchItemResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public KnwSearchItemResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public KnwSearchItemResponseSchema(KnwSearchItemResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cd40c984-d7b9-4f53-b5e1-61b6196c4daa");
			Name = "KnwSearchItemResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,148,201,110,194,48,16,64,207,69,226,31,70,226,210,94,146,158,155,46,7,138,16,106,15,8,250,3,174,51,4,171,142,157,122,41,66,168,255,94,219,89,26,66,37,18,149,19,120,22,251,101,242,28,65,114,212,5,161,8,83,133,196,48,25,77,101,193,184,52,227,209,97,60,186,178,154,137,12,214,123,109,48,79,198,35,23,153,40,204,152,20,48,229,68,235,59,120,17,187,133,203,61,163,33,140,135,130,56,142,225,94,219,60,39,106,255,88,173,87,88,40,212,40,140,6,179,69,72,67,181,6,185,1,34,128,185,126,96,34,100,62,132,220,113,76,51,4,141,68,209,45,184,182,66,10,141,81,189,115,220,218,186,176,239,156,81,160,30,165,75,114,117,8,52,13,239,82,201,2,149,97,232,160,151,161,175,204,119,113,67,96,142,142,84,42,7,81,17,91,193,62,45,2,75,221,51,176,13,67,229,217,125,194,195,71,205,62,109,184,154,110,110,89,250,4,30,109,145,194,1,50,52,137,223,55,129,239,33,0,134,25,142,253,15,213,70,249,23,247,22,186,142,14,133,135,42,25,205,242,194,236,147,33,16,92,82,175,136,24,204,241,90,55,246,64,153,160,72,203,119,22,214,229,148,58,193,83,9,215,65,23,63,229,85,101,76,31,25,73,227,87,105,225,70,201,220,5,91,22,74,171,104,45,99,47,7,255,2,185,172,139,167,18,118,121,207,11,25,72,67,237,127,164,236,220,81,160,82,24,199,214,75,137,122,58,151,179,19,149,114,203,47,194,237,153,1,84,4,179,80,127,177,227,53,149,170,185,161,191,147,177,252,204,60,82,233,126,17,214,161,189,75,115,59,8,161,245,93,237,119,61,143,190,153,208,250,219,229,16,184,59,46,190,190,233,127,93,93,236,7,58,31,101,107,103,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cd40c984-d7b9-4f53-b5e1-61b6196c4daa"));
		}

		#endregion

	}

	#endregion

}

