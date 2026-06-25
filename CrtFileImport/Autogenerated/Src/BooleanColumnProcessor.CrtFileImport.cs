namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: BooleanColumnProcessor

	/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
	/// <summary>
	///     An instance of this class is responsible for processing Boolean column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(BooleanColumnProcessor))]
	public class BooleanColumnProcessor : NonPersistentColumnProcessor<bool?>, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		///     Creates instance of type <see cref="BooleanColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public BooleanColumnProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override Guid DataValueTypeUId => DataValueType.BooleanDataValueTypeUId;

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override bool? ConvertValue(ImportColumnValue columnValue) {
			base.ConvertValue(columnValue);
			bool? convertedValue;
			try {
				convertedValue = BooleanUtilities.ConvertToBoolean(columnValue.Value);
			} catch (ArgumentNullOrEmptyException) {
				convertedValue = false;
			} catch {
				convertedValue = null;
			}
			return convertedValue;
		}

		#endregion

	}

	#endregion

}

