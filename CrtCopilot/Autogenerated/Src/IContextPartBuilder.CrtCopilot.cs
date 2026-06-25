namespace Creatio.Copilot {

	#region Interface: IContextPartBuilder

	/// <summary>
	/// Api for converting specific type of <see cref="BaseContextPart"/> ansestor to 
	/// LLM message part.
	/// </summary>
	public interface IContextPartBuilder {

		#region Methods: Public

		/// <summary>
		/// Converts specific type of <see cref="BaseContextPart"/> ansestor to 
		/// LLM message part.
		/// </summary>
		/// <param name="copilotContext"><see cref="CopilotContext"/> instance.</param>
		/// <returns>LLM message part. If there are no parts that can be handled returnd string.Empty.</returns>
		string BuildMessageContent(CopilotContext copilotContext);

		#endregion

	}

	#endregion

}
