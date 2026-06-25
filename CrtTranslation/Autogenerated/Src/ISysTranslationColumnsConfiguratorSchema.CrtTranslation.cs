namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISysTranslationColumnsConfiguratorSchema

	/// <exclude/>
	public class ISysTranslationColumnsConfiguratorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISysTranslationColumnsConfiguratorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISysTranslationColumnsConfiguratorSchema(ISysTranslationColumnsConfiguratorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("72ebf492-d797-436e-ab08-aef6a2bee1fc");
			Name = "ISysTranslationColumnsConfigurator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("0b3cc894-bd0d-4e1b-bb7d-87203708d013");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,193,110,195,32,12,64,207,141,148,127,64,221,101,187,132,123,155,229,82,77,85,14,147,38,173,63,64,137,147,33,129,83,217,112,168,170,254,251,8,73,218,174,83,181,157,0,155,103,63,3,42,7,124,80,26,196,14,136,20,247,173,47,54,61,182,166,11,164,188,233,177,216,145,66,182,105,159,103,167,60,91,4,54,216,137,207,35,123,112,241,174,181,160,135,36,23,91,64,32,163,215,121,22,111,61,17,116,49,42,106,244,64,109,108,176,18,117,100,110,170,69,52,56,228,107,183,158,18,41,165,20,37,7,231,20,29,171,233,60,145,35,40,244,72,198,245,138,22,51,41,111,208,67,216,91,163,133,153,29,254,165,176,56,37,141,203,4,239,224,191,250,134,87,226,35,85,27,147,247,146,41,176,5,207,127,168,66,113,161,229,61,94,18,248,64,200,85,201,0,66,19,180,175,203,95,178,179,43,44,101,85,202,153,24,74,212,111,24,28,144,218,91,40,31,99,213,96,249,248,9,160,121,126,89,79,243,3,54,227,19,164,243,121,252,214,31,193,243,55,149,178,9,108,63,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("72ebf492-d797-436e-ab08-aef6a2bee1fc"));
		}

		#endregion

	}

	#endregion

}

