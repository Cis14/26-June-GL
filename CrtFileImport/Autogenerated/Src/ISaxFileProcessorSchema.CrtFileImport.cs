namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISaxFileProcessorSchema

	/// <exclude/>
	public class ISaxFileProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISaxFileProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISaxFileProcessorSchema(ISaxFileProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6c7807a8-a16e-4cad-b5b6-7f795f2cb0e0");
			Name = "ISaxFileProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,83,77,107,27,49,16,61,59,144,255,48,36,151,246,226,189,39,219,189,4,215,93,104,192,196,73,239,178,52,235,21,93,141,22,141,148,216,132,252,247,72,90,175,107,59,253,34,133,30,53,188,55,243,62,16,9,131,220,11,137,112,143,206,9,182,141,159,222,88,106,244,58,56,225,181,165,233,103,221,97,109,122,235,252,249,217,243,249,217,36,176,166,53,44,183,236,209,92,159,188,35,181,235,80,38,30,79,231,72,232,180,140,152,136,186,116,184,142,83,168,201,163,107,226,189,43,168,151,98,147,150,47,156,149,200,108,93,6,246,97,213,105,9,122,196,253,12,54,73,58,246,43,111,209,183,86,241,21,44,50,53,111,153,20,69,1,37,7,99,132,219,86,227,96,183,2,25,214,250,17,9,58,205,30,90,20,10,29,79,247,172,226,148,86,246,194,9,3,20,163,250,116,161,115,20,139,52,193,40,145,107,117,81,213,164,112,3,113,47,167,192,202,34,227,127,208,29,250,224,136,171,175,135,231,202,98,28,39,92,61,163,96,208,137,85,135,229,16,118,76,50,24,170,96,142,254,203,64,249,48,15,90,193,219,251,31,175,127,99,249,166,69,249,29,124,43,60,52,49,68,144,150,188,208,196,163,14,16,164,192,217,167,127,242,255,75,199,199,38,31,109,212,159,5,213,156,26,253,38,58,173,222,229,106,87,228,97,141,72,94,123,29,171,109,156,53,128,27,137,221,255,170,116,60,253,199,78,103,9,184,173,224,46,102,63,219,145,222,229,127,137,30,66,31,123,179,30,88,182,104,4,172,182,160,213,223,58,78,196,101,230,61,156,246,151,75,170,73,251,187,61,102,144,120,196,25,213,93,34,169,225,27,230,247,203,240,215,143,134,47,175,134,121,243,174,99,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6c7807a8-a16e-4cad-b5b6-7f795f2cb0e0"));
		}

		#endregion

	}

	#endregion

}

