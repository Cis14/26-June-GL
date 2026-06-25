namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: PersistentColumnsAggregator

	/// <summary>
	/// An instance of this class is responsible for processing columns values.
	/// </summary>
	public class PersistentColumnsAggregator : BaseColumnsAggregator<IBaseColumnProcessor>, IPersistentColumnsAggregator
	{

		#region Fields: Private

		private IEnumerable<INonPersistentColumnProcess> _nonPersistentColumnProcessors;

		private IEnumerable<IPersistentColumnProcessor> _persistentColumnProcessors;

		#endregion

		#region Private: Properties

		private IEnumerable<IPersistentColumnProcessor> PersistentColumnProcessors => _persistentColumnProcessors ??
				(_persistentColumnProcessors = GetPersistentColumnsProcessors());

		private IEnumerable<INonPersistentColumnProcess> NonPersistentColumnProcessors => _nonPersistentColumnProcessors ??
				(_nonPersistentColumnProcessors = GetNonPersistentColumnsProcessors());

		#endregion

		#region Constructors: Public

		/// <inheritdoc cref="BaseColumnsAggregator{T}"/>
		/// <summary>
		/// Creates instance of type <see cref="PersistentColumnsAggregator"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public PersistentColumnsAggregator(UserConnection userConnection) : base(userConnection) { }

		/// <inheritdoc cref="BaseColumnsAggregator{T}"/>
		/// <summary>
		/// Creates instance of type <see cref="PersistentColumnsAggregator"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="nonPersistentColumnProcessors">Collection of non persistent providers </param>
		/// <param name="persistentColumnProcessors">Collection of persistent providers</param>
		public PersistentColumnsAggregator(UserConnection userConnection,
			IEnumerable<INonPersistentColumnProcess> nonPersistentColumnProcessors,
			IEnumerable<IPersistentColumnProcessor> persistentColumnProcessors)
			: this(userConnection) {
			_nonPersistentColumnProcessors = nonPersistentColumnProcessors;
			_persistentColumnProcessors = persistentColumnProcessors;
		}

		#endregion

		#region Methods: Private

		private IEnumerable<IPersistentColumnProcessor> GetPersistentColumnsProcessors() {
			var columnProcessors = ClassFactory.GetAll<IPersistentColumnProcessor>(new ConstructorArgument("userConnection",
					UserConnection)).ToList();
			columnProcessors.ForEach(columnProcessor => AddHandlerProcessError(columnProcessor));
			return columnProcessors;
		}

		private IEnumerable<IColumnProcessor> GetNonPersistentColumnsProcessors() {
			var columnProcessors = ClassFactory.GetAll<IColumnProcessor>(new ConstructorArgument("userConnection",
					UserConnection)).ToList();
			columnProcessors.ForEach(columnProcessor => AddHandlerProcessError(columnProcessor));
			return columnProcessors;
		}

		private void AddHandlerProcessError(IBaseColumnProcessor columnProcessor) {
			columnProcessor.ProcessError += HandleProcessError;
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseColumnsAggregator{T}"/>
		protected override List<IBaseColumnProcessor> GetColumnProcessors() {
			var userConnectionArgs = new ConstructorArgument("userConnection", UserConnection);
			var processor = new List<IBaseColumnProcessor>();
			processor.AddRange(NonPersistentColumnProcessors.Select(columnProcessor => (IBaseColumnProcessor)columnProcessor));
			processor.AddRange(PersistentColumnProcessors.Select(columnProcessor => (IBaseColumnProcessor)columnProcessor));
			return processor;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IPersistentColumnsAggregator"/>
		public void Process(ImportParameters importParameters) {
			PersistentColumnProcessors.ForEach(columnProcessor => {
				columnProcessor.Process(importParameters);
			});
		}

		#endregion

	}

	#endregion

}

