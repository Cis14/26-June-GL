namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportInfoLoggerSchema

	/// <exclude/>
	public class IFileImportInfoLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportInfoLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportInfoLoggerSchema(IFileImportInfoLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8ae8c7f3-cc95-4f57-90f0-6d91f024f280");
			Name = "IFileImportInfoLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,213,84,209,74,195,48,20,125,94,161,255,112,233,94,20,164,125,223,102,65,68,165,224,64,168,224,115,108,111,99,96,73,202,77,42,140,177,127,55,77,186,234,134,123,216,244,101,143,185,57,231,220,115,238,37,81,76,162,105,89,133,240,138,68,204,232,198,166,247,90,53,130,119,196,172,208,42,125,20,43,44,100,171,201,198,209,38,142,38,157,17,138,67,185,54,22,229,60,142,92,101,74,200,29,18,10,101,145,26,167,53,131,226,155,85,168,70,63,107,206,145,60,184,237,222,87,162,2,177,195,30,131,78,54,30,62,138,47,209,126,232,218,204,224,197,11,132,203,44,203,96,97,58,41,25,173,243,93,225,141,132,69,48,104,76,207,99,85,159,2,140,101,100,193,101,53,140,99,58,114,179,67,242,162,101,196,36,40,55,150,219,68,120,87,101,80,42,234,36,135,224,115,20,23,117,186,200,60,227,119,129,161,95,146,187,84,99,243,159,140,79,45,234,96,120,232,114,231,237,150,189,219,101,192,95,61,117,14,115,96,229,198,5,162,126,15,131,232,245,252,212,129,92,202,40,254,121,10,231,165,63,193,252,190,235,191,110,9,85,125,49,155,122,80,245,57,203,154,186,140,225,141,251,243,54,124,41,123,197,237,23,6,13,38,123,166,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8ae8c7f3-cc95-4f57-90f0-6d91f024f280"));
		}

		#endregion

	}

	#endregion

}

