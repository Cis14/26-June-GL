namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;

	#region Class: WebFormProcessHandlersFactory

	
	
	
	
	public class WebFormProcessHandlersFactory : IWebFormProcessHandlersFactory
	{
		#region Properties: Protected

		private List<IGeneratedWebFormProcessHandler> _processHandlers;
		protected virtual List<IGeneratedWebFormProcessHandler> ProcessHandlers {
			get {
				return _processHandlers ?? (_processHandlers = new List<IGeneratedWebFormProcessHandler>());
			}
		}

		#endregion

		#region Methods: Protected

		protected virtual Select GetProcessHandlersQuery(UserConnection userConnection, Guid webFormId) {
			var select = new Select(userConnection)
					.Column("FullClassName")
				.From("WebFormProcessHandlers")
				.Join(JoinType.Inner, "GeneratedWebForm").As("gwf")
					.On("gwf", "Id").IsEqual(Column.Parameter(webFormId))
				.Join(JoinType.Inner, "LandingType").As("lt")
					.On("lt", "Id").IsEqual("gwf", "TypeId")
				.Join(JoinType.Inner, "SysSchema").As("ss")
					.On("ss", "UId").IsEqual("lt", "SchemaUidId")
				.Join(JoinType.Inner, "SysPackage").As("sp")
					.On("ss", "SysPackageId").IsEqual("sp", "Id")
				.Where("WebFormProcessHandlers", "IsActive").IsEqual(Column.Parameter(true))
					.And("WebFormProcessHandlers", "EntityName").IsEqual("ss", "Name")
					.And("sp", "SysWorkspaceId").IsEqual(Column.Parameter(userConnection.Workspace.Id)) as Select;
			return select;
		}

		protected void ReadData(IDataReader reader) {
			string className = reader.GetColumnValue<string>("FullClassName");
			var handler = ClassFactory.ForceGet<IGeneratedWebFormProcessHandler>(className);
			ProcessHandlers.Add(handler);
		}

		protected virtual void LoadHandlers(UserConnection userConnection, Guid webFormId) {
			Select select = GetProcessHandlersQuery(userConnection, webFormId);
			select.ExecuteReader((reader) => ReadData(reader));
		}

		protected IEnumerable<T> GetProcessHandlers<T>(UserConnection userConnection, Guid webFormId) {
			if (_processHandlers == null) {
				LoadHandlers(userConnection, webFormId);
			}
			return ProcessHandlers.Where(handler => handler is T).Cast<T>();
		}

		#endregion

		#region Methods: Public

		
		
		
		
		
		
		public IEnumerable<IGeneratedWebFormPreProcessHandler> GetPreProcessHandlers(UserConnection userConnection,
				Guid webFormId) {
			return GetProcessHandlers<IGeneratedWebFormPreProcessHandler>(userConnection, webFormId);
		}

		
		
		
		
		
		
		public IEnumerable<IGeneratedWebFormPostProcessHandler> GetPostProcessHandlers(UserConnection userConnection,
				Guid webFormId) {
			return GetProcessHandlers<IGeneratedWebFormPostProcessHandler>(userConnection, webFormId);
		}

		#endregion

	}

	#endregion

}

