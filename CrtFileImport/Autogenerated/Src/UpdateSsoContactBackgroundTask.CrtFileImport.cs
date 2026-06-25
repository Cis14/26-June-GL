namespace Terrasoft.Configuration 
{
	using Terrasoft.Configuration.FileImport;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Tasks;

	#region Class: UpdateSsoContactBackgroundTask

	/// <summary>
	/// Class updates sso contact using SAML response from storage.
	/// </summary>
	public class UpdateSsoContactBackgroundTask : IBackgroundTask<bool>, IUserConnectionRequired
	{

		#region Fields: Private

		private UserConnection _uc;

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="="IBackgroundTask.Run"/>

		public void Run(bool parameters) {
			var ssoContactUpdater = ClassFactory.Get<ISsoContactUpdater>(new ConstructorArgument("userConnection", _uc));
			ssoContactUpdater.UpdateContact();
		}

		/// <inheritdoc cref="="IUserConnectionRequired.SetUserConnection"/>
		public void SetUserConnection(UserConnection userConnection) {
			_uc = userConnection;
		}

		#endregion

	}

	#endregion

}
