namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AIToolFilterDescriptorSchema

	/// <exclude/>
	public class AIToolFilterDescriptorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AIToolFilterDescriptorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AIToolFilterDescriptorSchema(AIToolFilterDescriptorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9b38d1b5-32d3-4d04-99b6-9875c0c4cbf6");
			Name = "AIToolFilterDescriptor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("2c11c81e-5f9f-4054-b4fd-e35ebc649618");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,145,65,107,2,49,16,133,207,21,252,15,131,189,40,180,187,247,106,133,178,210,226,161,69,170,183,210,67,140,163,4,178,73,200,204,82,172,244,191,119,146,93,69,91,188,44,153,153,151,239,205,219,56,85,35,5,165,17,170,136,138,141,47,42,31,140,245,220,239,29,250,189,155,134,140,219,193,114,79,140,245,248,79,45,74,107,81,203,29,71,197,11,58,140,70,255,211,188,55,142,77,141,197,82,166,202,154,239,100,225,68,37,186,219,136,59,41,160,178,138,232,1,158,230,43,239,237,179,177,140,113,134,164,163,9,236,99,86,150,101,9,19,106,234,90,197,253,180,171,79,192,181,69,216,116,250,132,243,91,80,64,72,36,197,61,105,31,112,3,44,100,216,102,116,113,228,149,103,192,143,153,98,85,121,199,81,105,254,76,141,115,124,106,132,102,109,141,6,157,118,189,186,234,205,33,175,123,74,182,136,226,30,217,160,196,91,100,64,59,207,118,175,88,175,49,14,223,228,1,224,17,6,188,15,152,206,131,81,178,59,250,17,199,244,51,87,221,16,14,176,67,30,75,60,249,252,92,135,5,21,229,36,219,209,37,110,62,51,249,189,36,246,164,69,223,117,22,83,88,156,238,92,186,8,207,225,23,92,191,57,28,141,187,216,232,54,109,242,92,183,251,93,54,165,247,11,147,45,68,243,114,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9b38d1b5-32d3-4d04-99b6-9875c0c4cbf6"));
		}

		#endregion

	}

	#endregion

}

