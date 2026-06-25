namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotToolProcessorSchema

	/// <exclude/>
	public class ICopilotToolProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotToolProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotToolProcessorSchema(ICopilotToolProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("45ff5045-0bfb-49fc-bfdd-55d44048d58c");
			Name = "ICopilotToolProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,144,65,106,195,48,16,69,215,14,228,14,130,110,90,8,58,64,82,186,17,37,13,52,16,154,92,64,181,39,182,96,172,49,154,9,36,132,222,189,35,219,113,27,74,87,146,254,252,255,230,163,232,91,224,206,151,96,92,2,47,129,172,163,46,32,201,124,118,157,207,138,19,135,88,155,253,133,5,90,157,32,66,169,158,200,118,13,17,82,40,87,147,231,0,41,121,166,163,216,215,168,131,166,133,40,118,19,5,210,81,233,108,93,227,197,81,219,33,100,128,253,208,173,202,1,86,130,50,30,18,212,42,155,41,176,52,155,177,200,129,8,119,137,148,193,148,122,115,200,166,232,209,132,155,251,63,115,113,237,3,19,126,11,210,80,197,75,179,59,125,98,40,135,225,123,96,121,30,243,91,13,250,26,94,204,155,143,21,66,198,57,143,200,143,247,245,111,237,77,249,71,90,152,17,181,87,84,222,201,195,185,200,171,138,95,53,29,105,251,179,24,249,185,63,173,198,186,16,171,161,113,255,254,26,190,232,78,84,237,27,67,109,77,213,187,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("45ff5045-0bfb-49fc-bfdd-55d44048d58c"));
		}

		#endregion

	}

	#endregion

}

