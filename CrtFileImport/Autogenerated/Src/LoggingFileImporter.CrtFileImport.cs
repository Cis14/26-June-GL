namespace Terrasoft.Configuration.FileImport
{
	using global::Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: LoggingFileImporter

	[Override]
	public class LoggingFileImporter : FileImporter
	{

		#region Constructors: Public

		public LoggingFileImporter(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		private ILog _log;
		protected ILog Log {
			get {
				return _log ?? (_log = LogManager.GetLogger("commonAppender"));
			}
		}

		#endregion

		#region Methods: Protected

		protected override void Import(ImportParameters parameters) {
			Log.Info("ImportStart");
			base.Import(parameters);
			Log.Info("ImportEnd");
		}

		protected override void MergeImportEntities(ImportParameters parameters) {
			Log.Info("MergeImportEntitiesStart");
			base.MergeImportEntities(parameters);
			Log.Info("MergeImportEntitiesEnd");
		}

		protected override void ProcessColumnValues(ImportParameters parameters) {
			Log.Info("ProcessColumnValuesStart");
			base.ProcessColumnValues(parameters);
			Log.Info("ProcessColumnValuesEnd");
		}

		protected override void InitImportEntities(ImportParameters parameters) {
			Log.Info("InitImportEntitiesStart");
			base.InitImportEntities(parameters);
			Log.Info("InitImportEntitiesEnd");
		}

		protected override void SaveImportEntities(ImportParameters parameters) {
			Log.Info("SaveImportEntitiesStart");
			base.SaveImportEntities(parameters);
			Log.Info("SaveImportEntitiesEnd");
		}

		#endregion

	}

	#endregion

}

