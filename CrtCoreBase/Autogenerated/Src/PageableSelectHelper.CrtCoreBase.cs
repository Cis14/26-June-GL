using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Factories;

namespace Terrasoft.Configuration.PageableSelectHelper
{

	#region Class: PageableSelectReadOptions

	/// <summary>
	/// Options for reading data in pages using PageableSelectHelper.
	/// </summary>
	public class PageableSelectReadOptions
	{
		/// <summary>
		/// Base select query.
		/// </summary>
		public Select BaseSelect { get; set; }

		/// <summary>
		/// Size of a page.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Alias/name of the Id column present in the rowset (e.g., "Id").
		/// </summary>
		public string IdColumnAlias { get; set; }

		/// <summary>
		/// Factory for Id column expression that matches the select (must be present in columns).
		/// </summary>
		public Func<QueryColumnExpression> GetIdExpression { get; set; }

		/// <summary>
		/// Optional hook for adding tie-breaker conditions for the next page.
		/// </summary>
		public Action<PageableSelectOptions, DataRow> ConfigureNextOptions { get; set; }

		/// <summary>
		/// Optional function returning a key used for detecting cursor repetition (infinite loop protection).
		/// Default uses last row's Id.
		/// </summary>
		public Func<DataRow, string> GetLoopGuardKey { get; set; }

		/// <summary>
		/// Number of last cursor keys to remember for loop protection. Default 64.
		/// </summary>
		public int MaxRememberedKeys { get; set; } = 64;

		/// <summary>
		/// DB connection kind.
		/// </summary>
		public QueryKind QueryKind { get; set; } = QueryKind.Limited;

		/// <summary>
		/// Cancellation token.
		/// </summary>
		public CancellationToken CancellationToken { get; set; } = default;
	}

	#endregion

	#region Interface: IPageableSelectHelper

	/// <summary>
	/// Helper class for reading data in pages using Creatio PageableSelect.
	/// </summary>
	public interface IPageableSelectHelper
	{

		/// <summary>
		/// Reads data in pages from the given options using pageable cursor logic.
		/// </summary>
		/// <param name="options">Options for reading pages.</param>
		/// <returns>An enumerable of data tables representing pages of data.</returns>
		IEnumerable<DataTable> ReadPages(PageableSelectReadOptions options);

	}

	#endregion

	#region Class: PageableSelectHelper

	/// <summary>
	/// Helper class for reading data in pages using Creatio PageableSelect.
	/// </summary>
	[DefaultBinding(typeof(IPageableSelectHelper))]
	public class PageableSelectHelper : IPageableSelectHelper
	{
		#region Class: Private

		private sealed class PagingContext
		{
			public Select BaseSelect { get; set; }
			public PageableSelectOptions Options { get; set; }
			public QueryParameter LastValueParameter { get; set; }
			public Select PageableSelect { get; set; }
		}

		#endregion

		#region Constants: Private

		private const string DefaultLastValueParameterName = "IdLastValue";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="PageableSelectHelper"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public PageableSelectHelper(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private PagingContext CreateContext(Select baseSelect, int pageSize, QueryColumnExpression idExpression) {
			var lastValueParameter = new QueryParameter(DefaultLastValueParameterName, null);

			var condition = new PageableSelectCondition {
				LastValueParameter = lastValueParameter,
				LeftExpression = idExpression,
				OrderByItem = new OrderByItem(idExpression, OrderDirectionStrict.Ascending)
			};

			var options = new PageableSelectOptions(condition, pageSize, PageableSelectDirection.First);
			Select pageableSelect = baseSelect.ToPageable(options);

			return new PagingContext {
				BaseSelect = baseSelect,
				Options = options,
				LastValueParameter = lastValueParameter,
				PageableSelect = pageableSelect
			};
		}

		private static DataTable ExecutePage(Select pageableSelect, DBExecutor dbExecutor) {
			using (IDataReader dr = pageableSelect.ExecuteReader(dbExecutor)) {
				var dt = new DataTable();
				dt.Load(dr);
				return dt;
			}
		}

		private static Guid GetLastRowIdOrThrow(DataRow lastRow, string idColumnAlias) {
			object raw = lastRow[idColumnAlias];
			if (raw == null || raw == DBNull.Value) {
				throw new InvalidOperationException($"Rowset can't contain a record with empty {idColumnAlias}");
			}
			if (raw is Guid guid) {
				return guid;
			}
			string value = raw.ToString();
			if (Guid.TryParse(value, out Guid parsed)) {
				return parsed;
			}
			throw new InvalidOperationException($"Rowset contains invalid {idColumnAlias} value: '{value}'");
		}

		private static void GuardAgainstInfiniteLoop(
			string key,
			Queue<string> recentKeys,
			HashSet<string> recentKeySet,
			int maxRememberedKeys) {

			if (key.IsNullOrEmpty()) {
				throw new InvalidOperationException("Rowset can't contain a record with empty Id");
			}

			if (recentKeySet.Contains(key)) {
				throw new InvalidOperationException($"Record with id {key} is uploading more than once which would cause infinite loop.");
			}

			recentKeys.Enqueue(key);
			recentKeySet.Add(key);

			while (recentKeys.Count > maxRememberedKeys) {
				string removed = recentKeys.Dequeue();
				recentKeySet.Remove(removed);
			}
		}

		private static void MoveToNextPage(
			PagingContext ctx,
			Guid lastId,
			DataRow lastRow,
			Action<PageableSelectOptions, DataRow> configureNextOptions) {

			ctx.LastValueParameter.Value = lastId;
			ctx.Options.Direction = PageableSelectDirection.Next;

			configureNextOptions?.Invoke(ctx.Options, lastRow);

			ctx.PageableSelect = ctx.BaseSelect.ToPageable(ctx.Options);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public IEnumerable<DataTable> ReadPages(PageableSelectReadOptions options) {
			options.CheckArgumentNull(nameof(options));
			options.BaseSelect.CheckArgumentNull(nameof(options.BaseSelect));
			options.GetIdExpression.CheckArgumentNull(nameof(options.GetIdExpression));
			options.IdColumnAlias.CheckArgumentNullOrEmpty(nameof(options.IdColumnAlias));

			if (options.PageSize <= 0) {
				throw new ArgumentOutOfRangeException(nameof(options.PageSize));
			}

			Func<DataRow, string> getLoopGuardKey = options.GetLoopGuardKey;
			if (getLoopGuardKey == null) {
				getLoopGuardKey = row => row[options.IdColumnAlias]?.ToString();
			}

			QueryColumnExpression idExpression = options.GetIdExpression();
			PagingContext ctx = CreateContext(options.BaseSelect, options.PageSize, idExpression);

			var recentKeys = new Queue<string>(Math.Min(options.MaxRememberedKeys, 128));
			var recentKeySet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection(options.QueryKind)) {
				while (true) {
					options.CancellationToken.ThrowIfCancellationRequested();

					DataTable page = ExecutePage(ctx.PageableSelect, dbExecutor);
					int count = page.Rows.Count;

					if (count == 0) {
						yield break;
					}

					DataRow lastRow = page.Rows[count - 1];

					Guid lastId = GetLastRowIdOrThrow(lastRow, options.IdColumnAlias);
					string guardKey = getLoopGuardKey(lastRow);

					GuardAgainstInfiniteLoop(
						guardKey,
						recentKeys,
						recentKeySet,
						options.MaxRememberedKeys);

					yield return page;

					if (count < options.PageSize) {
						yield break;
					}

					options.CancellationToken.ThrowIfCancellationRequested();

					MoveToNextPage(ctx, lastId, lastRow, options.ConfigureNextOptions);
				}
			}
		}

		#endregion

	}

	#endregion

}
