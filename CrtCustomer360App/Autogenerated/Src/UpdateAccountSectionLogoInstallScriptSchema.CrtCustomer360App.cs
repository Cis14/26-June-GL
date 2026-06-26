namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateAccountSectionLogoInstallScriptSchema

	/// <exclude/>
	public class UpdateAccountSectionLogoInstallScriptSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateAccountSectionLogoInstallScriptSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateAccountSectionLogoInstallScriptSchema(UpdateAccountSectionLogoInstallScriptSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9205c831-ef23-4ca9-a32c-7d0e0526bf68");
			Name = "UpdateAccountSectionLogoInstallScript";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9c9dce71-61f1-4751-aabc-14d22fc356f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,146,75,143,155,48,20,133,215,228,87,184,172,136,52,70,129,240,72,154,182,82,204,99,20,169,211,13,51,221,86,46,220,100,144,192,166,126,164,69,51,249,239,3,33,207,105,211,5,150,124,125,253,157,227,115,97,180,6,217,208,28,208,35,8,65,37,95,43,59,226,108,93,110,180,160,170,228,108,244,50,50,180,44,217,6,101,173,84,80,47,142,219,203,126,1,55,202,118,194,84,169,74,144,139,209,200,104,244,207,170,204,81,94,81,41,209,83,83,80,5,203,60,231,154,169,12,242,94,235,43,223,240,21,147,138,86,85,150,139,178,81,232,35,90,93,21,146,63,144,107,197,5,122,233,120,70,35,202,109,7,65,2,104,193,89,213,162,123,93,22,232,7,61,64,91,249,192,11,93,193,170,64,159,17,131,223,251,99,203,76,18,127,22,164,51,31,251,137,235,96,207,245,66,60,95,18,23,59,241,124,18,164,147,116,66,188,192,28,47,110,243,121,85,28,124,239,13,95,211,93,103,58,159,7,211,16,39,179,48,197,222,36,136,240,50,156,18,28,122,233,50,117,211,216,33,75,247,191,116,122,19,29,147,185,239,78,227,16,79,146,128,96,207,39,17,38,238,44,194,177,227,68,169,239,132,142,75,246,198,123,246,16,245,150,119,196,33,51,176,158,36,136,110,180,108,200,26,233,171,237,184,75,212,48,140,253,184,90,36,143,209,117,250,215,125,195,64,219,44,127,134,154,62,80,70,55,32,236,123,80,67,153,180,223,186,255,201,50,79,209,155,119,239,117,250,151,27,229,26,89,31,78,34,118,10,42,127,78,5,175,99,98,253,99,122,227,131,57,67,128,210,130,237,9,187,126,217,82,129,170,99,78,103,92,103,231,177,109,160,136,120,165,107,246,157,86,26,62,245,9,126,177,204,33,85,243,236,98,184,110,39,191,52,173,164,213,119,217,73,221,168,118,140,94,95,209,245,225,95,115,63,249,58,75,103,160,46,84,79,122,119,239,198,58,232,95,222,163,91,176,214,157,10,140,79,207,235,190,221,104,247,6,175,125,60,167,161,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9205c831-ef23-4ca9-a32c-7d0e0526bf68"));
		}

		#endregion

	}

	#endregion

}

