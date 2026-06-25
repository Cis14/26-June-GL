namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportHeadersProcessorSchema

	/// <exclude/>
	public class IFileImportHeadersProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportHeadersProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportHeadersProcessorSchema(IFileImportHeadersProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f6a6f9f3-e056-43f4-b54a-65bfa522f74f");
			Name = "IFileImportHeadersProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,81,75,106,195,48,16,93,39,144,59,12,222,180,133,98,29,160,142,55,38,109,179,78,233,94,81,70,137,64,31,51,35,21,66,233,221,43,203,73,108,104,3,2,33,233,205,251,201,75,135,220,75,133,240,129,68,146,131,142,117,23,188,54,199,68,50,154,224,235,87,99,113,235,250,64,17,190,87,203,69,98,227,143,176,59,115,68,151,145,214,162,26,96,92,191,161,71,50,234,229,134,153,19,18,214,27,31,77,52,200,3,32,175,62,237,173,81,96,124,68,210,131,254,118,18,122,71,121,64,226,142,80,198,64,69,118,1,66,8,104,56,57,39,233,220,222,110,10,6,25,204,232,80,5,155,156,103,208,121,172,167,160,144,25,15,112,42,124,15,12,95,210,38,228,122,226,19,127,8,155,94,146,116,224,115,47,235,138,66,136,59,117,66,39,171,182,97,68,80,132,122,93,149,40,231,203,131,104,57,36,202,1,184,156,139,180,42,174,174,110,234,70,20,210,59,34,35,234,179,88,171,218,113,7,77,193,129,206,141,252,51,76,24,19,121,110,167,246,33,104,152,249,27,91,236,10,111,246,215,136,235,196,192,177,221,248,228,144,228,222,98,51,7,182,151,46,199,242,199,59,126,156,71,133,169,142,231,129,233,46,85,201,208,194,60,216,211,240,237,63,171,101,94,191,9,103,190,54,115,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f6a6f9f3-e056-43f4-b54a-65bfa522f74f"));
		}

		#endregion

	}

	#endregion

}

