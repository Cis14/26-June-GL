namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotToolContextBuilderSchema

	/// <exclude/>
	public class ICopilotToolContextBuilderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotToolContextBuilderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotToolContextBuilderSchema(ICopilotToolContextBuilderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("637f58a9-8fc0-472c-8a54-678923daad48");
			Name = "ICopilotToolContextBuilder";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,145,221,106,195,48,12,133,175,91,232,59,8,122,211,193,200,3,180,91,161,13,163,228,98,48,200,94,64,117,180,70,76,145,131,237,192,70,233,187,207,113,210,210,253,208,155,93,217,242,249,142,100,142,20,27,242,45,26,130,220,17,6,182,89,110,91,22,27,102,211,227,108,58,233,60,235,1,202,79,31,168,137,138,8,153,200,168,207,118,164,228,216,172,46,204,15,123,246,76,1,43,12,24,137,200,204,29,29,162,15,10,13,228,222,226,184,37,20,35,249,106,173,228,54,190,127,132,109,199,82,145,75,14,238,73,69,1,62,91,110,58,38,199,228,186,12,138,227,107,91,249,37,188,116,123,97,51,136,191,253,144,26,228,53,134,146,188,143,198,43,109,49,226,163,2,126,56,239,86,183,155,109,90,46,223,89,228,95,157,118,116,93,46,138,39,237,26,114,184,23,122,24,233,77,90,68,159,114,17,119,179,6,76,117,127,247,247,240,7,223,39,175,161,52,53,53,184,78,161,106,240,240,8,218,137,156,63,50,39,173,134,252,82,125,26,86,247,237,241,244,5,96,123,97,62,49,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("637f58a9-8fc0-472c-8a54-678923daad48"));
		}

		#endregion

	}

	#endregion

}

