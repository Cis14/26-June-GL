namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ActualizeTokensRequestSchema

	/// <exclude/>
	public class ActualizeTokensRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ActualizeTokensRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ActualizeTokensRequestSchema(ActualizeTokensRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7382d289-41e1-4e0b-98a6-14275e44e3c6");
			Name = "ActualizeTokensRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9808838f-0c9f-4b08-81dd-7e505e163670");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,79,75,195,64,16,197,207,45,244,59,44,237,165,94,146,187,85,161,196,139,66,75,104,189,73,15,219,100,26,22,247,159,59,179,72,12,126,119,55,219,24,83,72,5,47,129,125,243,123,121,111,118,53,87,128,150,23,192,50,71,153,52,190,124,210,4,149,227,36,140,78,54,166,4,137,201,14,222,61,32,225,108,218,204,166,19,143,66,87,108,95,35,129,74,118,94,147,80,144,236,193,9,46,197,103,244,173,122,106,11,31,100,52,154,19,37,207,24,7,97,180,112,80,5,138,101,146,35,222,178,117,65,190,181,194,139,121,3,141,93,88,36,95,31,57,241,204,104,114,188,160,67,16,172,63,74,81,176,162,117,94,53,78,154,104,238,115,114,103,44,56,18,16,194,242,248,131,243,60,77,83,118,135,94,41,238,234,135,31,97,109,109,0,226,26,76,135,203,233,193,116,72,198,98,27,80,71,112,203,109,160,216,61,155,243,95,231,252,230,16,169,118,231,46,189,94,142,0,221,58,72,174,189,172,97,116,195,42,160,21,195,246,243,245,71,221,92,114,58,25,167,254,217,213,118,182,246,60,94,118,132,184,108,155,15,128,177,186,11,208,229,249,1,226,249,172,94,138,65,251,6,104,20,233,215,128,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7382d289-41e1-4e0b-98a6-14275e44e3c6"));
		}

		#endregion

	}

	#endregion

}

