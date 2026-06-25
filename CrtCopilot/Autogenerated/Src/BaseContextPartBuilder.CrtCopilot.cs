namespace Creatio.Copilot {
	using Terrasoft.Core;

	#region Class: BaseContextPartBuilder

	/// <summary>
	/// Base implementation of <see cref="IContextPartBuilder"/>.
	/// Provides common fields and properties for all IContextPartBuilder implementations.
	/// </summary>
	internal abstract class BaseContextPartBuilder : IContextPartBuilder {

		#region Field: Protected

		protected readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		public BaseContextPartBuilder(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IContextPartBuilder.BuildMessageContent(CopilotContext)"/>
		public abstract string BuildMessageContent(CopilotContext copilotContext);

		#endregion

	}

	#endregion

}
