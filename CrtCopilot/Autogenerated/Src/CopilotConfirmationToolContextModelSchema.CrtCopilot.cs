namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotConfirmationToolContextModelSchema

	/// <exclude/>
	public class CopilotConfirmationToolContextModelSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotConfirmationToolContextModelSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotConfirmationToolContextModelSchema(CopilotConfirmationToolContextModelSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d2b1d836-f133-45f5-8580-7f0d534f082d");
			Name = "CopilotConfirmationToolContextModel";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,148,81,75,195,48,16,199,159,55,216,119,8,243,101,123,233,7,112,42,72,7,34,56,17,55,124,17,31,178,244,28,145,52,9,201,85,157,99,223,221,75,187,141,174,52,163,19,95,74,46,119,247,107,239,127,127,170,121,14,222,114,1,44,117,192,81,154,36,53,86,42,131,131,254,102,208,239,21,94,234,21,155,175,61,66,78,25,165,64,80,141,246,201,29,104,112,82,76,154,53,207,133,70,153,67,50,167,44,87,242,39,32,53,85,81,221,133,131,21,5,44,85,220,251,75,182,123,79,106,244,187,116,121,89,183,48,70,81,140,240,141,51,147,129,42,219,94,167,28,121,184,117,92,224,27,93,216,98,169,164,96,34,96,186,81,122,97,148,10,52,131,124,9,110,244,72,99,179,107,54,228,213,56,195,113,0,239,201,15,210,227,85,4,124,91,54,148,220,27,86,5,158,109,216,10,112,194,124,120,108,9,171,225,171,51,100,52,14,26,110,43,133,64,103,149,72,103,8,86,131,253,93,175,35,72,92,46,12,210,114,165,238,179,99,197,60,186,224,129,197,33,125,44,73,249,93,81,96,56,71,113,101,225,25,176,41,120,225,164,13,211,180,50,107,249,174,88,203,29,157,16,92,155,77,106,102,123,218,215,237,220,113,136,79,24,36,222,222,221,23,81,70,7,59,156,232,141,187,64,199,22,118,206,178,178,255,95,20,174,109,196,72,148,232,10,249,228,170,104,80,204,242,131,126,123,236,37,100,154,152,214,13,209,221,47,154,120,244,11,88,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d2b1d836-f133-45f5-8580-7f0d534f082d"));
		}

		#endregion

	}

	#endregion

}

