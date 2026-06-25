namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetCreatioUrlExceptionSchema

	/// <exclude/>
	public class GetCreatioUrlExceptionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetCreatioUrlExceptionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetCreatioUrlExceptionSchema(GetCreatioUrlExceptionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e8a131b9-bd91-423b-a0a8-1db8f77c784a");
			Name = "GetCreatioUrlException";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("e340cd47-dd91-41d9-9076-90ff98ffd14e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,81,59,111,194,48,16,158,19,41,255,225,148,46,116,73,118,26,24,26,85,21,91,37,232,84,117,112,204,37,88,114,236,232,236,136,82,212,255,94,63,2,5,68,189,88,254,124,119,223,227,20,235,209,12,140,35,108,144,136,25,221,218,162,214,170,21,221,72,204,10,173,138,231,161,215,74,10,133,181,212,227,118,165,44,118,241,39,75,143,89,154,140,70,168,14,214,7,99,177,127,202,210,44,5,119,30,8,59,87,1,181,100,198,204,225,21,109,77,232,155,222,73,190,124,113,28,98,127,44,46,203,18,42,51,246,61,163,195,242,15,218,236,16,240,84,11,118,199,44,8,227,110,210,123,5,251,29,122,12,9,61,200,20,56,241,154,160,67,107,189,28,247,3,19,35,140,36,139,11,162,242,154,233,99,141,36,152,20,223,172,145,248,233,252,12,99,35,5,7,238,149,255,35,28,230,112,97,34,57,122,35,73,114,246,172,149,177,52,114,171,201,89,127,11,227,98,197,141,209,8,172,148,176,65,0,58,31,160,112,15,194,245,51,229,54,162,219,96,164,50,136,192,9,219,69,126,95,79,94,46,163,222,226,204,82,222,210,84,3,35,214,131,114,235,94,228,110,229,134,117,152,47,125,198,211,35,38,188,69,195,73,52,104,2,115,8,181,168,202,208,27,70,77,233,220,215,49,115,190,125,250,211,196,71,223,144,204,161,97,6,103,39,12,142,240,51,229,133,106,27,35,11,239,136,94,131,14,251,5,215,199,84,50,160,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e8a131b9-bd91-423b-a0a8-1db8f77c784a"));
		}

		#endregion

	}

	#endregion

}

