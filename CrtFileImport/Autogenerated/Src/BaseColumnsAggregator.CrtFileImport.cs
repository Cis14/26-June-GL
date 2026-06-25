namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: BaseColumnsAggregator

	public abstract class BaseColumnsAggregator<T> : IBaseColumnsAggregator
			where T : IBaseColumnProcessor
	{

		#region Fields: Private

		private List<T> _columnProcessors;

		#endregion

		#region Constructors: Protected

		/// <summary>
		/// Creates instance of type <see cref="BaseColumnsAggregator{T}"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected BaseColumnsAggregator(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Column processors.
		/// </summary>
		protected List<T> ColumnProcessors {
			get {
				if (_columnProcessors != null) {
					return _columnProcessors;
				}
				_columnProcessors = GetColumnProcessors();
				return _columnProcessors;
			}
		}

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Events: Public
	
		public event EventHandler<ColumnProcessErrorEventArgs> ProcessError;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets column processor that can work with given entity schema column.
		/// </summary>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <returns>Column processor.</returns>
		private T GetColumnProcessor(EntitySchemaColumn entitySchemaColumn) {
			return ColumnProcessors.First(columnProcessor => columnProcessor.CanProcess(entitySchemaColumn));
		}

		private void OnProcessError(ColumnProcessErrorEventArgs eventArgs) {
			ProcessError?.Invoke(this, eventArgs);
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets column processor that can work with given destination.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Column processor.</returns>
		protected T GetColumnProcessor(ImportColumnDestination destination) {
			return ColumnProcessors.First(columnProcessor => columnProcessor.CanProcess(destination));
		}

		/// <summary>
		/// Returns list of columns processors
		/// </summary>
		/// <returns></returns>
		protected abstract List<T> GetColumnProcessors();

		/// <summary>
		/// Handles column processor error.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs">
		/// <see cref="ColumnProcessErrorEventArgs"/>
		/// </param>
		protected void HandleProcessError(object sender, ColumnProcessErrorEventArgs eventArgs) {
			OnProcessError(eventArgs);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IBaseColumnsAggregator"/>
		public virtual ImportColumn CreateColumn(EntitySchema rootSchema, EntitySchemaColumn entitySchemaColumn,
				ImportColumnValue columnValue) {
			if (entitySchemaColumn == null) {
				return CreateColumn(columnValue);
			}
			T columnProcessor = GetColumnProcessor(entitySchemaColumn);
			return columnProcessor.CreateColumn(rootSchema, entitySchemaColumn, columnValue);
		}

		/// <inheritdoc cref="IBaseColumnsAggregator"/>
		public ImportColumn CreateColumn(ImportColumnValue columnValue) => new ImportColumn {
				Index = columnValue.ColumnIndex,
				Source = columnValue.Value
		};

		/// <inheritdoc cref="IBaseColumnsAggregator"/>
		public virtual object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			T columnProcessor = GetColumnProcessor(destination);
			return columnProcessor.FindValueForSave(destination, columnValue);
		}

		#endregion

	}

	#endregion
}

