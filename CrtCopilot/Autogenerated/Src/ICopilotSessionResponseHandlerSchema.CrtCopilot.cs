namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotSessionResponseHandlerSchema

	/// <exclude/>
	public class ICopilotSessionResponseHandlerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotSessionResponseHandlerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotSessionResponseHandlerSchema(ICopilotSessionResponseHandlerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ca6ba16c-f851-477a-baa5-430a2cb04d19");
			Name = "ICopilotSessionResponseHandler";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,142,75,10,194,64,12,134,215,22,122,135,44,21,164,23,168,10,210,141,110,109,47,144,78,83,29,58,77,74,51,93,20,241,238,206,80,93,136,184,20,2,129,252,143,47,140,61,233,128,134,160,24,9,189,149,172,144,193,58,241,105,114,79,147,213,164,150,175,80,206,234,169,207,170,91,176,52,225,144,255,84,178,10,181,211,160,7,199,48,213,206,26,176,236,105,108,35,225,252,170,46,73,213,10,95,2,88,88,233,132,220,56,26,67,34,18,87,177,97,87,139,184,3,20,200,139,120,212,153,205,250,51,14,186,236,109,180,25,114,46,126,207,149,116,196,96,190,46,123,104,168,197,201,249,77,254,134,192,127,186,31,105,18,230,9,68,210,209,141,88,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ca6ba16c-f851-477a-baa5-430a2cb04d19"));
		}

		#endregion

	}

	#endregion

}

