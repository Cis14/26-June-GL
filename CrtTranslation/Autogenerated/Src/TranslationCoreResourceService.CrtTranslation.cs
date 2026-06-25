namespace Terrasoft.Configuration.Translation
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using Creatio.FeatureToggling;
    using Terrasoft.Core;
    using Terrasoft.Core.ConfigurationBuild;
    using Terrasoft.Core.Factories;
    using Terrasoft.Web.Common;
    using Terrasoft.Web.Http.Abstractions;

    #region Class: TranslationCoreResourceService

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TranslationCoreResourceService : BaseService
    {

        #region Fields: Private

        private const string CoreResourcesFileName = "CoreTranslations.xlsx";
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        #endregion

        #region Properties: Private

        private HttpContext CurrentContext => HttpContextAccessor.GetInstance();

        private IConfigurationRuntimeDirectoryProvider ConfigurationRuntimeDirectoryProvider =>
            ClassFactory.Get<IConfigurationRuntimeDirectoryProvider>();

        #endregion

        #region Methods: Private

        private Stream GetExcelStream(string directoryPath, string fileName) {
            try {
                string fullPath = Path.Combine(directoryPath, fileName);
                if (!File.Exists(fullPath)) {
                    throw new FileNotFoundException($"File not found: {fullPath}");
                }
                string extension = Path.GetExtension(fullPath).ToLower();
                if (extension != ".xls" && extension != ".xlsx") {
                    throw new InvalidOperationException("File has to be a Excel file (.xls lub .xlsx).");
                }
                return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            } catch (Exception ex) {
                throw new ApplicationException("Error occurred while getting the excel file.", ex);
            }
        }

        private string GetCoreResourcesDirectoryPath() {
            return Path.Combine(ConfigurationRuntimeDirectoryProvider.GetConfigurationRuntimeDirectory(), "conf");
        }

        #endregion

        #region Methods: Public

        [OperationContract]
        [WebGet(UriTemplate = "GetExcelCoreResources")]
        public Stream GetExcelCoreResources() {
            if (!Features.GetIsEnabled("EnableCoreResourcesExport")) {
                var emptyStream = new MemoryStream();
                emptyStream.Position = 0;
                return emptyStream;
            }
            var directoryPath = GetCoreResourcesDirectoryPath();
            var excelStream = (FileStream)GetExcelStream(directoryPath, CoreResourcesFileName);
            excelStream.Seek(0, SeekOrigin.Begin);
            CurrentContext.Response.ContentType = ExcelContentType;
            CurrentContext.Response.AddHeader("Content-Disposition",
                $"attachment; filename=\"{CoreResourcesFileName}\"");
            return excelStream;
        }

        #endregion

    }

    #endregion

}

