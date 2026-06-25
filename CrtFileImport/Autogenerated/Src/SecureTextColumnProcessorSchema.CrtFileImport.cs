namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SecureTextColumnProcessorSchema

	/// <exclude/>
	public class SecureTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SecureTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SecureTextColumnProcessorSchema(SecureTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("327ab3d6-e22f-4c90-99c4-1e825c0a5c40");
			Name = "SecureTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,81,75,235,64,16,133,159,43,248,31,134,250,210,130,36,239,181,45,104,69,41,200,69,176,245,69,238,195,118,51,169,11,201,108,156,221,149,91,196,255,238,100,211,168,233,109,4,159,178,59,156,57,103,231,219,13,169,18,93,165,52,194,10,153,149,179,185,79,22,150,114,179,13,172,188,177,148,220,152,2,151,101,101,217,159,158,188,157,158,12,130,51,180,133,135,157,243,88,94,124,238,191,119,51,246,213,147,27,165,189,101,131,78,20,162,57,99,220,74,6,44,10,229,220,4,30,80,7,198,21,254,243,11,91,132,146,238,217,106,116,206,114,20,167,105,10,83,67,207,200,198,103,86,131,102,204,103,195,35,234,97,58,111,229,46,148,165,226,93,187,191,36,48,228,188,34,153,215,230,224,159,141,3,93,103,131,44,88,64,88,114,102,83,32,228,150,161,106,252,234,41,238,44,109,235,32,208,49,9,94,85,17,208,37,109,74,250,45,230,233,26,115,21,10,127,101,40,147,214,145,223,85,104,243,209,242,224,140,227,115,248,35,232,97,6,36,31,17,244,206,62,30,255,21,219,42,108,10,163,247,135,237,213,194,4,142,210,27,188,69,130,95,188,101,78,207,161,190,11,193,126,31,189,27,197,47,33,255,71,57,22,22,140,202,163,235,178,22,14,162,68,220,91,246,206,32,198,201,167,115,122,104,61,173,20,171,50,66,155,13,131,67,150,81,8,117,253,82,135,243,181,236,229,138,218,66,50,77,163,58,54,239,1,246,198,142,214,29,51,232,122,143,133,236,70,57,28,29,150,235,63,98,240,190,167,139,148,53,128,187,180,37,163,66,246,242,234,39,245,218,75,47,102,63,225,190,146,164,95,224,190,86,94,53,79,178,161,28,200,188,200,218,100,72,222,228,6,185,135,103,213,158,5,236,171,252,166,162,135,219,96,178,232,247,88,219,173,196,109,189,204,96,54,239,214,146,47,138,135,218,139,163,40,26,64,221,226,251,7,184,92,121,190,123,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("327ab3d6-e22f-4c90-99c4-1e825c0a5c40"));
		}

		#endregion

	}

	#endregion

}

