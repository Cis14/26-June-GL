namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: PhoneTextColumnProcessorSchema

	/// <exclude/>
	public class PhoneTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public PhoneTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public PhoneTextColumnProcessorSchema(PhoneTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("91251571-ba36-4a68-9430-619bc29975f7");
			Name = "PhoneTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,139,219,48,16,133,207,89,216,255,48,164,151,4,138,125,207,38,129,110,150,45,129,82,22,154,244,82,122,80,228,113,34,176,71,238,72,90,26,150,254,247,142,228,56,187,78,227,194,158,44,13,111,222,211,124,146,73,213,232,26,165,17,54,200,172,156,45,125,182,178,84,154,125,96,229,141,165,236,209,84,184,174,27,203,254,246,230,229,246,102,20,156,161,61,124,59,58,143,245,221,121,255,182,155,113,168,158,61,42,237,45,27,116,162,16,205,7,198,189,100,192,170,82,206,205,224,233,96,9,55,248,219,175,108,21,106,122,98,171,209,57,203,73,155,231,57,204,13,29,144,141,47,172,6,205,88,46,198,87,212,227,124,217,201,93,168,107,197,199,110,255,137,192,144,243,138,100,92,91,130,63,24,7,58,70,131,44,88,56,88,114,102,87,33,148,150,161,105,253,226,16,95,44,237,99,16,232,148,4,207,170,10,232,178,46,37,127,19,243,227,1,75,21,42,127,111,168,144,214,137,63,54,104,203,201,250,226,140,211,143,240,85,200,195,2,72,62,34,24,26,125,58,253,41,174,77,216,85,70,159,206,58,36,133,25,92,101,55,122,73,252,94,97,203,148,158,67,188,136,200,60,89,183,138,119,34,254,135,113,42,172,24,149,71,215,39,45,20,68,137,120,178,28,26,65,124,179,179,113,126,233,60,111,20,171,58,17,91,140,131,67,150,73,8,117,124,165,227,229,86,246,114,63,93,33,155,231,73,157,154,79,248,134,82,39,219,158,23,244,173,167,194,117,167,28,78,46,203,241,103,24,253,57,177,69,42,90,188,125,214,146,209,32,123,121,240,179,184,246,210,139,197,255,96,223,75,210,59,96,63,40,175,218,231,216,50,14,100,126,201,218,20,72,222,148,6,121,0,103,211,157,5,236,179,252,161,162,135,207,193,20,201,239,123,180,219,136,219,118,93,192,98,217,175,101,103,136,151,210,187,171,36,90,62,253,162,212,254,2,234,229,177,13,119,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("91251571-ba36-4a68-9430-619bc29975f7"));
		}

		#endregion

	}

	#endregion

}

