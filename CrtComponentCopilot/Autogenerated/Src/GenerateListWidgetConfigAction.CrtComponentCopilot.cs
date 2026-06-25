namespace Creatio.ComponentCopilot
{
    using Terrasoft.Common;

    public class GenerateListWidgetConfigAction : BaseGenerateViewElementAction
    {
        protected override string JsonSchemaName => "ListWidgetConfigJsonSchema";

        protected override string JsonSchemaPackageName => "CrtComponentCopilot";

        public override LocalizableString GetCaption() {
            return new LocalizableString("Generate List Widget Config Action");
        }

        public override LocalizableString GetDescription() {
            return new LocalizableString("Generates List Widget Config.");
        }
    }
}

