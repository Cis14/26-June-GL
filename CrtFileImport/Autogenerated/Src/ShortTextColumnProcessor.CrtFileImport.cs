namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: ShortTextColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing ShortText column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(ShortTextColumnProcessor))]
	public class ShortTextColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="ShortTextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public ShortTextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.ShortTextDataValueTypeUId;

		#endregion

	}

	#endregion

}
