namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TranslationServiceResponseSchema

	/// <exclude/>
	public class TranslationServiceResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TranslationServiceResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TranslationServiceResponseSchema(TranslationServiceResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5fe50c86-213a-4ba1-9a12-4686b15dab3c");
			Name = "TranslationServiceResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2c48a34f-0c95-44d9-a69e-4bfc769a17b3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,201,110,194,48,16,61,7,137,127,176,232,133,92,242,1,208,246,194,82,85,42,85,5,220,170,170,114,204,144,90,114,236,104,108,211,5,245,223,59,89,64,9,148,8,196,205,30,207,91,230,121,52,79,193,102,92,0,91,2,34,183,102,237,162,145,209,107,153,120,228,78,26,29,45,145,107,171,138,115,183,179,237,118,2,111,165,78,216,226,219,58,72,135,7,119,194,42,5,34,111,182,209,3,104,64,41,142,122,230,94,59,153,66,180,160,87,174,228,79,193,77,93,212,119,131,144,208,133,141,20,183,118,192,106,218,212,188,145,2,230,228,150,184,161,232,126,29,115,199,201,173,67,46,220,27,21,50,31,43,41,152,200,209,45,96,54,96,141,25,143,184,131,109,193,191,183,51,149,160,86,228,231,5,229,134,187,82,188,84,159,65,26,3,246,159,41,70,118,199,122,38,131,146,242,201,36,189,48,183,20,100,37,134,61,78,180,79,233,53,86,112,107,29,82,30,247,236,189,222,63,172,52,65,175,74,217,166,7,114,76,48,47,156,193,67,39,59,137,211,19,247,67,150,127,93,240,123,54,96,242,37,32,203,31,24,132,148,87,204,169,6,23,179,252,55,116,125,230,138,48,104,4,65,65,30,228,178,151,60,21,206,12,220,135,41,126,168,216,128,202,95,185,13,214,17,147,104,91,135,17,2,205,177,240,66,128,181,84,245,202,237,2,11,16,156,71,205,52,124,182,198,91,55,121,149,238,185,137,157,231,171,129,188,194,227,148,75,229,17,42,143,141,221,184,196,14,132,45,159,89,86,155,69,170,253,1,217,60,246,192,162,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5fe50c86-213a-4ba1-9a12-4686b15dab3c"));
		}

		#endregion

	}

	#endregion

}

