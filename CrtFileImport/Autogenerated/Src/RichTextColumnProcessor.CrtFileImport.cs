namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: RichTextColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing LongText column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(RichTextColumnProcessor))]
	public class RichTextColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="RichTextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public RichTextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.RichTextDataValueTypeUId;

		#endregion

	}

	#endregion

}

