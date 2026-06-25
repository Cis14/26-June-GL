namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImporterFactorySchema

	/// <exclude/>
	public class FileImporterFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImporterFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImporterFactorySchema(FileImporterFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a32b6e83-4c3b-c886-59ec-834898926b24");
			Name = "FileImporterFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,77,79,195,48,12,134,207,155,180,255,96,245,148,74,83,123,103,31,18,27,12,245,130,56,192,9,113,200,58,103,68,106,147,202,73,64,19,218,127,199,25,171,182,142,78,130,67,63,236,188,126,252,198,54,178,70,215,200,18,225,25,137,164,179,202,103,75,107,148,222,6,146,94,91,147,173,116,133,69,221,88,242,163,225,215,104,56,8,78,155,109,71,77,56,185,146,207,86,178,244,150,52,58,86,176,38,207,115,152,106,243,142,164,253,198,150,80,18,170,89,82,156,90,32,253,84,236,146,124,222,234,93,168,107,73,187,54,102,97,133,53,26,239,64,27,46,80,209,188,178,20,97,210,243,47,195,64,31,105,45,35,63,131,188,222,161,146,161,242,11,109,54,108,89,248,93,131,86,137,62,23,233,24,30,121,64,48,3,195,31,22,245,105,210,55,134,54,97,93,105,190,80,37,157,131,30,21,220,64,95,3,174,140,51,253,247,96,218,126,197,66,58,60,87,193,3,250,243,88,188,56,36,94,168,193,50,110,19,66,39,76,225,208,125,240,33,233,226,228,150,182,46,94,27,63,129,147,206,83,136,221,57,27,226,232,69,210,85,39,227,75,240,228,192,213,10,68,247,32,99,127,133,91,241,166,2,225,189,145,235,10,55,34,97,147,79,72,78,59,207,240,147,253,36,109,13,14,8,185,192,192,50,206,247,56,137,136,154,22,125,117,72,115,241,251,58,71,79,251,195,251,42,239,111,148,8,225,103,255,13,183,183,9,33,63,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a32b6e83-4c3b-c886-59ec-834898926b24"));
		}

		#endregion

	}

	#endregion

}

