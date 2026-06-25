namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: TranslationServiceResponse

	[DataContract]
	public class TranslationServiceResponse : ConfigurationServiceResponse
	{

		#region Fields: Private

		[DataMember(Name = "operationLog")]
		private IEnumerable<string> _operationLog;

		#endregion

		#region Constructors: Private

		private TranslationServiceResponse() {
		}

		private TranslationServiceResponse(Exception e) : base(e) {
		}

		private TranslationServiceResponse(IEnumerable<string> operationLog) {
			_operationLog = operationLog;
		}

		#endregion

		#region Methods: Public

		public static TranslationServiceResponse CreateSuccessResult() {
			return new TranslationServiceResponse();
		}

		public static TranslationServiceResponse CreateSuccessResult(IEnumerable<string> operationLog) {
			return new TranslationServiceResponse(operationLog);
		}

		public static TranslationServiceResponse CreateFailureResult(Exception e) {
			return new TranslationServiceResponse(e);
		}

		#endregion

	}

	#endregion

}

