namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotSessionProgressSchema

	/// <exclude/>
	public class CopilotSessionProgressSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotSessionProgressSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotSessionProgressSchema(CopilotSessionProgressSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450");
			Name = "CopilotSessionProgress";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,86,109,79,219,48,16,254,92,36,254,131,233,38,45,149,170,240,157,174,171,182,50,88,39,138,16,133,237,3,226,131,155,92,139,167,196,206,108,7,196,170,254,247,93,108,167,228,197,80,40,18,2,114,231,187,123,238,185,23,155,211,20,84,70,35,32,99,9,84,51,17,142,69,198,18,161,247,247,86,251,123,157,92,49,190,36,179,71,165,33,29,52,190,195,51,198,255,182,132,151,57,215,44,133,112,6,146,209,132,253,43,124,242,167,83,87,32,37,85,98,161,49,78,154,62,167,145,128,114,212,220,28,83,77,199,130,107,73,35,125,139,130,44,159,39,44,34,81,66,149,34,14,233,12,148,194,24,23,82,44,37,254,139,167,86,198,184,243,65,194,18,21,4,53,25,72,205,64,29,145,11,227,192,234,15,15,15,201,103,149,167,41,149,143,95,74,193,41,104,69,132,36,170,248,171,239,160,140,130,2,19,134,176,24,48,195,5,3,25,110,156,28,86,189,24,208,83,72,231,32,131,115,164,151,12,73,55,170,65,157,196,221,94,145,77,153,206,105,206,98,178,209,145,21,89,130,30,20,8,6,100,253,30,168,154,233,4,118,70,121,85,88,215,129,42,45,77,169,43,7,94,141,246,82,32,50,198,53,178,247,73,145,136,102,69,99,188,1,156,68,251,137,49,31,91,91,47,180,203,230,169,87,227,27,231,82,162,217,123,32,70,214,197,118,148,99,207,193,221,203,174,52,213,64,196,194,124,100,110,12,222,0,219,216,251,11,109,60,215,128,161,1,71,59,177,8,252,227,103,76,84,248,155,50,141,30,78,132,188,86,32,167,168,160,75,232,13,222,146,86,12,42,146,204,114,227,146,115,4,239,146,100,197,155,55,213,227,74,52,79,37,62,0,143,237,58,169,239,150,41,232,59,17,23,139,69,178,123,204,220,106,51,251,97,42,243,20,2,51,172,85,60,40,168,193,237,198,33,50,97,243,218,103,223,172,133,145,235,199,73,220,35,197,66,238,116,216,130,4,7,165,48,252,65,213,47,154,228,80,106,59,18,116,46,57,225,121,146,12,140,100,109,126,187,98,217,248,179,232,14,82,58,165,28,107,34,93,128,186,108,216,0,19,110,176,215,206,5,61,27,99,226,190,39,120,3,56,127,232,194,227,56,60,97,60,46,78,125,123,188,158,196,193,38,13,155,131,117,230,50,176,186,81,232,200,50,186,181,151,222,51,17,153,155,102,158,192,108,195,116,165,158,87,144,102,9,158,223,194,183,229,239,165,174,182,147,86,82,93,50,13,15,109,4,65,131,190,75,80,34,151,17,106,133,68,30,92,172,23,7,169,215,39,31,187,45,199,42,92,25,16,225,149,112,145,122,107,203,94,183,87,165,104,123,191,218,238,119,132,150,163,96,248,244,227,177,15,132,109,36,18,31,139,164,126,177,244,159,137,80,37,185,79,50,42,105,138,27,97,254,7,93,223,220,86,215,193,87,185,84,101,21,220,108,249,86,47,182,96,107,228,154,112,235,200,194,218,98,198,153,27,84,99,180,110,160,29,2,60,221,79,27,239,237,246,141,219,189,107,67,249,154,186,25,208,246,104,13,120,117,147,14,125,222,71,182,133,200,193,208,44,14,87,196,145,91,92,33,110,241,148,234,192,99,216,111,149,197,154,30,149,166,63,5,227,193,119,126,207,164,224,41,102,29,158,195,3,190,27,61,134,131,230,80,61,211,134,110,207,61,61,151,134,77,142,39,177,155,175,246,99,96,216,46,98,57,247,254,6,242,245,85,191,134,192,190,130,90,32,140,184,60,169,109,5,155,147,235,212,199,207,149,199,238,239,23,198,26,165,248,243,31,160,166,142,237,196,11,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateExecutingActionLocalizableString());
			LocalizableStrings.Add(CreateWaitingForAssistantMessageLocalizableString());
			LocalizableStrings.Add(CreateWaitingForUserMessageLocalizableString());
			LocalizableStrings.Add(CreateAgentSelectedLocalizableString());
			LocalizableStrings.Add(CreateSkillSelectedLocalizableString());
			LocalizableStrings.Add(CreateSessionStartedLocalizableString());
			LocalizableStrings.Add(CreateTitleUpdatedLocalizableString());
			LocalizableStrings.Add(CreateSkillMessageSummarizedLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateExecutingActionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("07f19ea2-e4a4-330b-afc6-63209cf7b939"),
				Name = "ExecutingAction",
				CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateWaitingForAssistantMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("6673cc57-96d6-8a62-a449-ecaf90a81acb"),
				Name = "WaitingForAssistantMessage",
				CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateWaitingForUserMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("2e0aa39a-7a19-f269-c454-5b4380e84bd2"),
				Name = "WaitingForUserMessage",
				CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateAgentSelectedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("7dd50df5-746e-82af-1114-5a4dc1d8fd71"),
				Name = "AgentSelected",
				CreatedInPackageId = new Guid("421a8d84-5f16-4efa-b563-3ab0d40eb264"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateSkillSelectedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("70deaa71-960f-65d8-a1ef-90a12e17c1d1"),
				Name = "SkillSelected",
				CreatedInPackageId = new Guid("421a8d84-5f16-4efa-b563-3ab0d40eb264"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateSessionStartedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("010d0b26-7b10-9243-e4fa-b9b88a8cc43e"),
				Name = "SessionStarted",
				CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateTitleUpdatedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("1bd7d953-a849-21f9-c0ae-681806e9b2ee"),
				Name = "TitleUpdated",
				CreatedInPackageId = new Guid("421a8d84-5f16-4efa-b563-3ab0d40eb264"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateSkillMessageSummarizedLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("c1b5a655-85c0-380c-7371-16aa8746a45c"),
				Name = "SkillMessageSummarized",
				CreatedInPackageId = new Guid("421a8d84-5f16-4efa-b563-3ab0d40eb264"),
				CreatedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"),
				ModifiedInSchemaUId = new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b0e8ee9b-f8b6-433f-85d3-3a0027859450"));
		}

		#endregion

	}

	#endregion

}

