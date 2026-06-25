namespace Terrasoft.Configuration.Translation
{
    using Creatio.FeatureToggling;

    #region Class: UseEntityDuringTranslationActualization

    internal class UseEntityDuringTranslationActualization : FeatureMetadata
    {

        #region Constructors: Public

        public UseEntityDuringTranslationActualization() {
            IsEnabled = false;
            Description = "Enables the use of an entity instead of a query when actualizing translations.";
        }

        #endregion

    }

    #endregion

    #region Class: UseStoredProcedureForCleaningUnusedResources

    internal class UseStoredProcedureForCleaningUnusedResources : FeatureMetadata
    {

        #region Constructors: Public

        public UseStoredProcedureForCleaningUnusedResources() {
            IsEnabled = false;
            Description = "Enables the use of stored procedure for cleaning unused translation resources.";
        }

        #endregion

    }

    #endregion

    #region Class: EnableCoreResourcesExport

    internal class EnableCoreResourcesExport : FeatureMetadata
    {

        #region Constructors: Public

        public EnableCoreResourcesExport() {
            IsEnabled = true;
            Description = "Enables downloading of translation resources for Creatio Core resources.";
        }

        #endregion

    }

    #endregion

}
