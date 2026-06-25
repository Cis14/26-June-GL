namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportExceededAvailableKeyColumnsLimitExceptionSchema

	/// <exclude/>
	public class FileImportExceededAvailableKeyColumnsLimitExceptionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportExceededAvailableKeyColumnsLimitExceptionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportExceededAvailableKeyColumnsLimitExceptionSchema(FileImportExceededAvailableKeyColumnsLimitExceptionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d");
			Name = "FileImportExceededAvailableKeyColumnsLimitException";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,82,91,107,194,48,20,126,174,224,127,56,116,123,168,32,237,187,142,129,148,9,50,25,99,142,189,199,246,180,134,229,82,114,153,118,226,127,95,154,84,173,236,205,167,228,36,231,124,183,68,16,142,186,33,5,194,39,42,69,180,172,76,154,75,81,209,218,42,98,168,20,233,146,50,92,241,70,42,3,199,241,104,60,138,172,166,162,134,77,171,13,242,249,165,30,142,115,46,197,220,247,62,40,172,29,8,228,140,104,61,131,43,214,203,161,64,44,177,92,252,16,202,200,150,225,43,182,185,100,150,11,189,166,156,250,251,166,227,247,48,89,150,193,147,182,156,19,213,62,247,245,165,3,168,6,179,83,114,47,96,191,67,225,246,8,156,28,40,183,28,88,135,5,178,2,114,230,129,111,108,161,8,76,221,36,246,66,160,180,170,243,81,57,137,64,131,95,217,96,8,65,167,103,17,217,64,69,99,183,140,22,80,116,222,238,177,6,51,88,52,141,131,240,28,3,199,81,8,250,154,158,83,96,148,45,140,84,46,196,119,79,27,58,122,9,119,144,39,171,15,212,210,170,2,55,14,150,212,8,58,172,83,183,241,73,184,231,109,24,49,248,230,190,200,180,35,139,168,48,125,116,185,180,194,76,102,176,37,26,147,208,159,46,165,226,196,36,2,247,176,150,5,97,244,183,227,223,248,203,228,12,238,113,226,59,244,198,97,244,49,254,135,173,211,227,80,234,41,253,34,204,98,60,153,222,104,157,116,191,55,138,78,125,178,40,202,16,174,175,195,233,237,225,233,15,64,70,145,26,27,3,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateErrorMessageTemplateForTypedColumnsLocalizableString());
			LocalizableStrings.Add(CreateErrorMessageTemplateForTextColumnsLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateErrorMessageTemplateForTypedColumnsLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("f54bb6d5-7423-9f20-9efd-02634cb9b91c"),
				Name = "ErrorMessageTemplateForTypedColumns",
				CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab"),
				CreatedInSchemaUId = new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d"),
				ModifiedInSchemaUId = new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateErrorMessageTemplateForTextColumnsLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("fbf8a93f-3b25-7e14-f58f-689472d1727b"),
				Name = "ErrorMessageTemplateForTextColumns",
				CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab"),
				CreatedInSchemaUId = new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d"),
				ModifiedInSchemaUId = new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7d5da20b-35dc-492e-8f0d-9ec207d9a43d"));
		}

		#endregion

	}

	#endregion

}

