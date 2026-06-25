namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: BinaryColumnProcessor

	/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
	/// <summary>
	/// An instance of this class is responsible for processing Binary column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(BinaryColumnProcessor))]
	public class BinaryColumnProcessor : NonPersistentColumnProcessor<object>, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="NonPersistentColumnProcessor{ TResult}"/>
		/// <summary>
		/// Creates instance of type <see cref="BinaryColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public BinaryColumnProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.BinaryDataValueTypeUId;

		#endregion

		#region Methods: Protected

		protected override void AddToResults(ImportColumnValue columnValue, object valueForSave) { }

		#endregion

	}

	#endregion

}

