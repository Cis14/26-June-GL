namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutorSchema

	/// <exclude/>
	public class UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutorSchema(UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("200eba97-2db0-4b5e-bcb0-90839e22300a");
			Name = "UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a6ded360-42cd-4008-9952-fcaf8207688b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,80,223,75,195,48,16,126,238,96,255,195,177,167,22,70,209,87,167,15,50,69,6,250,32,117,190,142,152,94,107,160,38,37,185,212,13,241,127,247,210,90,109,187,33,134,36,228,194,119,223,143,211,226,13,93,45,36,194,19,90,43,156,41,40,93,27,93,168,210,91,65,202,104,152,207,62,230,179,200,59,165,203,17,198,226,234,231,63,84,217,193,101,72,196,165,131,171,9,114,76,153,14,160,204,193,44,181,127,169,148,4,89,9,231,96,91,231,130,6,116,107,139,161,237,122,243,32,246,91,135,246,209,163,61,220,163,46,233,117,163,29,137,170,202,164,85,53,221,238,81,122,50,246,2,54,39,255,89,39,36,137,106,171,26,22,0,105,24,5,74,19,236,52,190,63,139,202,35,27,63,63,11,107,117,12,116,100,67,212,157,59,104,217,59,51,121,104,89,252,225,112,209,5,236,19,54,70,229,208,25,194,56,64,121,48,26,101,59,104,63,42,19,104,205,70,141,176,208,124,123,139,217,108,50,153,117,122,135,212,122,143,199,253,203,35,167,73,27,42,82,5,196,29,225,229,111,240,94,45,154,178,243,227,6,139,255,9,44,7,124,157,214,103,184,195,197,135,247,23,147,36,31,249,109,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("200eba97-2db0-4b5e-bcb0-90839e22300a"));
		}

		#endregion

	}

	#endregion

}

