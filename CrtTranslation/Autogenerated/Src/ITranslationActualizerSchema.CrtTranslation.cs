namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ITranslationActualizerSchema

	/// <exclude/>
	public class ITranslationActualizerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ITranslationActualizerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ITranslationActualizerSchema(ITranslationActualizerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e2bd260d-8139-4e57-b269-fae5dc20c315");
			Name = "ITranslationActualizer";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6d72ca66-8680-4c3f-b4a0-15ba7a19db7c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,84,93,139,219,48,16,124,190,131,251,15,75,250,146,64,177,223,47,105,224,72,239,168,161,133,194,165,237,179,34,175,29,81,91,50,171,85,74,122,220,127,239,90,206,135,157,228,142,208,55,89,154,157,29,205,172,108,85,141,190,81,26,97,137,68,202,187,130,147,133,179,133,41,3,41,54,206,38,75,82,214,87,113,125,119,251,114,119,123,19,188,177,37,60,111,61,99,61,61,124,247,203,9,147,71,203,134,13,122,1,8,228,3,97,41,245,144,89,70,42,164,219,61,100,61,222,7,205,65,85,230,47,82,68,55,97,85,25,13,102,15,126,19,123,243,18,241,7,250,199,13,90,246,247,240,61,18,116,103,216,238,117,39,95,148,205,43,164,217,47,50,140,95,157,110,105,212,170,194,159,170,10,24,17,15,84,250,57,92,62,39,114,52,189,204,8,34,202,108,12,111,69,167,254,141,249,116,39,11,109,222,41,27,202,252,134,188,118,249,137,206,52,77,97,230,67,93,43,218,206,247,27,135,203,122,224,163,5,201,1,159,158,22,204,26,69,170,6,43,169,126,26,169,125,245,19,185,122,52,95,174,17,114,197,8,236,192,179,34,134,61,32,178,66,33,168,100,150,70,134,72,184,113,38,63,74,232,101,48,254,44,52,75,83,35,12,90,76,166,215,93,165,58,90,11,155,214,91,255,198,141,134,2,78,19,241,227,73,204,227,255,90,66,225,8,20,248,6,181,41,100,218,116,168,56,200,224,94,233,237,14,158,217,194,117,206,238,54,100,106,133,184,238,114,234,155,121,198,96,252,147,35,141,63,154,54,147,209,60,179,185,209,178,242,240,103,45,243,33,67,37,49,21,45,2,66,132,188,19,205,185,51,153,60,207,197,81,34,244,228,126,132,149,115,21,12,218,191,155,220,51,242,96,252,218,225,225,171,141,234,21,138,81,189,33,62,187,142,244,201,124,15,177,88,43,91,98,62,142,191,146,109,95,193,228,242,3,123,237,254,53,131,205,215,127,78,104,209,244,225,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e2bd260d-8139-4e57-b269-fae5dc20c315"));
		}

		#endregion

	}

	#endregion

}

