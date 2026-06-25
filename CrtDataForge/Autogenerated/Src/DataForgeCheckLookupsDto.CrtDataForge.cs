namespace Terrasoft.Configuration.DataForge
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Core.ServiceModelContract;

	#region Class: DataForgeCheckLookupsRequestBody

	/// <summary>
	/// Represents the request body for checking lookup data via the Data Forge service.
	/// </summary>
	[DataContract]
	[Serializable]
	public class DataForgeCheckLookupsRequestBody
	{
		#region Properties: Public

		/// <summary>
		/// Gets or sets the collection of lookup summaries to be checked.
		/// </summary>
		[DataMember(Name = "lookups")]
		public List<LookupSummary> Lookups { get; set; }

		/// <summary>
		/// Gets or sets the collection of lookup values summaries to be checked.
		/// </summary>
		[DataMember(Name = "lookupValues")]
		public List<LookupValueSummary> LookupValues { get; set; }

		/// <summary>
		/// Gets or sets additional options for the lookups check operation.
		/// </summary>
		[DataMember(Name = "options")]
		public CheckLookupsOptions Options { get; set; }

		#endregion
	}

	#endregion

	#region Class: LookupSummary

	/// <summary>
	/// Represents summary information about a lookup, including structure and modification metadata.
	/// </summary>
	[DataContract]
	[Serializable]
	public class LookupSummary
	{
		#region Properties: Public

		/// <summary>
		/// Gets or sets the unique identifier of the lookup.
		/// </summary>
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the values schema associated with the lookup.
		/// </summary>
		[DataMember(Name = "valuesSchemaUId")]
		public Guid ValuesSchemaUId { get; set; }

		/// <summary>
		/// Gets or sets the checksum of the lookup
		/// </summary>
		[DataMember(Name = "checksum")]
		public string Checksum { get; set; }

		/// <summary>
		/// Gets or sets the last modification date of the lookup
		/// </summary>
		[DataMember(Name = "modifiedOn")]
		public string ModifiedOn { get; set; }

		#endregion
	}

	#endregion

	#region Class: CheckLookupsOptions

	/// <summary>
	/// Represents additional options for the lookups check operation.
	/// </summary>
	[DataContract]
	[Serializable]
	public class CheckLookupsOptions
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets a value indicating whether the application is a demo application.
		/// </summary>
		[DataMember(Name = "isDemoApp")]
		public bool IsDemoApp { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the application is a trial application.
		/// </summary>
		[DataMember(Name = "isTrialApp")]
		public bool IsTrialApp { get; set; }

		/// <summary>
		/// Gets or sets the list of editions applicable to the application.
		/// </summary>
		[DataMember(Name = "editions")]
		public string[] Editions { get; set; }

		/// <summary>
		/// Gets or sets the list of extra packages installed in the application.
		/// </summary>
		[DataMember(Name = "extraPackages")]
		public string[] ExtraPackages { get; set; }

		#endregion

	}

	#endregion

	#region Class: DataForgeCheckLookupsResponse

	/// <summary>
	/// Represents the response from the Data Forge service containing the list of lookup IDs that require updates or were validated.
	/// </summary>
	[DataContract]
	[Serializable]
	public class DataForgeCheckLookupsResponse : BaseResponse
	{
		#region Properties: Public

		/// <summary>
		/// Gets or sets the list of lookup IDs returned by the check.
		/// </summary>
		[DataMember(Name = "lookupIds")]
		public List<Guid> LookupIds { get; set; }

		#endregion
	}

	#endregion
}

