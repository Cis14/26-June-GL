namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;

	#region Class: TranslationProvider

	public class TranslationProvider : ITranslationProvider, ITranslationErrorHandler
	{

		#region Constants: Private

		private const int MAX_ERRORMESSAGE_LENGTH = 500;

		#endregion

		#region Constants: Protected

		protected const string TranslationSchemaName = TranslationConst.TranslationSchemaName;
		protected const string TranslationKeyColumnName = TranslationConst.TranslationKeyColumnName;
		protected const string TranslationErrorColumnName = TranslationConst.TranslationErrorColumnName;
		protected const string TranslationModifiedOnColumnName = TranslationConst.TranslationModifiedOnColumnName;

		#endregion

		#region Constructors: Public

		public TranslationProvider(UserConnection userConnection, ITranslationLogger translationLogger) {
			UserConnection = userConnection;
			TranslationLogger = translationLogger;
		}

		#endregion

		#region Fields: Private

		private readonly Regex _translationCultureIndexRegex = new Regex(@"Language(?<Index>\d+)", RegexOptions.Compiled);

		#endregion

		#region Fields: Protected

		protected DBExecutor _dbExecutor;

		#endregion

		#region Properties: Private

		private Dictionary<string, Update> _translationUpdateQueries;
		private Dictionary<string, Update> TranslationUpdateQueries {
			get {
				return _translationUpdateQueries ?? (_translationUpdateQueries = new Dictionary<string, Update>());
			}
		}

		private Dictionary<string, Insert> _translationInsertQueries;
		private Dictionary<string, Insert> TranslationInsertQueries {
			get {
				return _translationInsertQueries ?? (_translationInsertQueries = new Dictionary<string, Insert>());
			}
		}

		#endregion

		#region Properties: Protected

		protected UserConnection UserConnection {
			get;
			private set;
		}

		protected ITranslationLogger TranslationLogger {
			get;
			private set;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets translation culture index.
		/// </summary>
		/// <param name="columnName">Translation key.</param>
		/// <param name="cultureIndex">System culture index.</param>
		/// <returns>
		/// <c>true</c>, if parameter <paramref name="cultureIndex"/> was initialized; <c>false</c> otherwise.
		/// </returns>
		private bool TryGetTranslationCultureIndex(string columnName, out int cultureIndex) {
			Match match = _translationCultureIndexRegex.Match(columnName);
			if (!match.Success) {
				cultureIndex = 0;
				return false;
			}
			Group indexMatch = match.Groups["Index"];
			return int.TryParse(indexMatch.Value, out cultureIndex);
		}

		private Select GetChangedTranslationsSelect(List<ISysCultureInfo> sysCulturesInfo) {
			Select select =
				new Select(UserConnection)
					.Column(TranslationModifiedOnColumnName)
					.Column(TranslationKeyColumnName)
				.From(TranslationSchemaName);
			QueryColumnExpression isTranslationChangedParameter = Column.Parameter(true);
			bool isConditionAdded = false;
			foreach (ISysCultureInfo sysCultureInfo in sysCulturesInfo) {
				string isChangedColumnName = sysCultureInfo.IsChangedColumnName;
				select
					.Column(sysCultureInfo.TranslationColumnName)
					.Column(isChangedColumnName);
				if (isConditionAdded) {
					select.Or(isChangedColumnName).IsEqual(isTranslationChangedParameter);
				} else {
					select.Where(isChangedColumnName).IsEqual(isTranslationChangedParameter);
					isConditionAdded = true;
				}
			}
			return select;
		}

		private void AddResetChangedStateColumns(Update tranlationsResetQuery, IEnumerable<ISysCultureInfo> sysCulturesInfo) {
			var resetStateValue = Column.Parameter(false);
			sysCulturesInfo.ForEach(sysCultureInfo => {
				string isChangedColumnName = sysCultureInfo.IsChangedColumnName;
				tranlationsResetQuery.Set(isChangedColumnName, resetStateValue);
			});
		}

		private void AddResetChangedStateConditions(Update tranlationsResetQuery, IEnumerable<ISysCultureInfo> sysCulturesInfo) {
			tranlationsResetQuery.Where(TranslationErrorColumnName).IsEqual(Column.Parameter(string.Empty));
			var changedLanguagesCondition = GetLanguageConditions(sysCulturesInfo);
			if (changedLanguagesCondition != null) {
				tranlationsResetQuery.And(changedLanguagesCondition);
			}
		}

		private static QueryCondition GetLanguageConditions(IEnumerable<ISysCultureInfo> sysCulturesInfo) {
			QueryCondition conditions = null;
			var rightExpression = Column.Parameter(true);
			sysCulturesInfo.ForEach(sysCultureInfo => {
				var condition = new QueryCondition(QueryConditionType.Equal) {
					LeftExpression = new QueryColumnExpression(sysCultureInfo.IsChangedColumnName),
					RightExpressions = {
						rightExpression
					}
				};
				if (conditions == null) {
					conditions = new QueryCondition(QueryConditionType.Block);
				} else {
					condition.LogicalOperation = LogicalOperation.Or;
				}
				conditions.Add(condition);
			});
			return conditions;
		}

		private int GetChunkSize() {
			int chunkSize = UserConnection.AppConnection.MaxEntityRowCount;
			if (Core.Configuration.SysSettings.TryGetValue(UserConnection, "ApplyTranslationChunkSize",
					out object result)) {
				chunkSize = (int)result;
			}
			return chunkSize;
		}

		private void WriteTranslationUsingQuery(string key, string columnName, string columnValue, string isChangedColumnName) {
			var select = GetTranslationSelectQuery(key, columnName);
			int rowsAffected = select.ExecuteScalar<int>();
			if (rowsAffected > 1) {
				DeleteTranslationRecord(key);
			}
			Update update = GetTranslationUpdateQuery(key, columnName, columnValue, isChangedColumnName);
			ExecuteQuery(update);
			if (rowsAffected < 1) {
				Insert insert = GetTranslationInsertQuery(key, columnName, columnValue);
				ExecuteQuery(insert);
			}
		}

		private Select GetTranslationSelectQuery(string key, string columnName) {
			var select =
				new Select(UserConnection)
					.Column(Func.Count(TranslationKeyColumnName))
				.From(TranslationSchemaName);
			AddFilterByKey(key, select);
			return select;
		}

		private void DeleteTranslationRecord(string key) {
			var select = 
				new Select(UserConnection)
					.Top(1)
					.Column("Id")
				.From(TranslationSchemaName);
			AddFilterByKey(key, select);
			var delete = new Delete(UserConnection)
				.From(TranslationSchemaName);
			AddFilterByKey(key, delete);
			delete.And("Id").Not().IsEqual(select);
			delete.Execute();
		}

		private void AddFilterByKey(string key, Query query) {
			if (UserConnection.DBEngine.DBEngineType == DBEngineType.PostgreSql) {
				query.Where(Func.Upper(TranslationKeyColumnName))
					.IsEqual(Func.Upper(Column.Parameter(key)));
			} else {
				query.Where(TranslationKeyColumnName)
					.IsEqual(Column.Parameter(key));
			}
		}

		#endregion

		#region Methods: Protected

		protected virtual Update GetTranslationUpdateQuery(string key, string columnName, string columnValue, string isChangedColumnName) {
			Update tranlationUpdateQuery;
			if (!TranslationUpdateQueries.TryGetValue(columnName, out tranlationUpdateQuery)) {
				tranlationUpdateQuery =
					(Update)new Update(UserConnection, TranslationSchemaName)
						.Set(columnName, new QueryParameter(columnName, null))
						.Where(TranslationKeyColumnName).IsEqual(new QueryParameter(TranslationKeyColumnName, null));
				tranlationUpdateQuery.InitializeParameters();
				TranslationUpdateQueries.Add(columnName, tranlationUpdateQuery);
			}
			tranlationUpdateQuery.SetParameterValue(TranslationKeyColumnName, key);
			tranlationUpdateQuery.SetParameterValue(columnName, columnValue);
			return tranlationUpdateQuery;
		}

		protected virtual Insert GetTranslationInsertQuery(string key, string columnName, string columnValue) {
			Insert tranlationInsertQuery;
			if (!TranslationInsertQueries.TryGetValue(columnName, out tranlationInsertQuery)) {
				tranlationInsertQuery =
					new Insert(UserConnection)
						.Into(TranslationSchemaName)
						.Set(TranslationKeyColumnName, new QueryParameter(TranslationKeyColumnName, null))
						.Set(columnName, new QueryParameter(columnName, null));
				tranlationInsertQuery.InitializeParameters();
				TranslationInsertQueries.Add(columnName, tranlationInsertQuery);
			}
			tranlationInsertQuery.SetParameterValue(TranslationKeyColumnName, key);
			tranlationInsertQuery.SetParameterValue(columnName, columnValue);
			return tranlationInsertQuery;
		}

		protected virtual int ExecuteQuery(IDBCommand query) {
			return _dbExecutor != null ? query.Execute(_dbExecutor) : query.Execute();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Writes translation.
		/// </summary>
		/// <param name="key">Resource key.</param>
		/// <param name="columnName">Translation column name.</param>
		/// <param name="columnValue">Translation column value.</param>
		public virtual void WriteTranslation(string key, string columnName, string columnValue) {
			WriteTranslationUsingQuery(key, columnName, columnValue, string.Empty);
		}

		public virtual void WriteTranslation(string key, string columnName, string columnValue,
				string isChangedColumnName) {
			WriteTranslationUsingQuery(key, columnName, columnValue, isChangedColumnName);
		}

		/// <summary>
		/// Selects changed translation columns values.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="readMethod">Translation column value processing method.</param>
		public void ReadChangedTranslationColumnsValues(Entity entity,
				Action<string, string, int, DateTime> readMethod) {
			List<EntityColumnValue> changedColumnValues = entity.GetChangedColumnValues().ToList();
			foreach (EntityColumnValue changedColumnValue in changedColumnValues) {
				EntitySchemaColumn column = changedColumnValue.Column;
				if (column.UsageType.Equals(EntitySchemaColumnUsageType.Advanced)) {
					continue;
				}
				int cultureIndex;
				if (!TryGetTranslationCultureIndex(column.Name, out cultureIndex)) {
					continue;
				}
				string value = (string)changedColumnValue.Value;
				if (value.IsNullOrEmpty() && entity.StoringState.Equals(StoringObjectState.New)) {
					continue;
				}
				string key = entity.GetTypedColumnValue<string>(TranslationKeyColumnName);
				DateTime modifiedOn = entity.GetTypedColumnValue<DateTime>(TranslationModifiedOnColumnName);
				readMethod(key, value, cultureIndex, modifiedOn);
			}
		}

		/// <summary>
		/// Reads changed translations.
		/// <param name="sysCulturesInfo">Entity.</param>
		/// <param name="readMethod">Entity.</param>
		/// </summary>
		public void ReadChangedTranslations(List<ISysCultureInfo> sysCulturesInfo,
				Action<string, string, int, DateTime> readMethod) {
			Select select = GetChangedTranslationsSelect(sysCulturesInfo);
			select.ExecuteReader(dataReader => {
				string key = dataReader.GetColumnValue<string>(TranslationKeyColumnName);
				DateTime modifiedOn = dataReader.GetColumnValue<DateTime>(TranslationModifiedOnColumnName);
				foreach (ISysCultureInfo sysCultureInfo in sysCulturesInfo) {
					bool isChanged = dataReader.GetColumnValue<bool>(sysCultureInfo.IsChangedColumnName);
					if (!isChanged) {
						continue;
					}
					string value = dataReader.GetColumnValue<string>(sysCultureInfo.TranslationColumnName);
					readMethod(key, value, sysCultureInfo.Index, modifiedOn);
				}
			});
		}

		/// <summary>
		/// Resets changed translations state.
		/// <param name="sysCulturesInfo">System cultures information.</param>
		/// </summary>
		public void ResetChangedTranslationsState(IEnumerable<ISysCultureInfo> sysCulturesInfo) {
			var tranlationsResetQuery = new Update(UserConnection, TranslationSchemaName);
			AddResetChangedStateColumns(tranlationsResetQuery, sysCulturesInfo);
			AddResetChangedStateConditions(tranlationsResetQuery, sysCulturesInfo);
			ExecuteQuery(tranlationsResetQuery);
		}

		public void ResetChangedTranslationsStateForRecord(ISysCultureInfo sysCultureInfo, string key) {
			Update translationsResetQuery = new Update(UserConnection, TranslationSchemaName)
				.Set(sysCultureInfo.IsChangedColumnName, Column.Parameter(false));
			if (UserConnection.DBEngine.DBEngineType == DBEngineType.PostgreSql) {
				translationsResetQuery.Where(Func.Upper(TranslationKeyColumnName))
					.IsEqual(Func.Upper(Column.Parameter(key)));
			} else {
				translationsResetQuery.Where(TranslationKeyColumnName)
					.IsEqual(Column.Parameter(key));
			}
			ExecuteQuery(translationsResetQuery);
		}

		/// <summary>
		/// Executes action in transaction.
		/// <param name="action">Action.</param>
		/// </summary>
		public virtual void Transaction(Action action) {
			using (_dbExecutor = UserConnection.EnsureDBConnection()) {
				_dbExecutor.StartTransaction();
				try {
					action();
					_dbExecutor.CommitTransaction();
				} catch (Exception e) {
					_dbExecutor.RollbackTransaction();
					TranslationLogger.Error(e);
					throw;
				}
			}
		}

		/// <inheritdoc/>
		public EntityCollection GetChangedTranslationsByCulture(ISysCultureInfo sysCultureInfo) {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, TranslationSchemaName);
			esq.AddColumn(TranslationModifiedOnColumnName);
			esq.AddColumn(TranslationKeyColumnName);
			esq.AddColumn(sysCultureInfo.TranslationColumnName);
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				sysCultureInfo.IsChangedColumnName, true));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				TranslationErrorColumnName, string.Empty));
			esq.RowCount = GetChunkSize();
			return esq.GetEntityCollection(UserConnection);
		}

		/// <inheritdoc/>
		public void SetForceUpdate(ISysCultureInfo sysCultureInfo) {
			var update = new Update(UserConnection, TranslationSchemaName)
				.Set(sysCultureInfo.IsChangedColumnName, Column.Parameter(true))
				.Where(sysCultureInfo.TranslationColumnName).Not().IsEqual(Column.Parameter(string.Empty)) as Update;
			update.Execute();
		}

		public void SaveErrors(Dictionary<string, string> erroneousRecords) {
			erroneousRecords.ForEach(errorPair => {
				string errorToSave = StringUtilities.Truncate(errorPair.Value, MAX_ERRORMESSAGE_LENGTH);
				var Update = GetTranslationUpdateQuery(errorPair.Key, TranslationErrorColumnName, errorToSave, string.Empty);
				Update.Execute();
			});
		}

		public void ResetErrors() {
			Update resetErrorsUpdate = new Update(UserConnection, TranslationSchemaName)
				.Set(TranslationErrorColumnName, new QueryParameter(TranslationErrorColumnName, null))
				.Where(TranslationErrorColumnName).IsNotEqual(Column.Parameter(string.Empty)) as Update;
			resetErrorsUpdate.InitializeParameters();
			resetErrorsUpdate.SetParameterValue(TranslationErrorColumnName, string.Empty);
			resetErrorsUpdate.Execute();
		}

		public bool GetIsAnyConfigurationResourceChanged(IEnumerable<ISysCultureInfo> sysCulturesInfo) {
			Select select = new Select(UserConnection).Top(1)
					.Column(Column.Const(1))
				.From("SysTranslation");
			var configurationResourceParameter = Column.Parameter("Configuration:%");
			if (UserConnection.DBEngine.DBEngineType == DBEngineType.PostgreSql) {
				select.Where(Func.Upper("Key")).IsLike(Func.Upper(configurationResourceParameter));
			} else {
				select.Where("Key").IsLike(configurationResourceParameter);
			}
			select.And(GetLanguageConditions(sysCulturesInfo));
			var result = select.ExecuteScalar<int>();
			return result > 0;
		}

		#endregion

	}

	#endregion

}
