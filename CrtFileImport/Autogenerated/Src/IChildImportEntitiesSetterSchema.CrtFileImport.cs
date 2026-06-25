namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IChildImportEntitiesSetterSchema

	/// <exclude/>
	public class IChildImportEntitiesSetterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IChildImportEntitiesSetterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IChildImportEntitiesSetterSchema(IChildImportEntitiesSetterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("06530102-5f59-41f5-9ac8-4a7901f07ee8");
			Name = "IChildImportEntitiesSetter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,144,207,78,195,48,12,135,207,171,212,119,176,122,2,9,37,15,64,233,101,42,168,55,164,241,2,89,231,14,75,249,83,37,46,162,66,123,119,146,150,134,49,113,75,236,207,223,207,178,85,6,195,168,122,132,55,244,94,5,55,176,216,59,59,208,121,242,138,201,89,241,76,26,59,51,58,207,101,241,85,22,187,41,144,61,195,97,14,140,38,162,90,99,159,184,32,94,208,162,167,254,49,51,215,70,143,162,181,76,76,24,34,16,145,113,58,106,234,129,44,163,31,82,126,183,127,39,125,90,147,54,244,128,28,219,145,78,193,59,41,37,212,97,50,70,249,185,217,10,17,9,128,159,20,56,69,226,207,32,176,3,90,84,185,36,178,66,222,58,234,81,121,101,192,198,91,60,85,203,27,99,108,168,154,117,27,248,45,137,90,46,159,255,71,183,168,170,105,111,23,250,51,248,225,232,148,22,191,91,253,175,89,127,149,244,0,93,107,39,131,94,29,53,214,203,65,230,38,235,238,211,149,47,101,113,249,6,238,14,23,19,192,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("06530102-5f59-41f5-9ac8-4a7901f07ee8"));
		}

		#endregion

	}

	#endregion

}

