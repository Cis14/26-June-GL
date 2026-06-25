namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CreatioAIDocumentSchema

	/// <exclude/>
	public class CreatioAIDocumentSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CreatioAIDocumentSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CreatioAIDocumentSchema(CreatioAIDocumentSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("acf18860-6422-4915-b6e0-8cee95e572c3");
			Name = "CreatioAIDocument";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,146,63,107,195,48,16,197,231,4,242,29,142,100,105,23,123,79,250,135,226,144,226,161,37,212,99,233,32,203,87,231,64,150,140,116,30,90,147,239,94,201,142,83,210,154,66,241,98,124,239,157,126,239,33,164,69,133,174,22,18,33,177,40,152,76,148,152,154,148,225,197,188,93,204,103,141,35,93,66,246,225,24,171,205,143,57,122,105,52,83,133,81,134,150,132,162,207,112,92,251,45,191,183,178,88,250,1,18,37,156,91,15,236,135,116,107,100,83,161,230,110,41,142,99,184,33,125,240,199,185,48,50,190,243,218,235,86,176,72,140,102,43,36,191,5,225,76,207,21,6,161,110,114,69,18,100,32,255,6,195,26,210,145,180,89,219,37,158,123,237,173,169,209,50,161,47,183,239,128,189,63,86,169,239,244,132,85,142,246,234,217,223,23,220,194,146,52,123,112,90,44,175,67,167,161,212,99,67,197,61,164,39,15,90,40,145,55,224,194,231,248,207,0,135,206,249,166,227,9,217,96,78,138,120,39,133,99,124,216,117,198,100,118,248,191,164,59,182,225,253,236,78,230,228,132,76,30,176,18,127,230,124,175,140,165,173,80,23,253,147,232,230,94,189,20,143,95,76,141,105,50,35,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("acf18860-6422-4915-b6e0-8cee95e572c3"));
		}

		#endregion

	}

	#endregion

}

