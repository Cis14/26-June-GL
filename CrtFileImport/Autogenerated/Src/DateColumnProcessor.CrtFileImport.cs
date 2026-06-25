namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: DateColumnProcessor

	/// <inheritdoc cref="DateTimeColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Date column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(DateColumnProcessor))]
	public class DateColumnProcessor : DateTimeColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="DateTimeColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="DateColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public DateColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.DateDataValueTypeUId;

		#endregion

	}

	#endregion

}
