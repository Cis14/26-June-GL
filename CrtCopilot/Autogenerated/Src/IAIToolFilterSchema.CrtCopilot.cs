namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IAIToolFilterSchema

	/// <exclude/>
	public class IAIToolFilterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IAIToolFilterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IAIToolFilterSchema(IAIToolFilterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b63a9fe8-161f-4f49-b9e7-ef3b5922cb47");
			Name = "IAIToolFilter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2a14007c-a543-4bf5-b38e-4b55a0d5d02a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,82,203,110,2,49,12,60,131,196,63,88,234,133,30,202,222,233,10,9,65,145,246,80,169,42,252,64,54,49,37,82,54,142,242,168,160,85,255,189,121,44,143,182,28,232,41,177,99,143,61,51,209,172,67,103,24,71,88,88,100,94,210,100,65,70,42,242,163,225,231,104,56,26,14,238,44,190,73,210,208,104,143,118,27,11,167,208,204,155,13,145,90,73,21,83,185,168,170,42,168,93,232,58,102,15,179,62,94,163,115,177,241,193,113,50,40,96,155,171,193,239,152,7,78,218,91,82,14,250,89,224,35,28,224,222,144,11,22,39,61,192,43,250,96,53,212,124,230,109,192,186,226,51,216,82,66,64,200,152,5,203,237,40,40,1,22,59,38,53,4,93,230,160,56,162,212,213,197,94,38,180,74,114,144,71,46,191,169,12,10,231,19,233,103,244,59,18,110,10,47,185,177,60,182,105,217,117,30,219,104,174,130,192,164,141,246,227,121,83,46,5,109,17,73,226,190,144,141,231,253,99,15,141,90,20,244,28,127,21,145,127,38,175,105,190,68,199,173,108,89,171,240,6,241,159,246,113,145,36,127,150,172,104,226,122,241,153,6,19,35,233,60,48,112,104,37,83,242,35,225,130,200,51,140,143,45,177,51,11,93,76,60,138,217,116,70,97,23,41,166,159,162,29,48,139,201,54,228,62,58,236,9,140,165,119,41,48,226,246,74,187,84,201,147,135,43,75,221,242,132,95,220,100,60,94,15,55,58,117,85,0,248,243,27,111,178,240,178,229,188,20,108,232,28,140,255,97,88,204,125,3,89,85,183,250,72,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b63a9fe8-161f-4f49-b9e7-ef3b5922cb47"));
		}

		#endregion

	}

	#endregion

}

