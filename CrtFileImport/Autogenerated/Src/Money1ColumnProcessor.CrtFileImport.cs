namespace Terrasoft.Configuration.FileImport
{
    using System;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;

    #region Class: Money1ColumnProcessor

    /// <inheritdoc cref="FloatColumnProcessor"/>
    /// <summary>
    /// An instance of this class is responsible for processing Money column values.
    /// </summary>
    [DefaultBinding(typeof(IColumnProcessor), Name = nameof(Money1ColumnProcessor))]
    public class Money1ColumnProcessor : FloatColumnProcessor
    {

        #region Constructors: Public

        /// <inheritdoc cref="FloatColumnProcessor"/>
        /// <summary>
        /// Creates instance of type <see cref="MoneyColumnProcessor"/>.
        /// </summary>
        /// <param name="userConnection">User connection.</param>
        public Money1ColumnProcessor(UserConnection userConnection) : base(userConnection) {
        }

        #endregion

        #region Properties: Protected

        /// <inheritdoc cref="BaseColumnProcessor"/>
        /// <summary>
        /// Data value type unique identifier.
        /// </summary>
        protected override Guid DataValueTypeUId => DataValueType.Money1DataValueTypeUId;

        #endregion

    }

    #endregion

}
