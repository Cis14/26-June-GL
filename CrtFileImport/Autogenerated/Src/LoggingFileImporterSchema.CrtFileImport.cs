namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LoggingFileImporterSchema

	/// <exclude/>
	public class LoggingFileImporterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LoggingFileImporterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LoggingFileImporterSchema(LoggingFileImporterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("10ecd8e5-f4a7-4119-9170-ce80b8367b66");
			Name = "LoggingFileImporter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,84,93,75,195,48,20,125,238,192,255,16,230,75,247,210,31,80,17,145,161,50,112,56,240,227,69,68,178,246,46,6,218,164,220,164,123,17,255,187,55,105,252,152,137,117,178,135,178,244,246,156,123,206,185,55,76,241,22,76,199,43,96,119,128,200,141,222,216,98,174,213,70,138,30,185,149,90,21,151,178,129,69,219,105,180,71,147,215,163,73,214,27,169,4,19,141,94,243,166,44,231,186,109,9,116,173,133,160,242,201,231,247,239,221,16,126,171,23,151,188,178,26,37,24,66,16,230,24,65,144,38,155,55,220,152,146,133,174,95,14,0,61,236,241,102,75,109,100,13,79,244,210,245,235,70,86,172,114,148,20,131,149,108,183,65,246,234,155,124,137,105,101,44,246,206,8,105,174,124,187,1,17,90,39,154,230,247,6,144,136,10,42,55,36,214,239,188,206,72,115,205,13,228,63,203,110,126,217,91,144,7,85,15,14,118,237,172,80,119,128,150,102,82,186,179,37,46,212,193,15,202,45,183,192,22,228,136,61,55,218,207,155,170,1,52,212,221,227,117,50,1,54,156,50,4,219,163,242,28,118,118,198,114,127,56,117,216,37,87,92,0,22,87,96,93,78,138,54,173,252,78,207,187,142,28,2,78,103,51,47,227,124,255,105,126,9,246,69,215,41,231,31,30,117,88,29,219,106,73,142,253,60,243,225,103,197,145,110,35,77,215,176,238,243,24,134,150,145,185,98,161,54,58,159,14,224,91,203,209,78,131,53,55,236,34,244,250,70,61,73,50,47,84,29,120,111,227,230,150,128,2,62,56,86,186,149,252,207,105,162,65,108,59,165,50,146,33,1,223,59,16,45,165,2,99,230,186,233,91,245,192,155,254,191,129,18,13,226,64,41,149,145,64,9,248,222,129,22,74,218,67,22,20,243,19,215,42,214,24,187,98,17,122,239,48,183,124,123,208,109,139,249,113,152,132,198,72,152,24,29,133,249,241,71,48,84,119,139,84,123,7,78,213,184,34,102,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("10ecd8e5-f4a7-4119-9170-ce80b8367b66"));
		}

		#endregion

	}

	#endregion

}

