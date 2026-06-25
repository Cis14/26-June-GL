using System.Collections.Generic;


#region Interface: ISsoContactUpdater

/// <summary>
/// Update contact by data from SAML response after login via SSO.
/// </summary>
public interface ISsoContactUpdater {

	#region Methods: Public

	/// <summary>
	/// Update contact by data from <paramref name="contactValues"/>.
	/// </summary>
	/// <param name="contactValues">Contact values.</param>
	void UpdateContact(Dictionary<string, string> contactValues);

	/// <summary>
	/// Update contact by data from from internal storage.
	/// </summary>
	void UpdateContact();

	#endregion

}

#endregion
