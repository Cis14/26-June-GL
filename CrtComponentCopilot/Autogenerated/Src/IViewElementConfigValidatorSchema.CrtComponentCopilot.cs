namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IViewElementConfigValidatorSchema

	/// <exclude/>
	public class IViewElementConfigValidatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IViewElementConfigValidatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IViewElementConfigValidatorSchema(IViewElementConfigValidatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c30f13ab-5164-4b8b-b06b-61c30a719b79");
			Name = "IViewElementConfigValidator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("8884ce38-e203-4454-9ba9-edfcd0b3acef");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,145,207,78,194,64,16,135,239,36,188,195,132,147,94,218,7,160,146,152,198,24,136,209,3,134,251,90,166,48,201,254,203,204,20,67,140,239,238,150,133,130,160,61,244,48,187,223,55,51,191,237,132,252,6,150,123,81,116,69,29,172,197,70,41,120,41,158,209,35,83,51,29,143,198,35,111,28,74,52,13,66,205,104,210,121,186,233,98,240,232,181,14,145,108,208,241,232,171,191,8,233,43,203,18,42,233,156,51,188,159,157,75,117,240,202,166,81,104,3,195,206,88,90,39,81,106,109,96,71,248,9,104,209,37,29,52,193,183,180,41,46,84,229,111,87,236,62,44,53,64,94,145,219,126,164,249,42,241,79,25,175,15,244,42,219,3,103,98,152,236,159,233,78,229,35,134,2,186,69,144,46,70,75,184,62,78,4,139,229,219,107,113,229,41,255,22,85,209,176,113,208,167,246,48,201,248,66,130,159,204,222,147,247,66,7,26,78,73,96,81,149,7,234,90,197,168,29,123,185,42,63,130,37,81,8,237,144,100,240,128,204,41,218,244,82,98,54,40,83,64,23,117,15,212,30,214,57,182,37,201,196,205,38,183,125,230,47,169,69,37,202,233,149,102,67,56,119,185,0,231,181,238,167,153,249,238,99,78,191,31,223,212,123,72,82,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c30f13ab-5164-4b8b-b06b-61c30a719b79"));
		}

		#endregion

	}

	#endregion

}

