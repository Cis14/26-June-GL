namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: OpenIdSysAdminUnitEventListenerSchema

	/// <exclude/>
	public class OpenIdSysAdminUnitEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public OpenIdSysAdminUnitEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public OpenIdSysAdminUnitEventListenerSchema(OpenIdSysAdminUnitEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6031c2b2-7861-40a6-9700-b2042b435ba9");
			Name = "OpenIdSysAdminUnitEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("cafc62fc-f7d7-4a5d-acf5-62f836ef940a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,84,193,142,218,48,16,61,131,196,63,76,211,11,72,40,220,89,64,98,17,109,145,150,82,21,216,75,213,131,73,6,226,150,216,145,237,132,210,138,127,223,177,157,32,194,130,168,132,144,60,158,55,239,141,231,77,64,176,20,117,198,34,132,21,42,197,180,220,154,112,34,197,150,239,114,197,12,151,34,92,100,40,102,241,56,55,73,171,249,175,213,108,228,154,139,29,44,143,218,96,250,116,62,95,162,211,84,138,219,55,10,195,169,48,220,112,212,15,19,194,105,129,194,220,207,251,196,34,35,85,89,137,126,31,21,238,72,47,76,246,76,235,62,120,217,36,115,28,167,92,172,5,55,174,222,11,39,221,2,85,171,73,144,31,142,235,88,187,104,47,163,4,83,246,149,222,5,134,16,92,22,8,58,63,255,15,244,122,120,15,203,242,205,158,71,16,89,117,143,196,65,31,158,153,70,79,180,56,80,228,74,123,195,14,194,54,125,238,122,142,38,145,49,245,253,77,73,131,145,193,184,202,200,170,0,20,92,153,156,237,161,144,60,134,73,130,209,239,9,19,235,44,102,6,215,154,154,240,124,160,143,218,30,253,169,3,142,170,81,48,5,57,69,201,27,130,170,89,202,97,61,51,92,215,174,159,206,176,130,237,57,113,72,69,8,55,28,63,185,99,248,25,205,96,230,159,194,97,19,38,118,248,90,101,143,218,29,95,131,111,161,253,225,92,36,36,205,62,211,105,174,107,234,94,137,175,212,59,29,100,31,169,230,168,53,219,217,41,9,60,192,139,140,168,238,95,182,217,227,210,40,50,217,85,189,240,59,106,153,171,136,110,165,34,88,215,87,107,4,15,6,24,116,33,120,87,91,91,233,102,46,99,190,61,90,141,99,162,40,176,20,20,82,223,57,6,157,112,37,75,37,101,247,13,147,40,121,112,106,167,127,34,204,172,172,246,101,43,101,222,201,254,159,206,182,64,17,123,103,220,55,138,51,164,219,131,70,175,215,131,129,206,211,148,169,227,232,11,19,241,30,53,160,183,195,76,144,86,99,23,16,109,139,225,160,87,37,158,145,25,83,44,5,251,41,25,6,154,152,233,1,70,238,61,192,159,8,227,82,110,35,48,24,173,18,36,126,68,136,20,110,135,193,170,127,247,155,224,52,61,227,150,162,142,97,172,118,58,128,222,8,184,208,134,9,250,142,69,82,24,198,133,21,108,18,172,24,157,118,32,7,177,154,152,114,43,101,65,124,60,70,191,26,11,191,20,118,8,114,243,139,156,80,182,209,133,155,244,128,149,205,54,180,180,225,5,186,130,85,51,186,177,115,229,210,117,124,170,207,59,249,161,92,206,144,206,62,90,15,82,12,222,0,155,134,177,79,195,5,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateCantModifyUserActiveMessageLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateCantModifyUserActiveMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("9bf23670-1e6a-e323-1820-4acf68e5b432"),
				Name = "CantModifyUserActiveMessage",
				CreatedInPackageId = new Guid("cafc62fc-f7d7-4a5d-acf5-62f836ef940a"),
				CreatedInSchemaUId = new Guid("6031c2b2-7861-40a6-9700-b2042b435ba9"),
				ModifiedInSchemaUId = new Guid("6031c2b2-7861-40a6-9700-b2042b435ba9")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6031c2b2-7861-40a6-9700-b2042b435ba9"));
		}

		#endregion

	}

	#endregion

}

