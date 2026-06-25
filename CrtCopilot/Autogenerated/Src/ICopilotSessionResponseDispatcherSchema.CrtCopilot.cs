namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotSessionResponseDispatcherSchema

	/// <exclude/>
	public class ICopilotSessionResponseDispatcherSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotSessionResponseDispatcherSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotSessionResponseDispatcherSchema(ICopilotSessionResponseDispatcherSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("673671fa-005e-44fb-849b-c8483c87ec73");
			Name = "ICopilotSessionResponseDispatcher";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,142,75,10,194,64,12,134,215,22,122,135,44,21,164,23,16,23,82,55,110,109,47,48,78,83,27,58,205,148,38,93,20,241,238,102,80,17,5,33,16,200,151,255,193,110,64,25,157,71,40,39,116,74,177,40,227,72,33,106,158,221,242,108,53,11,241,21,170,69,20,135,162,238,236,165,177,195,238,47,41,106,39,189,124,248,143,169,1,67,227,124,9,228,129,88,113,106,83,244,233,133,43,20,161,200,103,107,20,89,240,72,214,76,125,135,147,137,82,155,85,114,135,247,249,32,11,251,245,183,20,228,185,183,80,58,246,24,66,74,231,58,246,200,224,21,246,208,96,235,230,160,155,84,241,158,103,54,15,140,139,209,59,2,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("673671fa-005e-44fb-849b-c8483c87ec73"));
		}

		#endregion

	}

	#endregion

}

