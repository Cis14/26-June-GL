namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TranslationLoggerSchema

	/// <exclude/>
	public class TranslationLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TranslationLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TranslationLoggerSchema(TranslationLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5a3f11c0-05c4-491f-b69b-c36875a97219");
			Name = "TranslationLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5bb0d1bd-fb48-4884-8a1c-84058bcf48c3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,219,106,195,48,12,125,78,161,255,32,186,151,20,70,62,32,165,148,209,149,17,88,161,108,221,243,112,82,213,203,72,236,32,59,101,165,244,223,231,216,73,215,92,182,238,201,150,116,116,142,116,108,193,114,84,5,75,16,182,72,196,148,220,235,96,41,197,62,229,37,49,157,74,17,108,137,9,149,217,251,120,116,26,143,188,82,165,130,195,235,81,105,204,103,151,152,103,50,102,89,24,46,101,158,155,174,103,201,185,73,255,212,175,233,9,127,203,95,171,25,140,65,221,17,114,19,192,50,99,74,133,112,85,175,36,144,44,168,40,227,44,77,32,169,48,125,8,132,16,13,244,121,39,219,251,163,32,133,210,84,38,90,146,17,218,88,74,135,168,233,123,28,254,155,66,50,109,2,147,42,9,101,43,156,66,229,150,231,117,64,243,14,172,178,194,59,215,163,160,216,185,105,218,163,109,72,22,72,58,197,106,48,74,15,76,99,61,153,11,160,163,209,9,221,28,28,181,213,242,84,125,57,183,57,34,179,20,188,103,118,179,89,175,80,91,121,161,170,111,30,161,46,73,52,125,176,88,128,223,220,231,85,211,154,9,102,130,224,9,117,109,218,164,231,227,100,58,117,147,157,255,225,197,26,245,135,220,13,191,208,65,166,59,120,196,184,228,190,140,63,205,246,96,126,183,50,250,205,91,56,189,192,65,154,90,203,140,43,162,21,145,36,127,245,149,96,97,109,236,178,184,186,249,51,230,31,7,171,188,208,199,123,184,197,22,61,216,39,121,65,85,102,26,200,30,131,180,174,20,172,111,204,24,137,189,252,123,87,139,24,90,181,99,176,203,182,147,38,247,13,183,167,214,242,33,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5a3f11c0-05c4-491f-b69b-c36875a97219"));
		}

		#endregion

	}

	#endregion

}

