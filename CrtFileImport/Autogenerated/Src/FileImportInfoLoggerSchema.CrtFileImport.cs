namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportInfoLoggerSchema

	/// <exclude/>
	public class FileImportInfoLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportInfoLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportInfoLoggerSchema(FileImportInfoLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("675e9ab2-9b09-4f89-aa1d-1b5a419d6253");
			Name = "FileImportInfoLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,83,77,107,194,64,16,61,43,248,31,150,180,135,4,74,114,143,85,177,182,74,160,66,193,66,15,165,148,53,153,196,133,100,55,236,110,4,9,254,247,78,62,52,213,166,31,66,122,219,153,121,111,230,189,97,150,211,4,84,74,125,32,207,32,37,85,34,212,246,76,240,144,69,153,164,154,9,110,207,89,12,94,146,10,169,7,253,124,208,239,101,138,241,136,172,118,74,67,50,60,198,81,44,214,52,118,221,153,72,18,36,61,138,40,194,116,83,255,220,93,130,61,167,190,22,146,129,66,4,98,174,36,68,56,139,204,98,170,148,75,154,145,30,15,69,209,11,100,137,123,189,135,144,102,177,190,99,60,192,174,166,222,165,32,66,211,107,35,88,214,27,50,210,108,29,51,159,248,69,227,214,190,196,37,173,116,228,230,229,204,163,184,39,41,82,144,26,69,187,248,102,91,170,161,2,164,85,64,60,100,146,247,184,164,15,191,20,234,113,163,241,1,66,38,19,98,30,222,163,162,190,164,156,98,96,47,64,87,104,211,104,164,77,211,20,120,0,210,176,172,97,45,12,227,74,219,169,208,37,232,141,8,10,149,165,249,170,232,56,14,185,101,124,3,146,233,64,224,70,36,132,35,163,213,186,253,130,24,88,129,82,216,109,234,23,87,176,210,84,234,37,102,80,159,225,140,75,115,213,102,183,130,5,228,103,130,185,200,16,195,202,41,53,200,11,110,136,210,178,184,140,164,2,89,164,56,174,94,175,150,80,168,49,175,141,252,140,181,71,22,54,118,73,94,211,246,134,85,174,122,223,133,203,75,12,254,135,183,174,76,253,201,205,169,141,223,5,31,74,221,173,251,129,7,151,108,188,129,119,186,116,252,68,223,158,211,217,15,171,178,167,73,204,125,0,189,178,186,196,68,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("675e9ab2-9b09-4f89-aa1d-1b5a419d6253"));
		}

		#endregion

	}

	#endregion

}

