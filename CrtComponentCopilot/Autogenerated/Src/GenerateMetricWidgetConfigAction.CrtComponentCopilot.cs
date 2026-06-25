namespace Creatio.ComponentCopilot
{
    using Terrasoft.Common;

    public class GenerateMetricWidgetConfigAction : BaseGenerateViewElementAction
    {
        protected override string JsonSchemaName => "MetricWidgetConfigJsonSchema";

        protected override string JsonSchemaPackageName => "CrtComponentCopilot";

        public override LocalizableString GetCaption() {
            return new LocalizableString("Generate Metric Widget Config Action");
        }

        public override LocalizableString GetDescription() {
            return new LocalizableString("Generates Metric Widget Config.");
        }
    }
}

