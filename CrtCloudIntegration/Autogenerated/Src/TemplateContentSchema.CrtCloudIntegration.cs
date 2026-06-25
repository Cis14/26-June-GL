namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TemplateContentSchema

	/// <exclude/>
	public class TemplateContentSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TemplateContentSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TemplateContentSchema(TemplateContentSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5dae23e0-f39b-4db5-9d47-a9196b501f89");
			Name = "TemplateContent";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,144,193,14,130,48,16,68,207,146,240,15,155,112,135,187,26,47,196,120,50,33,209,31,168,184,66,19,104,201,238,114,48,198,127,183,165,98,68,60,104,47,77,103,103,58,175,53,170,69,238,84,137,112,68,34,197,246,34,105,110,205,69,87,61,41,209,214,164,249,246,176,183,103,108,56,142,110,113,20,71,139,132,176,114,3,200,27,197,188,116,185,182,107,148,160,75,9,26,25,44,89,150,193,154,251,182,85,116,221,60,207,126,29,107,4,121,250,161,12,129,116,244,103,111,129,174,63,53,186,132,210,87,204,27,22,1,228,69,82,144,237,144,68,163,195,41,134,100,152,127,98,12,194,14,133,193,18,176,223,165,158,112,204,65,70,18,22,210,166,26,189,112,131,10,101,229,239,88,193,253,159,50,227,254,251,167,38,111,252,86,147,160,57,135,103,15,231,160,78,69,167,61,0,77,211,55,129,215,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5dae23e0-f39b-4db5-9d47-a9196b501f89"));
		}

		#endregion

	}

	#endregion

}

