namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenerateChartWidgetConfigActionSchema

	/// <exclude/>
	public class GenerateChartWidgetConfigActionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenerateChartWidgetConfigActionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenerateChartWidgetConfigActionSchema(GenerateChartWidgetConfigActionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("865a4c42-b70e-482f-a62d-de1821cf950f");
			Name = "GenerateChartWidgetConfigAction";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6859eba8-9d49-4a99-92b8-45e03befab3b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,144,205,74,195,64,16,199,239,129,188,195,144,83,123,233,3,88,20,52,74,65,68,132,138,158,167,155,105,186,184,153,9,179,27,11,138,239,238,166,219,54,146,122,80,186,44,236,97,102,127,255,15,198,134,124,139,134,160,84,194,96,101,86,74,211,10,19,135,82,90,235,36,228,217,103,158,65,60,157,183,92,195,51,169,162,151,117,232,23,27,225,121,158,165,113,219,173,156,53,96,28,122,15,11,98,82,12,84,110,80,195,171,173,106,138,56,94,219,250,218,68,13,134,11,184,65,79,135,173,23,75,219,59,71,77,20,77,243,68,220,235,238,224,42,129,76,160,10,228,61,26,176,21,129,15,218,251,185,247,194,75,179,161,6,31,99,20,184,188,130,226,68,116,216,41,142,118,255,74,125,66,243,134,53,13,112,13,227,134,70,208,212,195,145,248,32,6,157,253,192,149,163,101,130,47,162,47,108,251,156,147,233,207,144,253,81,10,157,50,48,109,79,63,78,138,67,97,176,139,8,41,35,164,144,144,170,43,166,243,129,248,245,111,99,183,228,141,218,179,204,249,223,220,205,198,190,246,79,188,223,234,108,15,133,131,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("865a4c42-b70e-482f-a62d-de1821cf950f"));
		}

		#endregion

	}

	#endregion

}

