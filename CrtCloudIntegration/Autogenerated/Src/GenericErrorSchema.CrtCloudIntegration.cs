namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenericErrorSchema

	/// <exclude/>
	public class GenericErrorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenericErrorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenericErrorSchema(GenericErrorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("65750127-9831-4494-b88e-c337e322ffc7");
			Name = "GenericError";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,81,203,106,195,48,16,60,219,224,127,88,232,37,133,146,15,112,232,201,13,37,167,230,210,15,144,165,197,8,92,73,172,214,189,24,255,123,36,91,193,175,164,244,184,59,51,187,179,59,70,252,160,119,66,34,84,196,85,107,59,117,49,140,13,9,214,214,28,191,89,183,154,53,250,227,153,200,146,47,242,190,200,179,23,194,38,160,80,181,194,251,18,62,209,32,105,57,50,138,60,224,174,171,91,45,65,212,158,73,72,6,25,121,43,26,148,112,73,252,172,31,53,243,80,107,130,172,147,28,214,149,112,37,203,40,25,213,68,114,247,114,53,237,16,4,218,52,128,177,168,172,194,55,72,29,133,94,146,118,241,150,87,136,214,179,236,124,39,193,251,44,56,141,208,199,204,14,224,66,59,194,67,242,137,70,77,86,215,190,131,83,135,20,127,21,92,143,15,72,150,167,103,36,67,243,246,30,26,228,19,56,210,191,130,17,124,44,134,189,96,233,233,137,100,239,42,116,166,254,198,236,31,193,125,25,156,147,219,7,22,96,216,38,157,245,207,114,91,93,190,25,243,207,176,74,168,133,199,195,130,181,207,242,113,34,143,46,31,110,16,210,84,122,231,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("65750127-9831-4494-b88e-c337e322ffc7"));
		}

		#endregion

	}

	#endregion

}

