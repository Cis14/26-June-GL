namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: EntityChunkDataSchema

	/// <exclude/>
	public class EntityChunkDataSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public EntityChunkDataSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public EntityChunkDataSchema(EntityChunkDataSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2514f3cc-6ad8-4976-a835-7cf0143ff738");
			Name = "EntityChunkData";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,191,78,195,48,16,135,231,86,234,59,156,218,5,150,60,0,5,150,16,80,7,80,68,144,24,42,6,199,189,166,22,142,29,157,237,161,68,188,59,231,152,86,42,253,35,150,68,119,254,125,247,217,103,68,139,174,19,18,225,13,137,132,179,107,159,229,214,172,85,19,72,120,101,77,246,168,52,46,218,206,146,159,140,251,201,120,20,156,50,13,84,91,231,177,157,255,169,25,213,26,101,228,92,246,132,6,73,201,163,204,107,48,94,181,152,85,124,42,180,250,26,52,156,226,220,140,176,225,2,32,215,194,185,27,40,56,233,183,249,38,152,207,7,225,197,144,89,238,185,90,227,7,55,186,80,107,37,65,70,228,152,24,245,3,181,31,93,146,237,144,188,66,30,95,14,100,58,95,198,248,51,182,53,210,213,11,47,5,238,96,138,113,24,39,167,215,209,179,19,45,10,19,90,164,168,191,77,139,73,82,134,25,176,247,233,14,140,65,15,13,250,57,184,248,249,62,175,113,65,74,116,142,111,22,127,184,250,125,131,229,61,29,154,149,241,80,157,15,255,215,215,237,216,119,229,55,5,145,165,139,198,242,82,252,148,115,134,102,149,182,61,212,169,123,216,228,222,15,228,182,22,166,122,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2514f3cc-6ad8-4976-a835-7cf0143ff738"));
		}

		#endregion

	}

	#endregion

}

