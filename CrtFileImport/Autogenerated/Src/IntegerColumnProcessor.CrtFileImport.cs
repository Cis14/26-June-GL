namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: IntegerColumnProcessor

	/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
	/// <summary>
	/// An instance of this class is responsible for processing Integer column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(IntegerColumnProcessor))]
	public class IntegerColumnProcessor : NonPersistentColumnProcessor<int?>, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		/// Creates instance of type <see cref="IntegerColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public IntegerColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.IntegerDataValueTypeUId;

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override int? ConvertValue(ImportColumnValue columnValue) {
			base.ConvertValue(columnValue);
			if (!int.TryParse(columnValue.Value, out int valueForSave))
				return null;
			return valueForSave;
		}

		#endregion

	}

	#endregion

}
