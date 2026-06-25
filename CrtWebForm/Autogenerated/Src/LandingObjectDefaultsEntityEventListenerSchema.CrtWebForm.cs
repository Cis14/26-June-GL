namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LandingObjectDefaultsEntityEventListenerSchema

	/// <exclude/>
	public class LandingObjectDefaultsEntityEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LandingObjectDefaultsEntityEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LandingObjectDefaultsEntityEventListenerSchema(LandingObjectDefaultsEntityEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ffbccb4e-3285-41e0-9bdf-349084f554a7");
			Name = "LandingObjectDefaultsEntityEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f00bb7e6-cace-4fbf-a3fe-7ba3de1f5d7a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,49,111,2,49,12,133,231,203,175,176,174,3,237,146,236,20,58,20,216,16,29,218,173,234,96,130,115,77,117,151,32,59,169,132,16,255,189,185,163,29,144,78,133,209,122,126,159,158,159,3,118,36,123,180,4,111,196,140,18,93,210,139,24,156,111,50,99,242,49,192,17,148,170,178,248,208,92,172,48,233,85,72,62,121,146,199,43,186,94,125,83,72,101,77,85,119,76,77,15,93,180,40,50,133,53,134,93,49,190,108,191,200,166,37,57,204,109,146,193,118,24,60,107,47,137,2,113,113,26,99,96,38,185,235,144,15,79,231,241,79,5,23,25,38,163,172,9,208,64,3,26,34,232,95,142,185,4,205,132,8,91,137,96,153,220,188,254,255,12,253,140,66,87,19,215,96,10,252,125,68,185,127,181,159,212,225,166,20,15,115,168,71,99,215,15,31,170,218,231,109,235,45,216,190,170,155,155,154,194,77,241,224,120,234,191,65,97,119,126,136,82,39,245,3,165,126,3,195,11,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ffbccb4e-3285-41e0-9bdf-349084f554a7"));
		}

		#endregion

	}

	#endregion

}

