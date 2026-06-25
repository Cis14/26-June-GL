namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenerateListWidgetConfigActionSchema

	/// <exclude/>
	public class GenerateListWidgetConfigActionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenerateListWidgetConfigActionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenerateListWidgetConfigActionSchema(GenerateListWidgetConfigActionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cf760c65-044e-4135-9f2d-237964bb9aa7");
			Name = "GenerateListWidgetConfigAction";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("446cbdcc-b5f9-4cb2-bd58-39eb952ac9f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,144,205,74,195,64,16,199,239,129,188,195,144,83,123,233,3,88,20,52,74,161,148,34,84,244,60,221,76,227,224,102,38,236,110,44,40,190,187,27,183,77,37,122,80,186,44,236,97,102,127,255,15,193,134,124,139,134,160,116,132,129,117,86,106,211,170,144,132,82,91,182,26,242,236,61,207,32,158,206,179,212,240,64,206,161,215,93,232,23,27,149,121,158,165,113,219,109,45,27,48,22,189,135,5,9,57,12,180,98,31,158,184,170,41,210,100,199,245,181,137,18,2,23,112,131,158,142,75,143,76,251,59,75,77,212,76,243,4,60,200,126,177,157,6,50,129,42,208,215,168,207,21,129,15,174,183,179,244,42,27,243,76,13,174,99,18,184,188,130,98,172,121,90,41,6,179,127,133,222,163,121,193,154,6,118,233,194,184,159,17,52,181,48,16,87,106,208,242,27,110,45,109,18,124,17,125,97,219,199,156,76,191,103,236,143,163,208,57,1,161,253,207,143,147,226,216,23,244,9,33,69,132,148,17,82,113,197,116,126,2,126,252,219,215,45,121,227,248,44,111,254,23,115,179,177,173,195,19,239,39,255,101,246,13,127,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cf760c65-044e-4135-9f2d-237964bb9aa7"));
		}

		#endregion

	}

	#endregion

}

