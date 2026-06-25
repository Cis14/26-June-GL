namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MoneyColumnProcessorSchema

	/// <exclude/>
	public class MoneyColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MoneyColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MoneyColumnProcessorSchema(MoneyColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1ada78ef-dac1-4b3e-9c3b-95c44f2ee05b");
			Name = "MoneyColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1a247561-b87d-48fb-9e13-b6a8baa960d3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,193,75,235,64,16,198,207,10,254,15,67,189,180,240,72,238,181,45,104,165,143,30,158,8,90,47,226,97,187,153,212,133,100,55,111,102,87,40,226,255,238,100,211,104,19,163,224,169,187,195,55,223,55,243,203,214,170,18,185,82,26,225,30,137,20,187,220,39,75,103,115,179,11,164,188,113,54,89,153,2,215,101,229,200,159,157,190,158,157,158,4,54,118,7,119,123,246,88,94,124,220,143,187,9,191,171,39,43,165,189,35,131,44,10,209,156,19,238,36,3,150,133,98,158,194,63,103,113,191,116,69,40,237,45,57,141,204,142,162,46,77,83,152,25,251,140,100,124,230,52,104,194,124,62,90,21,78,249,158,124,148,46,90,61,135,178,84,180,111,239,151,22,140,101,175,172,236,234,114,240,207,134,65,215,185,32,7,18,8,206,178,217,22,8,185,35,168,26,191,122,131,56,20,232,24,3,47,170,8,200,73,27,145,30,101,60,94,99,174,66,225,175,140,205,164,111,236,247,21,186,124,188,238,13,56,249,3,55,194,28,230,96,229,71,4,67,75,79,38,79,226,88,133,109,97,244,97,200,33,25,76,97,136,129,180,190,70,108,159,124,101,55,79,161,102,47,152,111,163,111,163,248,45,217,47,104,99,97,73,168,60,114,23,176,236,47,74,196,131,231,208,2,226,153,124,152,166,125,215,89,165,72,149,145,211,124,20,24,73,214,176,168,235,87,57,90,108,228,46,95,165,45,36,179,52,170,99,243,1,220,80,226,120,211,241,129,174,237,68,136,110,21,227,184,95,174,31,254,201,219,1,42,218,172,225,218,133,44,25,21,146,151,199,61,173,207,94,122,49,251,137,242,149,36,253,2,242,181,242,170,121,128,13,219,96,205,127,57,155,12,173,55,185,65,250,6,101,213,206,2,238,69,254,141,162,135,191,193,100,209,239,161,182,187,23,183,205,58,131,249,162,91,75,34,192,190,236,98,144,66,195,166,91,124,123,7,142,146,147,103,93,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1ada78ef-dac1-4b3e-9c3b-95c44f2ee05b"));
		}

		#endregion

	}

	#endregion

}

