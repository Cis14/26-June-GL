 namespace Terrasoft.Configuration
{
	/// <summary>
	/// Reason for webhook rejection.
	/// </summary>
	public enum WebhookRejectionReason
	{
		/// <summary>
		/// Failed to deserialize webhook.
		/// </summary>
		Service_JSONIntegrity = 0,

		/// <summary>
		/// Api key is missing or has invalid format.
		/// </summary>
		Service_InvalidApiKey = 1,

		/// <summary>
		/// Failed to validate webhook entities.
		/// </summary>
		Service_RestrictedEntity = 2,

		/// <summary>
		/// Accounts service is not available.
		/// </summary>
		Accounts_503 = 3,

		/// <summary>
		/// Api key was not found in accounts service.
		/// </summary>
		Accounts_UnknownApiKey = 4,

		/// <summary>
		/// Webhook destination is not found in accounts service.
		/// </summary>
		Accounts_RouteNotFound = 5,

		/// <summary>
		/// Identity service is not available.
		/// </summary>
		Creatio_IS503 = 6,

		/// <summary>
		/// Creatio is not available.
		/// </summary>
		Creatio_503 = 7,

		/// <summary>
		/// Failed to authenticate webhook request in Creatio.
		/// </summary>
		Creatio_401 = 8,

		/// <summary>
		/// Access rights issue in Creatio.
		/// </summary>
		Creatio_403 = 9,

		/// <summary>
		/// Unexpected error in Creatio.
		/// </summary>
		Creatio_500 = 10,

		/// <summary>
		/// Generic unknown reason for webhook rejection.
		/// Equivalent to missing or unrecognized reason.
		/// </summary>
		Unknown = 11
	}
}
