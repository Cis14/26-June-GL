namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: Float1ColumnProcessor

	/// <inheritdoc cref="FloatColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Float1 column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(Float1ColumnProcessor))]
	public class Float1ColumnProcessor : FloatColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="FloatColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="Float1ColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public Float1ColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		protected override Guid DataValueTypeUId => DataValueType.Float1DataValueTypeUId;

		#endregion

	}

	#endregion

}
