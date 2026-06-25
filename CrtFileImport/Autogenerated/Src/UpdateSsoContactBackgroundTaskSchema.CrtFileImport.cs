namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateSsoContactBackgroundTaskSchema

	/// <exclude/>
	public class UpdateSsoContactBackgroundTaskSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateSsoContactBackgroundTaskSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateSsoContactBackgroundTaskSchema(UpdateSsoContactBackgroundTaskSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("fc80e659-5c97-4b8f-a38f-24e4568bb179");
			Name = "UpdateSsoContactBackgroundTask";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,82,77,75,195,64,16,61,87,240,63,12,241,146,66,73,238,246,3,84,168,20,20,196,234,89,214,100,154,46,38,187,113,102,183,34,226,127,119,63,162,184,213,234,41,100,102,222,155,247,222,172,18,29,114,47,42,132,59,36,18,172,55,166,184,208,106,35,27,75,194,72,173,224,248,232,237,248,104,100,89,170,230,208,76,177,148,45,174,186,94,147,153,254,58,75,120,168,94,44,69,101,52,73,228,131,19,119,130,159,124,215,245,79,8,27,175,233,162,21,204,167,112,223,215,194,224,154,181,83,99,28,207,185,168,158,26,210,86,213,30,19,16,101,89,194,140,109,215,9,122,93,12,255,1,13,54,128,25,152,53,84,17,15,113,255,250,236,250,10,200,197,162,21,35,108,72,119,192,78,163,104,176,248,100,44,191,81,246,246,177,149,21,84,129,245,111,73,112,10,171,180,50,123,212,186,93,76,96,117,207,72,14,163,176,242,137,222,226,179,149,132,181,163,127,11,62,190,172,47,37,182,181,243,126,67,114,231,54,197,102,31,127,32,37,129,7,91,77,7,52,170,58,18,164,108,215,104,182,58,208,5,19,177,25,28,74,181,69,146,166,214,206,25,225,102,158,205,179,61,233,197,173,85,89,185,24,20,196,16,118,90,214,224,234,185,183,5,189,32,247,188,12,18,143,193,63,162,209,104,39,200,7,62,132,19,195,34,152,199,147,196,167,240,90,92,162,153,173,214,251,83,139,92,225,11,184,26,27,178,126,240,140,26,219,161,50,121,102,19,219,217,196,27,31,143,167,97,227,143,109,69,252,14,197,60,142,189,255,227,252,247,235,20,107,52,105,39,228,145,166,241,99,38,223,187,82,170,254,51,41,103,193,229,146,246,190,75,221,187,104,172,166,197,247,15,211,185,88,125,221,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("fc80e659-5c97-4b8f-a38f-24e4568bb179"));
		}

		#endregion

	}

	#endregion

}

