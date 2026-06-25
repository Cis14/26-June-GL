namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportTagManagerSchema

	/// <exclude/>
	public class IFileImportTagManagerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportTagManagerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportTagManagerSchema(IFileImportTagManagerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ee7c80a9-3a5c-4e84-9235-9be88e7c35d7");
			Name = "IFileImportTagManager";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,147,77,78,195,48,16,133,215,141,148,59,140,186,1,36,148,28,128,80,169,148,34,117,1,27,202,1,166,241,56,24,197,118,228,113,34,69,168,119,199,249,227,79,65,42,187,238,198,214,55,207,243,158,109,131,154,184,194,156,96,79,206,33,91,233,147,141,53,82,21,181,67,175,172,73,30,84,73,59,93,89,231,227,232,61,142,22,85,125,40,85,14,202,120,114,178,107,220,125,17,123,44,30,209,96,65,46,128,29,188,72,211,20,50,174,181,70,215,174,166,141,141,35,244,196,224,177,224,228,147,74,127,99,89,133,14,53,152,48,226,237,146,201,8,114,203,213,182,33,227,97,88,129,61,188,81,238,147,44,237,201,249,70,116,5,79,109,161,174,117,40,248,71,75,99,149,24,103,10,6,248,114,80,29,207,184,134,59,146,214,141,6,183,198,43,175,136,159,177,161,94,114,29,212,59,89,190,186,137,163,63,13,119,120,192,202,178,183,12,65,15,8,243,87,80,189,40,9,160,78,183,61,151,44,122,119,253,68,115,121,124,75,162,237,72,241,143,32,238,169,164,233,230,65,201,193,119,200,19,132,37,54,23,126,76,228,92,130,24,198,125,178,254,133,73,204,101,177,150,225,23,156,248,52,142,113,116,252,0,163,6,56,38,111,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ee7c80a9-3a5c-4e84-9235-9be88e7c35d7"));
		}

		#endregion

	}

	#endregion

}

