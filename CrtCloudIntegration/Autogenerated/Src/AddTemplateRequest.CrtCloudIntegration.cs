namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: AddTemplateRequest

	/// <summary>
	/// Class for request to add template.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class AddTemplateRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the template name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets sender email.
		/// </summary>
		/// <value>
		/// Email address.
		/// </value>
		[DataMember(Name = "from_email")]
		public string FromEmail { get; set; }

		/// <summary>
		/// Gets or sets sender name.
		/// </summary>
		/// <value>
		/// Sender name.
		/// </value>
		[DataMember(Name = "from_name")]
		public string FromName { get; set; }

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		/// <value>
		/// The subject.
		/// </value>
		[DataMember(Name = "subject")]
		public string Subject { get; set; }

		/// <summary>
		/// Gets or sets the html code.
		/// </summary>
		/// <value>
		/// The html code.
		/// </value>
		[DataMember(Name = "code")]
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the plain text.
		/// </summary>
		/// <value>
		/// The plain text text.
		/// </value>
		[DataMember(Name = "text")]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the email identifier.
		/// </summary>
		/// <value>
		/// The email identifier.
		/// </value>
		[DataMember(Name = "email_id")]
		public Guid EmailId { get; set; }

		/// <summary>
		/// Gets or sets the list of images.
		/// </summary>
		/// <value>
		/// The images.
		/// </value>
		[DataMember(Name = "images")]
		public IEnumerable<image> Images { get; set; }

		/// <summary>
		/// Gets or sets the replica checksum.
		/// </summary>
		/// <value>
		/// The replica checksum.
		/// </value>
		[DataMember(Name = "replica_checksum")]
		public string ReplicaChecksum { get; set; }

		/// <summary>
		/// Gets or sets the replica identifier.
		/// </summary>
		/// <value>
		/// The replica identifier.
		/// </value>
		[DataMember(Name = "replica_id")]
		public string ReplicaId { get; set; }
		
		/// <summary>
		/// Gets or sets the business time end.
		/// </summary>
		/// <value>
		/// The business time end.
		/// </value>
		[DataMember(Name = "business_time_end")]
		public long BusinessTimeEnd { get; set; }

		/// <summary>
		/// Gets or sets the business time start.
		/// </summary>
		/// <value>
		/// The business time start.
		/// </value>
		[DataMember(Name = "business_time_start")]
		public long BusinessTimeStart { get; set; }

		/// <summary>
		/// Gets or sets the delivery schedule days.
		/// </summary>
		/// <value>
		/// The delivery schedule days.
		/// </value>
		[DataMember(Name = "delivery_schedule_days")]
		public string DeliveryScheduleDays { get; set; }

		/// <summary>
		/// Gets or sets the due date when email must be canceled.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		[DataMember(Name = "expiration_date")]
		public DateTime ExpirationDate { get; set; }
		
		/// <summary>
		/// Gets or sets the priority of Email.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		[DataMember(Name = "priority")]
		public int Priority { get; set; }
		
		/// <summary>
		/// Gets or sets the name of the scope.
		/// </summary>
		/// <value>
		/// The name of the scope.
		/// </value>
		[DataMember(Name = "scope_name")]
		public string ScopeName { get; set; }

		/// <summary>
		/// Gets or sets the sending strategy identifier.
		/// </summary>
		/// <value>
		/// The sending strategy identifier.
		/// </value>
		[DataMember(Name = "sending_strategy_id")]
		public int SendingStrategyId { get; set; }
		
		/// <summary>
		/// Gets or sets the id of time zone for sending.
		/// </summary>
		/// <value>
		/// The target time zone identifier.
		/// </value>
		[DataMember(Name = "target_time_zone_id")]
		public string TargetTimeZoneId { get; set; }

		/// <summary>
		/// Gets or sets name of ESP.
		/// </summary>
		[DataMember(Name = "provider_name")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}

