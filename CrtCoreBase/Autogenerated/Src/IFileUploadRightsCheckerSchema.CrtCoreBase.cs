namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileUploadRightsCheckerSchema

	/// <exclude/>
	public class IFileUploadRightsCheckerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileUploadRightsCheckerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileUploadRightsCheckerSchema(IFileUploadRightsCheckerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0a60e23c-4cac-4673-8ab2-c259118ba9f2");
			Name = "IFileUploadRightsChecker";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1e78c195-217a-4877-a718-71dfe1dfe442");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,82,205,78,195,48,12,62,111,210,222,193,26,23,144,80,115,103,165,151,73,160,221,16,176,7,72,19,167,141,104,147,202,73,54,77,136,119,39,105,214,253,0,226,24,199,223,159,109,195,123,116,3,23,8,239,72,196,157,85,190,88,91,163,116,19,136,123,109,77,241,164,59,220,14,157,229,114,49,255,92,204,103,193,105,211,192,219,193,121,236,87,167,247,37,154,48,214,227,207,13,97,19,25,96,99,60,146,138,26,15,176,57,179,189,234,166,245,110,221,162,248,64,26,251,25,99,80,186,208,247,156,14,213,241,253,66,118,167,37,58,232,209,183,86,58,240,22,118,188,211,146,123,4,46,4,58,7,52,50,129,178,4,42,210,67,24,249,193,14,152,35,184,98,34,103,23,236,67,168,59,45,64,79,230,254,241,54,75,185,127,217,27,11,99,143,131,125,27,237,33,129,8,68,104,60,4,151,30,220,76,94,120,118,22,147,24,175,149,70,9,245,1,202,129,19,239,9,21,152,184,133,199,101,106,217,200,37,171,138,147,28,251,169,151,49,71,64,82,137,203,50,40,82,204,101,181,190,82,63,125,20,37,27,81,127,147,28,85,171,148,30,8,133,37,121,246,73,191,177,132,62,144,113,85,41,42,79,1,75,38,42,208,106,10,154,50,215,8,113,244,113,29,61,202,21,216,52,152,189,118,8,17,161,120,231,70,72,228,157,136,18,115,109,109,151,103,153,71,127,187,189,138,6,215,73,239,225,57,104,9,217,249,93,58,194,175,124,112,104,100,190,185,197,60,86,190,1,121,27,65,75,220,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0a60e23c-4cac-4673-8ab2-c259118ba9f2"));
		}

		#endregion

	}

	#endregion

}

