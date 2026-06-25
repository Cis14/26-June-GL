namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MicroserviceBaseRequestSchema

	/// <exclude/>
	public class MicroserviceBaseRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MicroserviceBaseRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MicroserviceBaseRequestSchema(MicroserviceBaseRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e403b733-11e3-45d0-a676-d6106e2dd742");
			Name = "MicroserviceBaseRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,145,75,75,195,64,16,128,207,45,244,63,12,237,69,47,201,221,170,160,21,164,135,74,105,5,15,226,97,155,76,227,226,62,226,204,132,82,139,255,221,205,166,41,161,22,124,92,150,221,121,124,251,49,227,148,69,46,85,134,48,33,153,24,95,229,83,39,88,144,18,237,93,50,243,57,26,78,22,248,94,33,11,15,250,187,65,191,87,177,118,5,44,183,44,104,147,69,229,68,91,76,150,72,90,25,253,17,251,198,131,126,168,75,211,20,46,185,178,86,209,246,122,255,94,96,73,200,232,132,65,1,53,88,16,15,44,138,194,229,21,65,85,225,12,204,44,146,96,109,252,38,105,97,105,135,246,124,167,68,77,188,19,82,153,188,132,64,89,173,140,206,32,51,138,25,110,21,227,222,58,164,118,209,167,55,34,44,106,230,156,124,137,36,26,249,2,230,177,171,201,31,11,199,192,99,237,84,150,166,21,210,121,109,183,214,72,201,161,167,235,213,136,205,208,174,144,206,30,194,116,225,10,134,29,192,240,188,150,109,109,89,168,30,230,77,231,131,29,20,40,99,224,250,248,252,65,172,52,74,214,158,44,184,240,209,31,124,218,190,147,50,243,22,250,91,147,123,12,235,244,84,23,114,220,225,6,87,236,179,55,148,16,98,254,247,212,14,152,101,67,153,230,39,125,159,190,149,157,50,31,161,203,155,253,199,119,136,198,196,23,40,108,233,2,0,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e403b733-11e3-45d0-a676-d6106e2dd742"));
		}

		#endregion

	}

	#endregion

}

