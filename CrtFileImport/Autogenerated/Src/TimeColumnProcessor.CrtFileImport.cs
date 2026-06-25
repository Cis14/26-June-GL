namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: TimeColumnProcessor

	/// <inheritdoc cref="DateTimeColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Date column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(TimeColumnProcessor))]
	public class TimeColumnProcessor : DateTimeColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="DateTimeColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="TimeColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public TimeColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.TimeDataValueTypeUId;

		#endregion

	}

	#endregion

}
