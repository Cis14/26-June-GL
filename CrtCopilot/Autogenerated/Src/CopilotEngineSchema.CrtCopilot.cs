namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotEngineSchema

	/// <exclude/>
	public class CopilotEngineSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotEngineSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotEngineSchema(CopilotEngineSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7");
			Name = "CopilotEngine";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,89,91,79,27,57,20,126,78,165,254,7,55,43,85,19,41,26,222,129,4,5,10,109,36,88,80,147,106,31,186,21,50,51,78,98,225,216,89,219,1,178,109,255,251,30,223,38,227,185,145,238,238,11,147,177,207,253,124,231,248,120,224,120,77,212,6,103,4,93,72,130,53,21,233,133,216,80,38,244,219,55,223,223,190,233,109,21,229,75,52,219,41,77,214,39,149,119,160,100,140,100,192,195,85,250,145,112,34,105,86,163,185,166,252,175,218,226,124,5,186,114,88,104,223,73,231,88,61,170,253,126,197,186,116,226,244,214,9,174,224,177,149,100,46,150,75,22,105,88,50,241,128,217,241,241,133,88,175,5,79,175,129,32,218,159,19,41,177,18,11,157,122,138,6,43,203,52,146,180,173,167,87,56,211,66,82,162,218,165,195,14,236,125,253,64,22,120,203,244,57,229,70,81,162,119,27,34,22,201,212,123,121,201,193,68,50,24,124,3,82,202,53,145,28,51,148,49,172,20,138,40,208,49,138,89,128,254,187,85,208,251,77,146,37,4,10,232,185,210,152,107,117,140,238,36,125,194,154,184,253,141,123,65,153,217,71,74,75,27,75,204,63,111,185,151,120,187,33,210,68,150,163,17,234,251,29,23,235,201,180,127,114,152,140,201,134,118,137,129,237,254,137,183,151,240,220,153,28,219,127,69,9,203,219,140,55,121,18,156,237,208,20,178,138,238,25,252,25,33,248,121,131,57,94,18,9,224,212,38,221,68,38,125,111,80,127,112,210,40,224,139,34,18,66,197,29,172,209,253,54,122,111,230,9,161,159,17,165,128,200,43,69,247,42,122,239,230,189,81,203,139,21,6,61,108,6,1,48,220,89,243,70,183,24,8,228,236,145,50,118,249,66,178,45,64,16,221,227,202,74,55,63,168,210,123,222,172,244,246,74,122,44,188,228,214,192,222,36,105,251,192,104,230,40,142,142,142,208,169,218,174,215,88,238,198,97,97,202,169,166,152,209,191,137,66,24,113,242,140,168,133,39,180,33,177,64,122,69,128,133,0,160,36,89,140,250,17,178,251,71,133,144,211,163,170,216,211,13,150,120,141,56,244,180,81,63,78,93,127,108,82,107,32,234,23,210,211,35,75,221,204,156,53,165,180,63,246,166,32,159,90,180,246,0,59,68,86,53,147,123,105,208,129,21,136,1,169,102,189,16,246,103,163,180,106,66,247,98,96,7,41,179,133,136,223,123,197,174,82,122,247,82,204,106,179,128,141,205,106,220,121,146,74,193,196,65,31,182,213,70,99,120,135,70,73,175,215,90,18,45,113,28,182,163,191,26,171,138,134,8,237,229,104,12,208,119,75,89,41,127,104,42,13,253,160,87,41,116,160,106,116,207,19,183,120,177,231,106,172,248,94,173,144,129,161,177,182,123,81,217,26,177,113,21,247,122,63,187,75,249,78,10,104,213,26,78,47,56,85,252,161,19,183,219,169,89,230,122,150,173,200,26,207,136,124,162,80,183,247,180,190,120,210,209,2,160,39,43,4,6,42,243,52,21,95,238,0,165,234,111,82,6,77,32,237,232,2,79,152,109,73,241,58,143,187,73,139,188,189,122,72,113,142,180,64,246,192,133,131,28,61,83,189,242,133,165,44,23,216,108,217,74,54,236,85,22,231,116,99,148,154,214,28,214,150,68,251,95,61,186,64,73,83,56,209,187,17,226,91,198,2,60,123,61,73,96,214,225,109,177,55,20,63,221,163,131,16,32,82,193,185,57,47,27,12,77,220,153,233,69,66,222,208,104,220,38,209,198,227,16,180,221,16,189,18,173,7,59,164,68,67,203,153,94,242,237,26,146,241,192,200,169,27,47,198,48,16,48,8,180,25,3,159,136,51,86,37,110,239,235,55,219,224,84,168,246,18,179,47,252,178,115,99,132,203,50,66,108,63,97,181,154,17,93,168,195,140,149,117,253,110,20,152,18,44,243,166,51,98,230,225,196,133,196,68,199,253,74,13,117,58,23,215,226,25,70,144,193,0,126,122,241,33,164,62,61,214,236,244,143,21,145,36,49,191,141,136,38,197,48,67,114,141,1,177,150,170,36,121,80,14,121,8,226,131,16,172,54,142,37,193,81,3,54,63,51,43,155,120,245,129,42,19,172,252,20,102,250,201,180,216,187,228,102,117,6,69,146,99,38,56,1,33,99,80,25,176,232,93,88,96,166,72,25,39,1,121,21,136,125,56,159,65,75,146,84,239,220,65,98,84,131,141,174,81,145,98,88,76,90,199,200,78,87,225,84,42,245,248,194,215,255,211,150,110,67,94,5,221,21,76,252,182,121,171,43,33,173,145,31,183,52,63,131,147,55,99,219,220,39,123,154,7,203,95,149,71,225,246,100,0,217,80,183,233,94,87,50,112,201,242,24,219,3,245,157,71,234,57,89,225,39,10,7,63,208,111,188,101,30,163,6,40,21,235,106,13,41,88,97,159,94,201,139,145,255,146,126,113,228,85,255,26,160,98,153,255,123,80,203,32,247,146,95,11,78,45,44,175,70,229,224,238,86,157,136,41,7,85,84,231,34,59,106,24,174,140,130,59,44,53,50,35,128,25,177,110,220,136,232,59,156,25,99,141,97,67,228,64,19,207,27,16,104,151,22,223,0,131,76,195,243,162,3,113,120,245,164,168,24,212,120,238,149,221,110,236,21,27,9,255,140,115,29,106,169,60,94,164,134,219,91,145,20,54,86,173,27,86,76,24,6,13,81,72,187,162,244,36,104,14,6,175,55,140,104,226,190,4,216,242,105,80,229,3,134,45,209,212,159,241,128,197,56,54,78,134,115,3,126,124,38,10,46,229,112,61,50,143,224,112,60,201,22,87,128,81,245,162,103,17,117,190,155,230,73,213,154,82,33,121,150,179,20,250,169,182,7,123,44,222,46,167,174,241,87,90,108,185,98,108,191,91,97,117,71,236,247,131,137,218,241,204,103,15,12,243,74,82,191,162,210,9,223,37,107,192,181,19,183,78,167,17,163,15,228,0,189,127,143,214,112,168,8,118,1,135,143,65,211,168,30,191,146,47,205,250,11,171,107,147,107,90,201,156,55,115,216,144,164,144,130,147,182,0,148,98,9,222,204,37,230,138,2,232,58,66,166,229,46,108,122,108,56,29,23,194,87,189,127,183,153,217,42,227,124,51,72,220,126,225,76,238,71,178,179,192,15,32,218,0,168,9,58,59,67,253,219,199,190,223,63,14,251,151,82,138,80,215,150,230,11,127,228,226,153,35,98,54,144,200,224,72,146,36,239,123,223,175,169,210,161,235,121,174,49,140,169,130,133,236,150,18,110,63,172,144,144,193,64,144,180,198,215,251,30,194,28,215,244,39,56,240,89,33,76,21,254,78,76,169,239,203,189,108,202,48,88,18,218,59,202,176,206,86,40,185,124,201,136,173,117,84,194,7,19,75,23,140,132,4,11,244,74,138,231,192,187,160,48,78,179,34,107,109,55,168,114,243,129,59,204,18,92,83,73,92,85,97,217,7,40,185,175,94,87,3,20,125,174,154,185,109,109,194,233,134,169,6,248,192,113,80,106,208,131,66,70,106,86,227,3,238,128,206,134,77,5,33,243,237,51,62,225,76,232,93,87,26,35,63,151,184,13,91,114,73,141,22,2,190,239,255,38,219,140,217,97,101,46,30,9,7,216,152,191,35,148,187,175,143,149,150,142,159,193,179,250,151,163,212,235,117,26,173,124,39,232,224,182,125,136,115,1,94,190,204,248,178,236,166,237,241,170,104,238,191,232,222,19,150,29,93,27,102,62,219,180,85,165,91,191,18,148,78,123,139,206,86,143,211,215,219,7,37,12,227,183,82,128,166,182,200,195,149,3,44,154,60,97,202,204,188,19,110,56,81,220,110,68,78,208,26,254,12,145,253,74,163,80,124,1,170,228,181,44,207,79,60,142,217,17,31,154,197,118,35,189,208,127,101,163,105,228,214,156,82,195,221,75,72,97,152,51,199,210,187,250,53,102,128,126,252,240,229,218,193,111,6,170,32,160,122,57,168,221,97,204,247,200,178,147,241,197,247,215,238,147,128,180,110,175,206,156,234,134,209,245,184,97,199,90,108,199,48,103,82,156,12,28,195,37,92,82,221,181,210,28,254,131,189,186,250,237,217,93,154,43,215,97,115,185,4,21,123,115,14,191,241,238,121,227,74,106,48,242,80,236,181,180,141,184,37,182,116,195,74,53,216,10,189,219,174,55,41,0,42,1,255,70,141,157,213,245,185,90,143,73,127,23,230,31,51,29,119,0,187,10,127,254,1,234,162,141,10,97,27,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateIntentNotFoundLocalizableString());
			LocalizableStrings.Add(CreateInactiveIntentLocalizableString());
			LocalizableStrings.Add(CreateStandaloneApiFeatureOffLocalizableString());
			LocalizableStrings.Add(CreateWrongIntentModeLocalizableString());
			LocalizableStrings.Add(CreateParsingFailedLocalizableString());
			LocalizableStrings.Add(CreateNoSkillReadRightLocalizableString());
			LocalizableStrings.Add(CreateNoIntentExecutionRightLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateIntentNotFoundLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("7e865c44-d539-1156-d3e0-924636cf6459"),
				Name = "IntentNotFound",
				CreatedInPackageId = new Guid("a5645ea0-1b73-4b60-b012-23b3100050d4"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateInactiveIntentLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("32a470be-2e3d-fc8c-a610-ee23d13cf1d4"),
				Name = "InactiveIntent",
				CreatedInPackageId = new Guid("a5645ea0-1b73-4b60-b012-23b3100050d4"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateStandaloneApiFeatureOffLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("9edecedf-57ed-99ca-eb3f-a3f024898430"),
				Name = "StandaloneApiFeatureOff",
				CreatedInPackageId = new Guid("a5645ea0-1b73-4b60-b012-23b3100050d4"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateWrongIntentModeLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("bdcdf5ed-41f4-f317-f502-20920066ae55"),
				Name = "WrongIntentMode",
				CreatedInPackageId = new Guid("a5645ea0-1b73-4b60-b012-23b3100050d4"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateParsingFailedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("b8f7df9f-a835-f108-b054-baf83b2f3bc8"),
				Name = "ParsingFailed",
				CreatedInPackageId = new Guid("a5645ea0-1b73-4b60-b012-23b3100050d4"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateNoSkillReadRightLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("c5e0187d-926c-f2f2-aec5-9f296144704b"),
				Name = "NoSkillReadRight",
				CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateNoIntentExecutionRightLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("37173347-9ccd-c332-50c0-c70f27b6f22e"),
				Name = "NoIntentExecutionRight",
				CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358"),
				CreatedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"),
				ModifiedInSchemaUId = new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("db5a80f6-ea66-4f1e-9a0c-14f8f087d1e7"));
		}

		#endregion

	}

	#endregion

}

