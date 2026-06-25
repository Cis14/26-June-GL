namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISocialOAuthHandlerSchema

	/// <exclude/>
	public class ISocialOAuthHandlerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISocialOAuthHandlerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISocialOAuthHandlerSchema(ISocialOAuthHandlerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("517c4ae3-d962-4f43-be2d-326fee09fce3");
			Name = "ISocialOAuthHandler";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,193,110,194,48,12,134,207,69,226,29,44,184,108,151,246,62,24,82,133,52,141,195,180,105,236,54,237,96,154,180,68,75,147,44,113,52,49,196,187,47,105,161,116,130,9,46,145,147,216,159,255,63,177,194,154,59,131,5,135,185,165,185,212,158,45,20,241,202,34,9,173,210,24,219,50,220,186,225,96,59,28,36,222,9,85,193,147,102,92,186,244,149,127,121,238,200,77,134,131,112,53,182,188,10,37,208,149,220,193,98,169,11,129,242,57,247,180,126,68,197,36,183,77,106,150,101,48,117,190,174,209,110,102,251,125,87,6,165,182,16,131,149,214,159,192,68,37,8,37,32,99,14,208,24,192,192,130,117,11,75,15,172,172,7,51,126,37,69,1,162,227,157,85,145,68,55,39,66,154,131,220,152,0,104,252,131,10,175,147,118,153,253,54,137,35,27,223,162,159,189,133,138,211,4,118,141,201,243,240,23,137,20,12,214,151,201,239,31,199,228,43,192,111,107,14,130,113,69,130,54,224,10,109,46,232,94,198,148,107,192,121,209,120,35,13,142,208,18,80,104,20,63,65,91,241,211,218,46,165,254,254,167,89,115,98,208,98,107,248,126,100,219,153,25,205,246,195,19,112,72,80,104,69,40,148,131,58,172,237,23,11,85,234,116,154,53,181,71,148,229,228,173,114,179,252,143,0,111,101,72,61,220,245,77,70,197,15,65,222,77,23,29,250,238,133,220,134,225,77,90,251,99,174,88,59,195,113,187,251,5,199,135,10,109,25,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("517c4ae3-d962-4f43-be2d-326fee09fce3"));
		}

		#endregion

	}

	#endregion

}

