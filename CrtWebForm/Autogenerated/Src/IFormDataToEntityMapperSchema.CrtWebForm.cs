namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFormDataToEntityMapperSchema

	/// <exclude/>
	public class IFormDataToEntityMapperSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFormDataToEntityMapperSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFormDataToEntityMapperSchema(IFormDataToEntityMapperSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c20ea6f3-be52-42d0-824d-0e9ad343f8f2");
			Name = "IFormDataToEntityMapper";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("30ff16fc-9eaa-40ad-9611-33924da6f041");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,82,205,106,195,48,12,62,167,208,119,16,61,109,48,146,7,88,215,75,187,142,29,122,106,97,103,213,81,18,67,98,7,217,110,9,99,239,62,57,63,93,75,41,3,99,108,163,239,71,159,108,176,33,215,162,34,56,16,51,58,91,248,116,109,77,161,203,192,232,181,53,233,7,25,146,35,229,95,116,220,90,110,246,196,39,173,104,62,251,158,207,146,224,180,41,111,160,76,233,187,241,218,107,114,175,82,32,43,203,50,88,186,208,52,200,221,106,188,31,24,141,43,132,205,65,220,33,71,143,224,45,80,132,118,41,76,176,236,10,215,134,99,173,21,104,227,137,139,232,248,51,218,217,8,242,96,123,201,110,135,109,75,44,165,209,218,157,110,255,176,173,177,4,95,161,23,158,92,43,233,203,193,185,34,95,17,67,35,240,216,142,112,68,83,148,131,11,74,145,115,69,168,235,46,189,112,94,155,74,142,214,214,176,31,234,160,215,77,74,242,177,245,228,103,62,123,232,99,39,229,88,210,104,197,129,85,42,176,40,230,129,163,133,209,202,3,77,231,251,162,137,227,79,21,254,147,93,51,245,45,15,57,67,193,182,129,26,37,137,135,90,253,75,139,140,13,24,249,43,111,139,98,12,125,177,138,251,45,197,50,235,43,47,192,59,240,160,187,88,13,243,2,107,100,230,231,74,171,106,248,1,215,100,49,149,24,3,229,55,180,39,171,115,144,65,63,77,195,135,201,208,11,140,172,131,200,115,156,129,100,33,235,23,129,214,231,65,229,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c20ea6f3-be52-42d0-824d-0e9ad343f8f2"));
		}

		#endregion

	}

	#endregion

}

