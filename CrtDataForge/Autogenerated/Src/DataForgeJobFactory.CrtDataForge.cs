using Terrasoft.Core;
using Terrasoft.Core.Factories;

namespace Terrasoft.Configuration.DataForge
{
	#region Interface: IDataForgeJobFactory
	/// <summary>
	/// Defines a factory for creating <see cref="IDataForgeJobScheduler"/> instances
	/// </summary>
	public interface IDataForgeJobFactory
	{
		/// <summary>
		/// Creates an instance of <see cref="IDataForgeJobScheduler"/> using the
		/// implementation name specified in system settings, and resolves required dependencies.
		/// </summary>
		/// <returns>
		/// An initialized <see cref="IDataForgeJobScheduler"/> implementation.
		/// </returns>
		IDataForgeJobScheduler Create();
	}

	#endregion

	#region Class: DataForgeJobFactory

	[DefaultBinding(typeof(IDataForgeJobFactory))]
	public class DataForgeJobFactory : IDataForgeJobFactory
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public DataForgeJobFactory(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public IDataForgeJobScheduler Create() {
			var scheduler = ClassFactory.Get<IAppSchedulerWraper>();

			var dataTypeMapper = ClassFactory.Get<IDataTypeMapper>();

			var schemaChecksumProvider = ClassFactory.Get<ISchemaChecksumProvider>(
				new ConstructorArgument("userConnection", _userConnection)
			);

			var tableMetadataBuilder = ClassFactory.Get<ITableMetadataBuilder>(
				new ConstructorArgument("dataTypeMapper", dataTypeMapper)
			);

			var dataStructureHandler = ClassFactory.Get<IDataStructureHandler>(
				new ConstructorArgument("userConnection", _userConnection),
				new ConstructorArgument("checksumProvider", schemaChecksumProvider),
				new ConstructorArgument("tableMetadataBuilder", tableMetadataBuilder)
			);

			var checksumProvider = ClassFactory.Get<IChecksumProvider>();

			var lookupHandler = ClassFactory.Get<ILookupHandler>(
				new ConstructorArgument("userConnection", _userConnection),
				new ConstructorArgument("checksumProvider", checksumProvider)
			);

			var dataForgeServiceFactory = ClassFactory.Get<IDataForgeServiceFactory>(
				new ConstructorArgument("userConnection", _userConnection)
			);

			return ClassFactory.Get<IDataForgeJobScheduler>(
				new ConstructorArgument("userConnection", _userConnection),
				new ConstructorArgument("scheduler", scheduler),
				new ConstructorArgument("dataStructureHandler", dataStructureHandler),
				new ConstructorArgument("lookupHandler", lookupHandler),
				new ConstructorArgument("dataForgeServiceFactory", dataForgeServiceFactory)
			);
		}

		#endregion
	}

	#endregion
}
