namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LookupValuesEntitySchemaColumnSchema

	/// <exclude/>
	public class LookupValuesEntitySchemaColumnSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LookupValuesEntitySchemaColumnSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LookupValuesEntitySchemaColumnSchema(LookupValuesEntitySchemaColumnSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("76ad8774-b047-419f-b521-b9a051bdfc9f");
			Name = "LookupValuesEntitySchemaColumn";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,82,75,75,3,49,16,62,111,161,255,97,160,23,5,217,189,91,21,100,177,82,40,34,84,189,199,236,108,27,76,38,107,30,135,82,250,223,77,210,172,110,91,168,122,204,151,153,239,149,16,83,104,59,198,17,94,208,24,102,117,235,202,90,83,43,86,222,48,39,52,149,51,33,113,174,58,109,220,120,180,29,143,10,111,5,173,96,185,177,14,213,116,60,10,200,196,224,42,76,2,212,146,89,123,13,11,173,63,124,247,198,164,71,251,64,78,184,205,146,175,81,177,90,75,175,40,173,84,85,5,55,214,43,197,204,230,46,159,239,9,4,89,199,40,152,209,45,184,181,176,192,35,35,112,77,142,133,59,160,224,22,24,53,128,137,22,108,226,5,79,226,211,35,136,38,194,173,64,19,247,101,114,209,79,242,164,93,246,210,213,64,187,243,239,82,240,44,245,155,247,34,86,240,147,120,38,80,54,33,242,115,226,72,209,78,178,37,96,113,224,230,196,112,249,189,56,116,214,91,123,244,162,129,161,155,215,121,51,253,179,216,62,122,234,238,188,140,117,38,62,237,83,24,204,236,19,164,102,31,53,159,115,238,240,67,194,176,231,78,155,163,240,153,234,124,141,23,151,176,133,221,127,22,82,3,120,216,192,85,111,56,38,11,140,145,173,56,106,9,110,143,183,166,105,44,102,12,119,180,143,90,20,187,211,188,25,27,66,1,249,2,154,116,46,235,48,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("76ad8774-b047-419f-b521-b9a051bdfc9f"));
		}

		#endregion

	}

	#endregion

}

