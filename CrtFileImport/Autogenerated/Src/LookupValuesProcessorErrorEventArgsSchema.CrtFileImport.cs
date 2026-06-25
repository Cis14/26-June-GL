namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LookupValuesProcessorErrorEventArgsSchema

	/// <exclude/>
	public class LookupValuesProcessorErrorEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LookupValuesProcessorErrorEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LookupValuesProcessorErrorEventArgsSchema(LookupValuesProcessorErrorEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8dbe4f07-e89f-42d7-b44a-3c89f43c4a8f");
			Name = "LookupValuesProcessorErrorEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,221,74,3,49,16,133,175,183,208,119,24,232,141,66,233,3,180,120,33,101,149,130,74,161,234,125,204,78,215,224,110,178,204,36,98,41,125,119,147,236,79,119,139,150,189,9,153,201,156,115,38,159,22,37,114,37,36,194,43,18,9,54,123,187,88,27,189,87,185,35,97,149,209,139,7,85,224,166,172,12,217,233,228,56,157,36,142,149,206,97,119,96,139,229,106,58,241,157,25,97,238,39,1,214,133,96,94,194,147,49,95,174,122,23,133,67,222,146,145,200,108,40,37,242,199,55,106,123,79,57,71,93,229,62,10,37,65,6,213,24,17,44,161,103,144,132,101,186,108,191,51,91,114,210,26,242,27,108,163,115,12,105,83,70,248,223,60,58,149,129,47,148,61,236,228,39,150,226,109,147,205,193,251,134,31,75,83,184,82,191,120,94,93,171,84,28,96,68,211,57,164,63,18,171,128,44,164,38,9,182,229,45,196,77,147,116,104,12,119,151,81,171,56,182,238,114,252,196,57,180,126,124,238,37,250,231,254,2,245,64,183,68,176,111,239,241,233,84,227,152,161,206,106,102,77,221,0,244,76,42,36,171,240,111,124,103,223,243,237,8,57,218,21,40,109,145,180,40,128,67,117,26,200,34,209,203,143,255,167,235,84,13,222,193,103,71,132,53,178,30,192,107,162,62,136,6,78,191,229,59,191,82,29,196,209,28,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8dbe4f07-e89f-42d7-b44a-3c89f43c4a8f"));
		}

		#endregion

	}

	#endregion

}

