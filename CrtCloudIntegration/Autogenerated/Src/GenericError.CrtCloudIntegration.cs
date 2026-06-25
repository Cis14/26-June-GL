namespace CrtCloudIntegration.Utilities.Errors
{
	#region Class: GenericError

	public abstract class GenericError : IError
	{

		#region Constructors: Protected

		protected GenericError(string errorCode, string description) {
			ErrorCode = errorCode;
			Description = description;
		}

		#endregion

		#region Properties: Public

		public string ErrorCode { get; private set; }
		public string Description { get; private set; }

		#endregion


	}

	#endregion

	#region Class: GenericErrorOne
	public class GenericErrorOne : GenericError
	{
		#region Constructors

		public GenericErrorOne(string errorCode, string description) : base(errorCode, description) {
		}

		#endregion

	}

	#endregion

}
