namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IToolExecutionTriggerSchema

	/// <exclude/>
	public class IToolExecutionTriggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IToolExecutionTriggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IToolExecutionTriggerSchema(IToolExecutionTriggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9a849186-b190-4768-82e9-df594f1d2c7f");
			Name = "IToolExecutionTrigger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("41e46e3a-7898-448f-b0fb-31200d6989e0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,144,77,142,194,48,12,133,247,149,122,7,139,213,204,166,57,0,157,110,208,44,88,3,7,48,193,45,145,242,83,217,169,4,66,220,157,148,166,195,76,53,68,89,88,47,239,125,177,237,209,145,244,168,9,54,76,24,77,168,54,161,55,54,196,178,184,149,5,164,163,148,130,90,6,231,144,175,205,75,218,250,72,220,142,193,54,48,56,242,41,235,141,239,0,187,84,11,24,15,250,140,177,250,5,81,127,41,253,112,180,70,39,227,12,218,238,67,176,223,23,210,195,200,218,179,233,58,226,201,155,123,121,211,207,44,79,89,18,136,9,4,26,173,165,19,28,175,112,16,226,106,65,80,255,35,234,30,25,29,248,180,150,175,149,144,72,234,100,213,228,157,64,22,170,90,61,109,203,44,83,28,216,75,83,171,185,122,25,50,98,55,17,32,79,247,51,237,199,226,61,255,244,185,158,8,247,178,72,247,1,24,89,175,225,173,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9a849186-b190-4768-82e9-df594f1d2c7f"));
		}

		#endregion

	}

	#endregion

}

