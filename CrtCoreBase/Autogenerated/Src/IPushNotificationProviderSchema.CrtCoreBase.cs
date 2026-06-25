namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPushNotificationProviderSchema

	/// <exclude/>
	public class IPushNotificationProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPushNotificationProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPushNotificationProviderSchema(IPushNotificationProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e415681b-39a4-4c62-9fc4-58927242174a");
			Name = "IPushNotificationProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("d653ba80-e9e2-4f78-8775-8ba14502d8a8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,65,107,194,64,16,133,207,10,254,135,193,94,20,74,82,232,165,168,88,68,65,60,180,149,170,167,210,195,154,157,164,67,147,221,176,179,17,164,248,223,187,155,68,177,162,98,47,97,147,188,239,61,222,204,42,145,33,231,34,66,88,162,49,130,117,108,131,177,86,49,37,133,17,150,180,106,53,127,90,205,70,193,164,18,88,108,217,98,214,63,121,119,250,52,197,200,139,57,152,162,66,67,145,211,56,213,157,193,196,125,133,153,178,104,98,23,210,131,217,188,224,175,87,109,41,166,168,244,159,27,189,33,137,166,4,194,48,132,1,23,89,38,204,118,88,191,31,96,208,49,228,142,6,117,132,67,94,243,28,236,249,240,200,32,47,214,41,69,64,7,143,43,249,13,95,180,177,209,36,97,129,74,118,78,149,19,97,5,72,247,232,250,9,236,170,134,78,88,149,252,83,120,156,10,230,30,156,115,184,216,243,29,115,131,140,202,242,153,150,62,246,90,193,200,7,94,200,171,122,125,188,173,89,167,104,177,211,158,248,36,167,64,9,110,141,110,40,79,193,67,240,24,192,138,209,175,116,36,51,82,43,69,118,38,221,224,220,134,133,108,119,63,189,71,29,198,214,248,237,47,245,55,42,40,205,27,9,218,126,121,224,250,80,141,103,79,76,11,146,207,167,222,55,145,251,44,178,41,254,139,120,65,102,145,220,200,76,168,188,190,110,166,131,10,191,175,109,134,48,146,146,202,127,105,185,255,203,118,103,111,196,238,23,138,201,128,22,96,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e415681b-39a4-4c62-9fc4-58927242174a"));
		}

		#endregion

	}

	#endregion

}

