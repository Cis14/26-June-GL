namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ErrorMessageBuilderSchema

	/// <exclude/>
	public class ErrorMessageBuilderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ErrorMessageBuilderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ErrorMessageBuilderSchema(ErrorMessageBuilderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("32bc75b4-5d74-4ce8-b499-e5a131cd2f40");
			Name = "ErrorMessageBuilder";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("39250377-f3a4-4f50-9f5d-2f777b2a2653");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,82,77,107,195,48,12,61,167,144,255,224,99,66,139,217,189,235,160,29,101,108,244,3,182,221,135,151,168,197,224,216,65,178,179,149,209,255,62,59,31,93,90,218,125,28,118,72,64,210,211,211,147,158,29,73,189,101,79,59,178,80,140,227,129,235,133,252,214,40,5,153,149,70,19,191,3,13,40,179,3,228,25,16,5,153,141,245,168,162,48,250,108,1,129,207,66,22,136,30,157,2,226,75,147,131,34,62,71,52,72,190,37,30,104,81,0,149,34,131,163,70,189,145,91,135,34,140,142,7,31,241,32,42,221,171,146,25,35,235,115,25,203,148,32,98,53,203,210,115,139,45,204,156,84,57,160,71,6,116,84,162,172,132,133,14,143,32,114,163,213,206,199,24,52,190,16,148,194,211,27,100,19,54,215,149,68,163,11,208,150,175,224,109,225,229,214,202,162,147,161,109,239,52,207,235,193,73,27,67,8,214,206,150,206,142,58,76,209,136,74,89,45,38,146,27,150,180,41,126,79,43,167,212,26,231,69,105,119,73,218,65,34,4,235,80,247,217,198,117,97,127,96,232,149,46,178,244,48,126,179,118,102,75,196,252,229,225,28,112,56,233,31,100,120,210,86,255,47,168,219,127,127,167,35,239,27,207,207,94,109,33,201,94,247,193,243,119,200,92,48,191,110,186,105,208,212,109,89,9,236,52,146,223,178,41,134,71,83,1,218,169,82,9,176,137,239,225,237,211,72,199,167,38,4,176,211,254,66,19,118,245,175,6,52,203,242,7,35,117,242,117,226,209,65,125,250,87,107,126,75,248,131,105,225,219,127,2,148,90,223,67,250,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("32bc75b4-5d74-4ce8-b499-e5a131cd2f40"));
		}

		#endregion

	}

	#endregion

}

