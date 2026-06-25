 namespace Terrasoft.Configuration
{
	using System.Collections.Generic;

	/// <summary>
	/// Response class for getting unprocessed webhooks count filtered by reason.
	/// </summary>
	public class GetUnprocessedWebhooksByReasonCountResponse : BaseWebhookServiceApiResponse
	{

		#region Properties: Public

		/// <summary>
		/// Count of unprocessed webhooks.
		/// </summary>
		public Dictionary<WebhookRejectionReason, long> Counts { get; set; }

		#endregion

	}

}

