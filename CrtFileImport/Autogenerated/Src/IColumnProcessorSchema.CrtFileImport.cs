namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IColumnProcessorSchema

	/// <exclude/>
	public class IColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IColumnProcessorSchema(IColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("30c9471b-7e5a-4dc5-b5d7-b25c0e8ae3a9");
			Name = "IColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("bdeb1980-c322-4103-af7f-58bfe7162080");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,142,177,10,195,48,12,68,231,4,252,15,130,172,37,31,208,177,129,130,151,146,161,63,224,166,114,48,216,146,145,236,41,228,223,235,182,116,40,25,239,238,221,113,228,18,106,118,11,194,29,69,156,178,47,227,196,228,195,90,197,149,192,52,94,67,68,155,50,75,49,253,102,250,110,16,92,155,15,150,10,138,111,205,51,216,137,99,77,52,11,47,168,202,98,250,198,229,250,136,97,129,240,195,14,20,180,226,141,105,70,209,160,5,169,252,229,39,176,23,167,120,88,238,54,216,63,251,3,210,243,123,229,45,155,247,2,228,109,96,208,203,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("30c9471b-7e5a-4dc5-b5d7-b25c0e8ae3a9"));
		}

		#endregion

	}

	#endregion

}

