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

	#region Class: CreateFilterFolder

	[DesignModeProperty(Name = "EntitySchemaName", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "f1d1ee0755e143e2b27044f8d339079a", CaptionResourceItem = "Parameters.EntitySchemaName.Caption", DescriptionResourceItem = "Parameters.EntitySchemaName.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "FilterJsonConfiguration", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "f1d1ee0755e143e2b27044f8d339079a", CaptionResourceItem = "Parameters.FilterJsonConfiguration.Caption", DescriptionResourceItem = "Parameters.FilterJsonConfiguration.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "FolderName", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "f1d1ee0755e143e2b27044f8d339079a", CaptionResourceItem = "Parameters.FolderName.Caption", DescriptionResourceItem = "Parameters.FolderName.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class CreateFilterFolder : ProcessUserTask
	{

		#region Constructors: Public

		public CreateFilterFolder(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("f1d1ee07-55e1-43e2-b270-44f8d339079a");
		}

		#endregion

		#region Properties: Public

		public virtual string EntitySchemaName {
			get;
			set;
		}

		public virtual string FilterJsonConfiguration {
			get;
			set;
		}

		public virtual string FolderName {
			get;
			set;
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
			if (UseFlowEngineMode) {
				if (!HasMapping("EntitySchemaName")) {
					writer.WriteValue("EntitySchemaName", EntitySchemaName, null);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("FilterJsonConfiguration")) {
					writer.WriteValue("FilterJsonConfiguration", FilterJsonConfiguration, null);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("FolderName")) {
					writer.WriteValue("FolderName", FolderName, null);
				}
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "EntitySchemaName":
					if (!UseFlowEngineMode) {
						break;
					}
					EntitySchemaName = reader.GetStringValue();
				break;
				case "FilterJsonConfiguration":
					if (!UseFlowEngineMode) {
						break;
					}
					FilterJsonConfiguration = reader.GetStringValue();
				break;
				case "FolderName":
					if (!UseFlowEngineMode) {
						break;
					}
					FolderName = reader.GetStringValue();
				break;
			}
		}

		#endregion

	}

	#endregion

}

