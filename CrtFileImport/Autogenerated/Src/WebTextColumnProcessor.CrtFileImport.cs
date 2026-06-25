namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: WebTextColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing LongText column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(WebTextColumnProcessor))]
	public class WebTextColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="WebTextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public WebTextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.WebTextDataValueTypeUId;

		#endregion

	}

	#endregion

}

