namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotContextSanitizerSchema

	/// <exclude/>
	public class CopilotContextSanitizerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotContextSanitizerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotContextSanitizerSchema(CopilotContextSanitizerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5613b6a0-96bf-4441-a75b-e810dd24a055");
			Name = "CopilotContextSanitizer";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,81,77,107,27,49,16,61,175,193,255,97,216,92,28,40,187,36,161,80,176,227,139,15,33,224,124,16,155,92,74,40,178,60,89,171,104,165,69,51,235,56,105,253,223,171,143,93,199,78,15,66,104,230,205,155,167,247,90,82,166,130,197,59,49,214,227,225,96,56,48,162,70,106,132,68,152,57,20,172,108,49,179,141,210,150,97,56,248,51,28,100,63,31,86,100,53,50,142,242,31,197,85,113,5,127,97,166,5,17,40,2,227,81,202,64,75,8,194,172,225,77,105,13,43,4,135,181,221,226,58,182,26,105,235,176,209,161,70,65,72,249,249,139,39,109,218,149,86,18,136,253,66,9,50,242,117,107,103,214,48,238,120,33,140,98,245,129,14,130,136,172,113,106,43,24,65,90,67,97,39,195,157,216,45,216,121,234,57,154,138,55,112,13,223,47,46,199,255,99,41,130,96,110,77,149,240,75,81,121,112,126,54,127,184,191,249,181,88,62,221,222,223,156,229,209,138,44,43,203,18,38,212,214,181,112,239,211,190,240,232,172,68,242,218,129,55,8,149,218,162,129,173,208,45,22,112,251,26,107,241,21,12,17,253,186,96,135,98,2,157,196,225,78,34,174,41,72,4,185,17,78,72,70,71,223,160,95,161,216,27,212,104,159,2,29,17,190,41,63,26,158,39,90,129,69,85,28,196,150,95,213,78,26,79,95,67,136,245,58,143,60,249,116,121,160,100,11,77,250,78,49,41,35,242,115,208,33,183,206,80,68,119,32,31,98,250,233,164,236,187,209,225,147,248,236,234,55,74,238,109,122,14,248,81,87,139,195,231,41,194,76,189,194,232,224,84,231,83,186,158,143,97,17,119,84,47,186,128,167,95,35,63,12,100,73,219,105,198,227,212,219,199,107,159,226,237,129,81,69,4,132,182,63,251,127,108,194,208,145,23,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5613b6a0-96bf-4441-a75b-e810dd24a055"));
		}

		#endregion

	}

	#endregion

}

