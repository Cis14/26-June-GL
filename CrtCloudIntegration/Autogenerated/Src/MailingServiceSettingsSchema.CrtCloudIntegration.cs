namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MailingServiceSettingsSchema

	/// <exclude/>
	public class MailingServiceSettingsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MailingServiceSettingsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MailingServiceSettingsSchema(MailingServiceSettingsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("aab368d1-5d3d-4df2-8f5a-686ab5543ad8");
			Name = "MailingServiceSettings";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,77,79,194,64,16,134,207,144,240,31,38,112,209,75,123,23,53,49,96,60,97,136,213,147,241,176,180,211,186,113,63,154,217,89,18,36,254,119,167,45,69,32,152,232,105,51,179,239,179,243,236,174,83,22,67,173,114,132,103,36,82,193,151,156,204,188,43,117,21,73,177,246,46,153,221,103,11,95,160,9,163,225,118,52,28,196,160,93,5,217,38,48,218,228,41,58,214,22,147,12,73,43,163,63,91,98,58,26,74,110,66,88,73,1,51,163,66,184,130,133,210,70,64,9,174,117,142,25,50,75,21,218,100,154,166,112,29,162,181,138,54,183,187,186,15,128,47,129,223,17,114,227,99,1,161,163,147,30,74,15,168,215,185,98,37,230,76,42,231,55,105,212,113,101,116,46,164,204,255,117,252,96,219,42,236,109,151,228,107,36,214,40,202,203,246,128,110,255,212,177,109,60,32,139,32,137,150,172,141,165,147,199,236,141,107,242,107,93,32,37,123,252,208,182,211,93,160,93,33,93,60,54,216,13,140,123,100,124,217,248,247,23,8,76,205,139,47,119,155,109,120,11,21,242,180,25,60,133,175,191,26,42,7,190,174,61,113,116,154,55,192,30,172,250,248,17,133,2,75,21,13,255,67,56,6,236,181,238,194,188,195,143,229,87,222,27,120,57,19,59,119,133,9,186,162,251,135,182,238,186,199,77,233,125,3,186,101,43,244,178,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("aab368d1-5d3d-4df2-8f5a-686ab5543ad8"));
		}

		#endregion

	}

	#endregion

}

