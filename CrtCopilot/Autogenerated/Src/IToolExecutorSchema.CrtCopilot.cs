namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IToolExecutorSchema

	/// <exclude/>
	public class IToolExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IToolExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IToolExecutorSchema(IToolExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("77b670a7-c9d7-450a-a2e1-50379f1044f0");
			Name = "IToolExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,65,110,219,64,12,60,219,128,255,64,184,135,182,128,33,221,107,85,23,167,48,12,52,64,208,244,3,235,21,45,51,144,118,85,114,213,196,48,242,247,82,43,109,226,180,73,208,155,68,114,134,51,195,117,166,69,233,140,69,216,48,154,64,62,219,248,142,26,31,22,243,243,98,62,235,133,92,13,183,39,9,216,106,167,105,208,234,140,147,108,139,14,153,236,122,49,215,169,60,207,161,144,190,109,13,159,202,233,127,231,2,242,97,32,62,120,6,124,64,219,135,129,107,162,135,224,125,35,89,2,231,23,232,174,223,55,100,129,158,8,118,63,117,246,91,100,240,172,3,231,184,116,246,129,177,86,45,112,195,190,67,14,132,242,5,110,34,118,236,255,173,42,22,182,24,4,12,252,54,77,143,186,162,34,107,162,172,251,35,134,35,50,88,239,14,196,237,144,132,3,18,96,252,213,19,99,5,123,84,31,8,29,123,139,88,69,8,133,35,40,8,76,204,36,123,218,121,105,102,182,87,237,176,147,205,5,239,143,196,121,134,26,195,26,30,39,63,232,170,209,210,75,127,215,170,204,87,255,99,110,204,8,37,134,11,214,232,185,84,248,41,101,254,134,194,88,233,12,155,22,156,190,134,175,203,1,189,81,240,174,90,150,67,244,64,21,186,64,7,66,206,138,60,78,190,14,52,92,247,173,142,202,136,251,168,73,167,202,251,64,65,17,181,186,44,211,235,152,10,255,162,24,67,207,78,202,34,79,95,67,235,59,73,40,38,236,181,66,77,141,101,74,227,147,4,30,206,245,236,106,5,87,20,79,166,9,20,99,119,5,126,127,167,111,187,124,86,188,74,177,221,142,90,146,166,207,235,87,207,165,71,124,132,63,18,32,66,12,78,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("77b670a7-c9d7-450a-a2e1-50379f1044f0"));
		}

		#endregion

	}

	#endregion

}

