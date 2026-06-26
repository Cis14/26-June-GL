namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ContactAddressEventListenerSchema

	/// <exclude/>
	public class ContactAddressEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ContactAddressEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ContactAddressEventListenerSchema(ContactAddressEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("87183a9a-f705-46f0-8f26-2a86491608a8");
			Name = "ContactAddressEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c306f334-b3c9-4e98-9644-0ec16cd9a358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,80,193,74,195,64,16,61,103,191,98,136,135,234,37,17,4,15,181,21,234,210,155,138,160,55,241,176,110,38,233,66,118,55,204,76,132,80,250,239,110,82,21,3,218,227,204,123,243,222,188,23,140,71,238,140,69,120,65,34,195,177,150,66,199,80,187,166,39,35,46,134,66,147,232,158,37,122,164,171,235,203,77,215,169,189,202,122,118,161,1,29,9,139,109,16,39,14,185,216,126,96,16,190,81,42,59,35,108,210,41,232,214,48,47,19,45,136,177,178,169,42,66,230,137,118,239,88,48,32,37,114,89,150,176,226,222,123,67,195,237,113,252,70,161,142,4,139,249,249,2,112,52,28,0,39,187,226,75,160,156,43,172,24,209,180,28,193,18,214,235,252,206,48,254,101,159,151,137,255,58,37,24,102,192,249,179,221,161,55,143,169,29,88,67,62,127,33,191,120,83,89,215,191,183,206,130,29,35,158,74,8,75,248,207,93,101,251,148,255,167,173,7,148,93,172,82,95,79,147,244,4,97,168,142,104,154,14,99,179,191,22,7,248,4,44,148,117,157,189,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("87183a9a-f705-46f0-8f26-2a86491608a8"));
		}

		#endregion

	}

	#endregion

}

