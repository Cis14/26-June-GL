namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ApplyTranslationsStagesEnumSchema

	/// <exclude/>
	public class ApplyTranslationsStagesEnumSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ApplyTranslationsStagesEnumSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ApplyTranslationsStagesEnumSchema(ApplyTranslationsStagesEnumSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e4af9b4d-936f-43e4-a7c4-6acbb3350321");
			Name = "ApplyTranslationsStagesEnum";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2f244451-ec5e-494f-9790-8d930a80007c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,144,79,75,3,49,16,197,207,187,208,239,16,240,42,69,235,95,4,15,101,169,210,131,32,107,245,30,147,183,219,96,118,178,76,178,66,149,126,119,103,179,30,180,189,101,230,247,222,228,205,144,238,16,123,109,160,54,96,214,49,52,105,94,5,106,92,59,176,78,46,208,124,195,154,162,207,239,89,249,61,43,103,101,113,194,104,165,84,43,26,186,59,85,195,4,182,43,235,210,83,176,200,130,126,120,247,206,40,8,87,203,190,247,187,63,67,226,75,210,45,226,232,21,165,12,44,138,53,185,228,180,119,95,142,90,117,175,206,79,199,102,229,161,233,149,134,8,91,163,1,131,12,162,208,69,166,207,28,164,140,15,129,13,242,15,66,46,38,223,22,230,99,73,187,127,91,212,136,97,16,105,172,182,154,90,88,81,95,102,245,81,58,33,87,153,60,130,32,102,72,220,228,140,76,75,160,36,244,58,211,55,176,107,14,141,55,83,128,192,12,147,214,244,41,43,217,3,201,237,175,164,235,61,82,206,177,56,27,91,251,233,176,32,59,221,118,44,247,63,64,93,122,178,157,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e4af9b4d-936f-43e4-a7c4-6acbb3350321"));
		}

		#endregion

	}

	#endregion

}

