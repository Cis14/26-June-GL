namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Collections.Generic;

	#region Class: CloudSenderDomainsInfo

	/// <summary>
	/// Represents response of the service for retrieving domains for sending email.
	/// </summary>
	public class CloudSenderDomainsInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the domains and it's options.
		/// </summary>
		/// <seealso cref="SenderDomain"/>
		public List<CloudSenderDomain> Domains { get; set; }

		/// <summary>
		/// Gets or sets the default dkim. Contains value if provider sets single key for all domains.
		/// </summary>
		public string DefaultDKIMKey { get; set; }

		/// <summary>
		/// Gets or sets the default dkim. Contains value if provider sets single key for all domains.
		/// </summary>
		public string DKIMRecord { get; set; }

		/// <summary>
		/// Gets or sets the SPF value.
		/// </summary>
		public string SpfValue { get; set; }

		/// <summary>
		/// Gets or sets the SPF value.
		/// </summary>
		public string DmarkValue { get; set; }

		/// <summary>
		/// Domain validation record.
		/// </summary>
		public string DomainValidationRecord { get; set; }

		/// <summary>
		/// Response status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Response code.
		/// </summary>
		public int Code { get; set; }

		/// <summary>
		/// Response message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the cache key.
		/// </summary>
		public string CacheKey { get; set; }

		#endregion

	}

	#endregion

}

