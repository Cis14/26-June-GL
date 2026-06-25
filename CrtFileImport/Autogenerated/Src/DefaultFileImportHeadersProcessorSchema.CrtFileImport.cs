namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DefaultFileImportHeadersProcessorSchema

	/// <exclude/>
	public class DefaultFileImportHeadersProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DefaultFileImportHeadersProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DefaultFileImportHeadersProcessorSchema(DefaultFileImportHeadersProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5d4b90d0-4c5e-4fab-b39c-029dbd674809");
			Name = "DefaultFileImportHeadersProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,83,75,111,219,48,12,62,123,192,254,3,209,93,18,32,176,239,89,155,67,189,116,11,176,161,5,178,245,174,200,116,34,64,150,92,82,42,16,4,253,239,163,45,103,113,147,173,5,118,51,41,126,15,243,225,84,131,220,42,141,240,19,137,20,251,58,228,165,119,181,217,70,82,193,120,151,223,25,139,171,166,245,20,224,240,241,67,22,217,184,45,172,247,28,176,249,124,22,11,210,90,212,29,140,243,175,232,144,140,190,168,249,110,220,211,41,57,86,37,252,87,62,95,186,96,130,65,150,2,41,249,68,184,21,13,40,173,98,158,195,23,172,85,180,225,100,244,27,170,10,137,31,200,107,100,246,212,131,138,162,128,107,227,118,98,42,84,94,131,38,172,111,174,110,21,227,5,176,36,84,193,211,85,177,56,226,56,54,141,162,253,49,22,230,103,83,33,67,149,164,65,119,78,160,246,4,109,18,133,93,162,202,143,12,197,136,162,141,27,107,244,0,122,215,61,204,225,45,151,194,119,232,127,240,212,22,105,127,160,168,229,81,186,243,208,139,165,138,65,248,93,201,201,47,70,18,26,151,134,9,241,85,56,237,168,178,57,108,196,213,228,236,9,14,240,50,184,65,87,37,67,175,221,253,192,176,243,85,103,140,124,16,20,86,233,253,188,207,255,55,48,248,131,28,55,60,107,143,90,224,159,101,179,100,118,208,175,212,126,173,119,216,40,217,219,216,56,184,51,174,74,159,147,213,210,197,6,73,109,44,94,95,86,46,128,188,15,227,12,207,64,122,222,109,174,238,227,71,101,35,78,251,139,201,50,194,16,201,93,98,228,182,136,195,61,13,243,152,36,40,220,44,6,174,124,249,20,149,229,33,159,151,170,237,122,60,27,75,204,96,221,151,150,190,105,21,25,150,131,189,167,202,56,101,87,91,39,151,83,74,187,166,211,238,174,178,191,207,37,101,199,201,151,223,246,24,193,55,20,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5d4b90d0-4c5e-4fab-b39c-029dbd674809"));
		}

		#endregion

	}

	#endregion

}

