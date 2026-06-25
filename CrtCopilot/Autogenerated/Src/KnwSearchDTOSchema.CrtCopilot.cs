namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: KnwSearchDTOSchema

	/// <exclude/>
	public class KnwSearchDTOSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public KnwSearchDTOSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public KnwSearchDTOSchema(KnwSearchDTOSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b3d865fa-7420-4592-95b7-46991bf8d712");
			Name = "KnwSearchDTO";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ed753793-30d5-4797-a3f9-3019dcc6e358");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,146,205,106,194,64,16,199,207,10,190,195,128,151,22,74,114,111,172,80,84,68,74,169,212,190,192,186,142,113,219,205,110,152,157,32,65,124,247,206,38,81,212,210,66,79,203,124,255,254,51,235,84,129,161,84,26,97,66,168,216,248,100,226,75,99,61,15,250,135,65,191,87,5,227,114,88,213,129,177,200,110,108,201,180,22,181,212,184,144,204,209,33,25,45,57,146,53,36,204,197,11,19,171,66,120,132,23,183,95,161,34,189,155,126,188,53,241,52,77,97,20,170,162,80,84,143,59,123,170,88,1,147,114,97,139,4,126,253,41,157,129,176,36,12,232,56,78,85,16,154,46,226,13,149,101,208,222,177,50,46,134,190,156,223,91,220,228,8,70,192,66,114,154,145,94,12,41,171,181,53,26,116,100,186,65,234,29,26,172,51,247,146,124,137,196,6,5,126,217,148,181,241,91,238,198,49,71,14,224,73,224,228,229,29,10,215,105,45,224,183,23,104,29,125,36,140,18,74,89,27,54,168,63,89,79,176,139,153,171,10,36,181,182,56,58,35,47,164,193,123,87,63,142,74,162,35,192,1,114,228,44,98,100,112,132,39,120,38,82,117,50,43,74,174,127,169,189,187,207,254,163,11,137,196,148,239,18,148,168,145,45,122,109,20,227,6,246,134,119,77,198,213,125,30,192,108,65,185,250,111,133,129,41,30,112,22,123,191,118,173,111,149,180,57,173,148,142,120,136,110,211,30,171,177,143,237,183,187,114,138,239,27,56,214,186,222,221,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b3d865fa-7420-4592-95b7-46991bf8d712"));
		}

		#endregion

	}

	#endregion

}

