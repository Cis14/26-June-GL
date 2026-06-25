namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportLookupChunksDataProviderSchema

	/// <exclude/>
	public class IImportLookupChunksDataProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportLookupChunksDataProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportLookupChunksDataProviderSchema(IImportLookupChunksDataProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("425cd682-be37-44b5-8c48-07738afab1de");
			Name = "IImportLookupChunksDataProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,193,10,131,48,16,68,207,10,254,195,98,239,230,94,172,23,75,65,232,161,135,254,64,170,27,27,212,36,172,73,161,20,255,189,106,84,164,151,30,119,103,230,49,140,226,29,246,134,151,8,119,36,226,189,22,54,201,181,18,178,118,196,173,212,42,185,200,22,139,206,104,178,240,137,194,40,12,14,132,245,40,64,161,44,146,24,163,71,40,188,225,170,117,227,76,254,116,170,233,207,220,242,27,233,151,172,144,230,24,99,12,210,222,117,29,167,119,182,220,11,183,26,189,96,22,51,8,77,163,17,17,74,66,113,138,119,208,137,25,3,203,146,21,199,118,60,227,30,173,44,65,174,173,254,149,130,173,246,198,94,165,212,255,167,215,172,165,63,29,178,108,154,34,24,252,28,168,42,191,72,20,14,95,216,153,146,72,79,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("425cd682-be37-44b5-8c48-07738afab1de"));
		}

		#endregion

	}

	#endregion

}

