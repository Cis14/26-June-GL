namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ColorColumnProcessorSchema

	/// <exclude/>
	public class ColorColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ColorColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ColorColumnProcessorSchema(ColorColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ece3bf3e-1c9f-4ab6-ae57-035b0707f269");
			Name = "ColorColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,75,235,64,16,199,207,10,126,135,161,94,90,120,36,247,218,22,158,21,31,189,136,96,235,69,60,108,55,147,58,144,236,230,205,238,138,69,252,238,78,54,141,54,49,10,94,146,221,225,63,255,153,249,101,98,84,137,174,82,26,97,141,204,202,217,220,39,75,107,114,218,5,86,158,172,73,174,169,192,85,89,89,246,103,167,175,103,167,39,193,145,217,193,221,222,121,44,47,62,238,199,217,140,223,197,147,107,165,189,101,66,39,10,209,156,51,238,164,6,44,11,229,220,20,150,182,176,44,143,80,154,91,182,26,157,179,28,117,105,154,194,140,204,19,50,249,204,106,208,140,249,124,180,198,23,223,83,143,210,69,43,119,161,44,21,239,219,251,95,3,100,156,87,70,70,181,57,248,39,114,160,235,178,32,7,22,6,214,56,218,22,8,185,101,168,26,191,122,128,216,19,232,88,6,158,85,17,208,37,109,137,244,168,198,195,21,230,42,20,254,146,76,38,121,99,191,175,208,230,227,85,175,193,201,31,184,17,228,48,7,35,47,17,12,205,60,153,60,138,99,21,182,5,233,67,147,67,50,152,194,0,2,201,124,141,208,62,233,202,104,158,67,77,94,32,223,70,219,70,241,75,174,95,192,198,192,146,81,121,116,93,188,50,189,40,17,15,150,67,237,139,103,242,97,154,246,93,103,149,98,85,70,74,243,81,112,40,217,198,160,174,87,114,180,216,200,93,190,73,27,72,102,105,84,199,228,3,182,161,138,227,77,199,7,186,182,19,225,185,85,14,199,253,112,189,245,39,111,7,166,104,178,6,107,151,177,212,168,144,189,108,246,180,62,123,201,197,236,39,200,151,82,233,23,144,175,148,87,205,250,53,108,131,161,255,114,166,12,141,167,156,144,191,65,89,181,189,128,125,150,95,81,244,240,47,80,22,253,238,107,187,181,184,109,86,25,204,23,221,88,18,1,246,101,23,131,20,26,54,221,224,219,59,168,94,36,28,90,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ece3bf3e-1c9f-4ab6-ae57-035b0707f269"));
		}

		#endregion

	}

	#endregion

}

