namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BeforeImportEntitiesSaveEventArgsSchema

	/// <exclude/>
	public class BeforeImportEntitiesSaveEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BeforeImportEntitiesSaveEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BeforeImportEntitiesSaveEventArgsSchema(BeforeImportEntitiesSaveEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ccd3636d-c0d8-49cf-8899-16272e80dadf");
			Name = "BeforeImportEntitiesSaveEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1101e5cd-b945-4f88-8001-faccb4fdb24c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,142,209,74,195,64,16,69,159,27,200,63,12,244,61,31,144,138,160,165,150,128,66,49,245,3,214,205,100,29,216,236,134,153,73,65,138,255,238,110,162,165,138,224,203,48,115,185,103,238,13,102,64,25,141,69,56,34,179,145,216,107,181,141,161,39,55,177,81,138,161,122,32,143,205,48,70,214,178,56,151,197,106,18,10,14,218,119,81,28,54,191,238,132,122,143,54,115,82,237,49,32,147,77,158,228,90,51,186,164,194,214,27,145,26,238,177,143,252,245,118,23,148,148,80,90,115,194,221,9,131,222,177,147,25,26,167,87,79,22,108,102,254,71,160,134,38,244,241,9,69,140,187,254,180,202,181,47,13,14,28,71,228,76,215,112,152,3,230,172,239,176,253,68,29,60,199,168,173,125,195,193,188,52,29,156,193,161,110,64,242,248,248,97,126,36,209,155,165,210,209,184,91,184,172,242,23,180,198,208,45,37,210,181,104,215,82,89,36,237,19,12,139,77,157,144,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ccd3636d-c0d8-49cf-8899-16272e80dadf"));
		}

		#endregion

	}

	#endregion

}

