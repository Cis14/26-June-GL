namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core;

	#region Class: PersistentColumnsProcessorAdapter

	public class PersistentColumnsAggregatorAdapter : PersistentColumnsAggregator, IColumnsAggregatorAdapter
	{

		#region Constructors: Public

		/// <inheritdoc />
		/// <summary>
		/// Creates instance of type <see cref="PersistentColumnsAggregatorAdapter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="nonPersistentColumnProcessors"></param>
		/// <param name="persistentColumnProcessors"></param>
		public PersistentColumnsAggregatorAdapter(UserConnection userConnection,
			IEnumerable<INonPersistentColumnProcess> nonPersistentColumnProcessors,
			IEnumerable<IPersistentColumnProcessor> persistentColumnProcessors)
			: base(userConnection, nonPersistentColumnProcessors, persistentColumnProcessors) { }

		/// <inheritdoc />
		/// <summary>
		/// Creates instance of type <see cref="PersistentColumnsAggregatorAdapter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public PersistentColumnsAggregatorAdapter(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IColumnsAggregatorAdapter"/>
		public void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
			ImportColumnDestination destination) { }

		/// <inheritdoc cref="IColumnsAggregatorAdapter"/>
		public void Process() { }

		#endregion

	}

	#endregion

}

