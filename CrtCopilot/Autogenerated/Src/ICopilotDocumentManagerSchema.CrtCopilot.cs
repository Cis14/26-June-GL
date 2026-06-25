namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotDocumentManagerSchema

	/// <exclude/>
	public class ICopilotDocumentManagerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotDocumentManagerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotDocumentManagerSchema(ICopilotDocumentManagerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6c3a12f5-e91d-4996-8eb7-215462094c76");
			Name = "ICopilotDocumentManager";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,145,209,74,195,64,16,69,159,45,244,31,134,62,41,148,228,3,26,3,37,138,4,236,83,253,129,109,50,49,11,217,221,176,179,81,74,241,223,157,77,55,105,180,21,21,20,2,33,153,153,51,247,222,209,66,33,181,162,64,200,44,10,39,77,148,153,86,54,198,205,103,135,249,236,170,35,169,159,97,187,39,135,138,43,77,131,5,247,104,138,30,80,163,149,197,106,236,121,66,107,5,153,202,69,247,154,11,181,66,237,162,92,59,180,21,211,41,202,106,225,50,163,218,6,61,128,231,120,50,142,99,72,168,83,74,216,125,26,190,199,17,168,140,5,37,180,120,246,248,210,20,157,71,18,72,61,74,93,231,64,72,196,188,104,160,197,19,92,219,237,26,89,240,192,64,204,131,183,187,0,219,120,58,90,110,61,244,122,206,4,245,63,214,101,73,147,253,206,128,171,17,138,209,11,112,130,196,28,150,86,245,165,138,213,117,22,65,18,160,22,187,6,203,104,132,199,159,233,73,43,172,80,160,249,14,183,139,224,102,145,6,161,163,189,36,238,219,46,79,13,251,23,233,163,36,7,166,186,36,238,3,226,197,200,210,251,26,130,160,211,101,54,161,255,58,72,216,30,21,12,74,150,224,119,36,254,154,161,51,29,87,220,172,126,158,162,32,50,133,20,14,75,120,149,174,62,70,218,89,203,197,254,96,252,10,65,79,46,252,103,17,14,254,243,126,211,152,194,23,158,127,227,171,178,70,245,178,131,137,66,52,205,127,58,57,155,242,11,79,35,19,21,23,3,200,184,240,157,253,37,132,255,199,180,252,72,15,12,177,188,205,103,252,188,3,76,129,93,126,72,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6c3a12f5-e91d-4996-8eb7-215462094c76"));
		}

		#endregion

	}

	#endregion

}

