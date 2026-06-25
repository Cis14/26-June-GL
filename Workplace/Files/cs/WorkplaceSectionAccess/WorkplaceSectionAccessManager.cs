namespace Terrasoft.Configuration.WorkplaceSectionAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Core.Factories;
	using WorkplaceApi;

	#region Class: WorkplaceSectionAccessManager

	[DefaultBinding(typeof(IWorkplaceSectionAccessManager))]
	public class WorkplaceSectionAccessManager : BaseWorkplaceSectionAccessManager, IWorkplaceSectionAccessManager
	{

		#region Constructors: Public

		public WorkplaceSectionAccessManager(UserConnection userConnection): base(userConnection) {
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetAllAllowedWorkplacesWithSections"/>
		public IEnumerable<AllowedWorkplaceStructureInfo> GetAllAllowedWorkplacesWithSections() {
			return GetAllAllowedWorkplaceStructuresInternal(UserConnection.CurrentUser.Id);
		}

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetAllAllowedWorkplacesIds"/>
		public IEnumerable<Guid> GetAllAllowedWorkplacesIds() {
			return GetAllAllowedWorkplacesInternal(UserConnection.CurrentUser.Id).Select(workplace => workplace.Id);
		}

		#endregion

	}

	#endregion

}