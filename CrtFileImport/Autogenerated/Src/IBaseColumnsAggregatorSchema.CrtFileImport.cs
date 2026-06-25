namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IBaseColumnsAggregatorSchema

	/// <exclude/>
	public class IBaseColumnsAggregatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IBaseColumnsAggregatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IBaseColumnsAggregatorSchema(IBaseColumnsAggregatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("bf20cba9-3c75-48fd-bbc1-dd610d81de84");
			Name = "IBaseColumnsAggregator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,144,205,106,195,48,16,132,207,53,248,29,150,156,90,40,214,189,117,13,105,104,65,183,82,74,239,27,101,109,4,150,100,118,165,64,40,121,247,250,55,113,211,227,174,190,153,217,145,71,71,210,161,33,248,34,102,148,80,199,98,23,124,109,155,196,24,109,240,197,187,109,73,187,46,112,204,179,159,60,187,83,74,65,41,201,57,228,83,53,207,159,212,49,9,249,40,176,71,33,168,147,55,131,24,91,27,79,80,7,6,19,218,228,188,0,54,13,83,131,49,240,98,165,86,94,93,218,183,214,128,245,145,184,30,142,210,175,189,221,110,210,110,47,82,120,2,253,193,193,144,8,29,190,177,77,36,253,120,180,7,226,71,208,215,131,39,225,76,190,49,135,155,103,38,140,139,123,159,62,180,251,87,111,92,76,164,128,29,117,115,153,226,130,171,91,190,236,144,209,129,239,63,247,101,51,209,227,153,155,106,74,131,227,48,21,165,26,185,171,140,41,38,246,82,233,63,57,165,90,246,3,184,174,6,235,10,247,235,151,49,14,86,209,15,207,189,248,156,103,231,95,216,117,112,102,242,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("bf20cba9-3c75-48fd-bbc1-dd610d81de84"));
		}

		#endregion

	}

	#endregion

}

