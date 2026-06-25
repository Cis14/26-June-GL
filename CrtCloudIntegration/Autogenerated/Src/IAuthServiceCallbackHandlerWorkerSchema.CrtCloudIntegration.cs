namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IAuthServiceCallbackHandlerWorkerSchema

	/// <exclude/>
	public class IAuthServiceCallbackHandlerWorkerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IAuthServiceCallbackHandlerWorkerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IAuthServiceCallbackHandlerWorkerSchema(IAuthServiceCallbackHandlerWorkerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("57e78de3-15fe-432d-8438-0ba6c8daa811");
			Name = "IAuthServiceCallbackHandlerWorker";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,79,107,195,48,12,197,207,13,228,59,136,238,178,193,72,238,109,87,24,189,44,135,193,88,55,118,118,99,181,49,77,28,79,178,11,165,244,187,207,113,254,44,237,160,236,168,103,189,159,159,45,105,81,33,27,145,35,172,200,174,202,218,201,76,91,220,145,176,170,214,201,26,233,160,114,228,56,58,197,209,196,177,210,59,88,31,217,98,53,143,35,175,220,17,238,124,31,52,30,218,122,202,12,178,103,103,139,206,183,18,101,185,17,249,254,69,104,89,34,125,213,180,71,10,198,52,77,97,193,174,170,4,29,151,93,253,142,134,144,81,91,6,91,32,168,158,9,219,154,130,82,52,152,38,66,222,113,129,240,219,33,219,4,122,100,58,98,26,183,41,85,62,226,252,35,218,228,20,226,13,15,123,69,91,212,146,103,240,22,96,237,225,117,248,32,180,32,254,147,109,72,207,6,115,181,85,40,193,148,194,122,181,74,6,88,122,77,91,24,65,162,2,237,167,243,52,237,251,167,203,15,207,25,220,139,52,52,221,246,124,50,82,38,47,157,224,188,8,74,250,175,110,2,209,109,146,48,198,191,60,236,67,139,25,9,23,206,67,173,100,247,13,247,108,169,25,84,127,227,35,92,9,109,172,65,30,33,31,230,221,4,80,203,118,8,161,62,183,251,246,43,198,209,249,7,84,145,9,5,188,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("57e78de3-15fe-432d-8438-0ba6c8daa811"));
		}

		#endregion

	}

	#endregion

}

