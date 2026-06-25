namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IKnwSearchClientSchema

	/// <exclude/>
	public class IKnwSearchClientSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IKnwSearchClientSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IKnwSearchClientSchema(IKnwSearchClientSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1b2e5d4e-0af8-4d04-bec4-193731cd775d");
			Name = "IKnwSearchClient";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,83,93,111,219,48,12,124,110,128,252,7,194,123,217,128,194,126,111,147,0,69,2,12,197,176,173,104,243,7,20,137,142,185,218,148,39,202,13,130,98,255,125,146,108,167,110,146,23,127,208,228,221,241,78,102,213,160,180,74,35,172,29,42,79,54,95,219,150,106,235,231,179,247,249,236,166,19,226,61,188,28,197,99,147,111,171,208,98,66,225,126,62,11,223,190,56,220,147,101,120,100,143,174,12,16,119,240,248,131,15,47,168,156,174,214,53,33,251,212,87,20,5,44,164,107,26,229,142,171,225,253,201,217,55,50,40,160,24,104,156,135,210,58,144,52,30,89,95,217,30,106,52,123,4,177,157,211,161,185,87,163,224,249,225,123,232,115,111,164,49,31,9,138,9,67,219,237,106,210,19,224,75,93,55,239,73,219,105,137,159,232,43,107,228,14,158,210,108,255,241,92,121,42,244,56,65,77,211,213,158,218,26,175,8,221,29,193,87,72,14,194,142,236,169,36,116,113,85,3,74,196,106,82,30,13,252,237,208,17,74,126,34,42,206,153,22,173,114,170,1,14,17,45,51,135,97,64,124,182,218,86,56,152,4,67,13,180,101,175,136,175,153,54,210,220,134,25,145,20,215,102,196,143,130,90,181,39,142,185,51,216,54,222,36,95,20,137,247,186,12,173,88,99,93,167,137,173,125,69,206,86,15,224,227,67,184,66,99,153,124,8,49,6,57,237,28,149,126,108,187,193,82,5,255,36,78,45,4,17,180,195,114,153,173,207,225,243,95,150,49,43,86,64,37,176,245,32,45,234,232,167,185,148,233,208,119,142,37,232,153,0,158,130,223,108,127,71,24,187,251,131,218,195,129,124,21,35,250,112,82,146,154,40,28,85,40,244,238,5,146,17,53,210,76,193,134,115,240,245,84,123,30,194,24,86,189,133,139,93,224,194,60,88,130,233,141,248,118,63,156,71,100,211,31,201,244,254,175,255,211,62,21,67,237,63,56,21,210,196,182,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1b2e5d4e-0af8-4d04-bec4-193731cd775d"));
		}

		#endregion

	}

	#endregion

}

