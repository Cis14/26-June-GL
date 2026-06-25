namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: GetAvailableProvidersResponse

	/// <summary>
	/// Represents the result of the throttling schedule.
	/// </summary>
	[DataContract]
	public class GetAvailableProvidersResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the collection of available mailing providers.
		/// </summary>
		[DataMember(Name = "availableProviders")]
		public IEnumerable<ProviderInfo> AvailableProviders { get; set; }

		#endregion

	}

	#endregion
	
	#region Class: ProviderInfo

	/// <summary>
	/// Represents the result of the throttling schedule.
	/// </summary>
	[DataContract]
	public class ProviderInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets a value indicating whether the provider is connected.
		/// </summary>
		/// <value>
		/// <c>true</c> if the provider is connected; otherwise, <c>false</c>.
		/// </value>
		[DataMember(Name = "isConnected")]
		public bool IsConnected { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether the provider is default for connected account.
		/// </summary>
		/// <value>
		/// <c>true</c> if the provider is default; otherwise, <c>false</c>.
		/// </value>
		[DataMember(Name = "isDefault")]
		public bool IsDefault { get; set; }
		
		/// <summary>
		/// Gets or sets the name of the provider.
		/// </summary>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}
