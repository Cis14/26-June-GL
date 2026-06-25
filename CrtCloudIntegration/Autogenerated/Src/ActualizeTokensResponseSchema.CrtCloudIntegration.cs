namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ActualizeTokensResponseSchema

	/// <exclude/>
	public class ActualizeTokensResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ActualizeTokensResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ActualizeTokensResponseSchema(ActualizeTokensResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ff3b8a59-2b9a-456e-b322-9ea63aef377b");
			Name = "ActualizeTokensResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9808838f-0c9f-4b08-81dd-7e505e163670");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,144,65,75,3,49,16,133,207,22,250,31,6,244,40,187,247,86,132,186,39,15,214,34,197,251,52,59,89,131,217,76,201,76,42,186,244,191,155,110,186,165,122,9,204,203,188,124,239,37,96,79,178,71,67,208,68,109,60,167,246,57,40,117,17,213,113,168,94,184,37,47,213,91,94,225,32,36,243,217,48,159,221,36,113,161,131,45,197,136,194,86,171,134,131,117,93,42,158,229,124,150,87,110,35,117,121,128,198,163,200,2,238,134,117,230,28,199,171,186,174,225,65,82,223,99,252,126,60,207,19,0,108,228,30,132,141,67,15,104,12,167,160,32,20,15,206,208,61,24,14,138,46,8,160,247,224,130,101,80,6,81,140,10,152,244,3,172,231,175,106,34,212,87,136,125,218,121,103,192,156,194,192,202,104,66,239,126,104,203,159,20,228,130,94,192,19,10,173,10,116,82,179,121,24,83,95,26,109,34,239,41,170,163,92,107,51,190,91,238,255,215,26,133,117,234,119,20,129,45,28,50,178,205,121,79,200,234,178,127,29,114,74,233,114,229,226,123,181,239,39,87,201,9,3,116,164,203,252,27,249,56,158,51,81,104,75,172,113,46,234,95,49,107,191,138,18,158,53,227,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ff3b8a59-2b9a-456e-b322-9ea63aef377b"));
		}

		#endregion

	}

	#endregion

}

