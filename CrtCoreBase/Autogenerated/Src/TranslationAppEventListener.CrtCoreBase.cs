namespace Terrasoft.Configuration.Translating
{
	using Common;
	using Core;
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core.DB;
	using Web.Common;

	#region Class : TranslateAppEventListener
	/// <summary>
	/// Class, that run all what translate feature need on app start.
	/// </summary>
	public class TranslateAppEventListener : AppEventListenerBase
	{

		#region Fields : Protected

		protected UserConnection UserConnection {
			get;
			private set;
		}

		#endregion

		#region Methods : Protected

		/// <summary>
		/// Gets user connection from application event context.
		/// </summary>
		/// <param name="context">Application event context.</param>
		/// <returns>User connection.</returns>
		protected UserConnection GetUserConnection(AppEventContext context) {
			var appConnection = context.Application["AppConnection"] as AppConnection;
			if (appConnection == null) {
				throw new ArgumentNullOrEmptyException("AppConnection");
			}
			return appConnection.SystemUserConnection;
		}

		private Select GetLanguageAndCultureSelect(UserConnection userConnection) {
			return new Select(userConnection)
					.Column("SysLanguage", "Id").As("SysLanguageId")
					.Column("SysLanguage", "Code").As("Code")
					.Column("SysCulture", "Id").As("SysCultureId")
				.From("SysLanguage")
				.LeftOuterJoin("SysCulture").On("SysLanguage", "Id")
					.IsEqual("SysCulture", "LanguageId") as Select;
		}

		private void InitalizeLanguageRegistry(UserConnection userConnection) {
			var languageSelect = GetLanguageAndCultureSelect(userConnection);
			List<LanguageInfo> languages = new List<LanguageInfo>();
			languageSelect.ExecuteReader(reader => {
				var code = reader.GetColumnValue<string>("Code");
				if (!string.IsNullOrWhiteSpace(code)) {
					languages.Add(new LanguageInfo(
						reader.GetColumnValue<Guid>("SysLanguageId"),
						code,
						reader.GetColumnValue<Guid>("SysCultureId")));
				}
			});
			LanguageRegistry.SetLanguages(languages);
		}

		#endregion

		#region Methods : Public

		/// <summary>
		/// Handles application start.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public override void OnAppStart(AppEventContext context) {
			InitalizeLanguageRegistry(GetUserConnection(context));
		}

		#endregion

	}

	#endregion

} 
