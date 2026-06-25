namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TextColumnHelperSchema

	/// <exclude/>
	public class TextColumnHelperSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TextColumnHelperSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TextColumnHelperSchema(TextColumnHelperSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a1a85a55-168d-4a14-be59-272acc0d963d");
			Name = "TextColumnHelper";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7e188b25-9774-4cd9-86fe-ed132c1bc48f");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,146,209,107,194,48,16,198,159,43,248,63,220,28,136,3,105,247,60,107,95,148,49,97,130,160,236,61,182,215,26,104,210,146,92,134,78,252,223,151,166,209,89,215,151,194,37,223,239,190,239,174,145,76,160,174,89,138,176,67,165,152,174,114,10,23,149,204,121,97,20,35,94,201,240,157,151,184,18,117,165,104,56,56,15,7,129,209,92,22,29,181,194,217,112,96,111,158,21,22,150,128,69,201,180,126,179,146,35,45,170,210,8,249,129,101,141,202,105,162,40,130,88,27,33,152,58,37,190,182,126,196,184,212,64,150,128,212,33,26,12,241,146,211,9,4,210,161,202,116,120,133,163,59,186,54,251,146,167,160,201,70,77,33,109,124,123,108,131,179,179,190,229,91,183,29,223,96,227,240,246,242,49,152,59,216,40,172,153,194,78,50,248,102,165,193,16,182,7,187,18,180,65,93,13,60,7,78,144,85,86,44,43,130,220,22,116,64,207,132,55,135,232,209,34,182,6,76,128,180,255,97,62,202,24,177,175,166,221,238,84,227,40,89,218,210,183,39,123,16,198,145,19,247,179,78,55,74,22,247,33,255,1,10,201,40,169,19,63,88,214,51,88,28,93,69,13,213,221,176,38,213,252,124,79,255,109,218,101,158,52,245,242,126,2,232,204,51,189,226,206,231,5,154,199,20,4,118,111,147,167,142,46,92,233,53,59,110,249,15,194,120,236,67,125,162,44,232,0,73,183,99,216,136,174,141,130,118,81,115,79,108,205,190,181,155,188,78,251,168,153,131,46,238,219,14,236,193,157,226,98,210,94,95,252,187,65,153,181,79,199,213,237,105,247,240,242,11,246,17,59,246,72,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a1a85a55-168d-4a14-be59-272acc0d963d"));
		}

		#endregion

	}

	#endregion

}

