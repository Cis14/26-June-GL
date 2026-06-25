namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ExceptionsSchema

	/// <exclude/>
	public class ExceptionsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ExceptionsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ExceptionsSchema(ExceptionsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0c2eb97d-52a5-4c3d-8c66-7c7e74fc11ec");
			Name = "Exceptions";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("66e9e705-64b4-4dda-925e-d1e05a389eb6");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,81,219,106,195,48,12,125,78,97,255,96,146,151,20,66,186,13,198,88,247,52,74,11,131,221,104,119,121,24,123,112,92,53,120,115,236,96,217,236,198,254,125,178,211,102,45,107,48,4,29,233,232,28,73,154,55,128,45,23,192,238,193,90,142,102,229,202,137,209,43,89,123,203,157,52,250,96,240,125,48,72,60,74,93,179,197,39,58,104,206,251,120,155,210,52,70,239,207,88,32,156,50,207,11,176,146,43,249,197,43,5,47,4,180,190,82,82,48,161,56,34,187,164,206,55,198,205,140,215,203,233,135,128,54,136,179,49,187,104,91,42,138,86,122,152,184,193,84,146,89,168,67,21,25,70,103,189,112,198,226,152,221,197,182,81,50,25,141,178,44,43,88,214,125,155,255,78,196,50,246,100,236,91,183,132,201,252,232,244,248,228,240,44,112,215,246,246,26,203,31,16,44,201,106,16,209,167,223,9,11,70,118,194,26,100,224,210,134,123,192,84,175,84,18,160,97,144,72,198,172,226,8,121,151,45,103,198,54,220,229,26,222,217,149,17,155,85,45,98,50,223,149,40,123,207,229,28,208,120,43,168,206,88,94,67,17,27,39,105,239,21,211,130,165,255,250,97,185,61,216,53,32,18,119,170,160,1,237,110,87,19,163,212,90,232,145,43,15,233,112,221,246,111,162,173,81,134,44,158,227,167,43,137,151,1,189,236,142,67,17,225,244,126,1,22,244,186,58,106,2,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateItemNotFoundMessageElementOfCollectionLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateItemNotFoundMessageElementOfCollectionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("ea6e1d20-4f48-49c8-b0b9-9625dfbcff06"),
				Name = "ItemNotFoundMessageElementOfCollection",
				CreatedInPackageId = new Guid("66e9e705-64b4-4dda-925e-d1e05a389eb6"),
				CreatedInSchemaUId = new Guid("0c2eb97d-52a5-4c3d-8c66-7c7e74fc11ec"),
				ModifiedInSchemaUId = new Guid("0c2eb97d-52a5-4c3d-8c66-7c7e74fc11ec")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0c2eb97d-52a5-4c3d-8c66-7c7e74fc11ec"));
		}

		#endregion

	}

	#endregion

}

