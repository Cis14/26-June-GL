namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationServiceSchema

	/// <exclude/>
	public class ITranslationServiceSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationServiceSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationServiceSchema(ITranslationServiceSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8368303c-f5ee-4bf3-920a-9f76b46f2286");
			Name = "ITranslationService";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6d72ca66-8680-4c3f-b4a0-15ba7a19db7c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,221,84,205,106,194,64,16,62,71,240,29,22,189,68,8,121,0,165,7,43,180,164,96,21,99,241,32,61,172,155,73,92,92,119,211,253,17,82,241,221,59,38,106,181,6,161,61,20,218,219,204,236,247,51,179,59,137,164,107,48,57,101,64,166,160,53,53,42,181,225,64,201,148,103,78,83,203,149,12,167,154,74,35,202,184,217,216,54,27,158,51,92,102,36,46,140,133,117,239,75,30,198,160,55,156,193,80,37,32,110,30,134,51,88,32,0,33,109,13,25,138,147,72,90,208,41,182,210,37,209,153,233,129,84,66,231,135,4,59,180,154,50,235,63,99,251,228,142,180,174,241,173,206,43,18,114,183,16,156,17,126,148,174,87,246,182,165,250,169,147,33,216,165,74,76,151,140,75,122,117,56,31,229,80,93,201,209,253,181,44,227,28,145,220,168,21,248,21,109,223,206,120,20,79,91,1,153,192,155,3,99,31,148,94,83,139,117,132,14,193,24,154,65,85,10,159,140,146,1,185,87,73,17,219,66,192,5,228,84,13,103,154,230,57,36,193,222,206,155,224,115,41,105,224,182,104,57,188,183,81,60,33,125,102,29,21,252,29,206,38,247,59,189,63,59,212,245,3,30,217,164,159,231,162,168,27,243,59,156,71,135,151,102,208,28,147,40,249,129,2,118,60,112,194,58,13,126,132,139,127,136,35,153,42,194,62,227,128,44,148,18,132,27,132,51,120,201,19,106,97,111,230,121,191,102,23,144,250,89,255,215,82,12,150,192,86,117,75,209,6,153,84,31,124,153,239,170,159,209,69,113,247,1,184,214,18,69,31,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8368303c-f5ee-4bf3-920a-9f76b46f2286"));
		}

		#endregion

	}

	#endregion

}

