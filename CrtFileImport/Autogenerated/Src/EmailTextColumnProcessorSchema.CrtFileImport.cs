namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: EmailTextColumnProcessorSchema

	/// <exclude/>
	public class EmailTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public EmailTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public EmailTextColumnProcessorSchema(EmailTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("70d219b6-f9e4-471d-9053-15d73a72c898");
			Name = "EmailTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,107,227,48,16,133,207,41,244,63,12,217,75,2,139,125,79,147,64,155,110,151,64,41,133,38,189,148,61,40,242,56,29,176,37,239,72,42,27,74,255,123,199,114,156,214,217,184,208,147,165,225,205,123,154,79,178,81,37,186,74,105,132,21,50,43,103,115,159,44,172,201,105,27,88,121,178,38,185,161,2,151,101,101,217,159,159,189,158,159,13,130,35,179,133,135,157,243,88,94,28,246,159,187,25,251,234,201,141,210,222,50,161,19,133,104,126,48,110,37,3,22,133,114,110,2,191,74,69,197,10,255,249,133,45,66,105,238,217,106,116,206,114,212,166,105,10,83,50,207,200,228,51,171,65,51,230,179,225,9,245,48,157,183,114,23,202,82,241,174,221,95,26,32,227,188,50,50,174,205,193,63,147,3,93,71,131,44,88,56,88,227,104,83,32,228,150,161,106,252,234,33,110,173,217,214,65,160,99,18,188,168,34,160,75,218,148,244,83,204,211,53,230,42,20,254,138,76,38,173,35,191,171,208,230,163,229,209,25,199,63,225,78,200,195,12,140,124,68,208,55,250,120,252,71,92,171,176,41,72,239,207,218,39,133,9,156,100,55,120,141,252,62,96,203,148,158,67,125,17,194,252,62,90,55,138,111,34,254,143,113,44,44,24,149,71,215,37,45,20,68,137,184,183,236,27,65,124,147,131,113,122,236,60,173,20,171,50,18,155,13,131,67,150,73,12,234,250,149,14,231,107,217,203,253,180,133,100,154,70,117,108,222,227,235,75,29,173,59,94,208,181,30,11,215,141,114,56,58,46,215,63,195,224,109,207,22,77,214,224,237,178,150,140,10,217,203,131,159,212,107,47,189,152,125,5,251,74,146,190,1,251,90,121,213,60,199,134,113,48,244,87,214,148,161,241,148,19,114,15,206,170,61,11,216,23,249,67,69,15,191,3,101,209,239,177,182,91,137,219,122,153,193,108,222,173,37,7,136,199,210,139,147,36,26,62,221,162,212,222,1,239,101,157,198,119,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("70d219b6-f9e4-471d-9053-15d73a72c898"));
		}

		#endregion

	}

	#endregion

}

