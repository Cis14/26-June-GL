namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: WebSocketDtoSchema

	/// <exclude/>
	public class WebSocketDtoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public WebSocketDtoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public WebSocketDtoSchema(WebSocketDtoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("43107381-18a7-4acf-937d-84d2939c6d9e");
			Name = "WebSocketDto";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,177,110,131,48,16,134,231,32,241,14,86,178,180,75,30,160,25,73,7,50,84,72,12,25,170,14,198,92,144,85,240,161,187,67,81,133,242,238,181,129,180,74,235,129,197,146,253,253,247,217,190,115,186,3,238,181,1,149,145,100,45,14,117,238,4,26,210,98,209,237,143,130,105,50,166,201,102,96,235,26,245,6,87,65,199,120,145,253,137,209,29,210,196,163,29,65,227,179,42,107,53,243,139,58,67,85,162,249,4,153,106,61,239,135,170,181,70,153,128,255,208,205,56,37,126,20,5,97,15,36,22,188,167,152,202,102,254,30,110,91,224,215,211,214,96,215,105,87,111,159,63,2,92,252,44,20,158,152,205,76,141,170,1,57,40,14,203,45,106,169,129,13,217,62,252,51,106,58,254,242,21,54,32,66,202,176,134,168,235,245,78,87,152,250,86,203,5,169,139,138,138,5,174,240,88,46,7,99,128,249,81,84,33,182,42,191,179,21,158,43,84,60,141,172,244,121,223,140,60,222,248,243,191,88,204,189,3,87,207,195,158,246,243,233,227,225,237,27,249,237,225,244,147,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("43107381-18a7-4acf-937d-84d2939c6d9e"));
		}

		#endregion

	}

	#endregion

}

