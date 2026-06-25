namespace Terrasoft.Configuration
{
	using System;
	using Core.DB;

	#region Class: ContactSelectModel

	/// <summary>
	/// Type of ContactQueryProvider result of GetSelectModel method.
	/// </summary>
	public class ContactSelectModel
	{

		#region Properties: Public

		/// <summary>
		/// UId of SysSchema for audience of Lead, EventTarget etc.
		/// </summary>
		public Guid AudienceSchemaUId { get; set; }

		/// <summary>
		/// The name of ContactId column.
		/// </summary>
		public string ContactIdColumnName {
			get;
			set;
		}

		/// <summary>
		/// Query that returns a list of Contact unique identifiers.
		/// </summary>
		public Select ContactSelect {
			get;
			set;
		}

		#endregion

	}

	#endregion

}
