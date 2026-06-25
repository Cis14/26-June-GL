namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IWebFormProcessHandlersFactorySchema

	/// <exclude/>
	public class IWebFormProcessHandlersFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IWebFormProcessHandlersFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IWebFormProcessHandlersFactorySchema(IWebFormProcessHandlersFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("30248ea6-6ed0-4ae8-b005-c4b54f120925");
			Name = "IWebFormProcessHandlersFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9d05c8ee-17e3-41aa-adfe-7e36f0a4c27c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,221,83,193,106,195,48,12,61,183,208,127,16,237,101,131,145,220,219,44,151,177,118,57,12,10,235,216,217,141,149,212,16,219,65,182,25,97,236,223,103,39,109,214,52,133,13,118,219,205,122,146,158,244,158,109,197,36,154,154,229,8,59,36,98,70,23,54,122,208,170,16,165,35,102,133,86,179,233,199,108,58,113,70,168,18,94,26,99,81,174,46,98,95,95,85,152,135,98,19,109,80,33,137,252,187,230,156,150,208,227,62,179,32,44,125,53,100,202,34,21,126,248,18,178,55,220,175,53,201,45,233,28,141,121,98,138,87,72,102,205,114,171,169,105,187,226,56,134,196,56,41,25,53,233,49,62,230,161,208,4,37,90,27,38,214,29,5,28,142,28,192,177,70,197,67,202,207,172,88,119,172,89,137,128,202,10,219,68,39,242,248,140,189,118,251,74,228,32,78,43,254,184,225,36,248,212,75,123,70,123,208,220,44,97,219,242,180,2,70,10,90,96,131,214,128,61,160,223,27,71,187,71,125,91,124,217,151,212,140,152,4,229,47,240,126,238,12,146,191,54,213,93,195,60,221,121,190,128,65,222,131,81,18,183,29,215,9,222,59,113,25,239,122,125,24,60,149,32,120,240,168,16,72,227,126,66,235,72,153,52,137,79,167,144,202,30,149,147,72,108,95,97,146,181,207,129,89,228,189,121,56,244,47,13,250,71,168,185,121,29,8,130,161,190,187,48,103,178,113,130,67,191,247,237,234,87,30,107,99,255,191,201,94,228,53,151,71,240,31,108,94,248,31,213,61,245,54,254,236,254,245,0,244,216,23,178,190,216,134,94,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("30248ea6-6ed0-4ae8-b005-c4b54f120925"));
		}

		#endregion

	}

	#endregion

}

