namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportStageFactorySchema

	/// <exclude/>
	public class IImportStageFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportStageFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportStageFactorySchema(IImportStageFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("65f0da7e-4515-4c52-863a-8b3f9080781c");
			Name = "IImportStageFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,82,75,106,195,48,16,93,199,224,59,12,217,180,133,16,29,160,78,32,13,13,120,87,250,57,192,68,25,27,65,36,153,25,105,97,74,238,94,41,78,26,39,13,116,103,73,239,51,239,141,29,90,146,14,53,193,39,49,163,248,38,204,215,222,53,166,141,140,193,120,55,223,152,61,213,182,243,28,202,226,187,44,38,81,140,107,175,208,76,207,101,145,94,148,82,80,73,180,22,185,95,158,206,239,212,49,9,185,32,96,92,32,110,178,85,227,25,52,19,134,244,153,212,193,28,229,31,4,36,96,75,114,150,82,35,173,46,110,247,70,143,52,234,97,166,143,204,216,160,14,158,251,4,203,3,254,153,227,120,177,30,252,6,171,193,8,182,61,152,221,47,67,221,82,170,14,25,45,184,84,209,98,106,46,118,211,101,165,142,79,247,145,81,136,83,133,142,116,238,239,31,176,246,251,104,157,188,177,215,36,226,249,14,156,41,68,118,178,172,132,40,215,214,44,166,47,40,116,217,203,48,148,74,204,51,52,115,235,59,160,83,11,163,155,199,27,132,188,186,104,97,20,118,6,95,87,121,224,58,222,44,91,37,179,245,144,99,213,182,76,45,166,109,172,118,216,165,93,193,109,192,167,244,175,76,14,101,113,248,1,223,50,183,7,122,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("65f0da7e-4515-4c52-863a-8b3f9080781c"));
		}

		#endregion

	}

	#endregion

}

