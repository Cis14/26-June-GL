namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AccountAddressEventListenerSchema

	/// <exclude/>
	public class AccountAddressEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AccountAddressEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AccountAddressEventListenerSchema(AccountAddressEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("680ab496-8f70-406d-8c48-ebc1f780718c");
			Name = "AccountAddressEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c306f334-b3c9-4e98-9644-0ec16cd9a358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,80,193,74,195,64,16,61,103,191,98,136,135,234,37,41,8,30,106,43,196,165,183,42,130,222,196,195,186,153,164,11,217,221,48,179,43,132,210,127,119,147,170,24,208,30,103,222,155,247,230,61,112,202,34,247,74,35,188,32,145,98,223,132,66,122,215,152,54,146,10,198,187,66,82,144,145,131,183,72,215,55,203,170,239,197,65,100,145,141,107,65,122,194,98,235,130,9,6,185,216,126,160,11,124,43,68,118,65,216,166,83,144,157,98,94,65,165,181,143,46,84,117,77,200,60,209,118,134,3,58,164,68,46,203,18,214,28,173,85,52,220,157,198,111,20,26,79,176,152,159,47,0,71,195,1,112,178,43,190,4,202,185,194,154,17,85,199,30,52,97,179,201,239,21,227,95,246,121,153,248,175,83,130,97,6,92,62,235,61,90,245,152,218,129,13,228,243,23,242,171,55,145,245,241,189,51,26,244,24,241,92,66,88,193,127,238,34,59,164,252,63,109,61,96,216,251,58,245,245,52,73,79,16,186,250,132,166,233,56,54,251,107,113,252,4,129,115,147,110,189,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("680ab496-8f70-406d-8c48-ebc1f780718c"));
		}

		#endregion

	}

	#endregion

}

