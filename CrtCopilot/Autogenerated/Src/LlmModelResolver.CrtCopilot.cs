namespace Creatio.Copilot
{
	using Common.Logging;
    using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using System;
	using Terrasoft.Configuration.GenAI;

	#region Interface: ILlmModelResolver

    public interface ILlmModelResolver
    {
		LlmModelReference ResolveForIntent(Guid? intentId);
    }

    #endregion

    #region Class: LlmModelResolver

	[DefaultBinding(typeof(ILlmModelResolver))]
    public class LlmModelResolver : ILlmModelResolver
    {
		private static readonly ILog _log = LogManager.GetLogger("LlmModelResolver");
		private readonly UserConnection _userConnection;
        private readonly ILlmModelRepository _repository;

		#region Properties: Internal

		private IIntentSchemaService _intentSchemaService;
		private IIntentSchemaService IntentSchemaService {
			get {
				if (_intentSchemaService != null) {
					return _intentSchemaService;
				}
				return _intentSchemaService = _userConnection.GetIntentSchemaService();
			}
		}

		#endregion

        public LlmModelResolver(UserConnection userConnection, ILlmModelRepository repository) {
            _userConnection = userConnection;
            _repository = repository;
        }

        public LlmModelReference ResolveForIntent(Guid? intentId) {
            if (!intentId.HasValue || intentId.Value == Guid.Empty) {
                return null;
            }
            CopilotIntentSchema intent = IntentSchemaService.FindSchemaByUId(intentId.Value);
            if (intent == null) {
                return null;
            }
			var modelCode = intent.LlmModel;
            if (string.IsNullOrEmpty(modelCode)) {
                return null;
            }
            LlmModelReference modelReference = _repository.FindByCode(modelCode);
            if (modelReference == null) {
                _log.Info($"LLM model with code '{modelCode}' not found in LlmModel table for intent '{intentId.Value}'");
            }
            return modelReference;
        }
    }

    #endregion
}

