namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IGeneratedWebFormPostProcessHandlerSchema

	/// <exclude/>
	public class IGeneratedWebFormPostProcessHandlerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IGeneratedWebFormPostProcessHandlerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IGeneratedWebFormPostProcessHandlerSchema(IGeneratedWebFormPostProcessHandlerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("42225206-bcb0-46e4-8cc2-2d5b3676a730");
			Name = "IGeneratedWebFormPostProcessHandler";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("91507ba4-1e7b-4fdd-954f-ec66d764e88d");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,83,77,79,195,48,12,61,131,196,127,176,224,2,18,172,119,62,118,25,108,236,128,152,180,33,14,136,67,72,220,17,169,73,138,147,78,76,136,255,142,211,180,27,45,48,113,180,251,158,253,158,95,106,133,65,95,10,137,176,64,34,225,93,30,6,35,103,115,189,172,72,4,237,44,124,28,236,239,85,94,219,37,204,215,62,160,185,232,213,12,47,10,148,17,235,7,19,180,72,90,110,49,35,71,184,173,234,207,34,160,122,196,151,177,35,51,71,90,105,25,1,12,57,34,92,198,133,83,27,144,114,150,116,14,211,62,97,230,124,152,145,147,232,253,173,176,170,64,170,169,89,150,193,165,175,140,17,180,30,54,53,195,86,90,161,7,131,225,213,41,200,29,65,201,116,40,19,63,234,113,57,132,87,4,73,24,119,0,218,160,195,26,114,114,6,10,30,31,33,165,88,226,160,93,145,125,219,81,86,47,133,150,160,91,181,255,17,251,155,163,14,32,94,155,103,111,110,113,87,107,247,231,48,171,183,165,143,125,183,117,227,230,29,101,21,216,110,52,212,247,25,189,251,18,165,206,53,219,236,91,251,233,45,117,74,65,194,128,229,23,114,117,88,121,36,126,23,54,5,125,56,92,240,150,216,3,185,105,14,46,179,154,241,251,128,102,233,84,37,110,171,129,19,226,163,179,44,218,77,103,7,230,90,4,145,216,177,2,197,101,202,106,118,63,95,156,17,190,85,232,195,238,49,41,225,86,68,147,247,31,26,86,78,171,246,172,199,15,29,255,208,61,199,41,76,42,189,185,235,84,157,66,76,118,172,177,80,62,138,126,122,134,86,127,3,109,117,156,92,52,121,163,85,41,242,186,254,76,63,68,167,201,189,47,250,97,64,113,173,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("42225206-bcb0-46e4-8cc2-2d5b3676a730"));
		}

		#endregion

	}

	#endregion

}

