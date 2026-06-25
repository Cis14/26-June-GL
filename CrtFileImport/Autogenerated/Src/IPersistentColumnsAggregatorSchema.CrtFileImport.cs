namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPersistentColumnsAggregatorSchema

	/// <exclude/>
	public class IPersistentColumnsAggregatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPersistentColumnsAggregatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPersistentColumnsAggregatorSchema(IPersistentColumnsAggregatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("333054bb-29a4-4f0e-82ae-cc1b1c7cc202");
			Name = "IPersistentColumnsAggregator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,205,49,10,195,48,12,64,209,185,6,223,193,7,40,57,64,183,54,80,240,150,161,23,112,141,108,4,182,100,36,121,10,185,123,218,57,243,231,241,41,117,208,145,50,132,15,136,36,229,98,203,202,84,176,78,73,134,76,203,27,27,196,62,88,204,187,221,187,219,152,223,134,57,32,25,72,249,195,184,129,40,170,1,217,202,109,118,210,103,173,2,53,25,75,120,92,243,38,156,65,245,30,226,43,41,92,200,111,177,135,35,120,119,156,31,161,248,203,156,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("333054bb-29a4-4f0e-82ae-cc1b1c7cc202"));
		}

		#endregion

	}

	#endregion

}

