namespace Creatio.ComponentCopilot
{
    using Terrasoft.Common;

    public class GenerateSalesPipelineWidgetConfigAction : BaseGenerateViewElementAction
    {
        protected override string JsonSchemaName => "SalesPipelineWidgetConfigJsonSchema";

        protected override string JsonSchemaPackageName => "CrtComponentCopilot";

        public override LocalizableString GetCaption() {
            return new LocalizableString("Generate Sales Pipeline Widget Config Action");
        }

        public override LocalizableString GetDescription() {
            return new LocalizableString("Generates Sales Pipeline Widget Config Action");
        }
    }
}

