namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core;

	#region  Class: NonPersistentColumnsAggregatorAdapter

	/// <inheritdoc cref="ColumnsProcessor"/>
	public class NonPersistentColumnsAggregatorAdapter : ColumnsProcessor, IColumnsAggregatorAdapter
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="NonPersistentColumnsAggregatorAdapter"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="columnProcessors"></param>
		public NonPersistentColumnsAggregatorAdapter(UserConnection userConnection,
				IEnumerable<IColumnProcessor> columnProcessors)
				: base(userConnection, columnProcessors) { }

		/// <inheritdoc/>
		/// <summary>
		/// Creates instance of type <see cref="NonPersistentColumnsAggregatorAdapter"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public NonPersistentColumnsAggregatorAdapter(UserConnection userConnection)
				: base(userConnection) { }

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IColumnsAggregatorAdapter"/>
		public void Process(ImportParameters importParameters) { }

		#endregion

	}

	#endregion

}

