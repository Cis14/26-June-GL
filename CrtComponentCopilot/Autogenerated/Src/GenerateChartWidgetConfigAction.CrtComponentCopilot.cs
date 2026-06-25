namespace Creatio.ComponentCopilot
{
    using Terrasoft.Common;

    public class GenerateChartWidgetConfigAction : BaseGenerateViewElementAction
    {
        protected override string JsonSchemaName => "ChartWidgetConfigJsonSchema";

        protected override string JsonSchemaPackageName => "CrtComponentCopilot";

        public override LocalizableString GetCaption() {
            return new LocalizableString("Generate Chart Widget Config Action");
        }

        public override LocalizableString GetDescription() {
            return new LocalizableString("Generates Chart Widget Config.");
        }
    }
}

