namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;

	#region Class: KnwSearchDTO

	/// <summary>
	/// Data transfer object representing a search result containing knowledge items.
	/// </summary>
	public class KnwSearchDTO
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the collection of knowledge search item responses.
		/// </summary>
		public IEnumerable<KnwSearchItemResponse> KnwItems { get; set; } = Array.Empty<KnwSearchItemResponse>();

		/// <summary>
		/// Gets or sets the error message associated with the search result, if any.
		/// </summary>
		public string ErrorMessage { get; set; } = string.Empty;

		#endregion

	}

	#endregion

}

