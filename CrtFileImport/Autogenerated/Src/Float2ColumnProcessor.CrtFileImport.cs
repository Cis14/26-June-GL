namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: Float2ColumnProcessor

	/// <inheritdoc cref="FloatColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Float2 column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(Float2ColumnProcessor))]
	public class Float2ColumnProcessor : FloatColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="FloatColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="Float2ColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public Float2ColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.Float2DataValueTypeUId;

		#endregion

	}

	#endregion

}
