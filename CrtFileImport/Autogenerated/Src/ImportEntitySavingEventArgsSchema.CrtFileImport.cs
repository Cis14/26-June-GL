namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportEntitySavingEventArgsSchema

	/// <exclude/>
	public class ImportEntitySavingEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportEntitySavingEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportEntitySavingEventArgsSchema(ImportEntitySavingEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9ddd5a40-8a06-4e75-ab40-8d237a2f6898");
			Name = "ImportEntitySavingEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("457f3a21-2903-49a8-8e29-be203e6136ba");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,143,65,10,194,64,12,69,215,45,244,14,3,238,61,128,93,73,81,112,33,136,246,2,99,77,135,129,54,41,147,140,82,196,187,27,167,34,138,32,238,146,159,151,255,19,180,61,240,96,27,48,53,132,96,153,90,153,87,132,173,119,49,88,241,132,243,181,239,96,211,15,20,164,200,175,69,158,69,246,232,204,97,100,129,190,44,114,85,102,1,156,146,166,234,44,243,194,76,240,10,197,203,120,176,103,165,87,103,64,89,6,199,9,31,226,177,243,141,105,30,244,47,216,168,21,182,180,5,102,235,224,205,35,187,38,159,87,238,46,208,0,65,60,104,248,46,153,79,243,103,80,244,40,166,38,177,221,158,46,92,81,212,246,241,72,150,57,144,50,21,252,44,110,223,139,106,222,232,5,112,250,119,121,6,120,154,14,75,253,164,126,138,170,221,1,66,107,25,210,120,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9ddd5a40-8a06-4e75-ab40-8d237a2f6898"));
		}

		#endregion

	}

	#endregion

}

