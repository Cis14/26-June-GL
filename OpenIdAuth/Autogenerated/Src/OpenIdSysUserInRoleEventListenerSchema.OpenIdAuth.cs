namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: OpenIdSysUserInRoleEventListenerSchema

	/// <exclude/>
	public class OpenIdSysUserInRoleEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public OpenIdSysUserInRoleEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public OpenIdSysUserInRoleEventListenerSchema(OpenIdSysUserInRoleEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1924c96a-4424-4b42-8958-8df3c4ce8fee");
			Name = "OpenIdSysUserInRoleEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5daf09f1-167a-4d95-90ab-547ed370e530");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,229,85,193,110,26,49,16,61,131,148,127,152,110,47,139,132,150,123,66,144,18,74,83,164,208,84,9,201,165,234,193,172,7,214,237,174,189,178,189,164,180,226,223,59,182,119,55,144,130,144,210,99,37,132,132,61,111,230,189,153,231,65,178,2,77,201,82,132,57,106,205,140,90,218,100,172,228,82,172,42,205,172,80,50,185,43,81,78,249,85,101,179,179,238,239,179,110,167,50,66,174,224,97,99,44,22,23,237,239,93,116,81,40,121,248,70,99,50,145,86,88,129,230,100,64,50,89,163,180,199,227,62,178,212,42,93,103,162,207,123,141,43,226,11,227,156,25,115,14,129,54,209,124,52,168,167,242,94,229,232,19,222,10,34,46,81,159,117,9,243,213,23,219,236,93,196,15,105,134,5,251,76,141,129,75,136,246,50,68,189,111,132,42,171,69,46,82,72,93,161,147,117,224,28,174,153,193,80,232,238,153,78,94,209,232,184,166,58,1,173,130,25,218,76,113,210,240,69,43,139,169,69,222,68,148,205,1,172,133,182,21,203,97,173,4,135,113,134,233,143,49,147,51,197,197,114,227,40,204,176,88,144,148,80,21,204,30,59,127,214,3,95,182,179,102,26,42,119,199,73,236,129,184,228,6,237,124,83,34,31,171,188,42,228,19,203,43,28,222,84,130,143,226,182,53,60,234,93,180,201,52,65,223,152,236,222,67,119,147,57,102,228,70,73,154,93,99,14,39,125,220,11,122,1,175,89,46,56,35,143,16,206,155,34,56,198,179,24,78,195,220,92,154,113,198,228,10,159,154,232,81,92,19,16,75,136,223,181,73,18,234,111,136,124,33,16,239,243,235,215,157,236,215,77,232,53,77,246,116,200,189,74,207,208,24,182,114,198,146,248,12,183,42,165,244,191,216,34,199,7,171,201,227,175,18,38,247,104,84,165,83,186,85,154,96,125,8,233,58,209,41,215,69,125,136,254,202,110,156,6,27,76,114,149,23,193,39,158,79,226,71,17,245,146,185,170,137,212,61,232,216,76,171,103,79,118,242,51,197,210,177,138,119,149,212,113,91,247,189,109,141,140,146,7,47,31,183,182,127,67,254,17,118,6,131,1,12,77,85,20,76,111,70,159,152,228,57,26,192,96,221,169,36,129,214,61,127,116,250,146,225,160,9,108,145,37,211,172,0,73,239,245,50,50,84,153,212,143,124,51,32,252,34,140,15,57,140,192,104,52,207,144,234,35,66,170,113,121,25,205,207,143,110,36,207,233,26,151,116,234,43,92,233,149,137,96,48,2,33,141,101,146,182,104,170,164,101,66,58,194,54,195,166,162,231,14,228,35,182,71,166,94,36,106,77,245,4,199,240,152,239,100,171,57,86,139,239,228,132,90,71,31,14,214,7,108,108,182,160,61,147,236,194,27,92,51,165,163,123,162,94,20,189,0,8,209,219,211,195,249,128,57,254,103,179,105,36,191,109,52,45,250,95,39,115,228,157,109,235,191,194,157,115,58,130,63,189,143,248,234,229,7,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateCantModifyAlmRoleMessageLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateCantModifyAlmRoleMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("0ce50dac-8e62-7139-ffcb-c63dc6a1c41e"),
				Name = "CantModifyAlmRoleMessage",
				CreatedInPackageId = new Guid("5daf09f1-167a-4d95-90ab-547ed370e530"),
				CreatedInSchemaUId = new Guid("1924c96a-4424-4b42-8958-8df3c4ce8fee"),
				ModifiedInSchemaUId = new Guid("1924c96a-4424-4b42-8958-8df3c4ce8fee")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1924c96a-4424-4b42-8958-8df3c4ce8fee"));
		}

		#endregion

	}

	#endregion

}

