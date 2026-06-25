namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: GuidColumnProcessor

	/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
	/// <summary>
	/// An instance of this class is responsible for processing Guid column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(GuidColumnProcessor))]

	public class GuidColumnProcessor : NonPersistentColumnProcessor<Guid?>, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		/// Creates instance of type <see cref="GuidColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public GuidColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.GuidDataValueTypeUId;

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override Guid? ConvertValue(ImportColumnValue columnValue) {
			base.ConvertValue(columnValue);
			if (Guid.TryParse(columnValue.Value, out Guid convertedValue)) {
				return convertedValue;
			}
			return null;
		}

		#endregion

	}

	#endregion

}
