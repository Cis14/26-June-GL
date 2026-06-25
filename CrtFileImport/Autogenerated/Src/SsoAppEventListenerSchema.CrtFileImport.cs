namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SsoAppEventListenerSchema

	/// <exclude/>
	public class SsoAppEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SsoAppEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SsoAppEventListenerSchema(SsoAppEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("aebf3d9a-9819-4190-84a0-91aa0f7bd750");
			Name = "SsoAppEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,82,93,107,227,48,16,124,78,161,255,97,201,189,164,112,216,239,173,27,184,134,246,224,232,199,129,91,242,172,200,235,84,212,150,204,238,42,189,82,242,223,111,37,59,165,13,125,17,104,87,51,59,179,35,111,122,228,193,88,132,71,36,50,28,90,41,86,193,183,110,27,201,136,11,254,244,228,253,244,100,22,217,249,237,151,39,132,23,223,212,215,184,209,94,223,7,175,93,237,255,32,220,42,9,172,58,195,124,14,53,135,95,195,112,189,67,47,183,142,5,61,82,126,86,150,37,84,28,251,222,208,219,114,186,103,8,12,20,118,174,65,6,133,208,27,12,193,121,129,54,16,212,245,3,24,155,20,50,232,128,200,72,192,200,156,166,177,24,146,226,192,91,126,34,30,226,166,115,22,108,230,254,70,13,156,195,113,233,202,48,42,242,61,11,253,48,116,135,242,28,26,181,244,55,51,142,205,99,27,185,80,39,49,12,113,104,140,32,48,7,176,193,139,74,7,49,252,82,124,224,202,99,96,53,24,50,61,120,77,232,114,158,48,248,79,230,203,138,17,193,18,182,151,243,131,210,213,212,43,151,224,188,122,247,22,139,170,204,232,76,54,153,14,59,141,73,119,9,187,224,26,120,240,245,184,173,172,111,113,196,5,211,188,51,72,233,207,102,174,133,197,239,46,108,76,167,15,107,20,209,216,185,184,65,35,145,80,247,248,199,201,218,233,70,162,212,246,25,155,216,33,29,160,179,244,87,138,71,245,202,249,44,242,192,123,124,77,128,39,141,77,71,122,204,73,86,79,121,73,202,183,26,87,116,101,236,203,150,66,244,77,66,254,132,77,8,221,114,33,20,241,236,34,147,239,211,185,159,162,65,223,140,233,228,251,88,253,90,220,255,7,130,218,73,143,239,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("aebf3d9a-9819-4190-84a0-91aa0f7bd750"));
		}

		#endregion

	}

	#endregion

}

