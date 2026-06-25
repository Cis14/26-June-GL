namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportParametersIteratorSchema

	/// <exclude/>
	public class ImportParametersIteratorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportParametersIteratorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportParametersIteratorSchema(ImportParametersIteratorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6a556f6f-f364-4e38-8c78-e99a9660b2e1");
			Name = "ImportParametersIterator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,82,219,74,3,49,16,125,78,161,255,48,224,203,22,202,126,128,181,130,84,133,62,8,5,197,247,152,157,221,14,100,147,37,23,161,148,254,187,105,178,219,38,85,124,201,100,38,231,156,185,69,241,30,237,192,5,194,7,26,195,173,110,93,189,209,170,165,206,27,238,72,171,250,149,36,110,251,65,27,55,159,29,231,51,230,45,169,14,222,15,214,97,191,154,207,66,228,206,96,23,144,176,145,220,218,123,72,224,29,55,65,218,161,177,219,112,112,167,77,196,14,254,75,146,0,235,130,184,0,113,102,252,67,96,199,72,186,100,120,67,183,215,77,200,177,139,50,233,177,148,252,214,212,64,82,192,234,86,25,134,203,117,121,166,50,246,36,206,77,62,36,224,139,114,228,14,203,177,160,141,150,190,87,165,247,201,165,199,50,244,140,214,145,138,179,122,4,26,75,95,192,49,234,183,218,32,23,123,168,242,4,64,133,163,178,170,234,24,36,180,147,192,173,66,202,9,34,153,146,155,222,174,84,246,171,242,145,151,238,235,162,142,176,103,213,100,208,42,65,23,171,81,139,90,168,10,246,26,148,151,242,154,140,9,29,132,148,199,137,113,26,237,159,29,100,83,131,38,187,211,212,91,157,33,178,150,216,52,225,138,138,149,137,113,89,34,95,83,38,188,184,169,42,153,120,158,198,63,134,170,73,223,44,250,41,90,6,79,63,56,180,91,209,46,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6a556f6f-f364-4e38-8c78-e99a9660b2e1"));
		}

		#endregion

	}

	#endregion

}

