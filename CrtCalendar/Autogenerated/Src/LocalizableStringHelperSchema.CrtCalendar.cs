namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LocalizableStringHelperSchema

	/// <exclude/>
	public class LocalizableStringHelperSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LocalizableStringHelperSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LocalizableStringHelperSchema(LocalizableStringHelperSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("372a558a-906c-4891-8361-c65204ffc892");
			Name = "LocalizableStringHelper";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("e9cdff4a-a92d-4d26-906f-df90167f5485");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,83,203,78,195,48,16,60,167,18,255,176,132,75,17,149,115,135,210,11,66,128,4,5,241,60,155,116,91,34,28,59,218,181,139,10,234,191,227,56,73,105,210,0,62,88,222,215,120,118,214,214,50,71,46,100,138,240,136,68,146,205,220,138,51,163,231,217,194,145,180,153,209,123,131,175,189,65,228,56,211,139,86,74,158,27,125,210,27,33,244,126,31,57,32,92,120,0,56,83,146,249,24,174,77,42,85,246,41,95,21,62,88,242,69,151,168,10,164,144,154,36,9,140,217,229,185,164,213,164,182,15,218,11,154,29,26,27,250,18,252,18,13,96,178,133,88,184,87,149,165,192,214,55,149,66,90,82,250,157,81,244,21,88,109,58,184,65,251,102,102,190,135,187,128,82,5,187,156,91,164,119,15,219,150,216,212,39,93,128,113,33,73,230,160,253,88,78,99,199,72,126,24,26,211,114,18,241,164,175,219,114,141,147,80,213,15,194,233,27,230,114,234,207,1,160,42,28,193,47,80,141,245,39,164,234,10,215,65,255,15,139,208,58,210,60,233,228,117,171,154,180,178,174,61,63,14,183,194,5,218,103,169,28,14,159,90,66,65,91,183,81,147,254,163,196,198,213,219,201,33,148,79,62,138,150,146,128,144,157,178,112,90,23,136,243,188,176,171,147,16,206,230,48,220,175,221,87,60,117,74,221,82,8,15,251,81,27,216,168,185,155,151,30,55,222,121,133,44,98,56,234,103,230,253,177,8,45,199,21,135,192,81,177,199,209,248,177,251,160,135,109,37,196,139,161,247,240,219,197,61,178,113,148,250,60,67,114,81,10,178,37,142,103,118,88,227,111,250,87,44,30,77,141,90,7,215,97,175,166,84,11,21,2,235,250,247,160,158,85,31,40,216,149,183,237,92,127,3,218,41,142,224,127,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("372a558a-906c-4891-8361-c65204ffc892"));
		}

		#endregion

	}

	#endregion

}

