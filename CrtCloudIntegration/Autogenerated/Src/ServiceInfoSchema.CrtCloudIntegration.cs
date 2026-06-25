namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ServiceInfoSchema

	/// <exclude/>
	public class ServiceInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ServiceInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ServiceInfoSchema(ServiceInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8e8ff62d-d15e-4cbb-bf69-9d73591e0c11");
			Name = "ServiceInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,61,111,194,48,16,64,103,144,248,15,167,176,180,75,178,151,194,2,85,213,129,10,53,221,170,14,38,156,83,75,137,141,238,46,149,40,234,127,239,197,9,20,16,11,83,238,195,239,238,217,241,166,70,222,154,2,225,29,137,12,7,43,233,60,120,235,202,134,140,184,224,211,249,83,190,12,27,172,120,52,220,143,134,131,134,157,47,33,223,177,96,157,190,53,94,92,141,105,142,228,76,229,126,34,49,25,13,245,220,152,176,212,4,230,149,97,126,0,61,241,237,10,124,241,54,196,118,150,101,240,200,77,93,27,218,205,250,124,129,214,121,215,142,0,27,8,138,42,52,27,224,14,60,32,217,9,243,177,48,98,84,86,200,20,242,169,133,109,179,174,92,161,160,174,60,223,56,216,199,173,71,171,21,133,45,146,56,84,181,85,164,186,254,165,86,44,60,163,48,168,16,183,95,249,66,240,250,104,16,108,140,123,191,244,72,159,26,118,138,75,172,215,72,119,175,45,53,133,164,39,218,52,185,111,181,15,222,44,20,223,246,191,15,123,40,81,38,237,230,9,252,222,162,168,129,232,48,190,212,84,20,17,10,66,59,77,150,198,85,122,166,223,151,247,68,146,205,110,186,76,79,157,221,228,250,100,56,6,87,174,53,70,191,233,126,78,204,187,234,121,81,107,127,17,64,250,219,175,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8e8ff62d-d15e-4cbb-bf69-9d73591e0c11"));
		}

		#endregion

	}

	#endregion

}

