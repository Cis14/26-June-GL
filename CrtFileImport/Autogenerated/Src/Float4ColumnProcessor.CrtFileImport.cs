namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: Float3ColumnProcessor

	/// <inheritdoc cref="FloatColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Float3 column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(Float4ColumnProcessor))]
	public class Float4ColumnProcessor : FloatColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="FloatColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="Float4ColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public Float4ColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.Float4DataValueTypeUId;

		#endregion

	}

	#endregion

}
