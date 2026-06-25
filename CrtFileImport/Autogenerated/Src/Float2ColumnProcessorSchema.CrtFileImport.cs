namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float2ColumnProcessorSchema

	/// <exclude/>
	public class Float2ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float2ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float2ColumnProcessorSchema(Float2ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("66ad8406-fee7-4774-941e-8dbeb16b8a1a");
			Name = "Float2ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("aaf0cd3b-b0e9-4378-a805-7163759e3c5e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,75,195,64,16,133,207,21,252,15,67,189,180,32,9,120,212,182,160,149,74,47,34,104,189,136,135,237,102,82,7,146,221,56,187,43,20,241,191,59,217,52,218,196,42,120,234,238,240,230,189,153,47,91,163,74,116,149,210,8,15,200,172,156,205,125,50,183,38,167,77,96,229,201,154,100,65,5,46,203,202,178,63,62,122,63,62,26,4,71,102,3,247,91,231,177,188,248,186,239,119,51,254,86,79,22,74,123,203,132,78,20,162,57,97,220,72,6,204,11,229,220,57,44,10,171,252,217,220,22,161,52,119,108,53,58,103,57,10,211,52,133,9,153,23,100,242,153,213,160,25,243,233,48,234,123,242,97,58,107,245,46,148,165,226,109,123,191,52,64,198,121,101,100,89,155,131,127,33,7,186,14,6,57,176,80,176,198,209,186,64,200,45,67,213,248,213,43,52,83,129,142,57,240,166,138,128,46,105,51,210,189,144,167,107,204,85,40,252,21,153,76,26,71,126,91,161,205,71,203,222,132,227,83,184,21,234,48,5,35,63,34,56,184,246,120,252,44,150,85,88,23,164,119,99,30,212,193,14,219,15,106,131,247,72,238,155,177,172,231,57,212,252,5,245,93,52,110,20,255,133,251,131,110,44,204,25,149,71,215,101,44,4,68,137,184,239,217,223,64,76,147,47,215,180,111,59,169,20,171,50,162,154,14,131,67,150,61,12,234,250,105,14,103,43,185,203,135,105,11,201,36,141,234,216,188,67,119,48,114,180,234,24,65,215,119,44,76,215,202,225,168,95,174,159,255,224,99,135,21,77,214,144,237,98,150,140,10,217,203,19,63,175,207,94,122,49,251,139,243,149,36,253,3,243,181,242,170,121,132,13,221,96,232,85,206,148,161,241,148,19,242,47,44,171,118,22,176,111,242,159,20,61,220,4,202,162,223,99,109,247,32,110,171,101,6,211,89,183,150,52,4,251,186,139,131,24,26,56,221,226,199,39,186,103,255,18,100,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("66ad8406-fee7-4774-941e-8dbeb16b8a1a"));
		}

		#endregion

	}

	#endregion

}

