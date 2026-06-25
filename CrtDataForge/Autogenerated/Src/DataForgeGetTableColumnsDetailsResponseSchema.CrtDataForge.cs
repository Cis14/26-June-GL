namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DataForgeGetTableColumnsDetailsResponseSchema

	/// <exclude/>
	public class DataForgeGetTableColumnsDetailsResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DataForgeGetTableColumnsDetailsResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DataForgeGetTableColumnsDetailsResponseSchema(DataForgeGetTableColumnsDetailsResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("766723ae-a677-497c-aea4-6134961b633b");
			Name = "DataForgeGetTableColumnsDetailsResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2640c8d2-a399-43bb-a3cd-67ad9ad998b6");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,83,203,78,131,64,20,93,219,164,255,48,169,27,77,12,31,208,170,11,65,187,177,46,90,226,198,184,184,12,23,50,201,48,131,243,48,169,77,255,221,25,160,180,52,197,148,186,33,97,238,121,220,115,6,4,20,168,75,160,72,98,84,10,180,204,76,16,74,145,177,220,42,48,76,138,32,2,3,47,82,229,56,30,109,198,163,43,171,153,200,201,106,173,13,22,14,201,57,82,15,211,193,28,5,42,70,103,199,152,165,21,134,21,24,172,220,20,56,251,169,84,247,168,67,91,85,161,190,25,197,133,76,145,187,61,140,2,106,28,216,193,175,21,230,142,73,66,14,90,79,73,187,214,28,77,12,9,71,183,139,45,132,142,208,0,227,122,233,82,185,173,176,162,126,120,240,78,237,211,29,148,54,225,140,18,234,165,206,85,34,83,242,4,26,247,194,87,190,142,90,123,129,69,130,234,230,205,149,73,30,200,36,117,71,147,91,111,180,115,58,161,235,121,149,55,217,144,28,205,140,104,255,216,58,210,182,206,139,34,173,35,159,138,223,35,120,70,220,94,102,127,30,227,41,254,165,27,74,27,85,221,224,110,122,28,164,79,142,214,230,93,177,87,166,205,253,193,110,205,106,143,164,89,245,191,45,53,122,195,10,106,73,253,221,212,97,122,203,9,219,241,176,118,66,40,253,127,50,185,35,207,5,51,17,102,96,185,121,7,110,61,38,3,174,177,223,173,225,14,51,140,80,83,197,46,55,61,224,15,51,142,215,229,95,213,249,241,48,193,37,102,168,116,44,171,123,188,40,75,71,97,168,249,151,101,10,211,110,162,68,74,222,106,215,128,51,190,103,119,242,11,136,126,75,161,159,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("766723ae-a677-497c-aea4-6134961b633b"));
		}

		#endregion

	}

	#endregion

}

