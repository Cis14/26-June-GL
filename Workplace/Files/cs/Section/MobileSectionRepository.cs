namespace Terrasoft.Configuration.Section
{
	using Core;
	using Core.Factories;
	using Terrasoft.Configuration.Domain;

	#region Class MobileSectionRepository

	[DefaultBinding(typeof(ISectionRepository), Name = "Mobile")]
	public class MobileSectionRepository : SectionRepository
	{
		#region Properties: Protected

		/// <summary>
		/// Section schema unique identifier column name for mobile sections.
		/// </summary>
		protected override string SectionSchemaUIdColumnName => "MobileSectionSchemaUId";

		/// <summary>
		/// Cache key scope for mobile sections - Mobile platform GUID.
		/// </summary>
		protected override string ScopeKey => ClientTypes.MobileClientTypeId.ToString();

		#endregion

		#region Constructors: Public

		public MobileSectionRepository(UserConnection uc) : base(uc) {
		}

		#endregion

	}

	#endregion

}
