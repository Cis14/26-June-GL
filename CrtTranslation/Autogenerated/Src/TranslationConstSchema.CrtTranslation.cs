namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TranslationConstSchema

	/// <exclude/>
	public class TranslationConstSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TranslationConstSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TranslationConstSchema(TranslationConstSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("47a437e3-bc45-4a8c-aefe-286748485a45");
			Name = "TranslationConst";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2f244451-ec5e-494f-9790-8d930a80007c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,193,14,130,48,12,134,207,144,240,14,11,222,125,0,141,39,226,201,160,38,242,2,117,20,92,2,157,105,199,129,24,223,221,49,52,64,226,129,219,250,245,239,151,180,83,4,45,202,19,52,170,2,153,65,108,229,182,153,165,202,212,29,131,51,150,182,5,3,73,19,222,73,252,74,226,36,142,54,140,181,47,85,214,128,200,78,205,18,126,84,92,200,60,187,123,99,180,18,231,185,86,122,72,254,9,70,163,112,50,14,24,200,121,235,53,8,198,246,87,166,135,174,87,178,161,122,46,187,233,7,182,112,246,171,168,131,74,111,189,204,122,233,126,133,224,132,125,102,155,174,165,159,195,131,85,131,71,102,203,203,209,128,114,20,129,26,87,57,114,91,154,202,96,121,161,165,104,226,131,38,92,9,169,28,15,21,234,247,248,25,11,232,217,7,100,236,47,195,212,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("47a437e3-bc45-4a8c-aefe-286748485a45"));
		}

		#endregion

	}

	#endregion

}

