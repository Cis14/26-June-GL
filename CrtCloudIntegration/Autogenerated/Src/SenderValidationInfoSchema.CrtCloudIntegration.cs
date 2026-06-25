namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SenderValidationInfoSchema

	/// <exclude/>
	public class SenderValidationInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SenderValidationInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SenderValidationInfoSchema(SenderValidationInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f6ab86a0-e08a-4fdf-9955-a869f72bbafb");
			Name = "SenderValidationInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,84,77,107,195,48,12,61,39,144,255,32,232,61,185,175,165,151,82,202,96,27,101,41,221,217,75,148,204,224,216,193,114,58,74,217,127,159,226,164,89,215,117,253,58,25,203,122,79,79,210,195,90,84,72,181,200,16,86,104,173,32,83,184,120,102,116,33,203,198,10,39,141,142,103,243,244,217,228,168,40,10,119,81,24,52,36,117,9,233,150,28,86,156,169,20,102,109,26,197,11,212,104,101,54,142,66,206,26,89,44,57,10,51,37,136,30,32,69,157,163,93,11,37,115,79,250,168,11,227,243,146,36,129,9,53,85,37,236,118,218,223,95,177,182,72,168,29,1,159,53,115,35,20,198,2,121,18,216,12,44,241,158,32,57,96,168,155,119,37,51,200,218,194,255,212,13,118,190,246,32,114,105,77,141,214,73,100,165,75,15,239,222,143,197,249,192,2,89,151,87,195,167,251,64,248,180,134,7,130,149,144,138,64,73,114,241,128,77,142,193,19,22,223,224,112,93,93,128,255,100,247,93,61,241,251,132,156,229,21,76,225,173,69,206,61,176,141,195,14,74,116,227,86,216,24,190,110,233,160,159,40,230,247,119,113,137,226,124,39,235,61,250,124,55,35,222,102,183,50,127,239,162,135,193,224,175,243,120,183,27,201,30,232,156,224,249,175,49,94,239,181,190,153,206,125,173,211,185,193,121,186,188,108,188,147,101,175,241,93,16,92,191,183,118,198,96,138,223,98,255,217,219,169,177,31,136,163,227,121,223,162,131,39,2,154,255,145,243,165,187,170,195,100,94,24,112,215,146,163,144,99,223,165,115,188,72,183,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f6ab86a0-e08a-4fdf-9955-a869f72bbafb"));
		}

		#endregion

	}

	#endregion

}

