namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: InfoMessageEventArgsSchema

	/// <exclude/>
	public class InfoMessageEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public InfoMessageEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public InfoMessageEventArgsSchema(InfoMessageEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("581d1ca8-4c25-474e-9e2b-79fe22e7b14e");
			Name = "InfoMessageEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1a247561-b87d-48fb-9e13-b6a8baa960d3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,145,77,10,194,48,16,133,215,22,122,135,1,247,30,64,87,34,85,186,16,10,245,2,177,157,134,64,155,132,153,68,20,241,238,38,173,138,63,89,185,9,201,227,203,123,121,19,45,6,100,43,26,132,3,18,9,54,157,91,108,140,238,148,244,36,156,50,122,177,85,61,150,131,53,228,242,236,154,103,51,207,74,75,168,47,236,112,88,229,89,80,230,132,50,144,0,155,94,48,47,161,32,50,180,71,102,33,177,56,161,118,107,146,60,146,214,31,123,213,64,19,185,52,6,75,40,117,103,126,111,207,98,246,43,170,34,99,145,156,194,16,87,141,166,163,255,51,96,231,85,11,211,163,235,224,20,110,148,45,92,65,162,91,1,199,229,246,129,23,231,6,109,44,251,182,75,208,115,212,237,148,31,78,147,246,46,37,38,145,174,242,61,136,20,21,230,240,111,121,118,20,127,232,225,248,71,145,160,220,1,200,116,25,213,23,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("581d1ca8-4c25-474e-9e2b-79fe22e7b14e"));
		}

		#endregion

	}

	#endregion

}

