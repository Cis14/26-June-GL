namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TranslationFeaturesSchema

	/// <exclude/>
	public class TranslationFeaturesSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TranslationFeaturesSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TranslationFeaturesSchema(TranslationFeaturesSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("938809f8-84fc-451d-9e71-df89a708b3a6");
			Name = "TranslationFeatures";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("0b3cc894-bd0d-4e1b-bb7d-87203708d013");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,148,207,106,194,64,16,198,239,130,239,48,216,75,123,241,1,20,15,37,42,244,80,144,86,31,96,204,78,98,96,187,155,206,236,98,173,248,238,221,252,211,136,77,201,161,230,146,48,59,51,223,183,223,15,98,240,131,36,199,152,96,77,204,40,54,113,227,200,154,36,75,61,163,203,172,25,175,25,141,232,242,123,56,56,14,7,16,30,47,153,73,33,98,42,202,227,101,120,121,166,181,77,83,29,234,211,225,160,234,122,96,74,195,20,68,26,69,38,176,17,90,24,151,185,195,220,115,104,107,237,125,142,157,71,157,125,215,34,213,116,102,28,177,65,13,113,49,222,119,26,38,80,219,121,37,135,10,29,86,219,142,205,218,43,99,214,136,99,31,59,203,193,223,202,111,117,22,183,251,242,178,210,87,250,241,9,142,151,217,226,121,145,133,193,173,38,5,51,72,80,11,77,175,207,231,36,49,103,121,233,123,6,163,170,89,192,237,40,36,76,96,19,64,3,84,74,135,56,196,17,170,178,8,159,158,248,0,251,29,25,192,218,66,1,196,93,156,201,120,212,18,59,93,221,158,140,170,2,104,170,231,227,219,163,91,134,239,33,45,82,43,182,49,169,16,243,210,114,164,9,77,208,223,152,224,90,189,145,88,207,49,73,55,200,222,43,238,68,179,183,254,61,144,74,41,14,121,163,14,137,229,16,77,165,15,190,52,208,38,9,220,184,185,15,210,202,98,20,76,157,175,189,248,202,45,187,14,126,157,253,247,128,213,41,246,55,153,176,185,39,24,101,247,70,91,84,69,246,1,206,175,185,151,132,234,159,29,20,94,254,21,201,233,7,127,204,176,17,131,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("938809f8-84fc-451d-9e71-df89a708b3a6"));
		}

		#endregion

	}

	#endregion

}

