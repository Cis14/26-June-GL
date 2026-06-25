namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CloudSenderDomainRecordDetailSchema

	/// <exclude/>
	public class CloudSenderDomainRecordDetailSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CloudSenderDomainRecordDetailSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CloudSenderDomainRecordDetailSchema(CloudSenderDomainRecordDetailSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1134be62-2a19-4bb7-914a-3735affab16a");
			Name = "CloudSenderDomainRecordDetail";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,146,63,107,195,48,16,197,231,24,242,29,14,50,100,41,246,30,151,46,78,105,151,66,136,67,119,197,62,187,2,91,50,119,167,130,49,249,238,149,229,36,132,182,1,183,211,73,167,167,247,126,250,99,84,139,220,169,2,225,128,68,138,109,37,113,102,77,165,107,71,74,180,53,113,246,156,191,217,18,27,94,70,195,50,90,56,214,166,134,188,103,193,54,93,70,190,179,34,172,189,16,178,70,49,111,32,71,83,34,109,109,171,180,217,99,97,169,220,162,40,221,4,109,146,36,240,200,174,109,21,245,79,231,249,30,59,66,70,35,12,218,84,150,218,144,11,234,104,157,64,25,124,128,130,17,148,193,137,227,139,83,114,99,213,185,99,163,11,40,70,10,207,98,93,121,159,100,49,4,154,43,250,142,108,135,36,26,61,255,46,248,76,235,223,113,67,227,5,61,169,37,224,177,190,90,22,227,239,48,190,202,111,153,46,80,44,52,222,218,69,12,3,212,40,233,232,144,194,105,70,84,62,214,67,223,205,139,25,133,127,142,8,167,121,87,141,155,151,17,148,255,11,249,249,44,153,18,172,45,245,179,146,239,111,159,141,147,139,18,231,129,42,144,15,60,255,173,13,172,85,33,250,19,215,15,176,214,230,60,158,71,52,217,253,146,190,242,172,211,23,11,243,169,123,219,60,125,1,90,186,210,199,128,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1134be62-2a19-4bb7-914a-3735affab16a"));
		}

		#endregion

	}

	#endregion

}

