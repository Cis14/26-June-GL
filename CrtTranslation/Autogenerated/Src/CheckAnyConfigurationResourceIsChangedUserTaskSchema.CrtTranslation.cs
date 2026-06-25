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

	#region Class: CheckAnyConfigurationResourceIsChangedUserTask

	[DesignModeProperty(Name = "ApplySessionId", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0729743859cc4f46be3b8be22257ce74", CaptionResourceItem = "Parameters.ApplySessionId.Caption", DescriptionResourceItem = "Parameters.ApplySessionId.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "HasChangedConfigurationResources", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0729743859cc4f46be3b8be22257ce74", CaptionResourceItem = "Parameters.HasChangedConfigurationResources.Caption", DescriptionResourceItem = "Parameters.HasChangedConfigurationResources.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "IsForceUpdate", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0729743859cc4f46be3b8be22257ce74", CaptionResourceItem = "Parameters.IsForceUpdate.Caption", DescriptionResourceItem = "Parameters.IsForceUpdate.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "LanguageId", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0729743859cc4f46be3b8be22257ce74", CaptionResourceItem = "Parameters.LanguageId.Caption", DescriptionResourceItem = "Parameters.LanguageId.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "UseSpecifiedLanguageOnly", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0729743859cc4f46be3b8be22257ce74", CaptionResourceItem = "Parameters.UseSpecifiedLanguageOnly.Caption", DescriptionResourceItem = "Parameters.UseSpecifiedLanguageOnly.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class CheckAnyConfigurationResourceIsChangedUserTask : ProcessUserTask
	{

		#region Constructors: Public

		public CheckAnyConfigurationResourceIsChangedUserTask(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("07297438-59cc-4f46-be3b-8be22257ce74");
		}

		#endregion

		#region Properties: Public

		public virtual Guid ApplySessionId {
			get;
			set;
		}

		public virtual bool HasChangedConfigurationResources {
			get;
			set;
		}

		public virtual bool IsForceUpdate {
			get;
			set;
		}

		public virtual Guid LanguageId {
			get;
			set;
		}

		public virtual bool UseSpecifiedLanguageOnly {
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
			if (!HasMapping("ApplySessionId")) {
				writer.WriteValue("ApplySessionId", ApplySessionId, Guid.Empty);
			}
			if (!HasMapping("HasChangedConfigurationResources")) {
				writer.WriteValue("HasChangedConfigurationResources", HasChangedConfigurationResources, false);
			}
			if (!HasMapping("IsForceUpdate")) {
				writer.WriteValue("IsForceUpdate", IsForceUpdate, false);
			}
			if (!HasMapping("LanguageId")) {
				writer.WriteValue("LanguageId", LanguageId, Guid.Empty);
			}
			if (!HasMapping("UseSpecifiedLanguageOnly")) {
				writer.WriteValue("UseSpecifiedLanguageOnly", UseSpecifiedLanguageOnly, false);
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "ApplySessionId":
					ApplySessionId = reader.GetGuidValue();
				break;
				case "HasChangedConfigurationResources":
					HasChangedConfigurationResources = reader.GetBoolValue();
				break;
				case "IsForceUpdate":
					IsForceUpdate = reader.GetBoolValue();
				break;
				case "LanguageId":
					LanguageId = reader.GetGuidValue();
				break;
				case "UseSpecifiedLanguageOnly":
					UseSpecifiedLanguageOnly = reader.GetBoolValue();
				break;
			}
		}

		#endregion

	}

	#endregion

}

