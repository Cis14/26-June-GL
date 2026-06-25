namespace Creatio.ComponentCopilot
{
    using Terrasoft.Common;

    public class GenerateFullPipelineWidgetConfigAction : BaseGenerateViewElementAction
    {
        protected override string JsonSchemaName => "FullPipelineWidgetConfigJsonSchema";

        protected override string JsonSchemaPackageName => "CrtComponentCopilot";

        public override LocalizableString GetCaption() {
            return new LocalizableString("Generate Full Pipeline Widget Config Action");
        }

        public override LocalizableString GetDescription() {
            return new LocalizableString("Generates Full Pipeline Widget Config Action");
        }
    }
}

