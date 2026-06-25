namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RichTextColumnProcessorSchema

	/// <exclude/>
	public class RichTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RichTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RichTextColumnProcessorSchema(RichTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0707d032-3302-49c0-85c6-57a87aecda8d");
			Name = "RichTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,107,227,48,16,133,207,41,244,63,12,217,75,2,197,190,167,73,96,155,210,18,88,74,217,38,189,148,61,40,242,56,25,176,37,239,72,42,13,165,255,189,99,57,110,235,108,188,208,147,165,225,205,123,154,79,178,81,37,186,74,105,132,21,50,43,103,115,159,44,172,201,105,27,88,121,178,38,185,161,2,151,101,101,217,159,159,189,158,159,13,130,35,179,133,135,189,243,88,94,126,236,191,118,51,246,213,147,27,165,189,101,66,39,10,209,252,96,220,74,6,44,10,229,220,4,126,147,222,173,240,197,47,108,17,74,115,207,86,163,115,150,163,52,77,83,152,146,217,33,147,207,172,6,205,152,207,134,39,212,195,116,222,202,93,40,75,197,251,118,255,211,0,25,231,149,145,105,109,14,126,71,14,116,157,12,178,96,193,96,141,163,77,129,144,91,134,170,241,171,103,248,101,205,182,14,2,29,147,224,89,21,1,93,210,166,164,95,98,158,174,49,87,161,240,87,100,50,105,29,249,125,133,54,31,45,143,206,56,190,128,59,1,15,51,48,242,17,65,207,228,227,241,31,49,173,194,166,32,125,56,106,143,18,38,112,146,220,224,53,210,251,36,45,51,122,14,245,45,8,240,251,232,220,40,190,9,248,31,194,177,176,96,84,30,93,151,179,48,16,37,226,193,178,103,2,177,77,62,124,211,99,227,105,165,88,149,17,215,108,24,28,178,12,98,80,215,47,116,56,95,203,94,46,167,45,36,211,52,170,99,243,1,94,79,232,104,221,177,130,174,243,88,168,110,148,195,209,113,185,254,15,6,111,7,178,104,178,6,110,151,180,100,84,200,94,222,250,164,94,123,233,197,236,127,168,175,36,233,27,168,175,149,87,205,83,108,8,7,67,127,101,77,25,26,79,57,33,247,208,172,218,179,128,125,150,159,83,244,112,27,40,139,126,143,181,221,74,220,214,203,12,102,243,110,45,105,25,30,43,47,79,130,104,240,116,139,82,123,7,119,83,120,149,113,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0707d032-3302-49c0-85c6-57a87aecda8d"));
		}

		#endregion

	}

	#endregion

}

