namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CloudSenderDomainSchema

	/// <exclude/>
	public class CloudSenderDomainSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CloudSenderDomainSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CloudSenderDomainSchema(CloudSenderDomainSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e61dae9a-2655-48a6-bc4a-42efa1e9c942");
			Name = "CloudSenderDomain";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,148,193,110,194,48,12,134,207,32,237,29,44,113,217,46,229,62,24,23,96,8,109,108,136,242,2,161,113,187,104,109,82,37,233,88,133,120,247,185,105,65,5,54,81,96,167,40,238,111,255,159,211,56,146,37,104,82,22,32,44,81,107,102,84,104,189,161,146,161,136,50,205,172,80,210,27,142,253,153,226,24,155,187,246,230,174,221,202,140,144,17,248,185,177,152,244,142,246,148,25,199,24,20,105,198,155,160,68,45,2,210,144,170,163,49,162,40,12,99,102,204,35,45,42,227,62,74,142,122,164,18,38,164,19,117,187,93,232,155,44,73,152,206,7,213,126,129,169,70,131,210,26,16,50,84,58,113,80,192,86,42,179,192,93,46,80,24,72,194,11,16,251,129,128,20,141,141,183,171,216,173,149,76,179,85,44,2,8,10,140,223,40,90,27,71,178,231,157,107,149,162,182,2,9,122,238,114,203,239,199,168,46,48,65,162,116,44,180,22,32,21,31,227,156,122,112,64,167,68,59,36,99,117,193,95,130,192,6,34,180,189,162,82,15,182,151,88,126,209,153,135,34,40,79,137,254,168,210,141,108,199,133,242,159,92,63,49,111,228,249,130,249,245,142,179,239,198,62,36,189,218,198,159,63,55,246,241,211,240,166,150,140,101,54,163,80,88,187,59,205,140,203,196,171,141,5,167,249,162,255,135,103,238,202,36,19,28,166,252,134,14,221,172,85,173,29,14,244,69,12,245,153,53,83,170,114,11,84,162,120,225,203,65,157,57,237,17,179,184,20,9,221,189,42,227,253,104,78,225,105,175,241,222,212,186,215,148,98,129,129,210,124,132,118,247,108,253,205,240,42,140,237,159,188,91,245,2,131,195,114,39,132,18,215,77,170,220,63,84,248,29,82,148,143,161,219,151,71,91,15,110,127,0,232,145,11,55,67,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e61dae9a-2655-48a6-bc4a-42efa1e9c942"));
		}

		#endregion

	}

	#endregion

}

