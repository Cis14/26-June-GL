namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: MoneyColumnProcessor

	/// <inheritdoc cref="FloatColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Money column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(MoneyColumnProcessor))]
	public class MoneyColumnProcessor : FloatColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="FloatColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="MoneyColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public MoneyColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.MoneyDataValueTypeUId;

		#endregion

	}

	#endregion

}
