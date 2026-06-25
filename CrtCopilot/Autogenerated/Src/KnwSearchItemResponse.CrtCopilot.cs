namespace Creatio.Copilot
{
	using System;

	#region Class: KnwItemDetail

	/// <summary>
	/// Represents the details of an item in the knowledge search response.
	/// </summary>
	public class KnwItemDetail
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the unique identifier of the item.
		/// </summary>
		public Guid? ItemId { get; set; }

		/// <summary>
		/// Gets or sets the title of the item.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the location of the item.
		/// </summary>
		public string Location { get; set; } = string.Empty;

		#endregion

	}

	#endregion

	#region Class: KnwSearchItemResponse

	/// <summary>
	/// Represents a response item from a knowledge source search.
	/// </summary>
	public class KnwSearchItemResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the identifier of the knowledge source.
		/// </summary>
		public Guid KnwSourceId { get; set; }

		/// <summary>
		/// Gets or sets the search response content.
		/// </summary>
		public string Response { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the error value.
		/// </summary>
		public string Error { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the score of the search result.
		/// </summary>
		public double Score { get; set; } = 0;

		/// <summary>
		/// Gets or sets the details of the item.
		/// </summary>
		public KnwItemDetail ItemDetail { get; set; } = new KnwItemDetail();

		#endregion

	}

	#endregion

}

