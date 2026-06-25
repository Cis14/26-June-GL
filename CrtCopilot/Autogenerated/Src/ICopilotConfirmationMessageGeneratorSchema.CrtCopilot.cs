namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotConfirmationMessageGeneratorSchema

	/// <exclude/>
	public class ICopilotConfirmationMessageGeneratorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotConfirmationMessageGeneratorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotConfirmationMessageGeneratorSchema(ICopilotConfirmationMessageGeneratorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("de4aa0ec-3245-4349-b351-2cd5ef15cff8");
			Name = "ICopilotConfirmationMessageGenerator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,143,177,14,194,48,16,67,231,84,202,63,68,98,129,133,15,128,49,3,234,208,141,31,8,173,91,69,74,239,162,228,144,144,16,255,78,74,131,88,58,48,158,237,231,147,201,205,200,209,245,48,54,193,137,231,163,229,232,3,139,110,158,186,209,141,218,37,76,158,201,180,36,72,99,9,158,76,91,35,150,105,244,105,94,40,234,144,179,155,112,1,33,57,225,244,97,227,253,22,124,111,252,23,253,147,84,229,179,82,53,90,109,83,125,108,160,251,141,214,43,115,40,183,224,33,29,15,8,70,126,194,225,92,250,95,235,58,208,176,14,92,206,162,189,1,137,1,162,192,16,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("de4aa0ec-3245-4349-b351-2cd5ef15cff8"));
		}

		#endregion

	}

	#endregion

}

