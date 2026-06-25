namespace Terrasoft.Configuration.CESResponses
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Configuration.CESModels;

	#region Class: CheckedEmailResponse

	/// <summary>
	/// Represents the checked email response from CES.
	/// </summary>
	[DataContract]
	public class CheckedEmailResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the result.
		/// </summary>
		[DataMember(Name = "result")]
		public IEnumerable<CheckedEmail> Result { get; set; }

		#endregion

	}

	#endregion

}

