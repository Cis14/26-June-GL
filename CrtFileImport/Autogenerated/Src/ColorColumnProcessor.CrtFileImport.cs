namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: ColorColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Color column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(ColorColumnProcessor))]
	public class ColorColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="ColorColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public ColorColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.ColorDataValueTypeUId;

		#endregion

	}

	#endregion

}
