namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportLoggerSchema

	/// <exclude/>
	public class IImportLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportLoggerSchema(IImportLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0b874d1d-c36b-481a-b709-ada9ef850f84");
			Name = "IImportLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,82,209,74,195,64,16,124,110,32,255,176,180,47,21,36,121,215,90,144,18,49,96,161,160,63,112,77,54,231,73,238,54,236,93,130,69,250,239,94,46,166,98,181,160,8,125,188,221,217,157,153,189,49,66,163,109,68,129,240,132,204,194,82,229,146,21,153,74,201,150,133,83,100,146,59,85,99,174,27,98,23,71,111,113,52,105,218,109,173,10,80,198,33,87,253,96,62,116,31,72,74,100,15,232,65,147,25,163,244,211,176,70,247,76,165,189,130,77,24,139,163,190,153,166,41,44,108,171,181,224,221,114,44,220,11,83,214,104,193,203,32,78,14,176,244,24,183,104,4,11,13,198,11,191,153,90,52,37,242,116,185,72,67,245,103,16,118,104,220,45,75,251,21,215,145,42,63,88,179,158,115,78,219,23,44,28,12,59,47,97,69,117,171,205,134,169,64,107,3,34,27,23,193,97,229,197,245,111,44,189,22,216,132,99,158,217,214,200,123,108,109,248,177,204,56,229,118,143,162,195,127,185,83,166,34,214,33,44,224,195,100,133,196,51,251,204,189,130,245,192,252,205,233,103,235,175,254,250,195,88,168,73,158,112,19,4,244,32,159,252,249,184,105,230,121,135,232,135,247,62,142,246,239,203,95,102,70,99,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0b874d1d-c36b-481a-b709-ada9ef850f84"));
		}

		#endregion

	}

	#endregion

}

