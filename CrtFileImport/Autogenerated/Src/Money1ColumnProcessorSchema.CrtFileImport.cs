namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Money1ColumnProcessorSchema

	/// <exclude/>
	public class Money1ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Money1ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Money1ColumnProcessorSchema(Money1ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("510269b4-72a5-41cd-9957-8f51f1c97799");
			Name = "Money1ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,84,193,106,227,48,16,189,23,250,15,67,122,73,160,216,236,53,77,2,155,148,148,28,186,20,182,217,75,233,65,145,199,169,192,30,121,71,82,33,132,254,251,142,237,184,181,93,111,91,234,139,229,209,155,247,70,239,217,38,149,163,43,148,70,184,71,102,229,108,234,163,149,165,212,236,3,43,111,44,69,107,147,225,38,47,44,251,243,179,227,249,25,200,21,156,161,61,252,62,56,143,249,85,187,212,230,96,252,96,43,90,43,237,45,27,116,2,170,97,23,140,123,209,131,85,166,156,155,194,173,37,60,252,88,217,44,228,116,199,86,163,115,150,27,108,28,199,48,51,244,132,108,124,98,53,104,198,116,62,90,103,86,249,94,199,40,94,180,90,92,200,115,197,135,86,233,39,129,33,231,21,137,3,54,5,255,100,28,232,114,2,144,5,139,53,150,156,217,101,8,169,101,40,106,214,242,56,213,120,160,43,49,120,86,89,64,23,181,132,226,174,210,195,53,166,42,100,126,105,40,145,238,177,63,20,104,211,241,166,55,236,228,18,126,73,30,48,7,146,155,0,6,77,152,76,30,107,214,34,236,50,163,79,227,14,66,97,10,67,166,212,237,199,198,205,142,251,114,94,207,161,12,71,66,184,171,20,218,184,111,56,255,31,247,155,242,138,81,121,116,221,24,196,31,193,35,158,248,171,195,189,231,143,122,2,241,176,194,172,80,172,242,202,211,249,40,56,100,57,36,161,46,223,238,209,98,43,207,146,99,83,136,102,113,133,110,81,156,108,30,52,120,188,237,208,65,151,125,34,254,239,148,195,113,191,124,124,99,127,233,164,128,148,212,65,12,101,35,170,5,178,151,143,102,90,174,189,176,97,242,121,56,75,153,224,155,217,92,43,175,234,183,187,142,36,144,249,43,107,147,32,121,147,26,228,47,36,80,52,147,130,125,150,127,128,244,194,77,48,73,197,253,167,164,190,23,230,237,38,129,249,162,91,139,106,199,251,184,171,143,13,123,245,179,187,245,242,15,250,48,108,247,233,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("510269b4-72a5-41cd-9957-8f51f1c97799"));
		}

		#endregion

	}

	#endregion

}

