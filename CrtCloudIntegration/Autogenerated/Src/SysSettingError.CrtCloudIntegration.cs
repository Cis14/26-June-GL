namespace CrtCloudIntegration.Utilities.Errors
{

	#region Class: SysSettingError

	/// <summary>
	/// Describe an error when some of sys setting has incorrect value.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.Errors.KnownError" />
	public class SysSettingError : IError
	{

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="SysSettingError"/> class.
		/// </summary>
		/// <param name="sysSettingCode">The system setting code.</param>
		/// <param name="group">The group.</param>
		public SysSettingError(string sysSettingCode, string group) {
			SysSettingCode = sysSettingCode;
			SysSettingGroup = group;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets the system setting code.
		/// </summary>
		/// <value>
		/// The system setting code.
		/// </value>
		public string SysSettingCode { get; }

		/// <summary>
		/// Gets or sets the system setting group.
		/// </summary>
		/// <value>
		/// The system setting group.
		/// </value>
		public string SysSettingGroup { get; set; }

		#endregion

	}

	#endregion

}
