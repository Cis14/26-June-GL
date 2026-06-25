namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BaseContextPartBuilderSchema

	/// <exclude/>
	public class BaseContextPartBuilderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BaseContextPartBuilderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BaseContextPartBuilderSchema(BaseContextPartBuilderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d56a1d1c-ef17-459e-b8f7-acafeceb9809");
			Name = "BaseContextPartBuilder";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("144e34a0-ee8b-48e4-b7e9-406724f6b9aa");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,83,77,79,195,48,12,61,23,137,255,96,149,11,92,218,251,216,118,160,18,18,135,73,59,192,25,121,137,187,69,74,147,202,73,17,19,226,191,227,126,108,90,75,7,167,40,241,243,243,243,179,227,176,162,80,163,34,40,152,48,26,159,21,190,54,214,71,248,186,189,73,154,96,220,30,94,137,25,131,47,163,196,152,30,111,111,36,114,199,180,55,222,65,97,49,132,5,60,97,160,194,187,72,159,113,139,28,159,26,99,53,113,135,204,243,28,150,161,169,42,228,227,122,184,183,112,48,85,109,169,34,23,219,178,14,124,41,48,34,80,76,229,42,125,249,205,150,230,235,108,200,223,178,255,48,154,2,40,95,85,146,91,26,178,58,0,58,13,53,251,154,56,26,9,150,158,1,173,133,25,174,73,241,112,34,94,230,23,74,141,100,177,67,11,184,11,145,81,69,80,109,183,87,154,133,197,108,161,175,206,132,179,95,207,173,210,69,219,64,36,21,73,247,209,250,116,5,25,130,246,206,30,225,45,16,11,157,147,231,54,239,189,25,221,31,7,86,114,186,39,30,87,17,160,40,110,84,244,44,195,217,54,59,107,84,143,152,78,163,123,200,90,96,118,14,231,211,248,178,70,198,10,156,236,202,42,29,11,73,215,23,67,27,107,150,121,129,17,29,232,20,101,203,188,227,232,40,235,78,207,21,27,239,39,141,143,203,61,116,107,153,36,19,59,96,5,191,252,73,146,239,191,77,218,80,60,120,61,231,143,113,7,98,19,181,87,215,183,49,235,206,13,133,128,251,190,13,23,239,135,191,51,160,31,196,129,139,126,207,91,36,71,251,173,254,39,144,245,30,241,205,79,189,111,115,252,248,253,3,105,90,8,166,216,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d56a1d1c-ef17-459e-b8f7-acafeceb9809"));
		}

		#endregion

	}

	#endregion

}

