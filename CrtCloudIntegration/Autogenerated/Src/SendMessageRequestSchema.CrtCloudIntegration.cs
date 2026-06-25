namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SendMessageRequestSchema

	/// <exclude/>
	public class SendMessageRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SendMessageRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SendMessageRequestSchema(SendMessageRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("394b7098-d52a-4248-b5d6-5a4098ccf4ac");
			Name = "SendMessageRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,147,193,78,195,48,12,134,207,76,226,29,172,113,129,75,123,223,0,9,6,154,56,12,16,219,13,33,100,90,183,88,106,154,17,187,147,96,226,221,73,210,109,176,13,13,141,83,106,215,191,155,239,183,91,163,33,153,98,70,48,33,231,80,108,161,201,192,214,5,151,141,67,101,91,39,131,235,241,200,230,84,201,97,103,126,216,57,104,132,235,18,198,239,162,100,250,27,113,242,208,212,202,134,146,49,57,198,138,63,98,7,95,229,235,142,28,149,62,128,65,133,34,61,24,83,157,143,72,4,75,122,160,183,134,68,99,85,154,166,112,42,141,49,232,222,207,23,113,84,64,97,29,184,182,18,212,130,120,61,176,49,148,51,42,129,105,91,37,203,22,233,70,143,83,33,194,74,44,100,142,138,179,238,159,172,201,37,10,121,138,25,103,203,251,117,33,13,221,30,175,80,209,171,212,97,166,79,62,49,109,94,42,206,32,139,183,220,198,130,30,108,247,242,178,121,228,93,217,114,239,236,148,156,50,121,111,238,99,199,246,253,166,33,49,49,36,21,240,126,72,56,245,149,128,12,114,37,63,93,216,182,161,205,204,176,106,104,21,78,94,87,214,193,213,228,238,135,240,187,46,2,143,200,188,144,59,190,245,219,2,103,208,93,104,186,39,193,128,165,3,215,225,18,11,120,88,158,115,40,73,251,225,166,125,248,220,7,41,15,99,69,63,228,176,80,96,139,56,241,176,106,92,131,168,11,79,126,37,12,234,126,180,113,111,214,68,187,73,67,253,51,234,58,233,226,251,97,216,23,250,127,196,56,53,224,156,252,79,83,48,185,253,72,118,168,119,35,69,225,51,231,235,76,195,134,243,118,132,55,249,111,72,71,30,182,221,213,24,183,217,245,164,207,125,1,153,117,57,156,79,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("394b7098-d52a-4248-b5d6-5a4098ccf4ac"));
		}

		#endregion

	}

	#endregion

}

