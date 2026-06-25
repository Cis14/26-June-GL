namespace CrtCloudIntegration.Utilities
{
	using CrtCloudIntegration.Dto;
	using CrtCloudIntegration.Utilities.Errors;

	#region Class: GenericErrorTranslator

	public class GenericErrorTranslator: BaseErrorTranslator, IErrorTranslator
	{

		#region Fields: Private

		private readonly GenericError _error;

		#endregion

		#region Constuctors: Public

		public GenericErrorTranslator(GenericError error) {
			_error = error;
		}

		#endregion

		#region Methods: Public

		public WebSocketDto Translate() {
			return CreateErrorDto(_error.Description, _error.ErrorCode);
		}

		#endregion

	}

	#endregion

}

