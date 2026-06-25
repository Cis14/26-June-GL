namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportFeaturesSchema

	/// <exclude/>
	public class FileImportFeaturesSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportFeaturesSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportFeaturesSchema(FileImportFeaturesSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("faca2563-4987-469f-bdd0-d33f34d766b5");
			Name = "FileImportFeatures";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,81,205,106,195,48,12,62,39,208,119,16,221,101,187,228,1,86,118,216,210,21,122,24,27,44,47,224,38,74,106,112,108,35,203,236,16,250,238,147,227,140,146,50,232,201,240,233,251,147,12,86,141,24,188,106,17,26,36,82,193,245,92,213,206,246,122,136,164,88,59,91,29,180,193,227,232,29,241,166,156,54,101,17,131,182,3,212,132,105,92,29,228,137,132,141,27,6,35,248,110,83,10,229,129,112,16,41,212,70,133,240,12,87,135,133,29,102,150,143,39,163,91,104,19,233,95,78,49,205,188,91,187,218,160,178,111,177,239,145,176,203,154,70,157,12,126,218,87,239,191,89,165,162,73,182,242,191,47,2,41,154,163,63,144,85,167,88,37,147,165,194,181,131,179,129,41,182,236,72,170,124,205,17,11,101,201,187,159,244,248,4,211,172,40,142,225,221,166,89,7,47,32,174,184,203,240,30,67,75,218,167,243,203,96,187,71,70,26,181,197,0,63,103,228,51,18,176,147,189,36,7,214,65,192,201,13,68,166,188,151,50,243,15,66,72,161,219,236,125,249,219,7,109,151,87,202,192,130,223,192,25,93,131,151,95,234,111,115,122,52,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("faca2563-4987-469f-bdd0-d33f34d766b5"));
		}

		#endregion

	}

	#endregion

}

