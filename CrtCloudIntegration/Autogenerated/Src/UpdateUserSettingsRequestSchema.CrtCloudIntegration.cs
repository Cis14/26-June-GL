namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateUserSettingsRequestSchema

	/// <exclude/>
	public class UpdateUserSettingsRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateUserSettingsRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateUserSettingsRequestSchema(UpdateUserSettingsRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("83429a60-95f6-4bbb-baae-d29f929fc531");
			Name = "UpdateUserSettingsRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,79,79,194,64,16,197,207,146,240,29,38,112,80,47,237,157,63,38,8,70,19,131,33,128,241,96,60,12,237,0,27,218,110,221,153,197,32,241,187,187,221,2,70,68,148,75,147,153,157,247,219,121,175,155,97,74,156,99,68,48,38,99,144,245,84,130,174,206,166,106,102,13,138,210,89,208,189,25,245,117,76,9,87,43,235,106,165,90,57,179,172,178,25,140,86,44,148,6,67,155,137,74,41,24,145,81,152,168,119,175,105,250,185,186,161,153,43,160,155,32,115,3,30,243,24,133,30,153,204,136,68,28,130,135,244,106,137,197,15,135,97,8,45,182,105,138,102,117,181,169,189,16,166,218,128,41,39,65,52,88,143,1,140,34,237,174,62,103,176,142,8,188,65,6,91,84,184,199,106,49,17,38,172,33,50,52,109,215,254,52,27,92,35,147,51,181,84,17,109,246,172,65,88,208,158,123,40,232,84,98,48,146,23,215,200,237,36,81,17,68,126,219,95,93,66,3,126,34,157,186,204,116,23,214,192,232,156,140,40,114,137,13,60,184,60,223,207,199,55,110,73,24,180,119,207,32,115,130,55,154,192,92,235,5,96,158,59,169,247,4,177,78,81,101,193,142,18,238,99,90,75,76,44,237,202,241,191,65,95,58,31,74,159,210,9,153,139,7,247,164,160,13,53,199,184,115,136,78,158,247,188,176,118,89,164,181,141,139,197,20,207,232,105,111,8,214,48,35,105,22,150,154,240,113,138,119,180,238,235,30,227,102,217,5,173,78,179,124,84,127,220,105,33,189,167,213,65,131,157,242,236,144,175,58,101,113,249,219,125,93,118,191,55,93,239,19,87,167,15,228,162,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("83429a60-95f6-4bbb-baae-d29f929fc531"));
		}

		#endregion

	}

	#endregion

}

