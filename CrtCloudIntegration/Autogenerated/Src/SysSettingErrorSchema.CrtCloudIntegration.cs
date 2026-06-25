namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SysSettingErrorSchema

	/// <exclude/>
	public class SysSettingErrorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SysSettingErrorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SysSettingErrorSchema(SysSettingErrorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("69961719-9444-4a29-85dd-698b7eef2210");
			Name = "SysSettingError";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,203,110,194,48,16,60,131,196,63,172,210,75,43,85,201,157,215,133,86,8,245,130,68,251,1,198,44,193,82,98,71,94,167,136,34,254,189,142,237,134,198,130,130,212,155,189,158,157,204,35,146,149,72,21,227,8,51,109,102,133,170,55,11,105,48,215,204,8,37,211,15,35,10,97,4,82,250,170,181,210,52,232,31,7,253,65,191,247,160,49,183,239,48,43,24,209,16,86,7,90,161,49,66,230,14,230,32,89,150,193,152,234,178,100,250,48,13,247,23,36,174,197,26,129,73,192,6,9,251,29,74,32,85,34,168,45,208,129,128,60,15,236,24,129,144,92,105,141,220,192,39,43,106,76,127,88,179,136,118,76,136,172,32,5,92,227,118,146,220,99,36,125,147,106,47,221,57,129,172,33,170,234,117,33,56,240,198,81,108,8,134,176,8,206,122,62,128,115,2,74,146,209,53,55,150,116,8,75,71,226,17,113,0,110,176,144,86,5,43,196,23,18,48,144,184,183,38,201,48,201,93,0,102,135,206,76,48,18,169,72,178,169,151,151,182,244,89,204,63,174,152,102,37,72,219,234,36,161,118,127,166,54,152,76,223,45,189,157,25,44,219,152,185,125,72,199,153,219,186,76,146,107,85,87,126,215,29,59,232,16,90,36,244,209,38,210,144,119,191,255,12,97,236,104,158,224,216,16,244,86,29,12,76,162,165,81,4,154,55,187,22,229,56,220,227,41,244,129,114,227,43,233,246,179,212,170,66,221,52,127,71,59,115,52,228,74,184,148,210,31,161,187,223,179,189,94,139,249,76,112,198,135,0,67,50,81,24,71,200,209,140,224,116,75,177,253,65,233,138,114,95,217,127,164,199,12,183,181,251,142,130,120,250,229,32,234,200,79,187,195,211,55,58,175,128,44,144,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("69961719-9444-4a29-85dd-698b7eef2210"));
		}

		#endregion

	}

	#endregion

}

