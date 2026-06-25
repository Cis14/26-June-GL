namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LlmModelCategoryEventListenerSchema

	/// <exclude/>
	public class LlmModelCategoryEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LlmModelCategoryEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LlmModelCategoryEventListenerSchema(LlmModelCategoryEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("158743d4-f637-40b7-8f69-5ff3310bdd3a");
			Name = "LlmModelCategoryEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,205,74,195,64,16,62,167,208,119,24,234,37,1,201,3,180,120,168,177,149,66,171,98,189,137,200,54,59,77,87,54,187,97,119,19,40,197,119,119,146,52,98,67,18,65,143,59,243,205,247,51,179,138,165,104,51,22,35,68,6,153,19,58,140,116,38,164,118,227,209,105,60,242,114,43,84,2,47,104,12,179,122,239,168,169,246,34,201,77,137,84,225,61,170,249,106,214,9,51,24,46,148,19,78,160,253,21,16,46,10,84,174,31,183,100,177,211,166,102,34,204,149,193,132,212,33,146,204,218,41,172,101,186,209,28,101,196,28,38,218,28,43,178,181,176,14,21,154,106,224,181,18,186,108,248,219,248,128,41,123,160,248,112,3,147,54,201,36,120,163,193,44,223,73,17,67,92,10,13,235,192,20,110,153,197,14,33,162,57,85,46,190,125,47,5,74,78,198,159,140,40,136,171,110,102,245,3,232,8,92,43,121,132,85,91,239,25,51,109,5,45,226,8,239,105,119,99,118,22,66,197,107,173,75,97,58,158,117,38,47,151,89,202,87,225,206,234,117,208,193,136,254,144,163,30,67,1,148,159,200,243,250,12,211,234,251,163,120,222,231,112,158,13,186,131,230,221,81,116,65,127,72,112,132,66,11,14,143,106,203,10,228,190,222,125,96,236,192,18,33,154,107,168,207,53,223,59,52,85,212,185,73,44,96,99,122,71,7,13,155,201,102,4,131,217,96,162,48,146,200,76,196,232,119,249,193,207,16,61,190,238,80,162,251,163,179,102,246,223,222,90,11,174,171,151,69,170,125,1,227,64,215,131,43,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("158743d4-f637-40b7-8f69-5ff3310bdd3a"));
		}

		#endregion

	}

	#endregion

}

