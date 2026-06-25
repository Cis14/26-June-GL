namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: AIToolFilterDescriptor

	/// <summary>
	/// Serializable description of a session-scoped tool filter.
	/// </summary>
	[DataContract]
	[Serializable]
	public class AIToolFilterDescriptor
	{

		#region Properties: Public

		[DataMember(Name = "typeName")]
		public string TypeName { get; set; }

		[DataMember(Name = "parameters")]
		public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

		#endregion

	}

	#endregion

}

