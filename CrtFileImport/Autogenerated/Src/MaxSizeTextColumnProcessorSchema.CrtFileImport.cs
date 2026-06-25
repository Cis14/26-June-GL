namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MaxSizeTextColumnProcessorSchema

	/// <exclude/>
	public class MaxSizeTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MaxSizeTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MaxSizeTextColumnProcessorSchema(MaxSizeTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cf2e8673-fb8d-4ba0-9c62-446b24f54ef1");
			Name = "MaxSizeTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("560ff4eb-7ab5-4d8f-8733-4031e1e3144b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,111,226,48,16,133,207,84,234,127,24,177,23,144,170,228,78,1,105,75,213,138,67,87,149,10,123,89,245,96,156,9,29,41,177,211,177,93,149,173,250,223,119,226,16,74,88,114,232,41,246,232,249,189,153,207,142,81,37,186,74,105,132,21,50,43,103,115,159,44,172,201,105,27,88,121,178,38,185,163,2,151,101,101,217,95,94,124,92,94,12,130,35,179,133,167,157,243,88,94,31,246,199,167,25,251,234,201,157,210,222,50,161,19,133,104,126,48,110,37,3,22,133,114,110,2,15,234,253,137,254,226,10,223,253,194,22,161,52,143,108,53,58,103,57,170,211,52,133,41,153,23,100,242,153,213,160,25,243,217,240,140,122,152,206,91,185,11,101,169,120,215,238,127,26,32,227,188,50,50,176,205,193,191,144,3,93,135,131,44,88,72,88,227,104,83,32,228,150,161,106,252,234,49,142,58,3,29,195,224,77,21,1,93,210,6,165,71,73,127,110,49,87,161,240,55,100,50,57,61,242,187,10,109,62,90,158,180,57,190,130,95,130,31,102,96,228,35,130,254,249,199,227,103,241,173,194,166,32,189,111,184,95,12,19,56,139,112,240,17,49,126,81,151,97,61,135,250,70,4,254,99,52,111,20,223,36,253,31,234,88,88,48,42,143,174,11,92,72,136,18,113,111,217,63,132,56,39,7,235,244,212,123,90,41,86,101,228,54,27,6,135,44,179,24,212,245,131,29,206,215,178,151,91,106,11,201,52,141,234,120,120,143,176,63,119,180,238,184,65,215,124,44,108,55,202,225,232,180,92,255,25,131,207,61,95,52,89,131,184,203,91,50,42,100,47,175,127,82,175,189,156,197,172,22,244,35,191,145,172,111,32,191,85,94,53,15,179,33,29,12,189,202,154,50,52,158,114,66,238,65,90,181,221,128,125,147,31,86,244,112,31,40,139,126,191,107,187,149,184,173,151,25,204,230,221,90,114,4,242,84,124,125,150,70,195,168,91,252,252,7,128,200,217,220,134,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cf2e8673-fb8d-4ba0-9c62-446b24f54ef1"));
		}

		#endregion

	}

	#endregion

}

