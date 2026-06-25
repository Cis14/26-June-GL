namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportFindExistsRowColumnProviderSchema

	/// <exclude/>
	public class IFileImportFindExistsRowColumnProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportFindExistsRowColumnProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportFindExistsRowColumnProviderSchema(IFileImportFindExistsRowColumnProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0b5f7147-1048-4e83-b190-46e2217d17c7");
			Name = "IFileImportFindExistsRowColumnProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,84,203,110,219,48,16,60,219,128,255,97,161,92,210,139,117,79,28,1,129,19,23,70,139,34,40,252,3,107,113,101,19,21,73,129,15,63,16,228,223,75,82,15,203,118,34,231,34,72,203,229,204,112,103,40,137,130,76,133,57,193,138,180,70,163,10,59,157,43,89,240,141,211,104,185,146,211,5,47,105,41,42,165,237,100,252,62,25,79,198,163,59,77,27,191,2,243,18,141,121,128,83,195,130,75,246,122,224,198,154,191,106,63,87,165,19,114,41,11,21,55,165,105,10,51,227,132,64,125,204,154,239,55,173,118,156,145,1,238,187,180,136,124,128,107,229,44,240,8,72,12,242,8,51,109,17,210,30,68,229,214,37,207,33,15,50,190,163,98,84,203,191,146,18,11,171,45,129,116,130,180,71,220,97,233,8,84,1,214,23,107,1,94,34,163,195,180,219,222,215,209,10,225,210,66,203,231,155,225,29,54,100,31,193,132,199,199,0,117,45,187,37,154,25,242,164,154,138,167,164,94,168,33,147,52,27,102,239,55,159,127,92,232,24,213,90,238,72,178,218,200,51,83,151,210,146,46,124,32,30,96,57,56,211,198,60,125,219,221,194,201,60,88,139,37,183,71,176,10,4,74,220,116,147,149,33,130,224,3,224,21,162,206,183,92,110,128,2,87,120,209,106,111,128,57,29,222,11,47,167,9,198,80,30,120,123,130,111,31,32,4,227,11,111,94,200,131,9,46,189,66,134,22,215,104,206,116,127,97,73,172,84,168,81,196,174,167,36,239,98,152,100,243,54,80,133,234,121,125,51,191,33,0,179,52,130,158,56,52,89,167,165,105,49,163,164,89,218,86,67,155,177,113,118,63,201,190,172,235,174,63,190,233,254,38,29,156,36,255,120,28,10,175,228,214,52,205,230,202,203,158,145,148,43,205,204,208,192,232,144,83,21,255,1,151,35,121,245,43,196,136,61,239,144,151,184,46,233,23,29,107,169,230,55,23,60,174,199,157,73,182,218,198,200,236,183,36,227,245,21,120,224,194,9,40,67,159,191,211,45,27,182,80,240,143,142,157,126,110,128,26,46,63,199,78,208,231,166,214,81,124,11,149,144,18,147,100,205,77,174,186,210,213,101,238,117,167,89,223,206,157,226,44,14,179,111,211,66,233,211,73,239,47,17,224,82,64,99,212,103,247,251,227,63,204,19,245,31,233,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0b5f7147-1048-4e83-b190-46e2217d17c7"));
		}

		#endregion

	}

	#endregion

}

