namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateOAuthClientAppGrantTypeInstallScriptExecutorSchema

	/// <exclude/>
	public class UpdateOAuthClientAppGrantTypeInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateOAuthClientAppGrantTypeInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateOAuthClientAppGrantTypeInstallScriptExecutorSchema(UpdateOAuthClientAppGrantTypeInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("58af1224-1323-4f86-b652-b80b8004e3ab");
			Name = "UpdateOAuthClientAppGrantTypeInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a6ded360-42cd-4008-9952-fcaf8207688b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,223,79,194,48,16,199,159,103,194,255,112,25,47,91,2,11,6,68,209,24,163,160,134,7,127,36,96,124,238,186,27,44,169,237,114,109,81,99,248,223,237,58,136,204,31,193,167,230,238,122,247,253,92,191,5,201,94,80,151,140,35,204,145,136,105,149,155,100,172,100,94,44,44,49,83,40,217,58,248,104,29,4,86,23,114,209,184,66,120,246,71,62,153,92,185,146,43,150,54,21,5,7,46,152,214,240,84,102,204,224,195,165,53,203,177,40,80,154,203,178,188,37,38,205,252,189,196,169,212,134,9,49,227,84,148,230,250,13,185,53,138,224,20,166,191,22,220,232,138,41,104,19,46,28,33,220,161,89,170,76,159,194,35,21,43,39,226,197,131,178,14,96,165,138,108,163,254,165,151,221,40,106,160,68,79,26,201,45,46,145,87,91,131,109,132,49,120,193,128,43,199,3,218,80,181,181,187,177,66,154,171,153,63,39,152,51,43,204,142,4,156,67,120,220,207,123,156,15,179,110,111,216,31,116,7,89,218,239,142,134,44,237,246,120,122,52,24,141,142,78,242,209,97,120,230,103,175,24,129,245,152,174,81,226,235,134,57,106,146,116,32,108,190,97,24,251,238,32,153,161,137,194,29,249,176,3,99,37,236,139,76,30,25,57,151,13,82,180,15,57,222,14,123,94,34,97,115,92,156,76,245,189,21,34,138,129,109,237,172,201,107,234,139,164,246,7,163,216,167,215,181,13,109,148,89,237,211,38,254,97,154,255,37,27,207,234,31,227,45,219,78,251,151,49,251,253,253,214,247,23,98,149,91,127,2,231,254,118,151,22,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("58af1224-1323-4f86-b652-b80b8004e3ab"));
		}

		#endregion

	}

	#endregion

}

