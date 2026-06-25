namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ApplyTranslationActivityTrackerSchema

	/// <exclude/>
	public class ApplyTranslationActivityTrackerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ApplyTranslationActivityTrackerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ApplyTranslationActivityTrackerSchema(ApplyTranslationActivityTrackerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9958eab8-fa96-46ca-bad4-d031c0f0794b");
			Name = "ApplyTranslationActivityTracker";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2f244451-ec5e-494f-9790-8d930a80007c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,82,239,107,219,48,16,253,236,66,255,135,35,133,226,64,39,10,251,182,108,140,144,146,173,236,7,131,56,251,50,70,80,228,139,171,85,150,140,36,39,132,146,255,125,39,217,113,237,116,89,191,200,214,233,238,189,187,247,78,243,18,93,197,5,66,134,214,114,103,54,158,205,140,222,200,162,182,220,75,163,89,102,185,118,42,254,95,94,60,93,94,36,181,147,186,128,197,222,121,44,39,221,125,102,49,228,176,57,125,106,139,153,41,10,69,241,231,132,62,190,69,138,211,203,149,197,130,112,97,166,184,115,239,96,90,85,106,223,227,155,10,47,183,210,135,144,120,68,27,75,170,122,173,164,0,17,42,94,47,72,66,195,207,52,70,59,207,181,39,170,31,86,110,185,199,8,217,97,134,103,112,222,134,118,35,244,2,157,163,194,47,184,207,176,172,136,3,225,3,140,78,89,221,234,233,246,48,106,38,74,174,80,231,13,93,123,111,185,231,18,85,254,130,184,185,0,105,151,27,173,246,96,214,127,80,120,88,241,30,251,178,202,41,231,171,17,143,68,174,113,215,38,165,227,73,31,226,142,142,76,150,248,17,86,36,141,159,190,0,88,122,241,74,139,223,208,63,152,216,99,212,99,160,205,214,200,28,26,160,62,116,20,60,242,166,75,135,150,4,214,212,90,0,171,7,215,155,227,100,253,193,238,243,49,68,127,18,185,129,180,93,28,199,62,161,191,119,119,210,241,181,194,60,29,17,238,169,224,125,242,163,219,228,217,104,124,196,75,44,18,150,142,2,37,135,120,110,185,5,109,118,36,225,81,41,70,138,124,55,187,73,247,204,135,150,83,106,179,11,108,110,108,201,125,122,102,37,110,78,135,106,16,85,48,44,61,227,100,215,104,152,252,188,97,236,51,119,63,185,170,17,174,175,33,13,237,191,249,143,189,44,166,142,89,102,60,87,11,164,117,206,29,188,135,183,183,29,219,64,151,86,152,100,232,20,11,208,82,68,157,103,92,60,224,175,19,89,126,135,45,60,202,150,156,239,166,159,22,153,14,255,220,190,38,58,12,30,254,2,187,71,107,237,150,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9958eab8-fa96-46ca-bad4-d031c0f0794b"));
		}

		#endregion

	}

	#endregion

}

