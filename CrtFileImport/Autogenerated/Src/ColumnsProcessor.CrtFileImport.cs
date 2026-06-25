namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: ColumnsProcessor

	/// <summary>
	/// An instance of this class is responsible for processing columns values.
	/// </summary>
	public class ColumnsProcessor : BaseColumnsAggregator<IColumnProcessor>, INonPersistentColumnsAggregator
	{
		#region Fields: Private

		private readonly IEnumerable<IColumnProcessor> _columnProcessors;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ColumnsProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public ColumnsProcessor(UserConnection userConnection) : base(userConnection) { }

		public ColumnsProcessor(UserConnection userConnection, IEnumerable<IColumnProcessor> columnProcessors)
			: this(userConnection) {
			_columnProcessors = columnProcessors;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets column processors.
		/// </summary>
		/// <returns>Column processors.</returns>
		protected override List<IColumnProcessor> GetColumnProcessors() {
			var columnProcessors = new List<IColumnProcessor>();
			if (_columnProcessors == null) {
				var workspaceTypeProvider = ClassFactory.Get<IWorkspaceTypeProvider>();
				var processorTypes = workspaceTypeProvider.GetTypes();
				foreach (Type type in processorTypes.Where(t => typeof(IColumnProcessor).IsAssignableFrom(t) && !t.IsInterface)) {
					IColumnProcessor columnProcessor = (IColumnProcessor)Activator.CreateInstance(type, UserConnection);
					columnProcessors.Add(columnProcessor);
				}
			} else {
				columnProcessors.AddRange(_columnProcessors);
			}
			columnProcessors.ForEach((columnProcessor) => columnProcessor.ProcessError += HandleProcessError);
			return columnProcessors;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="INonPersistentColumnsAggregator"/>
		public void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
				ImportColumnDestination destination) {
			IColumnProcessor columnProcessor = GetColumnProcessor(destination);
			columnProcessor.Add(importEntity, column, columnValue, destination);
		}

		/// <inheritdoc cref="INonPersistentColumnsAggregator"/>
		public void Process() {
			foreach (IColumnProcessor columnProcessor in ColumnProcessors) {
				columnProcessor.Process();
			}
		}

		#endregion

	}

	#endregion

}
