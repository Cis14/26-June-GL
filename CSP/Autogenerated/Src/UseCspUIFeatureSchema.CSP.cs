namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UseCspUIFeatureSchema

	/// <exclude/>
	public class UseCspUIFeatureSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UseCspUIFeatureSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UseCspUIFeatureSchema(UseCspUIFeatureSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("fe3ac458-ad46-7608-4980-ee525a6b38be");
			Name = "UseCspUIFeature";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9acc6780-473e-4622-a1b4-a9672747b2c4");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,53,142,65,10,194,64,12,69,215,45,244,14,193,149,110,122,0,197,85,85,232,194,93,61,64,156,166,101,160,206,12,73,6,17,241,238,166,213,46,126,2,143,199,79,2,62,72,18,58,130,142,152,81,226,160,117,19,195,224,199,204,168,62,134,170,124,87,101,145,197,135,17,26,166,153,213,23,91,153,169,139,227,56,25,63,84,165,41,41,223,39,239,192,77,40,2,55,161,70,210,173,133,61,252,229,43,41,246,168,104,230,92,184,234,171,184,221,193,130,139,86,206,1,239,19,245,112,4,229,76,135,133,158,72,28,251,52,63,100,124,243,83,4,2,61,193,142,80,239,53,50,12,22,23,131,82,80,16,114,153,189,190,32,69,59,243,218,44,53,31,27,150,207,23,71,226,67,193,245,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("fe3ac458-ad46-7608-4980-ee525a6b38be"));
		}

		#endregion

	}

	#endregion

}

