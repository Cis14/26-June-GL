namespace Terrasoft.Configuration
{
	using Terrasoft.Core;

	/// <summary>
	/// Provide information from account service.
	/// </summary>
	public interface IRootAccountServiceApi
	{
		/// <summary>
		/// Creates the root account by request.
		/// </summary>
		/// <param name="accountInfoRequest">The account info request.</param>
		BaseAccountResponse CreateAccount(RootAccountInfoRequest accountInfoRequest);

		/// <summary>
		/// Gets the root account.
		/// </summary>
		/// <returns>The root account.</returns>
		RootAccountInfoResponse GetRootAccount();

		/// <summary>
		/// Updates the root account by request.
		/// </summary>
		/// <param name="accountInfoRequest">The account info request.</param>
		BaseAccountResponse UpdateAccount(RootAccountInfoRequest accountInfoRequest);
	}

    #region Class: RootAccountServiceApi

    /// <inheritdoc cref="IRootAccountServiceApi"/>
    public class RootAccountServiceApi : AccountServiceApi, IRootAccountServiceApi
	{

		#region Constructors: Public

		/// <summary>
		/// Constructor of a class.
		/// </summary>
		/// <param name="userConnection">Instance of UserConnection.</param>
		/// <param name="scope">The name of identity scope.</param>
		public RootAccountServiceApi(UserConnection userConnection, string scope)
			: base(userConnection, scope) {
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IRootAccountServiceApi.CreateAccount"/>
		public BaseAccountResponse CreateAccount(RootAccountInfoRequest accountInfoRequest) {
			var url = $"{AccountServiceUrl}/api/account/create";
			var response = SendPostRequest<BaseAccountResponse>(url, accountInfoRequest);
			return response;
		}

		/// <inheritdoc cref="IRootAccountServiceApi.GetRootAccount"/>
		public RootAccountInfoResponse GetRootAccount() {
			var url = $"{AccountServiceUrl}/api/account/me";
			var response = SendPostRequest<RootAccountInfoResponse>(url, null);
			return response;
		}

		/// <inheritdoc cref="IRootAccountServiceApi.UpdateAccount"/>
		public BaseAccountResponse UpdateAccount(RootAccountInfoRequest accountInfoRequest) {
			var url = $"{AccountServiceUrl}/api/account/update";
			var response = SendPostRequest<BaseAccountResponse>(url, accountInfoRequest);
			return response;
		}

		#endregion

	}

	#endregion

}

