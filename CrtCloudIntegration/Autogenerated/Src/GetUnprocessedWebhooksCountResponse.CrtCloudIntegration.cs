namespace Terrasoft.Configuration
{
	using System.Collections.Generic;

	/// <summary>
	/// Response class for getting unprocessed webhooks count.
	/// </summary>
	public class GetUnprocessedWebhooksCountResponse : BaseWebhookServiceApiResponse
	{

		#region Properties: Public
		
		/// <summary>
		/// Count of unprocessed webhooks.
		/// </summary>
		public Dictionary<WebhookPersistenceType, long> Counts { get; set; }

		#endregion

	}

}

