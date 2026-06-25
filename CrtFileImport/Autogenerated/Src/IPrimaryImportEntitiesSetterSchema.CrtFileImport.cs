namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPrimaryImportEntitiesSetterSchema

	/// <exclude/>
	public class IPrimaryImportEntitiesSetterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPrimaryImportEntitiesSetterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPrimaryImportEntitiesSetterSchema(IPrimaryImportEntitiesSetterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8f3be9bf-a5b6-4e6a-a262-4785da28353f");
			Name = "IPrimaryImportEntitiesSetter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,145,205,74,196,48,16,199,207,91,232,59,12,61,41,72,250,0,214,94,164,74,241,178,176,190,64,182,78,151,97,155,164,76,18,177,200,190,187,73,99,187,31,136,183,100,250,251,127,116,162,165,66,59,202,14,225,29,153,165,53,189,19,207,70,247,116,240,44,29,25,45,94,104,192,86,141,134,93,158,125,231,217,198,91,210,7,216,77,214,161,10,232,48,96,23,57,43,94,81,35,83,247,184,50,151,142,140,162,209,142,28,161,13,64,64,70,191,31,168,3,210,14,185,143,249,237,150,73,73,158,82,214,2,239,208,5,32,240,49,122,83,150,37,84,214,171,200,213,203,32,32,22,240,139,172,139,161,248,43,4,103,128,102,171,117,36,86,139,242,214,163,26,37,75,5,58,108,227,169,152,207,24,98,109,81,167,54,112,30,137,170,156,47,127,75,151,168,162,110,110,11,253,47,60,226,20,118,233,149,14,210,55,156,160,75,151,43,209,167,161,143,248,183,119,169,212,118,237,116,81,239,1,218,70,123,133,44,247,3,86,243,22,167,122,237,112,253,53,217,164,216,26,206,13,238,227,19,158,242,236,244,3,60,38,239,56,29,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8f3be9bf-a5b6-4e6a-a262-4785da28353f"));
		}

		#endregion

	}

	#endregion

}

