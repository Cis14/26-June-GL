namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DateColumnProcessorSchema

	/// <exclude/>
	public class DateColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DateColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DateColumnProcessorSchema(DateColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f8c37261-6156-4687-9016-d3e9e21442ee");
			Name = "DateColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1a247561-b87d-48fb-9e13-b6a8baa960d3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,147,65,107,227,48,16,133,207,45,244,63,12,217,75,2,139,125,111,147,64,155,210,37,151,82,104,178,151,101,15,138,60,78,7,108,201,59,146,10,161,244,191,239,72,142,187,177,235,22,186,167,72,195,155,247,52,159,39,70,213,232,26,165,17,54,200,172,156,45,125,182,178,166,164,125,96,229,201,154,236,142,42,92,215,141,101,127,113,254,114,113,126,22,28,153,61,60,30,156,199,250,234,237,126,218,205,248,81,61,187,83,218,91,38,116,162,16,205,55,198,189,100,192,170,82,206,93,194,173,242,184,178,85,168,205,3,91,141,206,89,78,178,60,207,97,78,230,9,153,124,97,53,104,198,114,49,137,234,13,213,195,142,73,190,236,90,92,168,107,197,135,238,126,109,128,140,243,202,200,180,182,4,255,68,14,116,76,6,57,176,96,176,198,209,174,66,40,45,67,211,250,197,25,98,16,232,148,2,207,170,10,232,178,46,33,63,137,248,117,139,165,10,149,191,33,83,72,219,212,31,26,180,229,116,61,120,223,236,59,220,11,116,88,128,145,31,17,140,76,61,155,253,22,195,38,236,42,210,199,39,142,168,160,37,54,194,64,154,95,18,185,127,132,101,54,207,33,210,23,208,15,201,185,85,252,7,220,119,116,83,97,197,40,77,174,207,88,24,136,18,241,196,246,189,101,246,230,153,15,77,231,141,98,85,39,84,139,73,112,200,50,136,65,29,55,115,178,220,202,93,62,76,87,200,230,121,82,167,230,35,188,145,192,233,182,103,3,125,215,153,80,221,41,135,211,97,57,238,254,217,235,145,42,154,162,5,219,167,44,25,13,178,151,253,190,140,103,47,189,88,124,134,249,70,146,190,128,88,166,81,237,10,182,100,131,161,63,114,166,2,141,167,146,144,63,32,217,116,111,1,251,44,127,72,209,195,143,64,69,242,251,25,237,54,226,182,93,23,176,88,246,107,89,228,55,84,93,141,66,104,209,244,139,175,127,1,91,5,48,236,95,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f8c37261-6156-4687-9016-d3e9e21442ee"));
		}

		#endregion

	}

	#endregion

}

