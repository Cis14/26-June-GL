namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AddSenderDomainsInfoRequestSchema

	/// <exclude/>
	public class AddSenderDomainsInfoRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AddSenderDomainsInfoRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AddSenderDomainsInfoRequestSchema(AddSenderDomainsInfoRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7cef863d-9684-4a82-a74b-e9746da57616");
			Name = "AddSenderDomainsInfoRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,79,79,194,64,16,197,207,146,240,29,38,229,162,151,246,206,31,19,5,99,60,96,8,229,102,60,44,237,180,110,210,238,214,153,93,12,18,191,187,219,93,80,4,130,225,212,206,235,155,183,243,155,173,18,53,114,35,50,132,5,18,9,214,133,137,199,90,21,178,180,36,140,212,42,30,63,164,83,157,99,197,221,206,166,219,233,118,174,44,75,85,66,186,102,131,117,60,183,202,200,26,227,20,73,138,74,126,250,158,129,247,245,8,75,87,192,184,18,204,125,184,203,243,20,85,142,52,209,181,144,138,159,84,161,231,248,110,145,141,183,39,73,2,67,182,117,45,104,125,187,173,125,43,20,154,128,130,19,140,6,145,231,160,240,3,216,167,65,238,227,226,93,66,114,16,49,100,68,81,177,134,140,176,24,69,247,130,209,205,186,146,25,110,15,143,32,105,189,47,19,97,132,35,55,36,50,243,234,132,198,46,43,153,65,230,71,56,51,60,244,225,56,212,245,135,101,253,108,97,70,186,65,50,18,221,42,102,62,58,124,63,196,246,194,35,26,6,71,205,237,211,188,225,30,228,49,101,80,86,162,178,248,83,46,78,245,252,90,60,236,20,235,37,210,245,179,251,3,96,4,81,176,71,55,45,251,14,158,13,181,119,29,160,97,3,37,154,65,59,212,0,190,46,153,94,181,71,232,194,191,55,164,87,210,45,242,50,150,127,18,206,147,237,26,218,250,36,223,108,207,112,138,178,231,174,62,92,163,175,131,250,87,116,218,55,154,99,32,102,75,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7cef863d-9684-4a82-a74b-e9746da57616"));
		}

		#endregion

	}

	#endregion

}

