namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationCleaningServiceSchema

	/// <exclude/>
	public class ITranslationCleaningServiceSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationCleaningServiceSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationCleaningServiceSchema(ITranslationCleaningServiceSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("af8e1d2b-4545-4dfe-9252-84d3fd6123ef");
			Name = "ITranslationCleaningService";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1832addf-3d36-4f2f-bd0b-0099d01bacc9");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,144,187,14,194,48,12,69,103,42,245,31,44,177,192,210,238,128,88,152,24,144,16,143,15,48,141,83,34,165,14,114,18,36,132,248,119,74,194,115,97,179,175,125,207,181,204,216,145,63,97,67,176,35,17,244,78,135,106,225,88,155,54,10,6,227,184,218,9,178,183,169,46,139,107,89,148,197,96,40,212,246,45,44,57,144,232,222,60,129,229,215,218,194,18,178,225,118,75,114,54,13,37,203,41,30,172,105,192,188,28,255,13,131,156,243,14,90,81,56,58,229,39,176,78,152,60,172,235,26,102,62,118,29,202,101,254,18,30,40,1,180,22,34,71,79,10,194,39,6,132,188,139,210,144,7,45,174,251,25,41,12,88,189,169,245,55,246,236,140,202,220,125,66,110,72,147,16,247,152,209,120,250,188,147,88,229,83,83,127,203,95,250,17,147,86,22,119,40,74,25,24,112,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("af8e1d2b-4545-4dfe-9252-84d3fd6123ef"));
		}

		#endregion

	}

	#endregion

}

