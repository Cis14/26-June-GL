namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IUnifiedSummarizationSettingsSchema

	/// <exclude/>
	public class IUnifiedSummarizationSettingsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IUnifiedSummarizationSettingsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IUnifiedSummarizationSettingsSchema(IUnifiedSummarizationSettingsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("90a5b66d-1a3e-4abe-95be-1aa4849737d8");
			Name = "IUnifiedSummarizationSettings";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,144,177,110,195,48,12,68,103,27,240,63,16,232,30,239,77,209,197,93,60,4,8,154,246,3,84,155,118,8,88,148,42,81,67,26,248,223,75,219,42,224,161,200,120,210,221,241,145,108,44,70,111,58,132,38,160,17,114,135,198,121,154,156,84,229,189,42,171,178,120,10,56,146,99,104,89,48,12,106,124,134,246,147,105,32,236,47,201,90,19,232,103,137,241,5,69,136,199,184,134,234,186,134,151,184,126,223,94,179,126,195,129,24,35,200,21,33,102,51,4,252,78,20,176,135,193,5,72,91,45,196,125,47,16,175,145,140,165,209,24,245,249,240,55,165,222,141,241,233,107,162,78,19,25,245,49,41,232,130,69,17,37,168,128,115,112,214,203,7,90,63,25,65,184,195,136,114,132,121,113,104,29,156,136,223,51,234,73,1,204,168,139,60,240,52,87,19,76,167,20,255,186,150,83,178,196,198,37,150,86,113,214,133,246,198,121,187,60,114,191,29,127,145,243,47,229,40,2,42,170,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("90a5b66d-1a3e-4abe-95be-1aa4849737d8"));
		}

		#endregion

	}

	#endregion

}

