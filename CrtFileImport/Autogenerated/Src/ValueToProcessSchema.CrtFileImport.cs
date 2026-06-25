namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ValueToProcessSchema

	/// <exclude/>
	public class ValueToProcessSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ValueToProcessSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ValueToProcessSchema(ValueToProcessSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("dfa4da1a-566c-479e-a9dd-b9d6bfc7fa46");
			Name = "ValueToProcess";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,144,193,10,194,48,16,68,207,45,244,31,22,188,40,136,31,160,120,18,4,111,34,226,61,198,181,4,210,108,217,36,42,136,255,238,54,13,182,162,144,75,118,246,77,102,226,84,131,190,85,26,225,136,204,202,211,53,44,54,228,174,166,142,172,130,33,183,216,26,139,187,166,37,14,85,249,172,202,162,141,103,107,52,104,171,188,135,147,178,17,143,180,103,210,232,189,168,221,70,49,97,172,5,5,49,242,129,163,14,196,75,216,39,174,147,179,195,55,59,141,198,5,96,186,239,220,5,31,115,16,208,184,26,52,217,216,184,239,217,173,3,103,144,158,42,14,153,128,245,7,94,37,97,51,144,162,141,124,122,57,189,46,66,50,75,163,87,85,166,240,232,46,125,254,124,207,101,36,103,139,28,12,250,161,203,168,77,206,214,219,62,161,198,176,130,215,175,62,78,245,103,43,125,194,167,210,176,240,39,152,76,229,188,1,236,162,146,215,192,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("dfa4da1a-566c-479e-a9dd-b9d6bfc7fa46"));
		}

		#endregion

	}

	#endregion

}

