namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: INonPersistentColumnProcessSchema

	/// <exclude/>
	public class INonPersistentColumnProcessSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public INonPersistentColumnProcessSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public INonPersistentColumnProcessSchema(INonPersistentColumnProcessSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5b9b0b8a-a0c7-4250-a378-016529bb5c12");
			Name = "INonPersistentColumnProcess";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,65,107,195,48,12,133,207,9,228,63,136,246,210,65,73,238,91,87,24,221,6,57,108,132,49,118,119,99,37,53,36,114,176,157,150,50,250,223,151,216,77,231,132,181,236,40,75,223,243,123,150,137,213,168,27,150,35,124,162,82,76,203,194,196,27,73,133,40,91,197,140,144,20,191,138,10,211,186,145,202,68,225,119,20,6,77,187,173,68,14,130,12,170,162,7,211,119,73,25,42,45,180,65,50,27,89,181,53,101,74,230,168,117,55,222,35,193,92,97,217,105,193,27,154,157,228,250,30,50,43,18,133,125,51,73,18,88,233,182,174,153,58,174,135,131,39,206,53,40,118,128,61,171,90,4,179,99,6,14,162,170,96,139,208,56,113,228,241,5,79,166,252,170,97,138,213,64,93,188,199,153,176,246,95,200,8,115,156,173,93,24,64,91,198,171,196,78,254,13,230,54,204,5,113,229,127,144,175,222,245,132,115,81,110,211,28,181,17,100,31,126,74,123,173,145,198,94,10,222,63,215,34,245,82,130,31,121,9,174,229,86,115,214,27,31,90,191,224,121,95,246,210,129,63,242,252,123,191,239,229,238,225,198,22,63,90,210,163,244,122,88,158,160,242,202,246,108,158,243,255,89,12,234,115,36,238,254,144,173,79,81,120,250,1,16,158,160,29,186,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5b9b0b8a-a0c7-4250-a378-016529bb5c12"));
		}

		#endregion

	}

	#endregion

}

