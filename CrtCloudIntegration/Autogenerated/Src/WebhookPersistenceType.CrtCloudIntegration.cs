namespace Terrasoft.Configuration
{
	/// <summary>
	/// Type of persisted Webhook message.
	/// </summary>
	public enum WebhookPersistenceType
	{
		/// <summary>
		/// CantBeSent Message type.
		/// </summary>
		CantBeSent = 0,

		/// <summary>
		/// Restricted Message type.
		/// </summary>
		Restricted = 1,

		/// <summary>
		/// UnknownKey Message type.
		/// </summary>
		UnknownKey = 2,
	}
}
