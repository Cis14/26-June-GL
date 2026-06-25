namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CopilotIntentTypeQueryExecutor

	/// <summary>
	/// Provides an in\-memory <see cref="EntityCollection"/> for the <c>CopilotIntentType</c> schema.
	/// </summary>
	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "CopilotIntentTypeQueryExecutor")]
	public class CopilotIntentTypeQueryExecutor : IEntityQueryExecutor
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create a new instance of the <see cref="CopilotIntentTypeQueryExecutor"/> class.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public CopilotIntentTypeQueryExecutor(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public EntityCollection GetEntityCollection(EntitySchemaQuery esq) {
			EntitySchemaManager entitySchemaManager = _userConnection.EntitySchemaManager;
			EntitySchema entitySchema = entitySchemaManager.GetInstanceByName("CopilotIntentType");
			var collection = new EntityCollection(_userConnection, entitySchema);
			IReadOnlyList<CopilotIntentType> allIntentTypes = CopilotIntentTypeConstants.GetAllCopilotIntentTypes();
			foreach (CopilotIntentType intentType in allIntentTypes) {
				Guid intentTypeId = CopilotIntentTypeConstants.GetIntentTypeId(intentType);
				string code = CopilotIntentTypeConstants.GetCode(intentType);
				LocalizableString intentTypeCaption = CopilotIntentTypeConstants
					.GetIntentTypeCaption(_userConnection, intentType);
				Entity entity = entitySchema.CreateEntity(_userConnection);
				entity.PrimaryColumnValue = intentTypeId;
				entity.SetColumnValue("Code", code);
				entity.SetColumnValue("Name", intentTypeCaption);
				collection.Add(entity);
			}
			return collection;
		}

		#endregion

	}

	#endregion

}

