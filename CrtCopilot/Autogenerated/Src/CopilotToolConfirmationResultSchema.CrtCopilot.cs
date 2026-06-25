namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotToolConfirmationResultSchema

	/// <exclude/>
	public class CopilotToolConfirmationResultSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotToolConfirmationResultSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotToolConfirmationResultSchema(CopilotToolConfirmationResultSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("84f6e0b6-82e9-4f27-8b17-32e84bdd4675");
			Name = "CopilotToolConfirmationResult";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,80,75,10,194,48,16,93,183,208,59,12,117,239,1,20,23,18,112,93,138,23,136,233,88,34,233,76,72,82,55,210,187,155,79,17,93,168,171,188,228,125,9,201,9,189,149,10,65,56,148,65,243,86,176,213,134,67,83,63,154,186,169,171,141,195,81,51,129,48,210,251,29,172,236,153,217,8,166,171,118,83,50,81,143,126,54,33,27,236,124,49,90,129,15,145,80,160,146,237,159,171,42,85,175,174,147,70,51,196,178,46,71,21,110,141,85,76,62,196,112,167,105,132,163,181,142,239,8,7,104,101,129,67,187,255,38,238,241,134,42,36,173,203,232,151,182,67,26,210,25,197,118,133,234,109,119,50,230,189,145,43,147,243,125,41,31,246,241,184,60,1,50,197,139,243,97,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("84f6e0b6-82e9-4f27-8b17-32e84bdd4675"));
		}

		#endregion

	}

	#endregion

}

