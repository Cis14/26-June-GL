namespace Terrasoft.Configuration
{
	using System.Collections.Generic;

	/// <summary>
	/// Request class for resending unprocessed webhooks.
	/// </summary>
	public class ResendUnprocessedWebhooksRequest
	{
		#region Properties: Public

		/// <summary>
		/// Webhook ApiKey.
		/// </summary>
		public List<string> ApiKeys { get; set; }

		/// <summary>
		/// Persistance Types.
		/// </summary>
		public List<WebhookPersistenceType> Types { get; set; }

		#endregion

	}

}

