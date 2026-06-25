namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportEntitiesChunkProcessorSchema

	/// <exclude/>
	public class IFileImportEntitiesChunkProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportEntitiesChunkProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportEntitiesChunkProcessorSchema(IFileImportEntitiesChunkProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cea1c9a4-d455-4ca6-92a0-f145d998b68e");
			Name = "IFileImportEntitiesChunkProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,65,110,131,48,16,69,215,65,202,29,70,201,166,221,192,62,165,72,85,68,85,22,149,144,210,11,56,48,16,171,96,163,25,131,138,170,222,189,198,16,210,210,42,202,202,242,252,255,159,61,99,43,81,35,55,34,67,120,67,34,193,186,48,254,94,171,66,150,45,9,35,181,242,159,101,133,73,221,104,50,107,239,115,237,173,90,150,170,132,67,207,6,235,135,181,103,43,91,194,210,58,1,18,101,144,10,11,219,65,114,137,197,202,72,35,145,247,167,86,189,167,164,51,100,214,228,146,77,123,172,100,6,242,156,187,37,182,26,46,49,159,25,119,168,12,195,14,82,135,26,36,28,74,163,240,34,84,94,33,133,63,136,253,65,116,152,59,245,137,74,142,224,143,54,54,117,11,38,38,210,116,5,229,244,9,183,69,149,143,119,158,246,83,3,175,104,78,58,231,75,3,131,24,4,1,132,220,214,181,160,62,58,23,226,15,204,90,131,32,221,41,128,211,124,224,216,67,35,152,49,183,11,217,231,180,179,100,127,166,4,75,76,232,92,160,172,243,113,51,178,210,57,183,137,194,192,233,206,222,105,153,195,52,122,247,12,119,201,194,15,75,192,253,255,237,126,141,31,229,87,209,214,190,1,241,138,182,140,126,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cea1c9a4-d455-4ca6-92a0-f145d998b68e"));
		}

		#endregion

	}

	#endregion

}

