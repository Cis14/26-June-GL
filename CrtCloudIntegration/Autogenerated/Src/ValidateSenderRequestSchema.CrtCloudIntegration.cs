namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ValidateSenderRequestSchema

	/// <exclude/>
	public class ValidateSenderRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ValidateSenderRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ValidateSenderRequestSchema(ValidateSenderRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("efc01930-33c9-4ec2-8ddf-662cdd8b0a08");
			Name = "ValidateSenderRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,82,77,79,194,64,16,61,75,194,127,152,224,65,189,180,119,64,14,98,67,76,196,16,74,188,24,15,75,25,234,38,219,93,156,217,146,32,241,191,59,221,2,134,138,95,167,102,102,222,123,157,247,102,173,42,144,87,42,67,152,33,145,98,183,244,209,208,217,165,206,75,82,94,59,27,13,147,116,236,22,104,184,221,218,182,91,103,37,107,155,67,186,97,143,133,32,141,193,172,130,113,52,66,139,164,179,94,19,51,45,173,215,5,70,169,76,149,209,111,65,85,80,130,59,39,204,165,128,161,81,204,93,120,148,241,66,121,76,209,46,144,166,248,90,34,251,0,140,227,24,250,92,22,133,162,205,96,87,7,18,44,29,1,213,72,240,14,214,59,9,224,160,113,193,128,133,210,134,163,189,72,220,80,233,51,162,50,236,32,35,92,94,119,126,13,33,186,81,44,11,210,90,103,184,219,176,3,113,165,246,116,171,188,18,150,39,149,249,103,105,172,202,185,209,25,100,97,207,147,222,160,11,95,229,132,185,13,166,15,241,76,200,173,144,188,70,201,104,18,68,235,121,51,149,208,24,161,103,144,80,184,250,250,23,172,3,0,163,217,71,7,82,220,100,245,37,184,18,15,229,236,59,222,39,44,216,29,99,49,71,186,124,144,71,4,215,208,9,148,123,97,116,174,170,0,246,9,220,37,182,44,144,212,220,96,159,61,201,219,24,64,178,135,194,22,114,244,189,106,223,30,188,87,172,191,91,75,210,9,88,249,247,255,140,157,96,253,108,107,69,110,173,229,106,85,125,236,172,118,83,29,232,0,104,250,9,135,148,163,215,183,12,117,221,61,110,74,239,3,69,13,36,111,139,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("efc01930-33c9-4ec2-8ddf-662cdd8b0a08"));
		}

		#endregion

	}

	#endregion

}

