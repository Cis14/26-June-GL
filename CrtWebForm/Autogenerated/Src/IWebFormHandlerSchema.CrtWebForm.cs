namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IWebFormHandlerSchema

	/// <exclude/>
	public class IWebFormHandlerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IWebFormHandlerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IWebFormHandlerSchema(IWebFormHandlerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("85b340bd-4e82-4d09-9c7d-737d1496c5d3");
			Name = "IWebFormHandler";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9d05c8ee-17e3-41aa-adfe-7e36f0a4c27c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,144,193,78,195,48,12,134,207,171,212,119,176,182,11,92,154,251,40,187,12,13,122,64,154,96,18,231,172,113,186,72,77,82,57,73,37,132,120,119,210,180,29,219,16,183,216,250,62,251,143,13,215,232,58,94,35,28,144,136,59,43,125,177,181,70,170,38,16,247,202,154,60,251,202,179,69,112,202,52,255,33,197,51,26,140,79,20,31,120,220,89,210,239,72,189,170,241,33,207,162,186,34,108,34,4,149,241,72,50,110,90,67,53,113,47,220,136,22,41,97,140,49,40,93,208,154,211,231,102,170,247,100,123,37,208,129,154,93,152,73,118,129,118,225,216,170,250,2,186,153,255,187,240,13,93,104,125,101,164,141,218,240,175,115,186,87,244,39,43,220,26,246,105,88,138,244,39,83,106,140,67,29,248,19,130,140,51,65,112,207,139,51,206,110,249,178,227,196,53,152,120,232,199,229,32,60,69,126,185,57,92,233,37,75,84,146,122,171,196,180,101,200,124,183,155,28,152,229,251,241,176,139,21,26,49,198,79,245,247,120,238,171,102,236,253,0,54,255,177,51,226,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("85b340bd-4e82-4d09-9c7d-737d1496c5d3"));
		}

		#endregion

	}

	#endregion

}

