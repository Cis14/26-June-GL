namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: EnableParallelApplyTranslationsSchema

	/// <exclude/>
	public class EnableParallelApplyTranslationsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public EnableParallelApplyTranslationsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public EnableParallelApplyTranslationsSchema(EnableParallelApplyTranslationsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("41825f47-7e66-47e3-8ff9-fa62720464d4");
			Name = "EnableParallelApplyTranslations";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2f244451-ec5e-494f-9790-8d930a80007c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,143,77,106,196,48,12,70,215,9,228,14,98,186,105,55,57,192,12,93,148,180,133,46,10,179,200,5,52,142,98,12,142,109,100,187,80,134,222,189,178,29,250,179,154,149,229,143,167,39,201,225,70,49,160,34,152,137,25,163,95,211,56,121,183,26,157,25,147,241,110,156,25,93,180,181,30,250,235,208,119,57,26,167,97,98,42,217,248,42,79,102,154,189,214,86,242,211,208,11,114,199,164,133,135,201,98,140,71,120,113,120,177,116,70,70,107,201,62,133,96,63,255,88,99,109,49,46,17,59,180,160,74,207,173,22,56,194,62,248,157,18,46,152,80,20,215,42,250,29,46,92,226,172,146,103,217,225,156,47,214,168,70,132,90,223,154,113,255,0,229,220,174,123,139,141,92,224,17,68,72,167,154,62,83,84,108,66,65,37,63,52,36,66,216,117,128,197,39,252,143,16,86,207,96,182,192,254,67,84,129,88,254,27,58,69,227,161,26,191,246,245,201,45,237,130,250,111,233,255,80,178,111,147,133,76,21,184,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("41825f47-7e66-47e3-8ff9-fa62720464d4"));
		}

		#endregion

	}

	#endregion

}

