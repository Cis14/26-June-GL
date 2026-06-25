namespace Terrasoft.Configuration.DataForge
{
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Requests;

	#region Interface: IDataForgeServiceFactory

	/// <summary>
	/// Defines a factory for creating <see cref="IDataForgeService"/> instances
	/// </summary>
	public interface IDataForgeServiceFactory
	{
		/// <summary>
		/// Creates an instance of <see cref="IDataForgeService"/> using the
		/// implementation name specified in system settings, and resolves required dependencies.
		/// </summary>
		/// <returns>
		/// An initialized <see cref="IDataForgeService"/> implementation.
		/// </returns>
		IDataForgeService Create();
	}

	#endregion

	[DefaultBinding(typeof(IDataForgeServiceFactory))]
	public class DataForgeServiceFactory : IDataForgeServiceFactory
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public DataForgeServiceFactory(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public IDataForgeService Create() {
			string implementationName = SysSettings.GetValue(
				_userConnection,
				"DataForgeServiceName",
				"DefaultDataForgeService");

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

			var httpRequestClient = ClassFactory.Get<IHttpRequestClient>();

			var identityServiceWrapper = IdentityServiceWrapperHelper.GetInstance();

			return ClassFactory.Get<IDataForgeService>(
				implementationName,
				new ConstructorArgument("dataStructureHandler", dataStructureHandler),
				new ConstructorArgument("lookupHandler", lookupHandler),
				new ConstructorArgument("userConnection", _userConnection),
				new ConstructorArgument("httpRequestClient", httpRequestClient),
				new ConstructorArgument("identityServiceWrapper", identityServiceWrapper)
			);
		}

		#endregion

	}
}

