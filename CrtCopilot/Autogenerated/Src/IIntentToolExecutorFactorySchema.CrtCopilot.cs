namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IIntentToolExecutorFactorySchema

	/// <exclude/>
	public class IIntentToolExecutorFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IIntentToolExecutorFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IIntentToolExecutorFactorySchema(IIntentToolExecutorFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("07f57910-666c-4dbc-b33f-0000dc1dd45d");
			Name = "IIntentToolExecutorFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("41e46e3a-7898-448f-b0fb-31200d6989e0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,146,193,78,132,48,16,134,207,75,194,59,76,246,164,23,250,0,34,151,85,19,78,30,118,111,198,67,183,78,217,38,180,37,109,137,18,227,187,91,90,96,81,200,38,38,70,19,14,76,249,231,159,255,155,162,168,68,219,80,134,176,51,72,157,208,217,78,55,162,214,46,77,222,211,100,211,90,161,42,216,119,214,161,188,73,19,127,66,8,129,220,182,82,82,211,21,67,125,135,92,40,180,64,129,83,230,180,233,64,40,135,134,247,182,92,27,96,193,218,27,9,101,29,85,204,75,53,135,210,107,148,59,104,93,223,191,33,107,125,95,54,250,147,217,128,166,61,214,130,205,28,203,101,227,67,28,235,213,125,232,69,198,112,16,248,66,72,133,175,83,146,245,32,16,185,221,9,193,54,200,4,23,248,2,195,98,162,122,207,78,40,105,54,77,35,223,199,229,13,53,84,130,242,251,189,221,178,101,235,182,24,252,2,153,114,96,163,99,78,66,227,217,199,160,107,141,178,69,78,198,183,254,211,211,227,209,234,26,29,62,247,213,10,65,196,189,90,9,13,43,105,174,227,229,94,222,156,51,162,170,208,252,37,116,143,4,24,152,132,86,83,130,175,187,40,207,224,94,116,136,154,33,245,80,253,234,30,112,246,187,254,195,34,244,5,254,233,226,199,242,39,224,155,143,52,241,207,39,68,78,166,1,19,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("07f57910-666c-4dbc-b33f-0000dc1dd45d"));
		}

		#endregion

	}

	#endregion

}

