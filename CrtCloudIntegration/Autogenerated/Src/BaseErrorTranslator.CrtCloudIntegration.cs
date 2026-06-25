namespace CrtCloudIntegration.Utilities 
{

	using CrtCloudIntegration.Dto;
	using Terrasoft.Common;
	using Terrasoft.Core;

	#region Class: BaseErrorTranslator

	public abstract class BaseErrorTranslator
	{

		#region Constants: Private

		private static readonly string ResourceManagerName = "Translations";

		#endregion

		#region Methods: Public

		/// <summary>
		/// Receiving localizable value from resources
		/// </summary>
		/// <param name="userConnection">User connection</param>
		/// <param name="code">Schema code</param>
		/// <param name="lczKey">Localizable resourse code</param>
		/// <returns>Requested localizable value</returns>
		public string GetLocalizableValue(UserConnection userConnection, string code, string lczKey) {
			string lczString = new LocalizableString(
				userConnection.Workspace.ResourceStorage,
				ResourceManagerName, $"LocalizableStrings.{lczKey}.Value");
			return string.Format(lczString ?? "Localizable value missing for {0}", code);
		}

		/// <summary>
		/// Method create and return WebSocketDto
		/// </summary>
		/// <param name="description">Error description</param>
		/// <param name="errorCode">Error code</param>
		/// <returns>new WebSocketDto model</returns>
		public WebSocketDto CreateErrorDto(string description, string errorCode = "") {
			return new WebSocketDto {
				IsSuccess = false,
				Command = "show.ErrorScreen",
				Description = description,
				ErrorCode = errorCode
			};
		}

		#endregion
	}

	#endregion

}

