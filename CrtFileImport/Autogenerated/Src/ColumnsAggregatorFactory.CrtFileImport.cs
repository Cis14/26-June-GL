namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <inheritdoc cref="IColumnsAggregatorFactory"/>
	/// <summary>
	/// Implements interface for create columns aggregator
	/// </summary>
	[DefaultBinding(typeof(IColumnsAggregatorFactory), Name = nameof(ColumnsAggregatorFactory))]
	public class ColumnsAggregatorFactory : IColumnsAggregatorFactory
	{
		/// <inheritdoc cref="IColumnsAggregatorFactory"/>
		public IColumnsAggregatorAdapter GetColumnsAggregator(UserConnection userConnection) {
			IColumnsAggregatorAdapter _columnsProcessor;
			if (userConnection.GetIsFeatureEnabled("UsePersistentFileImport")) {
				_columnsProcessor = new PersistentColumnsAggregatorAdapter(userConnection);
			} else {
				_columnsProcessor = new NonPersistentColumnsAggregatorAdapter(userConnection);
			}
			return _columnsProcessor;
		}
	}
}

