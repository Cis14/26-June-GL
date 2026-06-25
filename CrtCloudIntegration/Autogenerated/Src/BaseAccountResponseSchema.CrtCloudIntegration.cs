namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BaseAccountResponseSchema

	/// <exclude/>
	public class BaseAccountResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BaseAccountResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BaseAccountResponseSchema(BaseAccountResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("70d366b9-fe0c-4c17-99b3-f6cd244e3e7c");
			Name = "BaseAccountResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("edc99e2c-9094-4ed6-903f-e907a7c24faf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,80,203,106,195,48,16,60,199,224,127,88,210,187,117,239,35,208,134,28,11,161,244,7,84,121,173,8,226,149,217,149,8,33,228,223,187,182,236,144,150,92,4,179,154,157,157,25,178,61,202,96,29,194,55,50,91,137,93,106,182,145,186,224,51,219,20,34,213,213,165,174,234,106,245,196,232,21,194,246,104,69,158,225,195,10,190,59,23,51,165,47,21,136,36,56,209,140,49,240,42,185,239,45,159,55,51,94,8,224,198,93,232,34,43,5,21,50,118,111,235,7,74,107,179,1,75,45,4,58,32,135,132,109,217,68,105,150,11,230,238,196,144,127,142,193,205,226,15,125,173,74,132,91,134,61,199,1,57,5,212,32,251,105,187,252,255,55,63,13,180,141,100,3,9,104,63,234,92,235,18,235,17,78,7,36,24,101,166,150,32,8,80,76,32,217,57,37,52,55,181,123,163,139,83,73,28,200,195,110,212,251,156,229,46,224,49,189,128,140,207,117,118,139,212,22,195,19,46,211,191,195,235,47,163,187,89,189,191,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("70d366b9-fe0c-4c17-99b3-f6cd244e3e7c"));
		}

		#endregion

	}

	#endregion

}

