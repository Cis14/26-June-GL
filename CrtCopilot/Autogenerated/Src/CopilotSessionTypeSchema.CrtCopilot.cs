namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotSessionTypeSchema

	/// <exclude/>
	public class CopilotSessionTypeSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotSessionTypeSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotSessionTypeSchema(CopilotSessionTypeSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f3a670f3-c82d-4655-8a44-75f58ff23acc");
			Name = "CopilotSessionType";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,75,10,194,48,16,134,215,41,244,14,3,110,181,217,139,8,82,188,64,245,2,211,118,180,129,230,65,38,93,20,241,238,198,62,160,88,65,87,97,38,127,190,111,50,6,53,177,195,138,32,247,132,65,217,44,183,78,181,54,164,201,35,77,210,68,108,60,221,149,53,112,54,157,222,195,116,121,33,230,216,188,246,142,134,144,148,18,14,220,105,141,190,63,78,117,65,206,19,147,9,12,161,33,8,49,11,246,6,56,51,128,71,72,54,191,151,11,128,235,202,86,85,64,81,250,213,41,226,112,98,165,253,244,34,84,13,134,93,137,76,245,82,183,246,137,60,6,183,195,95,126,83,13,160,83,127,81,79,78,197,227,57,46,146,76,61,238,242,93,198,222,11,222,223,222,58,123,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f3a670f3-c82d-4655-8a44-75f58ff23acc"));
		}

		#endregion

	}

	#endregion

}

