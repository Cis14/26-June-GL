namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportValidationServiceSchema

	/// <exclude/>
	public class IFileImportValidationServiceSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportValidationServiceSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportValidationServiceSchema(IFileImportValidationServiceSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cb43cabf-2447-44e4-b1b4-3716ee0c72e1");
			Name = "IFileImportValidationService";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,81,193,106,131,64,16,61,27,200,63,12,230,98,64,252,128,148,94,90,8,88,176,9,49,52,135,144,195,70,71,187,84,119,183,187,107,64,74,254,189,163,171,73,3,197,211,50,111,223,155,247,102,70,176,26,141,98,25,194,30,181,102,70,22,54,122,149,162,224,101,163,153,229,82,68,107,94,97,92,43,169,237,124,246,51,159,121,141,225,162,132,180,53,22,235,40,69,125,225,25,38,50,199,234,105,234,51,58,224,153,8,68,89,104,44,169,47,196,194,162,46,200,121,5,241,221,227,131,85,60,239,141,7,117,175,57,14,5,37,179,154,101,54,120,167,216,240,12,254,132,208,95,158,72,169,154,115,197,51,224,163,217,164,23,144,160,27,241,150,49,65,251,41,115,179,130,109,223,166,207,226,29,55,10,221,110,198,56,167,30,166,9,99,113,145,95,24,56,89,151,111,187,73,247,126,8,59,252,110,208,216,181,212,53,179,132,19,53,65,99,88,137,14,138,222,140,20,33,188,200,188,77,109,91,225,3,229,134,70,7,205,148,194,60,236,236,60,111,71,135,147,194,224,116,215,126,11,222,127,67,143,122,24,32,12,238,172,97,33,67,110,208,238,93,186,11,122,11,20,185,219,16,85,87,119,213,63,208,245,23,148,49,173,177,85,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cb43cabf-2447-44e4-b1b4-3716ee0c72e1"));
		}

		#endregion

	}

	#endregion

}

