namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportPrimaryEntityLoaderSchema

	/// <exclude/>
	public class IFileImportPrimaryEntityLoaderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportPrimaryEntityLoaderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportPrimaryEntityLoaderSchema(IFileImportPrimaryEntityLoaderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("67c9c358-07c2-4936-bf83-ed9335453799");
			Name = "IFileImportPrimaryEntityLoader";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,203,106,195,48,16,69,215,49,248,31,134,116,211,108,172,125,146,102,19,18,48,180,144,69,127,96,98,141,19,129,245,96,36,5,66,233,191,215,150,156,196,125,64,119,154,123,231,106,206,72,6,53,121,135,13,193,59,49,163,183,109,168,182,214,180,234,20,25,131,178,166,218,171,142,106,237,44,135,178,248,40,139,178,152,61,49,157,122,7,106,19,136,219,62,187,132,250,209,117,96,165,145,175,59,19,84,184,190,90,148,196,41,37,132,128,181,143,122,48,55,99,61,216,30,36,6,132,214,50,184,28,5,26,178,138,124,117,139,137,73,206,197,99,167,26,80,183,225,255,206,158,101,234,59,246,27,133,179,149,126,9,135,116,83,54,127,210,77,240,26,219,69,109,224,130,93,36,159,56,195,153,254,100,253,13,155,21,135,140,26,76,255,212,47,243,116,166,158,221,207,55,25,26,30,82,181,22,169,72,209,139,85,50,33,236,198,17,207,227,146,247,246,73,114,177,26,151,36,35,243,158,169,254,204,31,246,77,76,218,23,139,75,233,152,248,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("67c9c358-07c2-4936-bf83-ed9335453799"));
		}

		#endregion

	}

	#endregion

}

