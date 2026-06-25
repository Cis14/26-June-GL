namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: WebhookPersistenceTypeSchema

	/// <exclude/>
	public class WebhookPersistenceTypeSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public WebhookPersistenceTypeSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public WebhookPersistenceTypeSchema(WebhookPersistenceTypeSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8fdbb8a2-da42-4470-b583-8dd6d095523f");
			Name = "WebhookPersistenceType";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,143,209,10,130,48,20,134,175,21,246,14,231,1,66,171,235,236,34,47,35,136,50,186,158,235,104,195,60,147,109,18,18,190,123,90,26,131,40,188,220,225,251,190,159,17,47,209,84,92,32,36,168,53,55,42,179,65,172,40,147,121,173,185,149,138,152,255,96,190,23,134,33,172,76,93,150,92,55,235,225,157,52,21,130,202,160,66,109,164,177,120,129,51,166,87,165,10,232,146,134,231,24,140,98,232,152,85,157,222,164,0,164,186,28,249,253,16,32,129,125,179,131,250,201,175,205,215,33,230,100,55,120,68,178,176,123,175,128,237,156,224,35,184,91,158,67,71,48,159,49,255,103,247,128,198,106,41,250,95,76,232,58,116,4,139,127,221,19,21,164,238,180,197,102,74,215,161,35,88,118,93,175,101,126,251,4,175,120,175,137,164,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8fdbb8a2-da42-4470-b583-8dd6d095523f"));
		}

		#endregion

	}

	#endregion

}

