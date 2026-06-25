namespace IntegrationV2.Files.cs.Warnings.Exceptions
{
	using System;

	#region Class: BasicAuthWarningException

	public class BasicAuthWarningException: Exception
	{

		#region Constructors: Public

		public BasicAuthWarningException()
			: base("Basic Authentication is outdated and potentially unsafe. " +
				"It is officially deprecated by Microsoft. " +
				"For production usage please consider switching to modern OAuth authentication") {
		}

		#endregion

	}

	#endregion

}
