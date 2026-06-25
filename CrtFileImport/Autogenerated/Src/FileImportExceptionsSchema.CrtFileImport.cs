namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportExceptionsSchema

	/// <exclude/>
	public class FileImportExceptionsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportExceptionsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportExceptionsSchema(FileImportExceptionsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d8637c44-884d-415b-8ad0-91caafbca065");
			Name = "FileImportExceptions";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("52abf94a-4a51-4e9b-afae-86480a04ff1e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,82,77,107,2,49,16,61,43,248,31,134,237,69,161,236,222,173,245,98,45,45,244,139,42,189,143,235,172,6,54,201,146,201,98,109,241,191,119,54,174,159,221,67,161,148,158,66,102,222,123,243,50,121,6,53,113,129,41,193,148,156,67,182,153,143,71,214,100,106,81,58,244,202,154,248,86,229,116,175,11,235,124,167,253,217,105,183,74,86,102,1,147,53,123,210,87,251,251,49,91,107,107,164,35,189,11,71,11,209,128,81,142,204,125,24,235,194,175,239,8,231,228,198,239,41,21,149,126,192,37,73,2,3,46,181,70,183,30,214,247,233,146,128,118,40,240,75,244,160,88,78,103,87,6,86,75,50,144,137,51,152,91,98,48,214,67,106,141,71,101,0,97,25,38,196,59,221,228,72,184,40,103,185,74,33,173,252,52,218,129,62,60,149,121,254,236,66,243,200,101,235,51,56,61,60,201,26,246,174,76,189,117,242,178,151,160,187,69,156,63,38,20,70,142,208,139,83,37,44,52,178,110,155,129,95,23,36,72,34,72,29,101,215,81,147,159,40,25,198,123,209,228,92,117,80,160,67,13,70,62,241,58,98,177,130,11,138,134,175,196,182,116,50,163,174,196,131,36,224,2,173,222,64,211,172,238,253,142,57,217,18,119,2,189,138,216,234,195,12,153,186,134,86,240,96,83,204,213,7,206,114,129,58,249,255,110,141,188,132,232,144,151,189,48,71,82,255,198,225,184,201,196,35,49,87,158,223,48,47,41,234,245,160,202,92,107,83,47,159,204,124,187,255,112,223,86,207,138,77,153,187,65,143,127,151,184,185,168,255,48,109,39,70,254,61,107,39,110,254,52,105,39,147,254,39,103,39,22,126,155,178,205,23,54,101,250,130,56,5,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateEmptyHeaderExceptionMessageLocalizableString());
			LocalizableStrings.Add(CreateEmptyDataExceptionMessageLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateEmptyHeaderExceptionMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("e8ba9ed7-7f74-43c3-9ac9-e0f6b8e886fc"),
				Name = "EmptyHeaderExceptionMessage",
				CreatedInPackageId = new Guid("52abf94a-4a51-4e9b-afae-86480a04ff1e"),
				CreatedInSchemaUId = new Guid("d8637c44-884d-415b-8ad0-91caafbca065"),
				ModifiedInSchemaUId = new Guid("d8637c44-884d-415b-8ad0-91caafbca065")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateEmptyDataExceptionMessageLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("1d2b6f72-8681-4848-a93e-39afafa9f2f2"),
				Name = "EmptyDataExceptionMessage",
				CreatedInPackageId = new Guid("52abf94a-4a51-4e9b-afae-86480a04ff1e"),
				CreatedInSchemaUId = new Guid("d8637c44-884d-415b-8ad0-91caafbca065"),
				ModifiedInSchemaUId = new Guid("d8637c44-884d-415b-8ad0-91caafbca065")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d8637c44-884d-415b-8ad0-91caafbca065"));
		}

		#endregion

	}

	#endregion

}

