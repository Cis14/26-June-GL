namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AIFilterContextSchema

	/// <exclude/>
	public class AIFilterContextSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AIFilterContextSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AIFilterContextSchema(AIFilterContextSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2179d754-e560-425d-86c6-46f64e80d770");
			Name = "AIFilterContext";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2a14007c-a543-4bf5-b38e-4b55a0d5d02a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,77,106,195,48,16,133,215,14,228,14,3,221,180,27,29,32,166,152,98,154,226,69,67,32,237,1,84,121,112,5,182,36,164,49,212,152,220,189,250,35,181,139,75,151,111,230,227,241,222,12,40,62,160,51,92,32,212,22,57,73,205,106,109,100,175,105,191,155,247,187,98,116,82,117,112,153,28,225,80,222,244,47,148,189,34,241,150,19,247,132,103,238,44,118,82,43,168,123,238,220,1,158,154,70,17,42,58,202,158,208,214,218,139,47,138,160,25,63,122,41,64,4,238,47,172,152,35,250,99,170,149,35,59,10,210,214,123,159,163,67,34,178,219,166,207,125,78,154,86,23,241,137,3,7,25,197,3,132,158,69,145,86,240,152,199,101,28,158,252,117,110,163,138,5,153,22,239,77,187,152,7,85,85,249,76,236,101,148,45,123,30,12,77,137,125,155,204,210,36,200,184,184,230,102,168,218,84,110,221,244,108,181,65,75,18,183,123,250,43,132,87,196,132,51,116,72,37,92,87,64,136,1,33,217,230,118,117,144,16,169,130,152,243,127,56,95,47,223,107,205,47,187,228,126,235,122,126,246,13,237,65,245,52,115,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2179d754-e560-425d-86c6-46f64e80d770"));
		}

		#endregion

	}

	#endregion

}

