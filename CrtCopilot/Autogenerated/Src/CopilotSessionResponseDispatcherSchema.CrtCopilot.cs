namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotSessionResponseDispatcherSchema

	/// <exclude/>
	public class CopilotSessionResponseDispatcherSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotSessionResponseDispatcherSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotSessionResponseDispatcherSchema(CopilotSessionResponseDispatcherSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("83b9bdbd-fb0d-4936-b15a-bd2ab69d16c9");
			Name = "CopilotSessionResponseDispatcher";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,193,74,3,49,16,134,207,17,250,14,3,189,108,65,246,1,172,10,90,173,246,32,136,237,77,68,166,217,89,55,52,77,150,36,91,41,165,239,238,164,233,182,86,172,69,88,200,38,243,207,252,223,12,99,112,78,190,70,73,48,112,132,65,217,124,96,107,165,109,232,156,173,58,103,162,241,202,124,192,120,233,3,205,57,162,53,73,214,24,159,63,144,33,167,100,255,167,102,82,113,153,130,31,142,71,242,9,250,153,223,199,39,228,28,122,91,6,54,112,148,15,81,6,235,20,69,5,107,94,239,168,196,70,135,91,101,98,114,22,150,53,217,50,27,109,49,199,228,61,3,189,112,19,140,69,119,138,155,9,178,34,215,235,189,113,118,221,76,181,146,32,53,122,15,167,82,224,2,78,150,229,154,171,13,151,232,58,250,96,9,12,21,233,194,95,192,179,83,11,12,148,130,117,186,64,108,217,26,189,132,209,189,105,230,228,112,170,233,242,136,201,35,154,66,147,187,134,247,42,253,109,39,32,186,100,138,100,118,232,60,224,172,224,154,56,174,232,191,105,117,107,159,218,62,213,76,246,31,170,22,170,7,113,49,132,216,81,194,21,124,3,22,98,253,55,245,19,133,202,22,191,3,163,95,26,9,113,63,160,165,188,137,79,217,33,27,248,116,158,195,0,141,36,173,227,226,154,137,157,145,1,25,152,167,72,59,211,162,150,188,87,40,43,200,22,232,90,86,80,102,63,232,86,40,84,9,25,126,162,10,173,44,103,135,52,130,4,178,115,150,161,183,203,18,135,41,199,244,253,164,94,111,142,245,241,81,241,43,127,95,124,97,74,24,154,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("83b9bdbd-fb0d-4936-b15a-bd2ab69d16c9"));
		}

		#endregion

	}

	#endregion

}

