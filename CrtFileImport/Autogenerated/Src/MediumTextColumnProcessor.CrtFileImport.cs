namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: MediumTextColumnProcessor

	/// <inheritdoc cref="TextColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing MediumText column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(MediumTextColumnProcessor))]
	public class MediumTextColumnProcessor : TextColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="TextColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="MediumTextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public MediumTextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.MediumTextDataValueTypeUId;

		#endregion

	}

	#endregion

}
