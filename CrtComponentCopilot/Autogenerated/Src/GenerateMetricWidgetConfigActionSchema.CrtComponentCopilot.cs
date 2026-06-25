namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenerateMetricWidgetConfigActionSchema

	/// <exclude/>
	public class GenerateMetricWidgetConfigActionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenerateMetricWidgetConfigActionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenerateMetricWidgetConfigActionSchema(GenerateMetricWidgetConfigActionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("77f0bcfd-09ab-431d-a980-3c77113a120b");
			Name = "GenerateMetricWidgetConfigAction";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6859eba8-9d49-4a99-92b8-45e03befab3b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,144,205,74,195,64,16,199,239,129,188,195,144,83,123,233,3,88,20,52,74,161,84,17,42,122,158,110,166,113,112,51,19,118,55,22,20,223,221,141,219,166,146,122,80,92,22,246,48,179,191,255,135,96,67,190,69,67,80,58,194,192,58,43,181,105,85,72,66,169,45,91,13,121,246,158,103,16,79,231,89,106,120,32,231,208,235,54,244,139,141,202,60,207,210,184,237,54,150,13,24,139,222,195,130,132,28,6,186,165,224,216,60,113,85,83,228,201,150,235,75,19,69,4,206,224,10,61,29,214,30,153,118,55,150,154,168,154,230,9,185,23,254,162,59,13,100,2,85,160,175,209,1,87,4,62,162,163,161,165,87,89,155,103,106,240,46,102,129,243,11,40,78,85,143,75,197,96,248,183,216,123,52,47,88,211,64,47,93,24,119,52,130,166,38,6,226,74,13,90,126,195,141,165,117,130,47,162,47,108,251,160,147,233,247,148,253,113,20,58,39,32,180,59,253,56,41,14,141,65,202,8,41,36,164,148,144,202,43,166,243,35,242,227,207,206,174,201,27,199,255,114,231,127,180,55,27,27,219,63,241,126,2,248,238,230,52,135,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("77f0bcfd-09ab-431d-a980-3c77113a120b"));
		}

		#endregion

	}

	#endregion

}

