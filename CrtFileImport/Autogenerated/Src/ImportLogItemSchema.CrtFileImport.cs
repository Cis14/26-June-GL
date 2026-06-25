namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportLogItemSchema

	/// <exclude/>
	public class ImportLogItemSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportLogItemSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportLogItemSchema(ImportLogItemSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("567fdcd5-bfe6-4534-8033-3043e6eaac3e");
			Name = "ImportLogItem";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1101e5cd-b945-4f88-8001-faccb4fdb24c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,65,107,194,64,16,133,207,17,252,15,131,189,180,151,228,174,85,40,98,65,104,65,208,63,176,166,147,184,52,217,93,102,54,208,32,254,247,206,102,99,155,120,16,111,155,217,199,123,223,155,141,81,53,178,83,57,194,1,137,20,219,194,167,107,107,10,93,54,164,188,182,38,125,215,21,110,107,103,201,79,39,231,233,36,105,88,155,18,246,45,123,172,23,211,137,76,158,8,75,81,194,186,82,204,115,136,226,15,91,110,69,209,9,178,44,131,87,110,234,90,81,187,234,191,223,12,104,195,94,25,137,182,5,248,147,102,200,131,1,200,129,132,201,26,214,199,10,161,176,4,236,45,133,84,221,89,67,101,229,104,228,162,142,136,215,136,108,144,225,154,99,165,243,222,114,132,4,130,120,195,152,156,59,206,255,38,18,238,169,201,37,118,14,187,206,41,10,110,155,116,131,53,161,242,200,227,62,173,67,81,34,66,78,88,44,103,163,192,89,182,74,255,220,134,208,87,234,145,250,249,5,194,222,147,100,239,85,254,125,160,240,88,75,89,73,216,72,186,169,157,111,23,225,250,210,87,64,243,21,91,140,43,237,200,58,36,175,145,31,104,36,201,32,255,5,171,18,239,131,70,10,248,140,218,158,179,68,223,17,37,220,31,46,119,162,54,63,57,186,240,138,226,37,245,192,135,126,15,133,14,214,113,63,247,102,37,113,58,28,202,228,23,109,189,196,160,8,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("567fdcd5-bfe6-4534-8033-3043e6eaac3e"));
		}

		#endregion

	}

	#endregion

}

