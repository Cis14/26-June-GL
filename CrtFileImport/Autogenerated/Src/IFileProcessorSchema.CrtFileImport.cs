namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileProcessorSchema

	/// <exclude/>
	public class IFileProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileProcessorSchema(IFileProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("4e8d1b4d-9296-459a-b005-ca3460aef8ac");
			Name = "IFileProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("79cdeed7-eef0-4dd8-9765-07d140cf1035");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,65,75,195,64,16,133,207,45,244,63,12,237,165,130,36,247,182,6,68,80,115,16,139,85,60,111,146,73,58,210,236,198,217,73,165,20,255,187,187,219,164,165,138,160,199,55,243,205,219,153,151,104,85,163,109,84,142,240,140,204,202,154,82,162,27,163,75,170,90,86,66,70,71,183,180,193,180,110,12,203,104,184,31,13,7,173,37,93,193,106,103,5,235,249,81,159,166,95,49,139,238,69,154,232,58,179,194,42,247,38,214,129,14,157,48,86,78,65,170,5,185,116,143,206,32,245,246,75,54,57,90,107,56,80,77,155,109,40,7,234,161,31,204,96,31,184,163,221,3,202,218,20,118,6,203,48,121,104,198,113,12,11,219,214,181,226,93,210,23,58,19,180,80,209,22,53,148,206,56,58,210,241,119,124,209,40,86,53,104,151,209,213,216,179,227,196,175,18,45,226,208,8,220,214,80,209,251,250,230,212,223,190,52,46,157,194,203,240,196,197,252,31,59,153,236,13,115,185,132,220,197,70,5,178,79,151,196,75,81,164,45,20,74,20,152,18,80,11,201,14,108,190,198,90,245,102,31,36,107,104,53,189,183,8,110,216,33,37,33,119,135,48,150,221,45,108,140,172,194,224,75,90,140,227,228,175,25,156,207,37,79,78,118,11,128,43,252,154,203,99,56,105,122,215,186,210,153,69,31,204,4,117,113,248,152,65,127,30,254,150,179,162,171,125,1,178,104,181,4,172,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("4e8d1b4d-9296-459a-b005-ca3460aef8ac"));
		}

		#endregion

	}

	#endregion

}

