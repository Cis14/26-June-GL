namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportEntityColumnValueInfoSchema

	/// <exclude/>
	public class ImportEntityColumnValueInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportEntityColumnValueInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportEntityColumnValueInfoSchema(ImportEntityColumnValueInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b9fbcde3-0880-48e4-b9ce-ae5b644669f4");
			Name = "ImportEntityColumnValueInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,193,106,195,48,12,61,183,208,127,16,244,158,220,215,177,75,187,65,47,163,176,178,187,234,40,153,71,98,27,73,30,132,210,127,159,227,180,165,221,161,163,219,197,32,233,61,189,247,132,29,118,36,1,13,193,150,152,81,124,173,197,210,187,218,54,145,81,173,119,197,139,109,105,221,5,207,58,155,238,103,211,73,20,235,154,43,52,211,98,54,77,147,57,83,147,24,176,108,81,228,1,70,210,179,83,171,253,210,183,177,115,239,216,70,90,187,218,103,120,89,150,240,40,177,235,144,251,167,99,189,34,177,141,163,10,212,3,57,131,65,98,139,74,96,19,137,187,108,8,112,231,163,2,130,4,50,182,182,6,76,94,14,95,195,118,168,34,15,254,244,35,145,178,1,8,236,13,137,20,39,201,242,66,211,58,37,118,216,130,25,60,223,182,60,217,103,219,231,152,27,246,129,88,45,165,172,155,184,107,173,25,231,63,115,229,198,184,57,101,26,86,159,28,187,116,252,226,76,185,244,53,9,121,33,136,230,52,163,151,215,4,135,61,52,164,11,144,225,57,220,43,152,79,116,91,209,239,62,201,40,92,164,255,159,100,133,138,160,125,248,69,118,149,96,89,109,155,160,71,245,161,151,203,191,25,16,31,57,253,235,123,111,253,150,105,183,47,62,39,87,141,191,32,215,99,247,186,121,248,6,205,148,152,88,90,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b9fbcde3-0880-48e4-b9ce-ae5b644669f4"));
		}

		#endregion

	}

	#endregion

}

