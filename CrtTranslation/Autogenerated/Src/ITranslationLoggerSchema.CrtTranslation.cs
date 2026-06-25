namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationLoggerSchema

	/// <exclude/>
	public class ITranslationLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationLoggerSchema(ITranslationLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("fee74088-6ba4-462a-9370-fec3c8e1fb8d");
			Name = "ITranslationLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5bb0d1bd-fb48-4884-8a1c-84058bcf48c3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,144,65,10,194,48,16,69,215,22,188,195,128,27,221,244,0,117,37,234,162,160,32,234,5,210,116,26,35,109,166,76,18,81,138,119,55,166,88,42,186,74,254,252,55,127,38,49,162,65,219,10,137,112,70,102,97,169,114,233,154,76,165,149,103,225,52,153,244,204,194,216,58,222,167,73,55,77,38,222,106,163,224,244,176,14,155,229,160,199,237,140,227,174,192,4,106,198,168,130,128,220,56,228,42,12,204,32,31,65,59,82,10,57,146,173,47,106,45,65,127,192,191,220,164,139,236,16,187,71,119,161,210,102,112,136,221,189,121,35,93,194,6,11,175,230,84,92,81,58,8,143,181,66,225,98,57,216,91,102,226,249,246,46,177,125,231,195,175,151,175,228,219,57,162,245,181,3,142,199,8,202,77,69,127,226,227,114,104,202,126,191,168,159,253,71,124,21,67,237,5,122,210,8,212,131,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("fee74088-6ba4-462a-9370-fec3c8e1fb8d"));
		}

		#endregion

	}

	#endregion

}

