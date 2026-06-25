namespace Terrasoft.Core.Process.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;

	#region Class: ActualizeSysDataLocalizableValueUserTask

	[DesignModeProperty(Name = "HasErrors", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.HasErrors.Caption", DescriptionResourceItem = "Parameters.HasErrors.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "ProcessedSchemas", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.ProcessedSchemas.Caption", DescriptionResourceItem = "Parameters.ProcessedSchemas.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "SchemasWithErrors", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.SchemasWithErrors.Caption", DescriptionResourceItem = "Parameters.SchemasWithErrors.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "UpdatedSchemaNames", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.UpdatedSchemaNames.Caption", DescriptionResourceItem = "Parameters.UpdatedSchemaNames.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "UpdatedColumnNames", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.UpdatedColumnNames.Caption", DescriptionResourceItem = "Parameters.UpdatedColumnNames.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "DeletedRecords", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.DeletedRecords.Caption", DescriptionResourceItem = "Parameters.DeletedRecords.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "ResultMessage", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "78bee4e447464a1a815a350e18b03098", CaptionResourceItem = "Parameters.ResultMessage.Caption", DescriptionResourceItem = "Parameters.ResultMessage.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class ActualizeSysDataLocalizableValueUserTask : ProcessUserTask
	{

		#region Constructors: Public

		public ActualizeSysDataLocalizableValueUserTask(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("78bee4e4-4746-4a1a-815a-350e18b03098");
		}

		#endregion

		#region Properties: Public

		public virtual bool HasErrors {
			get;
			set;
		}

		public virtual int ProcessedSchemas {
			get;
			set;
		}

		public virtual int SchemasWithErrors {
			get;
			set;
		}

		public virtual int UpdatedSchemaNames {
			get;
			set;
		}

		public virtual int UpdatedColumnNames {
			get;
			set;
		}

		public virtual int DeletedRecords {
			get;
			set;
		}

		public virtual string ResultMessage {
			get;
			set;
		}

		private LocalizableString _failCase;
		public LocalizableString FailCase {
			get {
				return _failCase ?? (_failCase = new LocalizableString(Storage, Schema.GetResourceManagerName(), "LocalizableStrings.FailCase.Value"));
			}
		}

		private LocalizableString _successfulCase;
		public LocalizableString SuccessfulCase {
			get {
				return _successfulCase ?? (_successfulCase = new LocalizableString(Storage, Schema.GetResourceManagerName(), "LocalizableStrings.SuccessfulCase.Value"));
			}
		}

		#endregion

		#region Methods: Public

		public override void WritePropertiesData(DataWriter writer) {
			writer.WriteStartObject(Name);
			base.WritePropertiesData(writer);
			if (Status == Core.Process.ProcessStatus.Inactive) {
				writer.WriteFinishObject();
				return;
			}
			if (!HasMapping("HasErrors")) {
				writer.WriteValue("HasErrors", HasErrors, false);
			}
			if (!HasMapping("ProcessedSchemas")) {
				writer.WriteValue("ProcessedSchemas", ProcessedSchemas, 0);
			}
			if (!HasMapping("SchemasWithErrors")) {
				writer.WriteValue("SchemasWithErrors", SchemasWithErrors, 0);
			}
			if (!HasMapping("UpdatedSchemaNames")) {
				writer.WriteValue("UpdatedSchemaNames", UpdatedSchemaNames, 0);
			}
			if (!HasMapping("UpdatedColumnNames")) {
				writer.WriteValue("UpdatedColumnNames", UpdatedColumnNames, 0);
			}
			if (!HasMapping("DeletedRecords")) {
				writer.WriteValue("DeletedRecords", DeletedRecords, 0);
			}
			if (!HasMapping("ResultMessage")) {
				writer.WriteValue("ResultMessage", ResultMessage, null);
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "HasErrors":
					HasErrors = reader.GetBoolValue();
				break;
				case "ProcessedSchemas":
					ProcessedSchemas = reader.GetIntValue();
				break;
				case "SchemasWithErrors":
					SchemasWithErrors = reader.GetIntValue();
				break;
				case "UpdatedSchemaNames":
					UpdatedSchemaNames = reader.GetIntValue();
				break;
				case "UpdatedColumnNames":
					UpdatedColumnNames = reader.GetIntValue();
				break;
				case "DeletedRecords":
					DeletedRecords = reader.GetIntValue();
				break;
				case "ResultMessage":
					ResultMessage = reader.GetStringValue();
				break;
			}
		}

		#endregion

	}

	#endregion

}

