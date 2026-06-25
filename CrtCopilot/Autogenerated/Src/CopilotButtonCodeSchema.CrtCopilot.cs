namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotButtonCodeSchema

	/// <exclude/>
	public class CopilotButtonCodeSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotButtonCodeSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotButtonCodeSchema(CopilotButtonCodeSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e63eeb1d-2589-4d10-a6dc-de652e4ad9b7");
			Name = "CopilotButtonCode";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,143,203,10,2,49,12,69,215,22,250,15,97,220,251,1,138,27,11,46,197,173,203,218,134,161,80,147,161,233,44,68,230,223,237,99,16,68,92,38,247,228,92,66,246,129,50,89,135,96,18,218,28,120,103,120,10,145,179,86,47,173,180,218,108,19,142,129,9,76,180,34,123,88,211,211,156,51,147,97,143,13,154,230,123,12,14,36,23,131,3,87,209,95,18,10,216,157,31,233,57,96,244,197,122,109,247,61,91,93,142,73,114,49,166,64,35,220,80,224,8,195,19,101,56,252,99,46,92,17,226,74,180,14,36,223,107,218,188,244,111,190,150,11,188,1,186,228,162,66,255,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e63eeb1d-2589-4d10-a6dc-de652e4ad9b7"));
		}

		#endregion

	}

	#endregion

}

