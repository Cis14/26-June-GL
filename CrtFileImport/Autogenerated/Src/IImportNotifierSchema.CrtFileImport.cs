namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportNotifierSchema

	/// <exclude/>
	public class IImportNotifierSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportNotifierSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportNotifierSchema(IImportNotifierSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d3fe64f7-9465-476a-b7e1-7f9edad4dc1e");
			Name = "IImportNotifier";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,144,77,106,195,64,12,133,215,54,248,14,34,171,20,138,231,0,117,3,165,184,144,77,55,233,5,228,177,236,42,120,126,152,145,13,38,228,238,157,216,77,104,75,187,232,82,210,211,167,247,100,209,80,244,168,9,222,40,4,140,174,147,242,217,217,142,251,49,160,176,179,229,11,15,180,55,222,5,41,242,83,145,23,121,230,199,102,96,13,108,133,66,119,89,221,175,243,87,39,220,49,133,36,73,194,44,83,74,65,21,71,99,48,204,187,107,227,83,20,65,15,76,86,64,222,81,128,151,125,72,53,203,12,28,161,33,182,61,68,156,168,45,111,40,245,147,85,121,12,104,192,166,12,143,155,72,182,165,176,217,85,106,233,254,46,162,41,221,120,10,125,252,174,155,28,183,171,179,121,235,154,35,105,129,21,119,15,107,180,122,113,118,192,41,217,170,175,12,184,209,238,30,150,199,252,63,113,138,170,157,241,3,201,159,65,191,120,171,109,187,189,156,202,206,69,126,254,0,59,147,151,160,186,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d3fe64f7-9465-476a-b7e1-7f9edad4dc1e"));
		}

		#endregion

	}

	#endregion

}

