namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISsoContactUpdaterSchema

	/// <exclude/>
	public class ISsoContactUpdaterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISsoContactUpdaterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISsoContactUpdaterSchema(ISsoContactUpdaterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("210deb1a-27cb-45b7-b65b-28f2254acf3b");
			Name = "ISsoContactUpdater";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9cc09773-c057-4470-b5fb-7fbb57d82a07");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,146,219,74,196,48,16,134,175,87,240,29,134,221,27,5,105,238,119,107,65,86,144,5,23,133,162,247,179,237,180,6,114,40,73,90,40,226,187,155,38,169,226,82,16,150,64,194,76,254,249,231,203,161,183,92,181,80,142,214,145,204,246,90,8,170,28,215,202,102,79,164,200,240,106,119,125,53,141,141,161,214,167,225,160,28,153,6,43,218,194,161,180,122,175,149,195,202,189,117,53,250,252,36,100,140,65,110,123,41,209,140,69,12,227,46,84,81,11,167,17,124,140,208,24,45,161,124,56,62,131,33,219,249,150,4,216,120,23,16,186,229,10,6,142,80,150,47,89,178,100,191,158,93,127,18,188,2,62,163,44,144,192,231,196,178,154,169,143,228,62,116,109,183,240,26,74,195,222,25,232,234,63,210,188,67,131,210,80,3,10,37,221,175,147,232,29,69,79,118,205,138,108,246,100,103,166,177,112,177,170,72,212,48,132,56,203,89,208,78,149,131,230,117,226,73,162,155,71,30,158,198,91,231,214,25,255,108,119,16,215,2,254,216,222,238,46,58,96,152,194,165,42,20,222,89,27,108,105,241,84,11,108,169,231,134,84,29,239,124,10,191,194,207,249,73,125,3,83,109,243,230,107,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("210deb1a-27cb-45b7-b65b-28f2254acf3b"));
		}

		#endregion

	}

	#endregion

}

