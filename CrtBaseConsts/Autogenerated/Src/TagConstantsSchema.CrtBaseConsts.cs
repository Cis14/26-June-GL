namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TagConstantsSchema

	/// <exclude/>
	public class TagConstantsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TagConstantsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TagConstantsSchema(TagConstantsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ae28a44b-17af-49d1-987b-2e02f87bc1ca");
			Name = "TagConstants";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9e68a40f-2533-42d0-8fe0-88fdb6bf5704");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,79,203,78,132,64,16,60,47,9,255,48,89,46,122,104,151,101,30,64,54,30,96,30,198,155,137,248,1,35,204,18,18,118,32,12,104,136,217,127,151,197,147,7,162,125,232,78,186,170,187,170,38,215,216,26,189,206,110,52,151,147,239,249,158,213,23,227,122,93,26,84,152,97,208,174,59,143,15,188,179,231,166,158,6,61,54,157,245,189,47,223,219,245,211,123,219,148,200,141,203,174,68,101,171,157,67,133,174,23,166,27,221,130,223,56,187,195,33,8,2,20,220,26,172,99,173,27,242,251,124,48,186,234,108,59,163,167,169,169,208,203,138,45,207,138,185,55,111,207,21,122,68,214,124,174,216,221,94,48,149,19,33,25,132,73,152,2,57,42,9,89,66,20,48,17,17,202,115,78,21,142,246,247,167,109,245,255,56,224,221,208,119,75,90,179,97,130,138,56,149,56,197,160,18,25,2,73,112,8,89,204,114,80,152,199,57,151,52,74,165,216,54,241,103,254,161,249,216,214,78,68,172,152,96,28,8,207,40,144,156,96,200,133,72,33,163,50,60,102,52,137,34,22,254,104,95,125,239,250,13,126,99,119,71,223,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ae28a44b-17af-49d1-987b-2e02f87bc1ca"));
		}

		#endregion

	}

	#endregion

}

