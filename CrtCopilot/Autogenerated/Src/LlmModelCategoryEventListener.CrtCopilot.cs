namespace Creatio.Copilot
{
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;

	#region Class: LlmModelCategoryEventListener

	[EntityEventListener(SchemaName = "LlmModelCategory")]
	public class LlmModelCategoryEventListener : BaseEntityEventListener
	{

		#region Fields: Private

		private readonly ILlmModelCategoryRepository _modelCategoryRepository;

		#endregion

		#region Constructors: Public

		public LlmModelCategoryEventListener(ILlmModelCategoryRepository modelCategoryRepository) {
			_modelCategoryRepository = modelCategoryRepository;
		}

		#endregion

		#region Methods: Public

		public override void OnSaved(object sender, EntityAfterEventArgs e) {
			base.OnSaved(sender, e);
			_modelCategoryRepository.ClearCache();
		}

		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			_modelCategoryRepository.ClearCache();
		}

		#endregion

	}

	#endregion

}

