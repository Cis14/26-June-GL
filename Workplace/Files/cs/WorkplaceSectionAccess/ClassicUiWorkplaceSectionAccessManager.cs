namespace Terrasoft.Configuration.WorkplaceSectionAccess
{
	using System.Linq;
	using Core;
	using Core.Factories;
	using Common;
	using WorkplaceApi;

	#region Class: WorkplaceSectionAccessManager

	[DefaultBinding(typeof(IClassicUiWorkplaceSectionAccessManager))]
	public class ClassicUiWorkplaceSectionAccessManager : BaseWorkplaceSectionAccessManager,
		IClassicUiWorkplaceSectionAccessManager
	{

		#region Constructors: Public

		public ClassicUiWorkplaceSectionAccessManager(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Methods: Protected

		protected override string[] GetExcludedFromAdditionalUserSectionsLimits() {
			string[] defaultLimitation = base.GetExcludedFromAdditionalUserSectionsLimits();
			string[] classicLimitation = GetLicOperationValue(
				"ClassicUIExcludedFromSectionsCountLimitation")?.ToArray();
			return classicLimitation.IsNullOrEmpty() ? defaultLimitation : classicLimitation;
		}

		#endregion

	}

	#endregion

}