namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IBaseFileImportStageSchema

	/// <exclude/>
	public class IBaseFileImportStageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IBaseFileImportStageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IBaseFileImportStageSchema(IBaseFileImportStageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2e54df01-9a20-4cc1-b98f-462bb7d75af3");
			Name = "IBaseFileImportStage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a83ae89b-1206-453d-b626-f37ab41fffbf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,77,78,195,48,16,133,215,137,148,59,140,210,13,108,234,125,75,89,128,138,148,5,82,36,184,128,177,39,193,82,252,163,25,7,129,42,238,142,235,180,13,148,34,177,243,140,191,121,243,252,236,164,69,14,82,33,60,35,145,100,223,197,229,189,119,157,233,71,146,209,120,183,124,48,3,54,54,120,138,85,185,171,202,170,44,22,132,125,186,1,104,92,68,234,210,240,10,154,59,201,56,163,79,81,246,152,225,48,190,12,70,129,57,162,127,144,69,146,46,78,202,45,249,128,20,13,242,10,218,44,144,181,10,33,4,220,240,104,173,164,143,219,99,35,43,128,209,39,64,124,39,206,54,241,214,141,118,26,105,52,236,160,199,184,134,207,73,125,129,78,79,6,14,245,193,205,35,198,87,175,255,99,101,251,142,106,140,8,129,188,66,102,224,201,154,3,141,100,222,80,131,26,36,51,242,101,171,185,19,36,73,11,46,125,203,166,206,103,76,193,113,61,3,140,8,138,176,219,212,211,179,218,25,18,51,37,242,108,174,47,5,190,79,120,239,48,23,87,231,66,48,47,190,94,255,206,166,152,242,250,25,87,234,125,1,204,184,177,174,76,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2e54df01-9a20-4cc1-b98f-462bb7d75af3"));
		}

		#endregion

	}

	#endregion

}

