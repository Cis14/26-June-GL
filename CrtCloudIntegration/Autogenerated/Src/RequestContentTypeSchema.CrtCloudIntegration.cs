namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RequestContentTypeSchema

	/// <exclude/>
	public class RequestContentTypeSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RequestContentTypeSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RequestContentTypeSchema(RequestContentTypeSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9276eb3a-690d-4f9a-89bf-69e191abc4b4");
			Name = "RequestContentType";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,143,59,14,194,48,16,68,235,68,202,29,86,162,72,23,83,35,160,137,104,40,145,47,96,194,38,4,225,181,241,167,136,16,119,103,177,1,69,130,114,214,111,102,60,164,52,122,171,58,4,137,206,41,111,250,208,180,134,250,113,136,78,133,209,80,85,222,171,178,42,139,133,195,129,37,236,40,234,21,28,240,22,209,7,38,3,82,144,147,197,4,9,33,96,237,163,214,202,77,219,183,150,103,4,151,113,232,50,15,129,13,205,135,23,51,131,141,199,235,216,1,114,201,223,142,34,127,230,167,40,29,218,89,58,212,202,90,142,74,27,196,197,27,170,155,175,113,222,88,236,249,13,54,176,76,193,143,188,21,233,148,231,190,36,223,158,114,77,155,64,38,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9276eb3a-690d-4f9a-89bf-69e191abc4b4"));
		}

		#endregion

	}

	#endregion

}

