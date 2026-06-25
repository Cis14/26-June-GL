namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DefaultAgentsSubIntentsInstallScriptExecutorSchema

	/// <exclude/>
	public class DefaultAgentsSubIntentsInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DefaultAgentsSubIntentsInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DefaultAgentsSubIntentsInstallScriptExecutorSchema(DefaultAgentsSubIntentsInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9cf543c1-e507-4d69-a8af-50f72834dc81");
			Name = "DefaultAgentsSubIntentsInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,83,77,79,34,65,16,61,143,137,255,161,131,151,49,145,9,46,172,44,24,15,6,193,144,104,98,22,60,155,154,153,98,236,216,116,79,250,131,133,24,255,187,213,211,35,2,202,6,47,208,253,186,222,171,170,87,53,18,230,104,74,200,144,77,81,107,48,106,102,147,129,146,51,94,56,13,150,43,121,124,244,122,124,20,57,195,101,193,38,43,99,113,126,185,190,111,82,52,126,226,3,141,158,75,104,201,133,178,201,13,26,94,72,212,4,204,231,32,115,243,53,116,68,127,78,227,84,21,133,32,252,51,160,16,42,5,209,239,123,170,146,201,29,5,132,119,138,56,209,88,80,137,108,32,192,152,62,187,193,25,56,97,175,11,148,214,76,92,58,150,214,159,198,210,88,16,98,146,105,94,218,225,18,51,103,149,174,248,165,75,5,207,88,230,233,63,98,179,62,27,239,145,141,94,43,233,117,109,35,142,34,167,226,30,52,95,128,197,240,88,134,11,35,5,75,5,144,7,185,146,98,197,110,29,207,217,83,190,81,201,35,1,87,76,226,191,234,45,110,116,59,237,30,182,242,89,179,131,231,208,236,66,251,119,243,162,115,158,55,123,240,171,215,234,166,173,244,15,182,27,167,151,155,73,214,234,99,50,143,61,9,250,185,98,116,188,7,9,5,13,229,22,173,119,21,117,220,168,7,86,9,84,77,160,204,67,31,219,77,221,163,125,86,123,187,90,40,42,250,175,147,215,198,143,125,164,17,39,47,92,8,83,79,63,126,52,168,105,199,36,102,126,193,152,219,186,158,50,191,111,81,180,0,205,96,71,96,170,54,135,84,203,213,246,236,38,251,54,54,222,206,117,246,197,235,224,92,116,80,226,36,12,29,227,64,122,59,212,178,106,233,106,199,194,2,86,134,125,168,29,228,142,213,171,250,20,253,199,232,29,110,232,237,141,101,96,179,103,22,15,151,25,150,85,14,92,126,232,70,126,61,146,161,214,138,182,97,4,92,96,206,172,162,128,170,182,31,125,35,141,51,175,91,231,220,111,80,64,183,65,194,222,1,137,143,19,229,152,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9cf543c1-e507-4d69-a8af-50f72834dc81"));
		}

		#endregion

	}

	#endregion

}

