namespace Creatio.ComponentCopilot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Creatio.Copilot;
    using Creatio.Copilot.Actions;
    using Creatio.FeatureToggling;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Common;
    using Terrasoft.Configuration.Enrichment;
    using Terrasoft.Configuration.GenAI;
    using Terrasoft.Core.Applications.GenAI;
    using Terrasoft.Core.Factories;
    using Terrasoft.Enrichment.Interfaces.ChatCompletion;
    using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;

    /// <summary>
    /// Base class for actions that generate view element config using Creatio.AI.
    /// </summary>
    public abstract class BaseGenerateViewElementAction : BaseExecutableCodeAction
    {

        #region Methods: Private

        private List<ChatMessage> GetSessionMessages(ActionExecutionOptions options) {
            var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
            var session = sessionManager.FindById(options.CopilotSessionUId);
            var messages = session.Messages.Where(s => !String.IsNullOrEmpty(s.Content)).Select(s =>
                new ChatMessage(s.Role == CopilotMessageRole.Tool ? CopilotMessageRole.Assistant : s.Role, s.Content));
            return messages.ToList();
        }

        private JsonSchema GetJsonSchema() {
            var jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new PropertyDefinitionContractResolver()
            };
            var jsonSchemaManager = ClassFactory.Get<IJsonSchemaManager>();
            string jsonSchema = jsonSchemaManager.ReadSchema(JsonSchemaPackageName, JsonSchemaFileName);
            var propertyDefinition =
                JsonConvert.DeserializeObject<PropertyDefinition>(jsonSchema, jsonSerializerSettings);
            return new JsonSchema {
                Name = JsonSchemaName,
                Strict = true,
                Schema = propertyDefinition
            };
        }

        private string GenerateResponse(List<ChatMessage> messages, JsonSchema jsonSchema) {
            var genAiService = ClassFactory.Get<IGenAICompletionServiceProxy>();
            var request = new ChatCompletionRequest {
                Messages = messages,
                ResponseFormat = new ResponseFormat {
                    Type = CompletionStatics.ResponseFormat.JsonSchema,
                    JsonSchema = jsonSchema
                }
            };
            var response = genAiService.ChatCompletion(request);
            var content = response.Choices.FirstOrDefault()?.Message?.Content ?? string.Empty;
            var parsedJson = JsonConvert.DeserializeObject(content);
            var json = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            return json;
        }
        
        private string GetSummaryPrompt(List<ChatMessage> sessionMessages) {
            var messages = sessionMessages.ToList();
            messages.Add(new ChatMessage("system", "Summarize gathered information required for element generation."));
            var genAiService = ClassFactory.Get<IGenAICompletionServiceProxy>();
            var request = new ChatCompletionRequest {
                Messages = messages,
            };
            var response = genAiService.ChatCompletion(request);
            var summary = response.Choices.FirstOrDefault()?.Message?.Content ?? string.Empty;
            return summary;
        }
        
        private CopilotActionExecutionResult GenerateResult(List<ChatMessage> sessionMessages, JsonSchema jsonSchema) {
            try {
                List<ChatMessage> messages = sessionMessages.ToList();
                string summary = GetSummaryPrompt(sessionMessages);
                messages.Add(new ChatMessage("system", "Gathering information summary: " + summary));
                string response = "";
                IList<string> errors = new List<string>();
                for (int attempt = 0; attempt < 5; attempt++) {
                    response = GenerateResponse(messages, jsonSchema);
                    errors = Validate(response);
                    if (errors.Any()) {
                        messages.Add(new ChatMessage("system",
                            "Try to generate config again. The previous generated config was not valid. Config:\n```" +
                            response + "```\nErrors:" + string.Join(" ", errors)));
                    } else {
                        break;
                    }
                }
                if (errors.Any()) {
                    return new CopilotActionExecutionResult {
                        Status = CopilotActionExecutionStatus.Failed,
                        ErrorMessage = "View element config generation failed. " +
                            "The generated config is not valid. Errors: " + string.Join(" ", errors)
                    };
                }
                return new CopilotActionExecutionResult {
                    Status = CopilotActionExecutionStatus.Completed,
                    Response = response,
                    ResponseOptions = new ActionResponseOptions {
                        ForwardToClient = true,
                        OmitAssistantResponse = true,
                        ContentType = CopilotContentType.ViewElement
                    }
                };
            } catch (GenAIException ex) {
                return new CopilotActionExecutionResult {
                    Status = CopilotActionExecutionStatus.Failed,
                    ErrorMessage = "View element config generation failed. " + ex.RawError
                };
            } catch (Exception ex) {
                return new CopilotActionExecutionResult {
                    Status = CopilotActionExecutionStatus.Failed,
                    ErrorMessage = "View element config generation failed. " + ex.Message
                };
            }
        }

        #endregion

        #region Properties: Protected

        /// <summary>
        /// Gets the name of the JSON schema used for generating view element configuration.
        /// </summary>
        protected abstract string JsonSchemaName { get; }

        /// <summary>
        /// Gets the package name where the JSON schema is located.
        /// </summary>
        protected abstract string JsonSchemaPackageName { get; }

        /// <summary>
        /// Gets the JSON schema file name.
        /// </summary>
        protected virtual string JsonSchemaFileName => JsonSchemaName + ".json";

        #endregion

        #region Methods: Protected

        /// <summary>
        /// Validates the generated JSON config.
        /// </summary>
        /// <param name="json">The JSON config to validate.</param>
        /// <returns>List of validation error messages. Empty list if no errors.</returns>
        protected virtual IList<string> Validate(string json) {
            bool validatorFound =
                ClassFactory.TryGet(JsonSchemaName + "Validator", out IViewElementConfigValidator validator);
            return validatorFound ? validator.Validate(json) : new List<string>();
        }

        #endregion

        #region Methods: Public

        public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
            JsonSchema jsonSchema;
            try {
                jsonSchema = GetJsonSchema();
            } catch (Exception ex) {
                return new CopilotActionExecutionResult {
                    Status = CopilotActionExecutionStatus.Failed,
                    ErrorMessage = ex.Message
                };
            }
            var messages = GetSessionMessages(options);
            return GenerateResult(messages, jsonSchema);
        }

        #endregion

    }
}

