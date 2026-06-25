namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotHistoryStorageSchema

	/// <exclude/>
	public class ICopilotHistoryStorageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotHistoryStorageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotHistoryStorageSchema(ICopilotHistoryStorageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("94c696e0-f740-48c2-a029-90597ed84345");
			Name = "ICopilotHistoryStorage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,65,110,194,48,16,69,215,68,202,29,70,172,232,134,28,160,148,13,139,130,212,85,233,5,12,158,192,72,196,78,61,54,85,84,245,238,29,59,14,130,32,209,74,217,56,254,255,207,155,111,163,26,228,86,237,17,86,14,149,39,59,95,217,150,78,214,151,197,119,89,76,2,147,57,192,182,99,143,205,115,89,200,159,170,170,96,193,161,105,148,235,150,249,188,245,214,169,3,66,109,29,100,59,48,50,147,53,12,202,104,32,207,32,115,88,68,60,31,66,170,171,148,54,236,78,180,7,50,30,93,29,105,54,57,103,77,44,225,93,158,32,202,72,117,7,209,83,168,51,242,120,254,252,34,175,198,250,69,171,156,106,192,72,3,47,211,125,111,219,246,174,233,114,28,179,168,146,58,153,207,150,116,154,150,213,179,213,141,25,110,179,158,250,222,30,49,171,11,181,195,207,128,236,83,105,14,125,112,82,96,44,47,24,146,11,32,141,198,83,77,232,254,187,87,14,220,152,218,78,151,31,71,132,198,106,60,193,23,249,35,104,229,21,120,11,44,16,82,61,232,221,245,150,41,42,51,36,231,29,3,216,26,188,92,68,191,30,208,165,169,193,20,83,94,67,238,42,111,248,222,171,102,183,199,136,7,87,168,15,59,123,179,74,223,189,51,236,186,84,212,159,13,141,222,42,134,13,207,152,88,115,222,70,71,134,201,79,89,200,247,11,208,250,196,139,37,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("94c696e0-f740-48c2-a029-90597ed84345"));
		}

		#endregion

	}

	#endregion

}

