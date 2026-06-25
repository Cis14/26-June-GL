namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: Float8ColumnProcessor

	/// <inheritdoc cref="FloatColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Float8 column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(Float8ColumnProcessor))]
	public class Float8ColumnProcessor : FloatColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="FloatColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="Float8ColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public Float8ColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.Float8DataValueTypeUId;

		#endregion

	}

	#endregion

}
