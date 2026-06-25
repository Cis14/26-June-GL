namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportParametersRepositorySchema

	/// <exclude/>
	public class IImportParametersRepositorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportParametersRepositorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportParametersRepositorySchema(IImportParametersRepositorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0944487b-5887-4694-84cc-de8e541b2ac6");
			Name = "IImportParametersRepository";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a83ae89b-1206-453d-b626-f37ab41fffbf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,148,201,110,219,48,16,134,207,54,224,119,24,228,148,2,134,116,111,85,95,146,212,224,169,65,220,229,76,139,35,151,168,68,18,36,149,64,40,242,238,229,34,217,218,154,218,110,79,2,135,179,124,243,107,56,130,86,104,20,205,17,190,160,214,212,200,194,38,119,82,20,252,80,107,106,185,20,201,39,94,34,169,148,212,118,181,252,181,90,46,106,195,197,1,118,141,177,88,125,24,157,93,104,89,98,238,227,76,178,69,129,154,231,19,31,242,217,153,156,81,213,251,146,231,192,133,69,93,120,2,18,203,60,82,237,160,156,209,60,161,146,134,91,169,27,120,15,228,65,88,110,155,147,45,27,187,111,92,82,79,184,72,211,20,50,83,87,21,213,205,166,51,124,85,140,90,4,99,233,1,65,22,192,67,52,24,52,198,225,30,195,210,113,92,166,124,1,16,174,200,199,27,206,110,54,89,26,44,243,14,2,95,34,214,206,215,25,58,63,75,206,90,140,158,207,237,182,118,102,206,214,112,82,58,92,152,7,81,187,180,131,132,239,162,116,243,29,222,99,233,116,0,117,20,4,10,169,175,236,51,150,140,49,132,205,244,17,139,181,240,67,239,55,33,183,104,77,199,148,83,145,99,9,69,73,15,201,255,0,11,222,26,109,173,133,217,144,105,141,44,237,46,189,247,94,202,210,227,16,67,250,73,239,66,0,178,203,91,107,39,204,151,106,203,34,187,126,210,198,141,146,65,22,32,44,185,104,16,191,209,178,198,57,225,39,127,54,10,48,208,228,106,45,148,150,185,243,119,180,83,33,174,254,229,23,42,209,50,244,34,59,42,46,220,54,16,126,243,176,169,12,253,135,250,216,165,152,21,98,13,193,122,172,115,246,252,247,94,234,11,183,63,122,92,185,172,148,127,95,111,105,212,205,242,61,15,11,215,221,197,44,63,177,1,110,230,154,4,42,24,60,199,65,152,129,24,189,143,83,222,204,247,183,134,201,190,245,205,124,119,37,59,121,142,216,183,127,147,0,10,183,235,160,208,178,2,182,63,167,201,167,240,141,97,198,106,164,213,136,118,23,140,62,183,95,163,241,116,249,216,182,43,244,92,186,127,24,211,222,18,245,192,127,68,93,188,174,150,175,191,1,136,115,246,238,166,7,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0944487b-5887-4694-84cc-de8e541b2ac6"));
		}

		#endregion

	}

	#endregion

}

