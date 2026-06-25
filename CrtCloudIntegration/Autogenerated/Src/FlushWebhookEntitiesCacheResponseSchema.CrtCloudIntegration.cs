namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FlushWebhookEntitiesCacheResponseSchema

	/// <exclude/>
	public class FlushWebhookEntitiesCacheResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FlushWebhookEntitiesCacheResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FlushWebhookEntitiesCacheResponseSchema(FlushWebhookEntitiesCacheResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b9d6e8ee-058b-485d-b166-3b22c334e52a");
			Name = "FlushWebhookEntitiesCacheResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,143,77,75,196,48,16,134,207,91,232,127,120,193,123,123,119,197,203,162,224,109,81,193,115,26,39,109,176,205,148,76,194,34,139,255,221,124,180,178,120,25,152,201,60,111,158,129,83,11,201,170,52,225,157,188,87,194,38,116,39,118,198,142,209,171,96,217,181,205,181,109,218,230,208,247,61,30,36,46,139,242,223,143,91,255,154,80,118,66,208,179,18,129,97,15,51,71,153,172,27,113,161,97,98,254,2,185,96,131,37,129,86,122,162,110,79,234,111,162,214,56,204,86,111,33,207,57,224,163,194,79,27,123,202,232,254,89,2,170,209,225,206,211,152,12,113,246,188,146,207,139,247,56,151,172,250,254,95,185,12,210,113,65,89,39,176,6,25,43,71,194,10,36,106,77,34,221,31,121,171,184,59,14,204,51,94,228,173,238,226,138,145,194,17,146,203,207,38,69,238,179,122,149,190,76,83,249,5,136,121,195,11,105,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b9d6e8ee-058b-485d-b166-3b22c334e52a"));
		}

		#endregion

	}

	#endregion

}

