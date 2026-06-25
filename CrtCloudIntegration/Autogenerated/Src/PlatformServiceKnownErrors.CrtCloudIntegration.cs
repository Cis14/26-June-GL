namespace CrtCloudIntegration.Utilities.Errors
{
	/// <summary>
	/// Abstract class for the known errors.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.IError" />
	public abstract class KnownError : IError { }

	/// <summary>
	/// Describe an error when an ad account was not gets.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.KnownError" />
	public class CouldNotGetAdAccountError : KnownError
	{

		#region Properties: Public

		/// <summary>
		/// Gets the description localizable string.
		/// </summary>
		public string DescriptionLcz => "CouldNotGetAdAccountError_Description";

		#endregion

	}

	/// <summary>
	/// Describe an error when an account was not gets.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.KnownError" />
	public class CouldNotGetAccountError: KnownError
	{

		#region Properties: Public

		/// <summary>
		/// Gets the description localizable string.
		/// </summary>
		public string DescriptionLcz => "CouldNotGetAccountError_Description";

		#endregion

	}

	/// <summary>
	/// Describe an error when the platform services returns unexpected responses or unavailable.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.KnownError" />
	public class PlatformServicesUnavailable : KnownError
	{

		#region Properties: Public

		/// <summary>
		/// Gets the description localizable string.
		/// </summary>
		public string DescriptionLcz => "PlatformServicesUnavailable_Description";

		#endregion

	}

	/// <summary>
	/// Describe an error when the getting auth token failed.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.KnownError" />
	public class CouldNotAuthenticateToPlatformError : KnownError
	{

		#region Properties: Public

		/// <summary>
		/// Gets the description localizable string.
		/// </summary>
		public string DescriptionLcz => "SysSetting_IdentityServerInvalidValue_Description";

		#endregion

	}

	/// <summary>
	/// Access token doesn't have required scopes.
	/// </summary>
	public class AccessTokenDoesNotHaveRequiredScopes : KnownError { }

}
