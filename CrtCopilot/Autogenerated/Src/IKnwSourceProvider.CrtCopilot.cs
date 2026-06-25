namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;

	#region Class: KnwSourceDto

	/// <summary>
	/// Data transfer object describing a knowledge source available to the copilot session.
	/// </summary>
	public class KnwSourceDto
	{
		#region Properties: Public
		/// <summary>
		/// Unique identifier of the knowledge source.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Human readable description or caption of the knowledge source.
		/// </summary>
		public string Description { get; set; }
		#endregion
	}

	#endregion

	#region Interface: IKnwSourceProvider

	/// <summary>
	/// Abstraction for retrieving knowledge sources relevant to the current <see cref="CopilotSession"/>.
	/// Implementation is optional; absence must not break the chat flow.
	/// </summary>
	public interface IKnwSourceProvider
	{
		#region Methods: Public
		/// <summary>
		/// Returns the collection of knowledge sources applicable within the provided session context.
		/// May return <c>null</c> or an empty sequence when no sources are available.
		/// </summary>
		/// <param name="session">Active copilot session.</param>
		/// <returns>Enumerable of <see cref="KnwSourceDto"/> instances or <c>null</c>.</returns>
		IEnumerable<KnwSourceDto> GetSourcesInSession(CopilotSession session);

		#endregion

	}

	#endregion
}
