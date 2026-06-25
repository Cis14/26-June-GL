namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IKnwPromptBuilderSchema

	/// <exclude/>
	public class IKnwPromptBuilderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IKnwPromptBuilderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IKnwPromptBuilderSchema(IKnwPromptBuilderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("91a6c6fb-6d9f-42b6-8597-be5c286f99a2");
			Name = "IKnwPromptBuilder";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,83,219,74,3,49,16,125,110,193,127,24,218,7,43,200,238,123,173,5,45,34,69,11,133,250,3,113,119,186,13,102,147,144,201,182,138,244,223,157,205,102,87,123,17,159,124,204,92,206,156,115,102,162,69,137,100,69,134,48,115,40,188,52,201,204,88,169,140,191,232,127,94,244,123,21,73,93,192,234,131,60,150,156,81,10,51,174,209,148,60,162,70,39,179,155,174,230,5,157,19,100,214,62,121,208,156,216,148,168,125,50,215,30,221,154,209,41,153,109,132,159,153,210,42,172,1,184,143,59,135,14,11,126,64,87,54,134,249,147,222,45,29,215,249,251,74,170,28,93,40,76,211,20,38,84,149,165,112,31,211,248,14,121,130,55,109,118,10,243,2,129,76,229,88,135,67,37,60,230,96,3,10,176,60,18,5,18,172,141,3,1,81,29,16,135,121,116,210,130,167,63,208,101,77,71,11,5,178,229,117,142,86,239,51,80,235,68,44,208,111,76,78,99,88,86,175,74,102,77,242,152,120,8,220,229,204,91,156,50,143,140,71,20,236,78,43,66,215,209,191,170,201,24,240,155,80,182,149,57,43,140,57,200,186,189,36,221,204,244,120,232,196,10,39,74,208,188,239,219,65,84,63,152,30,217,193,80,44,249,221,39,147,52,148,159,239,110,57,213,237,237,100,96,114,194,90,212,57,20,245,105,132,21,116,222,123,115,128,184,53,50,175,93,96,83,87,65,122,99,237,40,178,89,69,50,145,212,53,60,75,242,147,250,128,22,13,224,244,219,150,155,191,125,174,207,83,225,169,221,173,125,209,214,56,237,146,160,219,191,149,22,149,212,248,47,182,158,152,176,160,226,23,7,90,149,67,182,183,185,182,240,222,55,159,232,32,184,255,2,198,148,42,179,209,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("91a6c6fb-6d9f-42b6-8597-be5c286f99a2"));
		}

		#endregion

	}

	#endregion

}

