namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotContentTypeSchema

	/// <exclude/>
	public class CopilotContentTypeSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotContentTypeSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotContentTypeSchema(CopilotContentTypeSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9bb83cd8-c94e-4aab-ad1a-77a733ec50ec");
			Name = "CopilotContentType";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,142,203,10,194,48,16,69,215,22,250,15,161,251,250,3,226,162,20,183,174,138,251,113,152,74,32,157,132,100,124,20,241,223,77,212,210,184,104,33,16,238,204,57,201,101,24,40,56,64,82,173,39,16,109,183,173,117,218,88,41,139,103,89,108,220,245,108,52,170,32,113,133,10,13,132,160,126,64,107,89,136,165,27,29,69,48,193,19,141,150,131,68,199,107,190,168,142,30,162,246,170,146,120,87,187,37,234,164,233,126,48,52,196,7,19,124,139,177,166,111,94,150,98,131,94,251,33,181,230,100,97,150,87,44,3,94,247,26,103,45,31,44,123,77,24,25,27,156,44,72,177,6,92,151,142,86,254,254,226,44,175,84,4,70,50,102,110,152,229,143,245,42,139,120,222,19,165,7,15,188,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9bb83cd8-c94e-4aab-ad1a-77a733ec50ec"));
		}

		#endregion

	}

	#endregion

}

