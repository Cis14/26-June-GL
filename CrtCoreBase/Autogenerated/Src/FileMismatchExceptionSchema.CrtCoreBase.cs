namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileMismatchExceptionSchema

	/// <exclude/>
	public class FileMismatchExceptionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileMismatchExceptionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileMismatchExceptionSchema(FileMismatchExceptionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("17b6e497-f978-4ed3-a543-6132990eb1ec");
			Name = "FileMismatchException";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1e78c195-217a-4877-a718-71dfe1dfe442");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,146,63,111,194,48,16,197,231,32,241,29,78,105,7,88,226,157,2,11,162,82,165,86,170,84,70,150,195,92,192,146,99,71,254,35,74,17,223,189,103,7,40,69,89,226,248,229,252,238,119,207,1,131,13,249,22,37,193,138,156,67,111,235,80,45,172,169,213,46,58,12,202,154,234,85,105,122,183,184,29,14,78,195,65,17,189,50,59,248,58,250,64,205,203,112,192,202,147,163,29,215,193,66,163,247,19,72,229,31,202,55,24,228,126,249,45,169,77,38,185,80,8,1,83,31,155,6,221,113,126,217,223,42,32,236,49,128,242,188,58,123,48,112,216,147,1,132,154,221,146,106,108,128,58,106,125,132,216,106,134,161,109,117,117,20,119,150,109,220,104,37,65,38,148,126,18,152,192,29,85,113,202,100,127,51,88,227,131,139,50,88,199,163,124,102,183,174,226,17,62,11,111,70,5,133,90,253,144,103,86,67,7,80,124,30,13,167,105,107,158,132,248,8,17,72,71,245,172,236,197,41,197,188,163,173,110,77,196,99,151,105,139,14,27,72,55,53,43,211,179,156,175,216,58,189,93,219,228,152,174,9,246,100,53,21,217,35,91,94,50,234,197,25,241,244,233,126,147,247,56,21,23,19,216,160,167,209,115,198,135,117,121,74,159,206,235,178,191,81,57,134,244,147,20,231,75,172,100,182,93,178,121,223,169,255,69,214,126,1,86,152,102,190,132,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("17b6e497-f978-4ed3-a543-6132990eb1ec"));
		}

		#endregion

	}

	#endregion

}

