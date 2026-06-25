namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TimeColumnProcessorSchema

	/// <exclude/>
	public class TimeColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TimeColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TimeColumnProcessorSchema(TimeColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("761b0013-5b74-46b7-87d9-cd9671c4ab75");
			Name = "TimeColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1a247561-b87d-48fb-9e13-b6a8baa960d3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,147,65,107,227,48,16,133,207,45,244,63,12,217,75,2,197,190,167,73,96,155,210,146,203,82,216,100,47,75,15,138,60,78,7,108,201,29,73,133,80,250,223,119,36,199,109,157,58,133,237,201,210,240,230,61,205,39,217,168,26,93,163,52,194,26,153,149,179,165,207,150,214,148,180,11,172,60,89,147,221,82,133,171,186,177,236,47,206,95,46,206,207,130,35,179,131,223,123,231,177,190,122,219,127,236,102,60,85,207,110,149,246,150,9,157,40,68,243,131,113,39,25,176,172,148,115,83,88,83,141,75,91,133,218,220,179,213,232,156,229,36,203,243,28,102,100,30,145,201,23,86,131,102,44,231,163,27,229,113,160,99,148,47,186,22,23,234,90,241,190,219,255,52,64,198,121,101,100,90,91,130,127,36,7,58,38,131,44,88,48,88,227,104,91,33,148,150,161,105,253,226,12,49,8,116,74,129,103,85,5,116,89,151,144,127,136,248,123,131,165,10,149,191,38,83,72,219,216,239,27,180,229,120,117,116,190,201,37,252,18,232,48,7,35,31,17,12,204,48,153,60,136,97,19,182,21,233,195,17,7,84,48,133,19,12,164,249,37,145,123,39,44,179,121,14,145,190,128,190,79,206,173,226,27,112,63,209,77,133,37,163,52,185,62,99,97,32,74,196,131,237,176,101,246,230,153,31,155,206,26,197,170,78,168,230,163,224,144,101,16,131,58,190,204,209,98,35,123,185,152,174,144,205,242,164,78,205,7,120,3,129,227,77,207,6,250,174,19,161,186,85,14,199,199,229,248,246,207,94,15,84,209,20,45,216,62,101,201,104,144,189,188,239,105,92,123,233,197,226,43,204,215,146,244,31,136,229,86,84,251,4,91,178,193,208,147,172,169,64,227,169,36,228,19,36,155,238,44,96,159,229,135,20,61,220,5,42,146,223,159,104,183,22,183,205,170,128,249,162,95,203,34,191,99,213,213,32,132,22,77,191,248,250,15,177,177,202,58,95,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("761b0013-5b74-46b7-87d9-cd9671c4ab75"));
		}

		#endregion

	}

	#endregion

}

