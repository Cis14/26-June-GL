namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotIntentTypeConstantsSchema

	/// <exclude/>
	public class CopilotIntentTypeConstantsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotIntentTypeConstantsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotIntentTypeConstantsSchema(CopilotIntentTypeConstantsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7");
			Name = "CopilotIntentTypeConstants";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,87,91,79,227,70,20,126,206,74,251,31,70,161,15,142,20,155,92,156,144,44,11,171,16,2,138,68,11,2,182,125,225,101,98,159,36,214,78,236,116,102,12,165,136,255,222,51,227,11,190,37,80,54,84,125,2,207,204,57,223,119,238,39,62,93,129,88,83,7,200,152,3,149,94,96,141,131,181,199,2,249,249,211,211,231,79,181,80,120,254,130,220,60,10,9,171,195,194,55,190,100,12,28,148,241,133,117,14,62,112,207,41,189,185,240,252,63,95,14,111,129,115,42,130,185,68,217,213,42,240,171,111,56,224,57,222,236,113,88,160,114,50,102,84,136,47,36,38,54,245,37,248,242,246,113,13,99,4,150,212,151,66,191,222,223,223,39,95,69,184,90,81,254,120,28,127,167,47,200,60,224,137,2,226,105,13,68,162,10,97,37,146,251,25,209,117,56,99,158,67,80,84,226,31,71,193,111,69,175,61,105,6,41,225,51,15,152,139,140,175,180,158,232,174,72,79,31,92,113,79,125,147,239,83,226,4,12,41,42,154,114,9,248,18,128,56,28,230,71,245,18,176,117,243,195,99,172,190,127,156,53,196,74,49,178,134,20,44,193,8,187,129,207,30,241,155,235,24,41,69,145,45,10,252,136,212,247,14,6,246,96,50,169,31,238,158,180,206,135,157,176,214,154,242,180,59,157,209,184,109,127,0,237,209,2,255,219,5,107,173,40,79,122,210,63,152,116,58,31,64,250,143,128,255,152,179,224,97,103,228,115,10,243,70,180,90,163,211,179,179,173,70,76,93,148,242,230,30,112,18,204,255,139,244,62,15,61,55,74,238,23,181,83,23,233,170,11,235,138,114,1,70,189,127,58,180,91,39,7,61,179,211,30,15,76,123,52,108,153,131,225,232,196,28,14,48,44,253,137,61,106,217,189,122,99,151,118,253,124,5,68,134,105,61,91,44,235,246,206,186,39,125,219,54,237,179,81,215,180,79,219,19,115,48,233,119,204,222,184,123,118,58,182,79,187,147,94,103,167,150,253,124,158,105,195,180,154,45,118,209,193,124,6,157,94,215,156,65,123,102,218,224,216,230,204,233,13,204,126,111,54,104,217,7,173,118,215,165,59,181,107,119,117,164,237,203,169,219,98,103,123,216,159,209,97,187,99,30,216,29,199,180,91,115,199,28,118,232,208,236,2,204,96,216,234,180,15,250,47,241,219,3,223,141,198,78,126,6,253,10,114,25,232,33,196,189,123,42,33,186,93,71,31,9,191,139,192,161,204,251,155,206,24,220,68,149,126,14,217,17,71,215,106,186,27,223,5,112,28,119,126,52,236,73,152,251,108,38,77,130,131,8,66,238,192,20,179,243,55,92,43,26,68,109,16,181,90,225,26,141,253,165,94,2,22,214,83,81,254,217,250,157,178,16,84,107,65,45,211,235,248,250,70,6,156,46,32,213,151,124,31,21,120,233,232,233,213,198,42,136,70,10,57,200,144,251,196,135,135,178,27,140,130,242,38,241,145,80,48,55,54,111,1,141,102,202,168,161,1,158,223,26,159,215,150,132,107,77,84,16,74,88,196,19,92,226,68,145,73,199,129,88,131,163,242,218,173,90,114,54,164,170,62,89,83,78,87,218,186,163,122,222,127,245,99,114,139,154,157,144,115,165,72,93,226,12,202,38,129,75,100,160,141,102,247,144,225,150,184,65,88,8,168,245,87,195,121,169,19,99,168,12,101,242,176,12,4,164,102,138,101,16,50,151,204,84,212,149,51,192,173,208,29,93,137,244,96,148,45,238,82,136,85,57,203,37,149,168,113,141,140,65,45,137,202,147,155,125,92,237,209,44,106,190,248,119,82,92,58,85,107,165,180,35,47,206,75,203,236,193,147,206,146,24,229,155,154,67,209,151,27,6,237,151,232,73,82,14,149,20,139,5,95,47,204,214,248,93,61,74,252,205,120,122,112,189,15,176,48,243,222,136,168,59,237,187,0,11,61,250,141,120,185,14,255,46,220,13,51,162,136,239,194,156,134,44,133,144,75,30,60,232,86,54,226,139,112,133,66,151,161,188,156,95,83,127,1,147,191,28,136,192,226,30,150,73,144,102,38,143,176,199,133,140,197,0,207,153,14,182,181,41,233,194,120,153,170,119,198,212,189,107,36,195,117,39,93,41,219,38,170,187,68,6,191,162,81,108,236,19,185,254,160,230,111,218,18,82,133,162,96,70,150,126,190,244,181,102,72,92,29,43,221,18,140,122,42,117,171,98,231,163,41,224,199,134,163,108,217,118,181,125,8,226,163,15,41,17,225,122,29,112,137,132,182,173,47,74,228,94,77,208,140,167,83,134,21,253,74,239,40,185,44,157,186,229,129,247,97,157,167,180,175,191,163,153,148,87,227,127,223,31,74,219,217,251,75,126,227,194,247,127,171,98,170,55,213,59,83,239,170,204,19,82,85,48,101,44,202,31,65,144,168,231,235,252,127,45,227,182,85,117,105,64,231,180,225,130,71,221,75,36,112,129,248,79,183,207,42,125,113,223,144,212,243,213,236,84,108,222,148,237,226,77,195,57,135,246,181,164,237,88,21,194,136,177,210,133,48,146,220,158,78,124,140,17,87,227,189,74,62,246,220,17,81,207,44,212,166,151,89,97,168,222,81,181,72,54,26,214,152,86,82,49,26,185,133,53,54,242,54,80,204,141,134,53,18,137,41,70,110,241,124,37,224,241,86,238,4,46,236,118,139,212,252,202,173,26,87,197,5,72,13,147,96,110,110,205,47,189,17,8,160,255,200,10,86,51,108,239,10,65,179,45,54,202,8,84,245,72,204,219,123,234,49,21,150,67,18,32,28,127,240,4,52,243,84,203,130,119,232,208,104,75,187,51,238,26,111,74,33,145,46,117,99,52,168,162,83,106,221,73,186,196,209,75,178,65,253,192,217,156,11,205,68,246,219,183,184,133,39,236,140,109,63,46,162,211,252,33,158,253,3,29,116,156,211,228,21,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateWorkflowAgentIntentTypeCaptionLocalizableString());
			LocalizableStrings.Add(CreateSystemIntentTypeCaptionLocalizableString());
			LocalizableStrings.Add(CreateAgentIntentTypeCaptionLocalizableString());
			LocalizableStrings.Add(CreateSkillIntentTypeCaptionLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateWorkflowAgentIntentTypeCaptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("199b0e5d-cf24-5048-4a82-14d0a5fd691e"),
				Name = "WorkflowAgentIntentTypeCaption",
				CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0"),
				CreatedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7"),
				ModifiedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateSystemIntentTypeCaptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("cff97fc0-cde5-95ab-3c15-601a485543f2"),
				Name = "SystemIntentTypeCaption",
				CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0"),
				CreatedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7"),
				ModifiedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateAgentIntentTypeCaptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("da89b7e1-fe1b-9f12-d702-be603a29c20d"),
				Name = "AgentIntentTypeCaption",
				CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0"),
				CreatedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7"),
				ModifiedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateSkillIntentTypeCaptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("dcec38f5-fb3d-68e4-5e1c-d00fcd060550"),
				Name = "SkillIntentTypeCaption",
				CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0"),
				CreatedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7"),
				ModifiedInSchemaUId = new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("4fc5cf1b-030d-4ac8-8a0f-ca9dea2544c7"));
		}

		#endregion

	}

	#endregion

}

