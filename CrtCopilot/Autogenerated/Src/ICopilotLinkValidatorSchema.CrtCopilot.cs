namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ICopilotLinkValidatorSchema

	/// <exclude/>
	public class ICopilotLinkValidatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ICopilotLinkValidatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ICopilotLinkValidatorSchema(ICopilotLinkValidatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6984e724-4f51-4d32-9c1a-28e45f17d23a");
			Name = "ICopilotLinkValidator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a3a8162-4be1-46b5-bd50-b3efc2df6d2e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,81,65,106,195,48,16,60,215,224,63,44,62,181,16,236,7,212,13,132,156,12,61,20,10,189,43,182,156,44,181,118,141,86,78,41,165,127,175,36,75,161,36,165,160,131,180,204,204,206,140,72,25,45,179,234,53,236,173,86,14,185,222,243,140,19,187,178,248,42,139,178,184,107,154,6,90,89,140,81,246,115,155,222,29,57,109,199,64,26,217,194,89,77,56,120,42,29,97,66,122,23,64,130,36,2,94,92,212,81,75,157,149,154,95,82,243,114,152,176,247,240,172,214,37,218,179,151,121,91,85,217,122,224,234,228,198,74,28,36,156,22,80,52,128,213,134,207,58,88,136,174,146,161,209,178,1,119,210,208,179,223,69,174,190,168,53,215,114,237,172,172,50,64,190,150,167,74,188,123,100,170,182,57,78,26,212,109,19,97,127,179,210,146,192,138,23,112,156,59,210,255,51,81,118,51,86,219,142,6,236,99,36,28,163,237,220,48,19,160,196,206,119,47,221,173,148,213,110,177,36,151,189,31,232,78,87,77,172,253,12,158,155,193,129,45,206,134,223,203,85,134,250,229,62,101,126,93,35,231,232,27,72,224,148,114,3,7,230,9,162,243,135,199,248,79,223,101,225,207,15,57,129,59,163,89,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6984e724-4f51-4d32-9c1a-28e45f17d23a"));
		}

		#endregion

	}

	#endregion

}

