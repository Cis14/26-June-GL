namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: PlatformServiceKnownErrorsSchema

	/// <exclude/>
	public class PlatformServiceKnownErrorsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public PlatformServiceKnownErrorsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public PlatformServiceKnownErrorsSchema(PlatformServiceKnownErrorsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("21cce7d0-88dc-4544-823a-a5510c009480");
			Name = "PlatformServiceKnownErrors";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,213,85,75,111,26,49,16,62,7,137,255,48,162,135,222,224,158,38,145,16,137,90,212,170,66,37,233,53,26,236,1,172,24,123,107,143,161,4,229,191,119,236,93,10,68,77,85,165,77,91,78,43,63,230,155,239,161,241,58,92,80,172,80,17,12,2,15,172,79,122,232,152,102,1,217,120,215,189,97,99,13,27,138,221,171,16,124,136,237,214,166,221,58,233,245,122,112,22,211,98,129,97,125,209,172,251,147,200,1,21,131,178,24,35,76,125,0,158,19,220,57,191,114,64,165,184,187,45,237,61,170,61,139,68,104,163,7,21,104,122,222,249,21,34,221,97,249,118,160,151,65,170,52,177,70,1,30,114,120,159,123,151,107,112,10,245,125,216,192,67,187,245,132,132,75,138,42,152,9,1,54,140,97,53,39,151,87,168,1,149,242,201,49,172,48,130,243,12,51,226,63,43,104,199,246,80,84,173,101,224,147,213,31,61,191,37,238,235,126,205,101,43,109,87,41,85,155,34,239,228,85,160,153,180,129,81,240,21,133,220,231,20,70,5,176,62,127,172,191,108,8,120,44,169,233,226,68,149,137,130,245,10,173,185,199,137,37,16,123,141,155,117,191,3,236,235,222,242,173,239,52,102,22,136,15,234,30,206,47,160,243,164,134,219,189,203,157,55,141,0,114,186,214,80,214,207,74,237,255,137,108,79,236,113,230,245,194,105,101,17,149,69,150,103,99,1,145,194,210,40,138,16,136,83,112,17,146,163,175,21,41,38,45,91,177,242,46,202,161,148,38,135,75,52,54,75,253,139,185,142,26,158,227,134,230,205,142,197,17,13,227,79,84,188,84,192,50,127,156,217,96,226,57,176,191,147,237,169,180,36,253,15,134,178,47,28,200,177,81,200,116,237,183,102,28,219,139,58,94,199,113,109,234,237,80,103,57,188,206,121,82,24,186,165,180,208,159,209,166,223,139,83,6,159,196,182,58,45,237,41,186,215,12,115,92,146,76,226,151,100,130,140,100,84,98,200,143,31,214,3,235,107,168,235,140,116,41,64,18,194,59,193,249,212,192,140,11,202,129,247,219,159,245,195,55,250,104,57,84,164,8,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("21cce7d0-88dc-4544-823a-a5510c009480"));
		}

		#endregion

	}

	#endregion

}

