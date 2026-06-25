namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CrtCloudIntegrationConstsSchema

	/// <exclude/>
	public class CrtCloudIntegrationConstsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CrtCloudIntegrationConstsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CrtCloudIntegrationConstsSchema(CrtCloudIntegrationConstsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5a426501-b6e0-4259-bea1-d072e1c00a72");
			Name = "CrtCloudIntegrationConsts";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,142,177,10,195,48,12,68,231,26,252,15,34,191,81,58,121,202,146,165,244,3,92,91,13,2,87,10,146,50,148,210,127,175,186,134,194,45,119,188,59,142,235,19,109,171,13,161,168,151,33,123,159,217,113,213,234,36,156,211,59,167,211,182,223,7,53,48,143,172,65,27,213,236,31,92,132,205,45,248,95,231,80,82,172,93,120,188,194,43,241,10,183,121,17,167,7,161,94,145,59,234,18,47,224,2,211,113,115,58,199,214,39,167,208,23,96,107,92,177,169,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5a426501-b6e0-4259-bea1-d072e1c00a72"));
		}

		#endregion

	}

	#endregion

}

