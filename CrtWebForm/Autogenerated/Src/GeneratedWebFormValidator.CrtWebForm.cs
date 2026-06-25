namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Web.Http.Abstractions;

	#region Class: GeneratedWebFormValidator

	public class GeneratedWebFormValidator : IGeneratedWebFormValidator
	{
		#region Fields: Private

		/// <summary>
		/// Lead stage - active.
		/// </summary>
		private static readonly Guid EnabledLandingStateCode = new Guid("FBE29614-C02C-446E-AC8C-E7F62C4899EC");
		
		private static readonly Regex ProtocolRegex = new Regex(@"http[s]{0,1}:\/\/");

		private static readonly Regex WwwRegex = new Regex(@"www\.");

		#endregion

		#region Constructors: Public

		public GeneratedWebFormValidator(UserConnection userConnection) {
			UserConnection = userConnection;
			if (HttpContext.Current.Request.UrlReferrer != null) {
				ReferrerUri = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
			}
		}

		public GeneratedWebFormValidator(string referrerUri, UserConnection userConnection) {
			UserConnection = userConnection;
			ReferrerUri = referrerUri;
		}

		#endregion

		#region Properties: Public
		
		/// <summary>
		/// Instance of user connection.
		/// </summary>
		public UserConnection UserConnection {
			get;
			set;
		}

		/// <summary>
		/// Value of http referer header.
		/// </summary>
		public string ReferrerUri {
			get;
			set;
		}

		/// <summary>
		/// Determines whether to start URL validation.
		/// </summary>
		public bool DisableExternalUrlValidation {
			get;
			set;
		}

		#endregion

		#region Methods: Private

		private bool CheckExternalURLContainsRequest(string externalURLs) {
			if (string.IsNullOrEmpty(ReferrerUri)) {
				return false;
			}
			var requestUrl = ProtocolRegex.Replace(ReferrerUri, string.Empty);
			requestUrl = WwwRegex.Replace(requestUrl, string.Empty);
			externalURLs = ProtocolRegex.Replace(externalURLs, string.Empty);
			externalURLs = WwwRegex.Replace(externalURLs, string.Empty);
			return externalURLs
				.Split(new char[] { ';', '*', ' ', ',', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Any(p => requestUrl.StartsWith(p));
		}

		private EntitySchemaQuery GetEntityESQ(EntitySchemaManager entitySchemaManager) {
			var formESQ = new EntitySchemaQuery(entitySchemaManager, "GeneratedWebForm");
			formESQ.PrimaryQueryColumn.IsAlwaysSelect = true;
			formESQ.AddColumn("State");
			formESQ.AddColumn("ExternalURL");
			return formESQ;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Validates landing entity.
		/// </summary>
		/// <param name="webFormId">Identifier of landing.</param>
		/// <returns>Instance of ValidationResult</returns>
		public ValidationResult Validate(Guid webFormId) {
			var result = new ValidationResult() {
				Succes = true
			};
			EntitySchemaManager entitySchemaManager = UserConnection.EntitySchemaManager;
			EntitySchemaQuery formESQ = GetEntityESQ(entitySchemaManager);
			Entity enity = formESQ.GetEntity(UserConnection, webFormId);
			if (enity == null) {
				result.Message = GeneratedWebFormLczUtilities
					.GetLczStringValue("UnknownWebFormIdentifierMessage", "GeneratedWebFormValidator", UserConnection);
				result.Succes = false;
				return result;
			}
			EntitySchemaColumn landingStateColumn = enity.Schema.Columns.GetByName("State");
			Guid landingStateColumnValue = enity.GetTypedColumnValue<Guid>(landingStateColumn.ColumnValueName);
			if (landingStateColumnValue != EnabledLandingStateCode) {
				result.Message = GeneratedWebFormLczUtilities
					.GetLczStringValue("LandingDisabledMessage", "GeneratedWebFormValidator", UserConnection);
				result.Succes = false;
				return result;
			}
			if (!DisableExternalUrlValidation) {
				EntitySchemaColumn externalURLColumn = enity.Schema.Columns.GetByName("ExternalURL");
				string externalURLColumnValue = enity.GetTypedColumnValue<string>(externalURLColumn.ColumnValueName);
				if (!CheckExternalURLContainsRequest(externalURLColumnValue)) {
					result.Message = GeneratedWebFormLczUtilities
						.GetLczStringValue("NotAllowedURLMessage", "GeneratedWebFormValidator", UserConnection);
					result.Succes = false;
					return result;
				}
			}
			return result;
		}

		#endregion
	}

	#endregion
}

