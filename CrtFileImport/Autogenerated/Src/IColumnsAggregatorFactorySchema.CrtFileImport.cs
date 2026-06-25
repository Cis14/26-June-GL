namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IColumnsAggregatorFactorySchema

	/// <exclude/>
	public class IColumnsAggregatorFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IColumnsAggregatorFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IColumnsAggregatorFactorySchema(IColumnsAggregatorFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c9f4596c-5acf-4767-9176-f091a98db598");
			Name = "IColumnsAggregatorFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,81,203,106,195,48,16,60,199,224,127,88,124,106,47,209,7,212,49,4,67,138,47,165,148,246,3,182,202,218,21,88,15,118,165,67,40,249,247,74,73,211,214,24,122,17,236,99,102,103,70,14,45,73,64,77,240,74,204,40,126,140,219,222,187,209,76,137,49,26,239,182,7,51,211,96,131,231,88,87,159,117,181,73,98,220,180,216,102,122,168,171,60,81,74,65,43,201,90,228,83,247,93,191,80,96,18,114,81,192,82,252,240,71,24,61,131,102,194,72,160,253,156,172,19,192,105,98,154,48,122,190,177,168,63,52,33,189,207,70,131,113,145,120,44,74,135,254,138,219,255,192,14,168,243,123,202,203,69,225,74,200,165,209,255,115,115,125,244,218,9,200,104,193,229,140,118,77,18,226,156,140,35,93,98,105,186,86,93,166,191,203,76,49,177,147,174,21,162,226,112,220,53,195,147,119,207,196,98,36,230,8,86,178,27,149,89,110,176,194,179,118,182,63,98,200,190,225,145,214,240,187,183,133,36,88,42,188,207,159,178,57,215,213,249,11,222,179,155,242,227,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c9f4596c-5acf-4767-9176-f091a98db598"));
		}

		#endregion

	}

	#endregion

}

