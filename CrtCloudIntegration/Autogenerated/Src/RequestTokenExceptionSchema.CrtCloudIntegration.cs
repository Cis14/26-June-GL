namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RequestTokenExceptionSchema

	/// <exclude/>
	public class RequestTokenExceptionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RequestTokenExceptionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RequestTokenExceptionSchema(RequestTokenExceptionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7333162c-1720-409d-8f39-077934931a56");
			Name = "RequestTokenException";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,146,207,78,194,64,16,198,207,109,194,59,76,224,2,9,233,3,208,224,193,166,7,15,38,68,184,25,15,75,59,52,27,183,179,117,102,171,34,225,221,221,165,21,81,122,48,30,186,201,252,251,125,243,77,74,170,70,105,84,129,176,65,102,37,118,231,146,204,210,78,87,45,43,167,45,37,183,77,109,201,104,194,204,216,182,188,35,135,85,87,25,197,135,81,28,181,162,169,130,245,94,28,214,233,40,246,153,9,99,229,203,144,25,37,178,128,7,124,105,81,220,198,62,35,229,239,5,54,221,172,111,124,92,35,107,101,244,135,218,26,156,206,158,124,170,105,183,70,23,80,132,209,225,73,88,192,5,37,10,43,68,225,59,171,90,18,199,109,225,44,123,241,213,137,119,82,251,98,15,82,167,51,56,145,142,127,104,245,248,96,217,223,77,84,133,51,191,208,86,9,78,207,241,63,65,243,111,95,160,137,144,207,225,111,137,249,85,253,16,117,71,232,85,39,72,101,119,142,62,238,111,179,98,219,32,59,141,195,151,177,175,254,23,208,37,66,191,216,125,39,7,203,27,24,103,70,35,57,112,193,1,112,103,7,222,148,0,43,45,88,130,34,240,195,150,147,113,250,131,217,163,242,80,203,172,103,31,160,66,151,130,132,231,8,203,190,33,201,235,198,237,211,235,245,131,165,96,237,50,119,252,4,189,192,215,43,181,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7333162c-1720-409d-8f39-077934931a56"));
		}

		#endregion

	}

	#endregion

}

