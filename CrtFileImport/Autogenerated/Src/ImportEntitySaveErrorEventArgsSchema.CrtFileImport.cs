namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportEntitySaveErrorEventArgsSchema

	/// <exclude/>
	public class ImportEntitySaveErrorEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportEntitySaveErrorEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportEntitySaveErrorEventArgsSchema(ImportEntitySaveErrorEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5beeed51-04c6-4feb-97c6-7d344852fe83");
			Name = "ImportEntitySaveErrorEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,142,75,10,131,48,16,134,215,10,222,97,192,189,7,208,85,17,11,93,20,10,246,2,169,29,67,64,147,48,51,10,34,189,123,163,161,197,174,186,25,152,143,255,101,213,136,236,85,135,112,71,34,197,174,151,162,118,182,55,122,34,37,198,217,226,108,6,188,140,222,145,100,233,154,165,201,196,198,106,104,23,22,28,171,44,13,36,39,212,65,9,80,15,138,185,132,168,110,172,24,89,90,53,99,67,228,168,153,209,202,137,52,239,22,63,61,6,211,65,183,25,254,232,161,132,29,92,145,89,105,60,228,36,219,156,111,251,141,156,71,18,131,97,193,109,143,223,155,62,85,199,146,223,103,5,141,82,1,111,231,21,61,57,218,103,140,13,95,100,71,20,200,27,212,65,154,168,56,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5beeed51-04c6-4feb-97c6-7d344852fe83"));
		}

		#endregion

	}

	#endregion

}

