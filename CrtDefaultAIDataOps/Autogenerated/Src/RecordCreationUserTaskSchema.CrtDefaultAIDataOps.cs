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

	#region Class: RecordCreationUserTask

	[DesignModeProperty(Name = "ColumnValueMappingString", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.ColumnValueMappingString.Caption", DescriptionResourceItem = "Parameters.ColumnValueMappingString.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "ObjectCode", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.ObjectCode.Caption", DescriptionResourceItem = "Parameters.ObjectCode.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "CreatedRecordId", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.CreatedRecordId.Caption", DescriptionResourceItem = "Parameters.CreatedRecordId.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "OutputError", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.OutputError.Caption", DescriptionResourceItem = "Parameters.OutputError.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "DryRun", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.DryRun.Caption", DescriptionResourceItem = "Parameters.DryRun.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "HasError", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "7a3badcbc4d048bcae879060e49a4606", CaptionResourceItem = "Parameters.HasError.Caption", DescriptionResourceItem = "Parameters.HasError.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class RecordCreationUserTask : ProcessUserTask
	{

		#region Constructors: Public

		public RecordCreationUserTask(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("7a3badcb-c4d0-48bc-ae87-9060e49a4606");
		}

		#endregion

		#region Properties: Public

		public virtual string ColumnValueMappingString {
			get;
			set;
		}

		public virtual string ObjectCode {
			get;
			set;
		}

		public virtual Guid CreatedRecordId {
			get;
			set;
		}

		public virtual string OutputError {
			get;
			set;
		}

		public virtual bool DryRun {
			get;
			set;
		}

		public virtual bool HasError {
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
			if (!HasMapping("ColumnValueMappingString")) {
				writer.WriteValue("ColumnValueMappingString", ColumnValueMappingString, null);
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("ObjectCode")) {
					writer.WriteValue("ObjectCode", ObjectCode, null);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("CreatedRecordId")) {
					writer.WriteValue("CreatedRecordId", CreatedRecordId, Guid.Empty);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("OutputError")) {
					writer.WriteValue("OutputError", OutputError, null);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("DryRun")) {
					writer.WriteValue("DryRun", DryRun, false);
				}
			}
			if (!HasMapping("HasError")) {
				writer.WriteValue("HasError", HasError, false);
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "ColumnValueMappingString":
					ColumnValueMappingString = reader.GetStringValue();
				break;
				case "ObjectCode":
					if (!UseFlowEngineMode) {
						break;
					}
					ObjectCode = reader.GetStringValue();
				break;
				case "CreatedRecordId":
					if (!UseFlowEngineMode) {
						break;
					}
					CreatedRecordId = reader.GetGuidValue();
				break;
				case "OutputError":
					if (!UseFlowEngineMode) {
						break;
					}
					OutputError = reader.GetStringValue();
				break;
				case "DryRun":
					if (!UseFlowEngineMode) {
						break;
					}
					DryRun = reader.GetBoolValue();
				break;
				case "HasError":
					HasError = reader.GetBoolValue();
				break;
			}
		}

		#endregion

	}

	#endregion

}

