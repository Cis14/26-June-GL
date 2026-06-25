namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DeleteOAuth20FeatureIntegrationInstallScriptSchema

	/// <exclude/>
	public class DeleteOAuth20FeatureIntegrationInstallScriptSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DeleteOAuth20FeatureIntegrationInstallScriptSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DeleteOAuth20FeatureIntegrationInstallScriptSchema(DeleteOAuth20FeatureIntegrationInstallScriptSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b1374130-94f3-4d9c-9123-d4180791100b");
			Name = "DeleteOAuth20FeatureIntegrationInstallScript";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a6ded360-42cd-4008-9952-fcaf8207688b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,83,77,111,219,48,12,61,187,64,255,3,231,93,92,160,176,135,29,155,15,160,73,154,33,135,110,3,178,253,0,85,166,109,13,182,100,72,114,54,35,200,127,31,35,217,93,108,56,88,123,20,249,248,248,72,62,129,100,21,154,154,113,132,31,168,53,51,42,179,241,90,201,76,228,141,102,86,40,121,123,115,188,189,9,26,35,100,14,251,214,88,172,102,163,55,225,203,18,249,25,108,226,47,40,81,11,254,15,115,73,171,241,90,60,222,172,174,166,158,164,21,86,160,33,0,65,62,106,204,169,19,172,75,102,204,3,108,176,68,139,223,30,27,91,124,254,180,69,102,27,141,59,105,49,247,234,119,210,88,86,150,123,174,69,109,93,125,221,188,148,130,3,63,151,191,171,26,30,96,55,8,60,253,65,222,88,165,137,244,232,168,95,181,61,163,45,84,74,234,190,107,113,96,22,125,182,246,15,32,10,75,10,14,74,164,255,19,208,69,162,159,6,53,93,69,250,45,67,51,120,222,193,249,66,65,224,246,212,238,121,129,21,123,102,146,229,168,1,39,98,139,81,125,60,81,56,187,96,132,204,171,160,194,9,58,186,184,245,184,85,251,149,204,20,133,157,232,240,126,172,211,147,30,88,47,139,82,169,112,3,45,64,226,111,216,8,135,99,186,157,27,171,201,9,247,160,94,126,81,241,178,155,48,56,66,184,86,233,153,58,236,118,118,177,172,16,78,14,117,242,125,68,6,209,135,78,122,188,69,203,139,173,86,213,102,21,141,154,223,245,251,11,64,35,129,229,236,146,165,39,240,135,138,252,12,167,238,220,40,83,127,241,107,231,119,94,243,201,36,73,96,110,154,170,162,241,150,125,192,91,136,44,225,29,150,41,13,169,107,52,61,95,47,230,149,47,25,19,206,107,166,89,5,231,95,189,8,135,235,15,151,206,189,146,190,186,202,192,22,72,114,16,129,107,204,22,225,208,96,97,178,4,219,214,24,207,19,199,231,232,187,143,227,108,219,233,126,155,47,223,232,241,41,179,76,47,218,71,135,65,138,253,5,246,166,153,37,204,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b1374130-94f3-4d9c-9123-d4180791100b"));
		}

		#endregion

	}

	#endregion

}

