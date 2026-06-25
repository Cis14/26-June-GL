namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IProcessedValuesProviderSchema

	/// <exclude/>
	public class IProcessedValuesProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IProcessedValuesProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IProcessedValuesProviderSchema(IProcessedValuesProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d35a2012-d756-4c04-a033-754a28ca5950");
			Name = "IProcessedValuesProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,143,77,75,196,48,16,134,207,91,232,127,24,246,164,32,205,15,176,246,178,82,216,219,130,226,125,54,157,74,164,249,96,38,41,136,236,127,183,77,169,150,234,49,153,231,125,231,25,135,150,36,160,38,120,37,102,20,223,199,234,228,93,111,222,19,99,52,222,85,173,25,232,108,131,231,88,22,95,101,113,8,233,58,24,13,198,69,226,126,14,158,47,236,53,137,80,247,134,67,34,153,158,163,233,136,39,118,230,15,74,41,168,37,89,139,252,217,172,31,173,113,157,64,88,147,48,206,209,234,7,87,123,190,14,200,104,193,77,186,79,199,142,36,26,151,245,142,205,226,6,218,15,201,58,216,140,170,90,229,208,255,29,11,159,141,247,29,139,203,159,52,83,76,236,164,185,236,164,107,181,78,102,212,95,63,72,199,124,95,46,111,61,191,224,72,119,203,138,83,222,240,252,43,185,21,126,128,45,148,211,176,209,188,127,44,139,105,195,173,44,110,223,6,133,52,35,182,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d35a2012-d756-4c04-a033-754a28ca5950"));
		}

		#endregion

	}

	#endregion

}

