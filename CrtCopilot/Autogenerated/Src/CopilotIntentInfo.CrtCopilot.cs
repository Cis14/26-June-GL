namespace Creatio.Copilot
{
	using System;
	using System.Runtime.Serialization;

	#region Class: CopilotIntentInfo

	[DataContract]
	public class CopilotIntentInfo
	{

		#region Properties: Public

		/// <summary>
		/// Intent id.
		/// </summary>
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		/// <summary>
		/// Intent name.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Intent description.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		#endregion

	}

	#endregion

}
