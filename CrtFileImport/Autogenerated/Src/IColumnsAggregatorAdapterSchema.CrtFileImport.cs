namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IColumnsAggregatorAdapterSchema

	/// <exclude/>
	public class IColumnsAggregatorAdapterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IColumnsAggregatorAdapterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IColumnsAggregatorAdapterSchema(IColumnsAggregatorAdapterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e345191c-d19b-4e05-ac17-16031da58852");
			Name = "IColumnsAggregatorAdapter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,144,193,138,194,64,12,134,207,91,232,59,4,189,46,157,251,226,10,34,8,189,136,7,95,96,172,153,50,208,73,134,100,122,16,241,221,77,183,42,11,203,122,75,242,229,255,19,126,242,9,53,251,14,225,136,34,94,57,148,102,203,20,98,63,138,47,145,169,217,197,1,219,148,89,74,93,93,235,170,174,62,150,130,189,17,128,150,10,74,48,241,23,180,91,30,198,68,186,233,123,163,190,176,108,206,62,27,254,81,56,231,96,165,99,74,94,46,235,71,255,18,67,96,1,193,60,76,181,96,64,65,234,80,193,46,172,20,17,58,155,125,47,30,254,7,97,99,202,178,112,235,230,233,236,126,89,231,241,52,196,14,226,203,253,223,207,192,190,222,51,29,80,52,106,65,42,127,22,63,161,125,67,225,10,183,57,15,164,243,28,201,212,218,236,14,29,155,151,34,85,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e345191c-d19b-4e05-ac17-16031da58852"));
		}

		#endregion

	}

	#endregion

}

