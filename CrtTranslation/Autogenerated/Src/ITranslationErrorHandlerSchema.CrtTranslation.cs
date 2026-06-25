namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationErrorHandlerSchema

	/// <exclude/>
	public class ITranslationErrorHandlerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationErrorHandlerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationErrorHandlerSchema(ITranslationErrorHandlerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("89e47229-db11-45c3-8172-96402d1727d6");
			Name = "ITranslationErrorHandler";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("0b3cc894-bd0d-4e1b-bb7d-87203708d013");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,82,203,110,194,48,16,60,131,196,63,172,224,210,74,85,114,135,52,151,182,106,57,84,66,192,15,24,123,19,172,58,118,180,235,32,69,21,255,94,219,41,143,34,149,147,119,215,227,153,209,172,59,214,182,134,77,207,30,155,236,197,25,131,210,107,103,57,123,71,139,164,229,98,50,158,140,173,104,144,91,33,17,182,72,36,216,85,62,96,109,165,235,142,68,132,103,91,18,150,77,170,39,227,239,248,102,52,35,172,67,11,75,235,145,170,240,120,14,203,43,216,27,145,163,15,97,149,65,74,248,182,219,25,45,65,159,224,119,208,163,65,225,44,241,137,126,239,20,207,97,149,56,134,203,60,207,161,224,174,105,4,245,229,105,176,66,98,205,158,1,35,33,131,147,178,35,84,160,58,138,57,136,182,53,125,44,252,69,154,179,51,91,126,75,87,180,130,68,3,49,159,231,105,164,180,174,227,53,74,71,138,167,101,50,13,33,58,22,53,50,200,33,221,160,86,133,113,192,80,8,213,89,117,163,7,95,216,115,86,228,137,58,41,29,156,86,176,17,7,76,132,252,240,170,211,142,130,145,130,125,244,253,4,195,89,194,141,135,199,197,157,44,214,216,184,67,240,37,140,25,226,184,88,173,200,53,224,247,97,223,87,190,188,216,25,252,39,140,100,113,141,140,254,215,227,73,121,134,86,13,91,74,253,113,248,26,127,134,71,248,1,84,94,232,17,133,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("89e47229-db11-45c3-8172-96402d1727d6"));
		}

		#endregion

	}

	#endregion

}

