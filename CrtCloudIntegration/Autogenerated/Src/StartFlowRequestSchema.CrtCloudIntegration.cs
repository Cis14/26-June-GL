namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: StartFlowRequestSchema

	/// <exclude/>
	public class StartFlowRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public StartFlowRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public StartFlowRequestSchema(StartFlowRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6e75e9ef-b097-4f1c-a537-00209c3c90e6");
			Name = "StartFlowRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,144,193,106,195,48,16,68,207,49,248,31,22,122,183,239,73,232,161,134,150,28,2,33,73,63,64,181,54,174,64,150,220,221,21,37,152,252,123,37,217,49,105,123,89,216,209,204,232,73,78,245,200,131,106,17,26,146,198,250,160,119,78,176,35,37,198,187,106,239,53,90,174,142,248,21,144,133,203,98,44,139,85,96,227,58,56,93,89,176,223,148,69,84,158,8,187,104,135,198,42,230,53,156,68,145,188,90,255,61,231,178,167,174,107,216,114,232,123,69,215,231,121,63,226,64,200,232,132,65,1,77,102,16,15,156,10,64,62,17,84,136,211,137,105,51,15,92,98,105,117,47,171,31,218,134,240,97,77,11,109,2,248,119,63,172,225,69,49,46,52,171,49,19,45,216,7,242,3,146,24,140,236,135,92,52,157,255,69,206,194,57,82,161,211,16,24,9,140,78,112,23,131,84,45,129,71,172,59,215,91,48,58,126,48,166,87,188,199,224,78,195,8,29,202,6,56,141,219,204,19,123,39,164,188,79,234,111,241,246,3,22,56,19,156,176,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6e75e9ef-b097-4f1c-a537-00209c3c90e6"));
		}

		#endregion

	}

	#endregion

}

