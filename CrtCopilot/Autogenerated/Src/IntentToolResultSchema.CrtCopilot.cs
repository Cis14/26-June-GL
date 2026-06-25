namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IntentToolResultSchema

	/// <exclude/>
	public class IntentToolResultSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IntentToolResultSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IntentToolResultSchema(IntentToolResultSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8f81cb1b-e15f-486d-ae3b-d410c6798fd1");
			Name = "IntentToolResult";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("421a8d84-5f16-4efa-b563-3ab0d40eb264");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,144,193,78,195,48,12,134,207,173,212,119,176,122,2,9,245,5,38,14,168,219,97,7,56,172,8,14,136,131,27,188,42,34,77,170,216,1,141,105,239,78,66,53,193,80,135,202,33,150,146,223,254,252,255,177,216,19,15,168,8,106,79,40,218,85,181,27,180,113,82,228,251,34,207,2,107,219,65,179,99,161,190,218,4,43,186,167,170,33,175,209,232,143,212,110,23,69,30,251,158,150,40,88,59,43,30,149,60,199,7,109,133,188,69,3,202,32,51,172,227,213,202,189,115,102,67,28,76,164,103,9,63,206,221,82,223,146,191,184,139,86,224,26,74,205,77,80,138,152,203,203,68,202,134,208,26,173,160,141,195,176,62,106,176,135,142,100,1,156,202,33,142,137,15,52,122,153,132,190,163,183,49,74,121,5,171,94,203,146,182,24,93,60,160,9,73,221,162,97,58,89,198,226,83,240,199,113,234,116,217,249,37,244,150,66,238,6,42,167,96,171,163,58,23,167,112,72,63,60,9,171,71,109,46,234,133,88,121,125,30,183,252,214,103,187,11,222,199,64,55,93,44,211,22,127,52,252,19,218,188,106,99,254,130,126,53,252,134,102,241,28,224,19,76,204,181,71,210,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8f81cb1b-e15f-486d-ae3b-d410c6798fd1"));
		}

		#endregion

	}

	#endregion

}

