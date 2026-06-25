namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationActualizersFactorySchema

	/// <exclude/>
	public class ITranslationActualizersFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationActualizersFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationActualizersFactorySchema(ITranslationActualizersFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("eb492bfc-0b97-4d2d-85d4-269f62222c69");
			Name = "ITranslationActualizersFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("ab054f7f-9309-4520-a5a0-bfba22ceff76");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,193,10,194,48,12,134,207,27,236,29,10,187,232,101,15,48,79,34,40,59,8,59,236,5,98,155,205,194,108,71,154,9,42,190,187,165,101,67,217,192,91,242,39,223,255,135,24,184,161,27,64,162,104,144,8,156,109,185,56,88,211,234,110,36,96,109,77,209,16,24,215,135,58,75,95,89,154,165,73,78,216,249,86,84,134,145,90,15,151,162,250,90,219,75,30,161,215,79,36,119,4,201,150,30,129,26,198,75,175,165,208,19,244,151,73,98,218,28,119,70,190,90,229,74,81,7,167,56,92,55,17,39,228,213,193,102,187,91,114,53,217,187,86,11,106,146,103,38,71,163,226,45,161,127,199,103,252,136,94,251,0,157,77,209,35,83,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("eb492bfc-0b97-4d2d-85d4-269f62222c69"));
		}

		#endregion

	}

	#endregion

}

