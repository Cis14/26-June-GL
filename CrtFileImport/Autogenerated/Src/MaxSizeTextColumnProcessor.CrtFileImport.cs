namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: MaxSizeTextColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing MaxSizeText column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(MaxSizeTextColumnProcessor))]
	public class MaxSizeTextColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="MaxSizeTextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public MaxSizeTextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected
		
		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.MaxSizeTextDataValueTypeUId;

		#endregion

	}

	#endregion

}
