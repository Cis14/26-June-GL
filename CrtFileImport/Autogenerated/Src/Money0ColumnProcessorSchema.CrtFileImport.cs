namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Money0ColumnProcessorSchema

	/// <exclude/>
	public class Money0ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Money0ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Money0ColumnProcessorSchema(Money0ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("4c88b0a1-4904-488d-94fa-4ce1f3bb042a");
			Name = "Money0ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,84,193,78,227,48,16,189,35,241,15,163,114,105,37,148,236,185,180,149,182,69,69,61,176,66,90,186,23,196,193,117,38,197,82,50,206,142,109,164,170,226,223,119,146,52,144,132,44,32,114,137,51,126,243,222,248,189,36,164,114,116,133,210,8,247,200,172,156,77,125,180,178,148,154,125,96,229,141,165,104,109,50,220,228,133,101,127,126,118,60,63,3,185,130,51,180,135,223,7,231,49,191,106,151,218,28,140,31,108,69,107,165,189,101,131,78,64,53,236,130,113,47,122,176,202,148,115,83,184,181,132,135,31,43,155,133,156,238,216,106,116,206,114,131,141,227,24,102,134,158,144,141,79,172,6,205,152,206,71,235,204,42,223,235,24,197,139,86,139,11,121,174,248,208,42,253,36,48,228,188,34,113,192,166,224,159,140,3,93,78,0,178,96,177,198,146,51,187,12,33,181,12,69,205,90,30,167,26,15,116,37,6,207,42,11,232,162,150,80,220,85,122,184,198,84,133,204,47,13,37,210,61,246,135,2,109,58,222,244,134,157,92,194,47,201,3,230,64,114,19,192,160,9,147,201,99,205,90,132,93,102,244,105,220,65,40,76,97,200,148,186,253,216,184,217,113,95,206,235,57,148,225,72,8,119,149,66,27,247,13,231,255,227,126,83,94,49,42,143,174,27,131,248,35,120,196,19,127,117,184,247,252,81,79,32,30,86,152,21,138,85,94,121,58,31,5,135,44,135,36,212,229,219,61,90,108,229,89,114,108,10,209,44,174,208,45,138,147,205,131,6,143,183,29,58,232,178,79,196,255,157,114,56,238,151,143,111,236,47,157,20,144,146,58,136,161,108,68,181,64,246,242,209,76,203,181,23,54,76,62,15,103,41,19,124,51,155,107,229,85,253,118,215,145,4,50,127,101,109,18,36,111,82,131,252,133,4,138,102,82,176,207,242,15,144,94,184,9,38,169,184,255,148,212,247,194,188,221,36,48,95,116,107,81,237,120,31,119,245,177,97,175,126,118,183,94,254,1,73,23,145,217,233,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("4c88b0a1-4904-488d-94fa-4ce1f3bb042a"));
		}

		#endregion

	}

	#endregion

}

