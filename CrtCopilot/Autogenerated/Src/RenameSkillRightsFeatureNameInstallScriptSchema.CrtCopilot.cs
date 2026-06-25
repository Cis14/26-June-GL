namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RenameSkillRightsFeatureNameInstallScriptSchema

	/// <exclude/>
	public class RenameSkillRightsFeatureNameInstallScriptSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RenameSkillRightsFeatureNameInstallScriptSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RenameSkillRightsFeatureNameInstallScriptSchema(RenameSkillRightsFeatureNameInstallScriptSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a4adad64-4377-4369-8371-d8e772ce145f");
			Name = "RenameSkillRightsFeatureNameInstallScript";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,83,203,110,219,48,16,60,43,64,254,97,161,92,20,192,80,238,113,91,192,241,35,240,33,73,17,53,189,211,212,74,102,67,145,2,73,57,21,12,255,123,151,164,210,88,105,141,62,142,154,221,157,157,29,142,20,107,208,182,140,35,124,65,99,152,213,149,203,231,90,85,162,238,12,115,66,171,243,179,253,249,89,210,89,161,106,40,122,235,176,161,186,148,200,125,209,230,183,168,208,8,62,253,217,115,76,99,240,20,158,47,149,19,78,160,165,6,106,185,48,88,19,29,204,37,179,246,26,30,81,145,172,226,89,72,249,40,234,173,179,43,100,174,51,120,79,232,90,89,199,164,44,184,17,173,11,195,109,183,145,130,3,247,179,127,63,10,215,176,30,1,203,239,200,59,167,13,49,238,3,239,155,42,237,251,20,9,251,108,196,142,57,140,229,54,126,0,247,101,176,206,248,51,31,100,121,180,17,62,66,74,14,205,214,3,102,243,39,27,197,21,124,139,13,123,104,49,186,28,181,166,211,147,188,247,248,242,39,222,181,114,168,220,255,19,47,208,6,35,252,201,196,191,84,108,35,17,102,156,163,247,53,240,64,165,13,4,253,246,106,86,211,54,155,167,211,193,44,84,101,244,107,108,222,29,186,173,46,189,119,225,153,6,235,226,147,237,180,40,33,250,142,25,93,96,200,105,21,147,5,221,232,243,18,124,10,147,36,196,166,135,42,42,38,153,227,182,24,171,62,122,112,199,20,171,209,80,68,93,132,111,122,239,93,150,14,247,166,147,247,75,130,75,201,142,25,111,81,41,66,194,105,135,194,23,88,136,208,194,76,255,33,26,55,1,189,249,70,115,159,6,101,201,30,210,185,46,61,235,187,12,28,66,253,16,201,69,5,217,160,62,95,161,227,219,149,209,205,226,38,123,91,56,129,138,73,139,151,175,39,39,175,237,5,58,250,243,186,70,125,101,178,163,51,60,57,109,27,39,99,184,225,228,208,32,241,223,134,142,146,49,154,61,194,127,161,96,59,204,226,33,177,18,92,56,252,54,43,17,29,131,132,253,0,153,70,229,4,155,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a4adad64-4377-4369-8371-d8e772ce145f"));
		}

		#endregion

	}

	#endregion

}

