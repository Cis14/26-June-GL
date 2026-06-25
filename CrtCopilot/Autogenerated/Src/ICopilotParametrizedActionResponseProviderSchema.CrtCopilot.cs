namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotParametrizedActionResponseProviderSchema

	/// <exclude/>
	public class ICopilotParametrizedActionResponseProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotParametrizedActionResponseProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotParametrizedActionResponseProviderSchema(ICopilotParametrizedActionResponseProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0f550306-ffed-4a2f-96af-6009daaa7f5c");
			Name = "ICopilotParametrizedActionResponseProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c57b9697-4890-481a-8f98-2e9a2e48aaa1");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,221,84,77,107,227,64,12,61,39,144,255,32,210,203,22,130,125,111,93,67,200,178,37,176,165,33,251,113,159,218,114,50,139,51,99,52,114,217,108,232,127,95,121,252,209,216,77,104,11,61,21,140,241,200,239,61,73,79,178,141,218,161,43,84,130,176,32,84,172,109,176,176,133,206,45,79,198,135,201,120,84,58,109,54,240,99,239,24,119,215,131,179,32,243,28,19,225,24,23,220,162,65,210,201,51,102,32,23,220,33,171,84,177,122,70,252,68,34,229,108,198,130,33,172,226,114,93,16,110,68,16,150,134,145,50,169,235,10,150,141,196,74,145,20,203,164,255,97,58,247,105,215,82,186,36,199,21,217,71,157,34,77,198,162,16,134,33,68,174,220,237,20,237,227,230,220,0,28,240,22,129,26,22,216,204,159,139,90,23,189,48,40,175,12,248,23,147,178,122,10,90,201,240,72,179,40,31,114,157,128,110,139,124,87,141,163,131,175,179,107,85,156,217,218,212,93,193,202,171,214,47,135,93,248,192,26,185,36,227,78,84,221,246,20,116,220,112,72,142,60,3,140,176,110,166,165,67,90,88,99,234,241,77,227,185,145,102,28,43,147,116,182,68,14,17,18,194,236,102,250,171,143,14,99,224,125,129,65,20,122,201,211,25,106,31,191,162,75,72,23,108,233,181,28,141,129,243,33,237,77,201,58,51,126,171,188,196,91,100,121,156,198,223,74,83,15,147,183,138,197,162,19,230,193,99,69,120,41,222,128,227,118,122,224,100,166,102,35,192,246,77,5,173,131,32,249,86,199,211,104,73,95,250,190,65,223,244,25,84,18,163,51,125,195,208,191,25,84,237,68,131,69,67,170,190,171,165,201,236,12,236,195,31,81,142,225,148,25,151,215,159,100,173,186,250,220,107,234,203,53,170,244,222,228,251,239,218,241,225,172,111,79,159,126,195,122,70,156,95,160,163,197,113,31,179,108,23,104,210,250,31,231,207,79,254,222,15,74,236,63,72,159,142,93,130,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0f550306-ffed-4a2f-96af-6009daaa7f5c"));
		}

		#endregion

	}

	#endregion

}

