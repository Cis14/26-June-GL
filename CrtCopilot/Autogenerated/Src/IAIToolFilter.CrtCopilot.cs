namespace Creatio.Copilot
{

	#region Interface: IAIToolFilter

	/// <summary>
	/// Session-scoped filter that controls Copilot tool exposure.
	/// Return <c>true</c> for the scope that should remain unfiltered.
	/// </summary>
	public interface IAIToolFilter
	{

		#region Methods: Public

		bool ShouldIncludeIntent(AIIntentFilterContext context);

		#endregion

	}

	#endregion

	#region Interface: IDescribableToolFilter

	/// <summary>
	/// Extension for filters that can persist a serializable descriptor on the session.
	/// Implementations are expected to provide a public static <c>FromDescriptor</c> factory.
	/// </summary>
	public interface IDescribableToolFilter : IAIToolFilter
	{

		#region Methods: Public

		AIToolFilterDescriptor ToDescriptor();

		#endregion

	}

	#endregion

}

