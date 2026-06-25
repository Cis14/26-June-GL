namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutorSchema

	/// <exclude/>
	public class UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutorSchema(UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1e866043-e715-4572-b908-a8cc1a39eb09");
			Name = "UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("a6ded360-42cd-4008-9952-fcaf8207688b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,81,203,106,195,48,16,60,187,208,127,88,114,114,32,24,122,232,165,33,167,180,20,31,122,114,211,107,80,228,117,16,168,146,89,173,220,132,210,127,175,252,10,150,113,41,72,66,43,102,103,103,70,70,124,162,171,133,68,120,71,34,225,108,197,217,222,154,74,157,61,9,86,214,192,247,253,93,226,157,50,231,8,65,184,189,189,183,85,113,117,5,50,135,210,193,110,134,140,9,179,9,52,112,4,150,218,159,180,146,32,181,112,14,14,117,41,24,223,132,210,39,123,41,174,70,230,134,145,26,161,39,109,31,66,123,204,141,99,161,117,33,73,213,252,114,65,233,217,210,19,228,139,239,157,139,164,38,213,4,114,144,54,96,64,25,134,163,213,229,51,86,29,97,208,253,176,253,3,102,240,107,10,123,92,192,57,166,54,140,163,187,233,220,219,178,69,175,22,204,172,122,231,163,245,198,170,18,122,173,152,30,28,82,72,204,160,236,242,247,81,185,238,157,36,141,32,104,6,57,105,144,184,158,125,66,246,138,220,201,77,227,254,205,92,224,186,179,146,168,10,210,129,111,23,165,50,14,76,230,3,194,101,196,252,55,99,19,5,56,76,252,105,207,246,8,59,172,95,250,222,12,69,137,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1e866043-e715-4572-b908-a8cc1a39eb09"));
		}

		#endregion

	}

	#endregion

}

