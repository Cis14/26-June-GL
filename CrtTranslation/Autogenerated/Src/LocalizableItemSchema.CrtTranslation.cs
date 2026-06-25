namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LocalizableItemSchema

	/// <exclude/>
	public class LocalizableItemSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LocalizableItemSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LocalizableItemSchema(LocalizableItemSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("19dfea59-9b33-4952-b46b-b44061ba40b3");
			Name = "LocalizableItem";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6d72ca66-8680-4c3f-b4a0-15ba7a19db7c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,83,203,106,195,48,16,60,59,144,127,88,232,165,5,19,223,147,182,80,18,40,166,45,13,52,244,174,88,107,35,42,75,70,143,128,27,242,239,149,37,199,118,92,48,129,220,172,221,217,153,213,104,44,72,137,186,34,25,194,14,149,34,90,230,102,177,150,34,103,133,85,196,48,41,22,59,69,132,230,254,123,62,59,206,103,145,213,76,20,240,85,107,131,229,170,59,15,199,21,14,167,28,198,161,238,20,22,238,0,107,78,180,94,194,187,204,8,103,191,100,207,49,117,60,30,82,217,61,103,25,100,13,98,12,128,37,164,255,102,162,163,159,235,185,165,208,70,217,204,72,229,36,182,158,46,32,90,234,17,195,189,67,55,187,255,96,29,67,251,125,32,220,98,12,175,150,81,200,44,55,86,97,74,99,216,16,131,59,86,34,148,146,178,156,33,253,20,49,236,165,228,192,244,139,53,178,64,129,206,48,164,15,208,120,20,69,111,88,195,83,195,188,242,199,239,134,214,21,60,125,40,173,207,236,174,220,41,133,214,71,39,226,122,189,98,104,166,151,130,14,49,90,193,195,78,173,53,40,104,112,231,210,170,173,146,21,42,195,112,100,84,146,36,240,168,109,89,18,85,63,159,11,238,46,139,174,153,12,187,173,175,173,119,205,157,195,237,11,52,97,217,74,177,131,219,9,116,91,56,77,200,120,143,174,18,10,110,222,32,213,122,15,140,162,48,141,187,106,90,215,199,161,127,176,27,148,195,211,102,254,207,0,234,166,166,133,187,220,13,34,113,131,250,6,13,170,146,9,212,192,242,16,70,151,30,32,195,248,76,47,228,51,63,142,224,117,27,141,178,24,170,151,197,211,31,110,51,161,225,144,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("19dfea59-9b33-4952-b46b-b44061ba40b3"));
		}

		#endregion

	}

	#endregion

}

