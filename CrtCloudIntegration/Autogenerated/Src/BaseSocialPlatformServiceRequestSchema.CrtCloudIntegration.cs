namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BaseSocialPlatformServiceRequestSchema

	/// <exclude/>
	public class BaseSocialPlatformServiceRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BaseSocialPlatformServiceRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BaseSocialPlatformServiceRequestSchema(BaseSocialPlatformServiceRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7c55249a-8bea-465d-85dd-98f32e454b2d");
			Name = "BaseSocialPlatformServiceRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,65,107,194,64,16,133,207,10,254,135,69,47,246,146,220,107,91,104,211,139,5,37,40,61,21,15,107,50,134,133,236,110,58,51,219,98,197,255,222,205,38,90,3,65,234,101,97,103,190,247,230,237,176,70,106,160,74,102,32,18,228,164,180,46,159,27,134,2,37,43,107,162,133,205,161,164,104,5,159,14,136,105,52,60,140,134,3,71,202,20,98,189,39,6,29,173,156,97,165,33,90,3,42,89,170,159,160,155,157,169,37,124,179,53,100,119,28,189,81,104,248,214,4,161,240,148,72,74,73,116,47,94,36,193,218,102,94,158,150,146,119,22,181,55,251,82,25,180,99,131,230,227,85,178,76,172,97,148,25,111,124,161,114,219,82,101,34,171,61,254,97,49,56,4,155,243,236,20,109,5,200,10,124,128,52,88,53,253,56,142,197,3,57,173,37,238,159,78,133,231,170,242,64,120,154,48,126,97,103,48,190,36,67,196,5,232,45,224,116,233,41,241,40,198,242,79,57,190,219,4,170,222,67,59,125,63,237,1,218,135,17,99,189,192,203,209,7,81,0,207,4,213,199,241,74,220,211,14,110,204,90,181,178,250,222,31,182,135,232,166,77,47,128,219,227,58,2,20,42,7,255,163,118,10,240,246,228,239,222,96,158,95,207,222,97,250,211,55,72,95,254,9,152,188,249,64,225,222,84,187,197,227,47,180,182,173,105,82,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7c55249a-8bea-465d-85dd-98f32e454b2d"));
		}

		#endregion

	}

	#endregion

}

