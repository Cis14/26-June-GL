namespace Creatio.Copilot
{
    using System;
    using System.Collections.Generic;
    using Creatio.Copilot.Actions;
    using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;
    using Terrasoft.Core.Process;

    #region Class: CancelExecutionAction

    public class CancelExecutionAction : BaseExecutableCodeAction, IUserConnectionRequired
    {

        #region Constants: Private

        private const string ReasonParameterName = "Reason";

        #endregion

        #region Fields: Private

        private UserConnection _userConnection;

        #endregion

        #region Constructors: Public

        public CancelExecutionAction() {
            Parameters = new List<SourceCodeActionParameter>() {
                new SourceCodeActionParameter {
                    Name = ReasonParameterName,
                    Caption = new LocalizableString("Reason"),
                    Description = new LocalizableString("Reason for cancellation in concise form. Must not be exposed in the confirmation."),
                    IsRequired = true,
                    DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId,
                }
            };
        }

        #endregion

        #region Methods: Protected

        /// <inheritdoc />
        protected override bool GetIsEnabled() {
            return true;
        }

        #endregion

        #region Methods: Public

        /// <inheritdoc/>
        public override LocalizableString GetCaption() {
            return new LocalizableString("Cancel execution");
        }

        /// <inheritdoc/>
        public override LocalizableString GetDescription() {
            return new LocalizableString("Cancel execution");
        }

        /// <inheritdoc/>
        public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
            var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
            CopilotSession session = sessionManager.FindById(options.CopilotSessionUId);
            if (session?.ProcessElementId is Guid elementUId && elementUId != Guid.Empty) {
                try {
                    Process process = _userConnection.ProcessEngine.FindProcessByProcessElementUId(elementUId);
                    if (process != null) {
                        _userConnection.ProcessEngine.ProcessExecutor.CancelExecutionAsync(process.UId);
                    }
                } catch (Exception e) {
                    return new CopilotActionExecutionResult {
                        Status = CopilotActionExecutionStatus.Failed,
                        ErrorMessage = $"Unable to cancel execution due to an exception: {e.Message}"
                    };
                }
            } else {
                return new CopilotActionExecutionResult {
                    Status = CopilotActionExecutionStatus.Failed,
                    ErrorMessage = "Unable to cancel execution because the skill is running outside of the process."
                };
            }
            string cancellationReason = options.ParameterValues.GetValueOrDefault(ReasonParameterName, "Execution cancelled.");
            return new CopilotActionExecutionResult {
                Status = CopilotActionExecutionStatus.Completed,
                Response = cancellationReason,
                ResponseOptions = new ActionResponseOptions {
                    ContentType = CopilotContentType.Cancellation,
                    OmitAssistantResponse = true
                }
            };
        }

        /// <inheritdoc/>
        public void SetUserConnection(UserConnection userConnection) {
            _userConnection = userConnection;
        }

        #endregion

    }

    #endregion

}

