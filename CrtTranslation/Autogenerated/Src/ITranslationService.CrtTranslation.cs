namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Web;

	#region Interface: ITranslationService

	[ServiceContract(Name = "TranslationService")]
	public interface ITranslationService
	{

		#region Methods: Public

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		void ActualizeTranslation();

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		TranslationServiceResponse ApplyTranslation();

		TranslationServiceResponse ApplyTranslation(Guid sessionId);

		TranslationServiceResponse ApplyTranslationForCulture(ISysCultureInfo cultureInfo, bool isForceUpdate);
		
		TranslationServiceResponse ApplyTranslationForCulture(ISysCultureInfo cultureInfo, bool isForceUpdate, Guid sessionId);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		TranslationServiceResponse CheckTranslation();

		#endregion

	}

	#endregion

}
