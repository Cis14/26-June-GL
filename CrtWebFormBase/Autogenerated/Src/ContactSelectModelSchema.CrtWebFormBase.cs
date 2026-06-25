namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ContactSelectModelSchema

	/// <exclude/>
	public class ContactSelectModelSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ContactSelectModelSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ContactSelectModelSchema(ContactSelectModelSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c995aa53-261b-474b-b723-c7bbe16e4062");
			Name = "ContactSelectModel";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("bb225b1d-9856-4e2d-8d05-b81de9745531");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,97,75,195,48,16,134,63,119,176,255,112,176,175,210,126,119,34,232,148,33,168,76,86,127,64,76,175,109,160,77,234,229,50,40,99,255,221,36,237,70,39,82,191,148,220,93,222,247,158,230,213,162,69,219,9,137,144,35,145,176,166,228,116,99,116,169,42,71,130,149,209,203,197,113,185,72,156,85,186,130,125,111,25,219,245,165,222,24,194,244,233,209,55,124,107,69,88,249,251,176,105,132,181,183,126,166,89,72,222,99,131,146,223,76,129,77,188,149,101,25,220,89,215,182,130,250,251,177,206,251,14,193,148,103,201,135,67,234,119,100,14,170,64,2,66,235,26,14,227,45,78,221,160,69,174,77,145,158,61,179,137,105,231,190,26,37,65,6,146,63,65,146,99,132,185,48,251,109,29,18,43,244,224,187,40,30,230,191,105,99,227,243,165,8,56,254,49,246,178,198,86,64,105,8,132,43,20,106,25,255,227,21,69,113,3,207,7,212,156,11,170,144,1,89,166,23,191,41,233,25,117,235,84,1,15,163,199,96,27,214,28,193,171,215,96,195,231,52,131,148,215,8,218,39,57,121,69,175,150,166,113,173,158,95,108,153,134,36,71,209,38,106,222,131,87,200,61,73,2,64,60,216,241,48,199,17,163,3,174,5,251,220,216,145,182,32,160,81,150,39,96,224,180,250,118,8,62,93,205,170,84,72,118,30,113,72,238,58,199,127,224,86,168,139,33,218,88,15,221,235,230,233,7,17,25,210,62,250,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c995aa53-261b-474b-b723-c7bbe16e4062"));
		}

		#endregion

	}

	#endregion

}

