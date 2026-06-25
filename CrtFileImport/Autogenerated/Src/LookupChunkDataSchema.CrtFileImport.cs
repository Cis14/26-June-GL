namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LookupChunkDataSchema

	/// <exclude/>
	public class LookupChunkDataSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LookupChunkDataSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LookupChunkDataSchema(LookupChunkDataSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c8fe945c-3ca6-454e-abfa-54dbb19e8a55");
			Name = "LookupChunkData";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,141,65,10,194,64,12,69,215,22,122,135,57,65,47,224,178,34,8,10,162,197,125,58,77,117,232,204,100,156,36,43,241,238,182,212,110,90,200,234,191,247,72,132,128,156,192,162,105,48,103,96,234,165,170,41,246,238,169,25,196,81,172,142,206,227,41,36,202,82,22,159,178,216,37,109,189,179,198,122,96,54,103,162,65,83,253,210,56,28,64,96,196,147,178,56,51,125,128,87,228,134,174,153,44,50,95,48,96,20,50,171,249,46,32,184,223,196,127,138,221,172,47,241,106,222,196,45,145,159,62,184,110,4,55,124,171,203,216,213,228,53,68,158,188,111,89,140,247,3,27,222,230,190,253,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c8fe945c-3ca6-454e-abfa-54dbb19e8a55"));
		}

		#endregion

	}

	#endregion

}

