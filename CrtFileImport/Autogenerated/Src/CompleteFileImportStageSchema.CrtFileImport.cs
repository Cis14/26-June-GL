namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CompleteFileImportStageSchema

	/// <exclude/>
	public class CompleteFileImportStageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CompleteFileImportStageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CompleteFileImportStageSchema(CompleteFileImportStageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f9010370-d507-4e89-b413-ae509e726a22");
			Name = "CompleteFileImportStage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,86,93,79,219,48,20,125,14,18,255,193,43,47,169,132,210,119,160,69,80,202,84,105,3,52,198,211,52,77,38,185,41,22,137,29,249,163,91,133,248,239,187,137,147,54,73,227,52,240,210,38,246,185,199,247,158,251,225,112,154,130,202,104,8,228,39,72,73,149,136,117,48,23,60,102,43,35,169,102,130,7,183,44,129,101,154,9,169,201,241,209,219,241,145,103,20,227,171,6,92,194,185,99,61,184,165,161,22,146,129,66,4,98,78,36,172,144,148,144,121,66,149,58,35,115,145,102,9,104,216,29,242,168,233,10,10,236,100,50,33,23,140,191,128,100,58,18,33,9,37,196,211,209,53,85,109,244,104,50,171,224,202,164,41,149,155,234,125,241,15,66,163,129,132,229,49,36,70,75,194,10,211,202,100,82,179,249,117,3,49,53,137,190,102,60,194,96,124,189,201,64,196,254,178,227,208,241,41,185,67,237,200,148,112,252,67,144,35,148,241,248,55,18,103,230,57,97,24,66,30,181,43,104,114,70,58,206,65,227,92,244,173,114,183,12,146,8,149,123,144,108,77,181,85,202,203,236,11,89,90,195,7,42,209,39,13,82,253,128,76,40,134,25,216,144,63,204,185,119,222,193,177,224,154,105,204,219,252,197,240,87,117,67,53,125,144,98,205,34,144,21,147,27,209,197,247,77,136,87,147,185,217,92,251,13,46,172,76,165,165,201,75,234,74,174,76,10,92,147,39,5,18,215,57,132,121,185,54,17,100,58,35,28,254,118,217,249,35,211,48,28,157,182,152,198,182,98,189,19,224,145,213,190,124,47,19,129,14,102,32,115,1,182,201,32,195,179,209,179,133,62,247,228,138,92,94,230,103,120,158,223,7,154,218,6,179,221,183,9,190,130,190,232,241,102,230,247,137,56,174,148,24,94,33,7,1,187,24,123,64,237,72,251,248,156,241,186,141,62,23,181,179,142,15,108,239,34,118,66,218,241,186,185,156,209,186,76,6,198,234,170,245,26,60,175,246,98,152,149,234,216,193,230,24,105,173,83,73,179,229,78,109,184,203,185,72,76,202,213,213,106,133,199,209,220,165,136,102,88,161,56,182,139,13,12,34,4,165,132,28,91,131,51,242,140,115,210,111,145,237,163,201,27,121,255,64,15,215,162,250,224,213,83,169,32,214,120,249,161,220,164,133,82,11,110,82,82,60,46,163,188,18,186,246,3,135,134,7,82,243,29,244,139,112,94,8,107,193,162,109,114,44,169,223,158,3,36,219,62,162,100,133,196,119,66,179,120,115,207,43,75,191,6,41,6,178,119,3,59,198,123,212,176,248,86,160,73,94,115,123,224,247,14,159,246,78,24,224,21,139,137,255,101,183,28,220,1,68,143,40,139,229,170,80,158,4,109,36,183,110,190,23,191,107,42,203,75,191,128,178,222,22,170,32,51,223,210,121,159,190,64,78,15,17,180,7,56,82,212,162,46,149,110,58,30,216,96,23,60,242,171,166,237,146,183,47,61,3,148,174,137,92,205,207,252,67,199,36,137,245,201,125,149,4,79,89,68,187,10,166,199,196,58,155,151,125,205,44,40,91,0,91,25,93,95,70,13,26,247,80,47,201,134,18,185,230,229,80,154,3,243,165,214,156,66,99,85,64,244,201,249,82,153,247,143,152,75,178,228,232,43,38,186,28,130,67,114,221,154,14,123,137,179,237,180,75,126,71,200,229,90,83,5,92,251,15,71,199,103,8,93,12,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateCompleteRemindingSubjectLocalizableString());
			LocalizableStrings.Add(CreateNotImportedRowsCountMessageTemplateLocalizableString());
			LocalizableStrings.Add(CreateCompleteRemindingDescriptionTemplateLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateCompleteRemindingSubjectLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("6f65dc87-b8f6-49d8-8bf6-3ac2dbbdbbb6"),
				Name = "CompleteRemindingSubject",
				CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab"),
				CreatedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22"),
				ModifiedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateNotImportedRowsCountMessageTemplateLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("844066a9-4ae1-4da8-a4c2-43d603e52b7d"),
				Name = "NotImportedRowsCountMessageTemplate",
				CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab"),
				CreatedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22"),
				ModifiedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateCompleteRemindingDescriptionTemplateLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("a5398abd-87e5-4737-9e25-5cb7b840b6bb"),
				Name = "CompleteRemindingDescriptionTemplate",
				CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab"),
				CreatedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22"),
				ModifiedInSchemaUId = new Guid("f9010370-d507-4e89-b413-ae509e726a22")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f9010370-d507-4e89-b413-ae509e726a22"));
		}

		#endregion

	}

	#endregion

}

