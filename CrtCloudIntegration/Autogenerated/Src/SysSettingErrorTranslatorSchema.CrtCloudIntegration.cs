namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SysSettingErrorTranslatorSchema

	/// <exclude/>
	public class SysSettingErrorTranslatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SysSettingErrorTranslatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SysSettingErrorTranslatorSchema(SysSettingErrorTranslatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1fd68ecf-fbcc-4ad5-92f1-9d2e6b350bf8");
			Name = "SysSettingErrorTranslator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,146,209,106,194,48,20,134,175,83,216,59,28,116,23,21,74,31,64,217,205,234,38,130,23,3,117,187,44,49,61,147,176,44,41,39,169,160,226,187,47,109,212,98,171,187,10,57,249,207,249,191,63,137,230,191,104,75,46,16,50,114,153,50,85,49,215,14,183,196,157,52,58,93,59,169,164,147,104,159,162,227,83,196,42,43,245,246,174,112,234,204,228,95,193,117,82,250,70,100,200,182,234,21,18,113,107,190,93,154,25,66,95,247,39,67,194,173,239,130,76,113,107,199,176,220,219,37,58,231,213,77,243,138,184,182,138,59,67,141,184,172,54,74,10,16,181,246,177,116,12,175,220,98,167,152,192,188,55,145,213,73,175,4,239,18,85,225,17,62,72,238,184,195,198,144,149,97,3,132,188,48,90,237,187,174,144,99,189,76,238,106,215,22,41,51,90,163,168,47,6,242,234,102,31,242,179,33,234,34,16,156,29,67,198,135,233,226,46,65,3,144,116,205,110,189,70,208,68,101,129,22,94,160,165,102,29,44,127,216,227,100,236,116,3,247,133,155,165,17,63,232,252,103,128,11,26,198,23,151,29,39,40,208,10,146,101,61,96,33,14,126,232,243,160,5,207,143,1,36,109,75,51,50,85,121,154,235,29,87,178,248,228,170,194,124,218,142,24,4,84,66,87,145,255,43,254,134,93,120,96,15,16,207,208,45,140,240,125,7,190,81,216,244,198,141,188,151,45,129,158,111,102,10,76,58,180,163,209,37,243,57,119,251,70,209,41,250,3,190,194,160,152,72,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1fd68ecf-fbcc-4ad5-92f1-9d2e6b350bf8"));
		}

		#endregion

	}

	#endregion

}

