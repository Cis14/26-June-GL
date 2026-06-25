namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IErrorSchema

	/// <exclude/>
	public class IErrorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IErrorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IErrorSchema(IErrorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("22563143-447f-43d6-bc4b-b93b90c35ffb");
			Name = "IError";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,203,75,204,77,45,46,72,76,78,85,112,46,42,113,206,201,47,77,241,204,43,73,77,47,74,44,201,204,207,211,11,45,201,204,201,44,201,76,45,214,115,45,42,202,47,42,230,229,170,230,229,226,212,215,215,87,176,41,46,205,205,77,44,170,180,131,242,125,19,139,178,83,139,20,50,129,154,139,210,64,198,165,229,23,41,192,52,65,116,232,35,105,41,40,77,202,201,76,70,82,238,9,86,171,80,173,80,203,203,197,203,85,11,0,182,91,229,154,151,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("22563143-447f-43d6-bc4b-b93b90c35ffb"));
		}

		#endregion

	}

	#endregion

}

