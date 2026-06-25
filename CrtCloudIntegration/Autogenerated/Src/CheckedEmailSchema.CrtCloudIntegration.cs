namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CheckedEmailSchema

	/// <exclude/>
	public class CheckedEmailSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CheckedEmailSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CheckedEmailSchema(CheckedEmailSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("29627a54-240f-4b4f-a2e5-afc88eca3ba7");
			Name = "CheckedEmail";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,144,49,79,195,48,16,133,231,70,202,127,56,181,11,44,201,78,128,37,84,136,161,168,106,216,16,66,78,114,77,45,98,59,186,187,12,165,226,191,227,56,109,105,43,6,24,239,221,123,190,207,207,42,131,220,169,10,225,5,137,20,187,181,36,185,179,107,221,244,164,68,59,155,228,243,98,225,106,108,57,142,118,113,20,71,147,158,181,109,160,216,178,160,73,86,189,21,109,48,41,144,180,106,245,103,200,100,193,55,35,108,252,0,121,171,152,111,32,223,96,245,129,245,220,40,221,134,125,154,166,112,203,189,49,138,182,247,251,121,133,29,33,163,21,6,217,32,224,96,6,237,15,1,139,18,76,14,177,244,36,247,250,160,68,121,102,33,85,201,155,23,186,190,108,117,5,213,112,247,226,236,100,252,194,145,109,73,174,67,18,141,30,112,25,98,227,254,146,45,8,143,232,177,28,1,227,41,94,114,244,159,66,141,84,11,52,37,210,213,179,47,25,238,96,26,252,211,235,129,241,0,201,66,67,155,1,15,118,208,160,100,195,251,25,124,253,27,228,167,162,63,226,104,126,175,198,118,206,153,74,231,90,120,226,125,115,191,81,205,208,214,99,131,97,30,213,115,209,107,223,110,36,25,198,92,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("29627a54-240f-4b4f-a2e5-afc88eca3ba7"));
		}

		#endregion

	}

	#endregion

}

