namespace Terrasoft.Configuration.FileImport
{
    using System;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;

    #region Class: Money0ColumnProcessor

    /// <inheritdoc cref="FloatColumnProcessor"/>
    /// <summary>
    /// An instance of this class is responsible for processing Money column values.
    /// </summary>
    [DefaultBinding(typeof(IColumnProcessor), Name = nameof(Money0ColumnProcessor))]
    public class Money0ColumnProcessor : FloatColumnProcessor
    {

        #region Constructors: Public

        /// <inheritdoc cref="FloatColumnProcessor"/>
        /// <summary>
        /// Creates instance of type <see cref="MoneyColumnProcessor"/>.
        /// </summary>
        /// <param name="userConnection">User connection.</param>
        public Money0ColumnProcessor(UserConnection userConnection) : base(userConnection) {
        }

        #endregion

        #region Properties: Protected

        /// <inheritdoc cref="BaseColumnProcessor"/>
        /// <summary>
        /// Data value type unique identifier.
        /// </summary>
        protected override Guid DataValueTypeUId => DataValueType.Money0DataValueTypeUId;

        #endregion

    }

    #endregion

}
