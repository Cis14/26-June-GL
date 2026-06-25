namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportLogItemSchema

	/// <exclude/>
	public class IImportLogItemSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportLogItemSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportLogItemSchema(IImportLogItemSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a68587a1-ef86-40c0-811b-ca07c771c3c7");
			Name = "IImportLogItem";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1101e5cd-b945-4f88-8001-faccb4fdb24c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,221,10,194,48,12,133,175,55,216,59,4,188,223,238,167,120,35,10,3,133,129,123,129,58,179,82,180,63,164,25,40,226,187,219,173,42,3,97,222,165,39,95,206,73,106,132,70,239,68,139,208,32,145,240,182,227,124,99,77,167,100,79,130,149,53,249,78,93,177,210,206,18,103,233,35,75,179,52,89,16,202,208,129,202,48,82,23,102,75,168,34,177,183,178,98,212,35,229,250,211,85,181,160,62,208,15,147,68,183,175,93,77,214,33,177,66,95,66,61,14,199,126,81,20,176,242,189,214,130,238,235,143,16,92,32,108,238,133,196,252,11,21,83,202,51,41,35,225,16,33,120,12,90,34,145,151,99,225,223,197,115,38,99,123,107,209,13,127,0,158,69,123,1,166,112,198,124,218,113,0,155,129,251,19,184,64,115,142,119,143,239,168,78,197,160,188,0,174,230,212,133,156,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a68587a1-ef86-40c0-811b-ca07c771c3c7"));
		}

		#endregion

	}

	#endregion

}

