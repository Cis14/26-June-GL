namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IContextPartBuilderSchema

	/// <exclude/>
	public class IContextPartBuilderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IContextPartBuilderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IContextPartBuilderSchema(IContextPartBuilderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("afbc40f1-607d-4f90-a15d-ccdb8e0e6d7a");
			Name = "IContextPartBuilder";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("144e34a0-ee8b-48e4-b7e9-406724f6b9aa");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,205,78,195,48,12,62,51,105,239,96,149,11,92,218,251,216,42,177,138,67,165,77,218,43,120,169,187,69,106,157,40,113,17,19,218,187,19,146,14,86,6,39,14,57,248,179,243,253,196,97,236,201,91,84,4,149,35,20,109,242,202,88,221,25,129,247,249,108,62,187,187,119,116,208,134,161,102,33,215,134,185,5,212,149,9,197,155,236,208,201,122,208,93,67,46,142,22,69,1,75,63,244,61,186,83,57,214,207,86,67,107,28,40,195,175,228,68,243,1,188,37,165,91,173,64,78,150,192,180,225,14,17,40,71,237,42,91,163,167,43,246,172,40,1,217,147,151,64,33,6,70,210,205,102,11,193,181,199,3,129,13,99,249,69,188,184,82,183,195,190,11,34,250,226,251,55,219,99,198,175,144,91,146,163,105,252,2,118,241,114,106,254,140,21,129,42,229,241,255,76,243,103,156,219,60,9,9,125,236,129,195,210,86,153,74,139,26,21,178,242,74,185,154,182,130,176,102,47,200,138,242,101,17,57,190,41,29,201,224,216,151,55,54,160,110,65,142,228,8,48,28,54,17,245,1,66,1,133,12,123,130,35,114,211,81,3,137,163,1,47,46,172,56,127,233,173,156,130,210,133,250,83,43,181,32,190,252,54,201,68,123,44,15,83,183,48,205,245,248,52,238,136,184,73,107,138,245,57,253,206,9,120,254,0,235,253,102,33,205,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("afbc40f1-607d-4f90-a15d-ccdb8e0e6d7a"));
		}

		#endregion

	}

	#endregion

}

