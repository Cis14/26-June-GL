namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AfterImportEntitiesSaveEventArgsSchema

	/// <exclude/>
	public class AfterImportEntitiesSaveEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AfterImportEntitiesSaveEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AfterImportEntitiesSaveEventArgsSchema(AfterImportEntitiesSaveEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("232e8c67-42ba-4203-936e-0c03b535963d");
			Name = "AfterImportEntitiesSaveEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1101e5cd-b945-4f88-8001-faccb4fdb24c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,142,193,10,130,64,16,134,207,10,190,195,64,119,31,64,79,33,5,29,130,200,94,96,179,113,89,208,89,153,153,53,66,122,247,212,141,168,83,151,129,253,102,191,255,31,50,61,202,96,26,132,11,50,27,241,173,230,149,167,214,217,192,70,157,167,124,239,58,60,244,131,103,205,210,41,75,147,32,142,44,212,15,81,236,203,44,157,201,134,209,206,63,161,234,140,72,1,219,86,145,163,177,35,117,234,80,106,51,226,110,68,210,45,91,89,157,33,92,59,215,64,179,40,127,13,40,224,64,173,63,162,136,177,223,65,201,180,134,125,46,56,177,31,144,23,191,128,211,218,16,247,239,182,224,72,33,246,224,237,236,239,82,249,48,147,9,44,106,9,178,140,231,59,16,233,22,51,215,119,164,191,112,102,47,105,10,159,238,61,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("232e8c67-42ba-4203-936e-0c03b535963d"));
		}

		#endregion

	}

	#endregion

}

