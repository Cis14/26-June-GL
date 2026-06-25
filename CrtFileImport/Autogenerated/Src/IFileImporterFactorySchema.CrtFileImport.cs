namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImporterFactorySchema

	/// <exclude/>
	public class IFileImporterFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImporterFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImporterFactorySchema(IFileImporterFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("03765a8d-a731-806b-9649-74b2d943ce1c");
			Name = "IFileImporterFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,80,187,110,195,48,12,156,99,192,255,64,120,106,151,232,3,234,120,104,128,20,94,139,244,3,88,149,74,5,68,15,144,210,16,4,249,247,74,110,131,216,112,71,242,142,119,199,243,232,72,34,106,130,35,49,163,4,147,182,251,224,141,61,101,198,100,131,223,30,236,153,70,23,3,167,182,185,182,205,38,139,245,167,5,155,233,165,109,10,162,148,130,94,178,115,200,151,225,111,126,167,200,36,228,147,128,163,244,29,190,192,4,6,205,132,137,192,20,105,176,147,54,241,93,64,205,20,98,254,60,91,13,214,23,220,212,144,227,35,13,241,1,117,10,124,41,188,154,107,101,63,45,246,255,59,173,173,126,55,17,25,29,248,82,202,174,203,66,92,170,240,164,107,15,221,208,171,9,125,144,153,82,102,47,67,47,68,245,37,179,235,198,87,20,154,103,236,84,185,187,19,235,229,138,1,111,148,230,243,211,199,194,23,150,49,158,75,213,155,91,219,220,126,0,254,147,77,208,185,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("03765a8d-a731-806b-9649-74b2d943ce1c"));
		}

		#endregion

	}

	#endregion

}

