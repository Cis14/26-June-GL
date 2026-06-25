namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ColumnProcessErrorEventArgsSchema

	/// <exclude/>
	public class ColumnProcessErrorEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ColumnProcessErrorEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ColumnProcessErrorEventArgsSchema(ColumnProcessErrorEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a1ce7950-dd79-4139-9857-1c8e2032f3d9");
			Name = "ColumnProcessErrorEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,143,65,10,194,48,16,69,215,45,244,14,1,247,61,64,93,73,169,208,133,32,234,5,98,253,13,129,116,82,102,82,65,196,187,155,166,69,116,227,110,230,207,159,247,103,72,15,144,81,119,80,23,48,107,241,125,40,107,79,189,53,19,235,96,61,149,123,235,208,14,163,231,80,228,207,34,207,38,177,100,212,249,33,1,195,182,200,163,178,97,152,232,84,181,211,34,149,170,189,155,6,58,178,239,32,210,48,123,110,238,160,176,99,35,201,62,78,87,103,59,213,205,238,127,102,85,169,150,122,127,136,3,109,240,197,200,158,137,243,201,141,219,35,56,88,196,240,99,130,47,243,53,72,2,207,23,215,112,238,132,30,12,138,223,206,159,100,153,65,216,166,66,214,226,181,146,65,183,5,158,250,69,253,21,163,246,6,184,178,5,28,60,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a1ce7950-dd79-4139-9857-1c8e2032f3d9"));
		}

		#endregion

	}

	#endregion

}

