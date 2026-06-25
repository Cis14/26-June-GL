namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotConfirmationMessageModelSchema

	/// <exclude/>
	public class CopilotConfirmationMessageModelSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotConfirmationMessageModelSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotConfirmationMessageModelSchema(CopilotConfirmationMessageModelSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("576fd4bf-8686-48a4-a64c-a9f6e47f79f7");
			Name = "CopilotConfirmationMessageModel";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,77,79,195,48,12,134,207,171,212,255,96,109,23,184,244,7,48,113,89,16,183,34,180,113,67,28,188,212,84,150,242,81,37,238,97,171,246,223,73,19,62,4,76,140,75,36,219,111,158,71,182,67,75,113,64,77,160,2,161,176,111,148,31,216,120,169,171,169,174,22,99,100,215,195,238,16,133,108,179,29,157,176,165,102,71,129,209,240,113,142,187,117,93,165,220,42,80,159,10,80,6,99,188,129,119,134,242,238,149,131,205,185,150,98,196,158,90,223,145,201,95,158,239,80,48,37,36,160,150,151,212,24,198,189,97,13,122,70,92,38,44,166,76,249,52,223,51,153,46,169,31,51,165,204,178,162,37,187,167,112,245,144,22,133,91,88,234,66,220,140,34,222,61,177,24,90,94,207,246,15,125,148,48,175,172,126,197,96,130,158,100,13,113,126,78,127,8,208,105,50,23,249,63,83,255,197,219,114,133,141,239,14,103,193,237,215,252,28,114,69,174,43,23,203,117,233,126,111,158,222,0,113,232,35,245,20,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("576fd4bf-8686-48a4-a64c-a9f6e47f79f7"));
		}

		#endregion

	}

	#endregion

}

