namespace IntegrationV2.Files.cs.Warnings
{
	using IntegrationApi.MailboxDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using System.Collections.Generic;

	#region Class: WarningExtension

	public static class WarningExtension
	{

		#region Fields: Private

		private static List<string> _office365Domains = new List<string> {
			"outlook.office365.com",
			"outlook.office365.us"
		};

		#endregion

		#region Methods: Public

		public static bool IsOffice365Account(this Mailbox mailbox) {
			return _office365Domains.Contains(mailbox.GetServerAddress());
		}

		public static bool IsOffice365Account(this Calendar calendar) {
			return _office365Domains.Contains(calendar.Settings.ServiceUrl);
		}

		#endregion

	}

	#endregion

}
