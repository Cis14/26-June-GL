namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core;

	public abstract class NonPersistentColumnProcessor<TResult> : BaseColumnProcessor, IBaseColumnProcessor, INonPersistentColumnProcess
	{

		#region Fields: Private

		private Dictionary<string, TResult> _results;

		#endregion

		#region Constructors: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="NonPersistentColumnProcessor{TResult}"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected NonPersistentColumnProcessor(UserConnection userConnection) : base(userConnection) { }

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Results.
		/// </summary>
		protected Dictionary<string, TResult> Results => _results ?? (_results = new Dictionary<string, TResult>());

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Add converted value to results collection
		/// </summary>
		/// <param name="columnValue"><see cref="ImportColumnValue"/></param>
		/// <param name="valueForSave">object to save</param>
		protected virtual void AddToResults(ImportColumnValue columnValue, TResult valueForSave) {
			var value = columnValue.Value;
			if (!Results.ContainsKey(value)) {
				Results.Add(columnValue.Value, valueForSave);
			}
			if (valueForSave == null) {
				SendProcessError(columnValue);
			}
		}

		/// <summary>
		/// Convert column value to result's type
		/// </summary>
		/// <param name="columnValue"></param>
		/// <returns></returns>
		protected virtual TResult ConvertValue(ImportColumnValue columnValue) {
			return default(TResult);
		}

		#endregion

		#region Public 

		/// <inheritdoc cref="IProcessedValuesProvider"/>
		public object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			if (Results.TryGetValue(columnValue.Value, out TResult savedValue)) {
				if (savedValue == null) {
					SendProcessError(columnValue);
				}
				return savedValue;
			}
			TResult convertedValue = ConvertValue(columnValue);
			AddToResults(columnValue, convertedValue);
			return convertedValue;
		}

		/// <inheritdoc cref="INonPersistentColumnProcess"/>
		public virtual void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
			ImportColumnDestination destination) {
		}

		/// <inheritdoc cref="INonPersistentColumnProcess"/>
		public virtual void Process() { }

		#endregion
	}

}
