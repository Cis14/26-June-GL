namespace Creatio.ComponentCopilot
{
	using System;
	using System.Collections.Generic;
	using Creatio.Copilot.Actions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Newtonsoft.Json;

	/// <summary>
	/// A custom executable code action that demonstrates how to extend BaseExecutableCodeAction.
	/// </summary>
	public class FindLookupValueAction : BaseExecutableCodeAction
	{
		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="FindLookupValueAction"/> class.
		/// </summary>
		public FindLookupValueAction()
		{
			// Define the parameters required for this action
			Parameters = new List<SourceCodeActionParameter>
			{
				new SourceCodeActionParameter
				{
					Name = "tableName",
					Caption = new LocalizableString("Table name"),
					Description = new LocalizableString("Table name"),
					IsRequired = true,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId,
				},
				new SourceCodeActionParameter
				{
					Name = "lookupDisplayName",
					Caption = new LocalizableString("Lookup display name"),
					Description = new LocalizableString("The display name of the lookup"),
					IsRequired = true,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId,
				}
			}.AsReadOnly();
		}

		#endregion

		#region Methods: Private



		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public override LocalizableString GetCaption()
		{
			return new LocalizableString("Find lookup value");
		}

		/// <inheritdoc/>
		public override LocalizableString GetDescription()
		{
			return new LocalizableString("Finds lookup value.");
		}

		/// <inheritdoc/>
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options)
		{
			if (!options.ParameterValues.TryGetValue("tableName", out string tableName))
			{
				throw new ArgumentException("The 'tableName' parameter is required.");
			}
			if (!options.ParameterValues.TryGetValue("lookupDisplayName", out string lookupDisplayName))
			{
				throw new ArgumentException("The 'lookupDisplayName' parameter is required.");
			}
			var userConnection = ClassFactory.Get<UserConnection>();
			var entitySchema = userConnection.EntitySchemaManager.GetInstanceByName(tableName);
			var esq = new EntitySchemaQuery(entitySchema);
			esq.RowCount = 100;
			esq.PrimaryQueryColumn.IsVisible = true;
			esq.AddColumn(entitySchema.PrimaryDisplayColumn.Name);
			
			var displayColumnFilter = esq.CreateFilterWithParameters(FilterComparisonType.Contain, 
				entitySchema.PrimaryDisplayColumn.Name, lookupDisplayName);
			esq.Filters.Add(displayColumnFilter);
			
			var collection = esq.GetEntityCollection(userConnection);
			var result = new CompositeObjectList<CompositeObject>();
			foreach (Entity entity in collection) {
				var compositeObject = new CompositeObject();
				IEnumerable<string> columnValueNames = entity.GetColumnValueNames();
				foreach (string columnValueName in columnValueNames) {
					var columnValue = entity.FindEntityColumnValue(columnValueName);
					if (columnValue.IsLoaded) {
						compositeObject[columnValueName] = columnValue.Value;
					}
				}
				result.Add(compositeObject);
			}

			return new CopilotActionExecutionResult
			{
				Status = CopilotActionExecutionStatus.Completed,
				Response = JsonConvert.SerializeObject(result)
			};
		}

		#endregion

	}
}
