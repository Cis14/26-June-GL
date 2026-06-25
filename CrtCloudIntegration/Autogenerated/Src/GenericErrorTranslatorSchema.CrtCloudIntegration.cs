namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenericErrorTranslatorSchema

	/// <exclude/>
	public class GenericErrorTranslatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenericErrorTranslatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenericErrorTranslatorSchema(GenericErrorTranslatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("aca0b116-a4c6-4063-89a7-9a142e9ebd4e");
			Name = "GenericErrorTranslator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,145,207,74,196,48,16,135,207,41,236,59,12,120,233,194,210,7,216,197,139,93,149,61,8,130,138,71,201,38,67,13,198,164,76,166,130,44,251,238,166,73,187,80,173,61,149,78,62,126,223,252,113,242,19,67,43,21,66,77,92,91,223,233,131,99,108,72,178,241,174,122,97,99,13,27,12,171,226,180,42,68,23,140,107,102,193,61,251,221,34,112,73,170,110,137,60,133,72,71,254,138,176,137,175,80,91,25,194,22,238,209,33,25,149,136,103,146,46,88,201,158,18,217,118,71,107,20,168,30,252,135,219,194,141,12,248,171,184,129,195,159,56,113,74,145,23,251,157,65,171,163,254,145,204,151,100,204,143,109,254,1,66,169,189,179,223,19,41,188,97,255,217,13,57,232,116,142,154,230,214,222,5,238,84,116,246,225,105,128,33,59,15,51,63,70,57,17,37,207,26,250,237,11,145,173,112,13,163,93,136,243,114,11,15,200,239,94,207,235,95,241,248,228,213,7,114,60,30,140,126,44,71,25,33,119,20,135,136,11,224,188,213,200,149,185,133,106,143,65,145,105,251,211,110,134,101,228,195,214,94,227,122,161,179,92,157,22,207,197,15,161,65,32,9,134,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("aca0b116-a4c6-4063-89a7-9a142e9ebd4e"));
		}

		#endregion

	}

	#endregion

}

