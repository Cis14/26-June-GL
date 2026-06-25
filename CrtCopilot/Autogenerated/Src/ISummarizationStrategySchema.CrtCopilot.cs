namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISummarizationStrategySchema

	/// <exclude/>
	public class ISummarizationStrategySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISummarizationStrategySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISummarizationStrategySchema(ISummarizationStrategySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ed1b7e1d-0c94-441c-8495-e0327be8ec2d");
			Name = "ISummarizationStrategy";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,144,189,10,194,64,16,132,107,5,223,97,193,70,65,146,94,69,144,216,216,216,36,47,112,94,54,241,240,178,27,110,47,69,20,223,221,203,31,136,96,103,117,236,206,240,205,222,144,170,80,106,165,17,18,135,202,27,142,18,174,141,101,191,152,63,23,243,89,35,134,74,72,91,241,88,69,217,45,88,242,176,216,253,84,162,76,201,93,130,30,28,75,135,165,97,130,51,121,116,69,136,216,194,57,109,170,74,57,243,232,146,40,245,78,121,44,219,222,29,199,49,236,165,151,219,195,56,159,176,48,132,2,254,134,160,153,130,93,123,40,216,129,2,249,4,129,140,164,104,2,197,31,164,186,185,90,163,193,76,103,252,184,2,186,255,206,186,251,247,87,102,123,128,11,98,62,57,241,40,45,233,213,216,77,138,34,125,236,240,110,32,81,164,209,218,158,151,241,29,9,244,247,102,189,155,240,240,103,232,107,104,27,41,31,10,239,198,215,27,98,241,181,39,216,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ed1b7e1d-0c94-441c-8495-e0327be8ec2d"));
		}

		#endregion

	}

	#endregion

}

