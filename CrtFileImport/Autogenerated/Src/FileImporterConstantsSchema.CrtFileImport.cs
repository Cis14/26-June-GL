namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImporterConstantsSchema

	/// <exclude/>
	public class FileImporterConstantsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImporterConstantsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImporterConstantsSchema(FileImporterConstantsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1dc2dd32-8b13-47cd-be30-3ed1dbba2572");
			Name = "FileImporterConstants";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,144,193,10,131,48,12,134,207,10,190,67,97,119,113,135,93,148,157,10,131,29,6,142,237,5,58,141,174,80,91,105,90,152,136,239,190,172,50,196,193,6,59,133,252,249,255,47,33,90,116,128,189,168,128,93,193,90,129,166,113,41,55,186,145,173,183,194,73,163,211,131,84,112,236,122,99,93,18,143,73,28,121,148,186,101,151,1,29,116,69,18,147,178,177,208,146,147,113,37,16,115,182,4,192,18,10,157,208,14,131,177,247,55,37,43,70,138,163,82,189,236,223,220,209,24,18,11,251,61,202,89,25,40,243,120,77,148,218,177,147,120,156,61,216,129,184,68,68,110,60,137,123,182,203,178,226,119,160,20,150,126,241,95,134,223,165,170,63,54,109,67,42,220,14,186,158,207,15,253,52,63,107,37,78,79,72,157,138,60,128,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1dc2dd32-8b13-47cd-be30-3ed1dbba2572"));
		}

		#endregion

	}

	#endregion

}

