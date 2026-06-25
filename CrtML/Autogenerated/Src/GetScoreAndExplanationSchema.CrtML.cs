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

	#region Class: GetScoreAndExplanation

	[DesignModeProperty(Name = "ColumnsAffectingScore", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "c9e6dbe076e641178697f18898b550f7", CaptionResourceItem = "Parameters.ColumnsAffectingScore.Caption", DescriptionResourceItem = "Parameters.ColumnsAffectingScore.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "IdForAnalysis", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "c9e6dbe076e641178697f18898b550f7", CaptionResourceItem = "Parameters.IdForAnalysis.Caption", DescriptionResourceItem = "Parameters.IdForAnalysis.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "SchemaType", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "c9e6dbe076e641178697f18898b550f7", CaptionResourceItem = "Parameters.SchemaType.Caption", DescriptionResourceItem = "Parameters.SchemaType.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class GetScoreAndExplanation : ProcessUserTask
	{

		#region Constructors: Public

		public GetScoreAndExplanation(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("c9e6dbe0-76e6-4117-8697-f18898b550f7");
		}

		#endregion

		#region Properties: Public

		public virtual string ColumnsAffectingScore {
			get;
			set;
		}

		public virtual Guid IdForAnalysis {
			get;
			set;
		}

		public virtual string SchemaType {
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
				if (!HasMapping("ColumnsAffectingScore")) {
					writer.WriteValue("ColumnsAffectingScore", ColumnsAffectingScore, null);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("IdForAnalysis")) {
					writer.WriteValue("IdForAnalysis", IdForAnalysis, Guid.Empty);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("SchemaType")) {
					writer.WriteValue("SchemaType", SchemaType, null);
				}
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "ColumnsAffectingScore":
					if (!UseFlowEngineMode) {
						break;
					}
					ColumnsAffectingScore = reader.GetStringValue();
				break;
				case "IdForAnalysis":
					if (!UseFlowEngineMode) {
						break;
					}
					IdForAnalysis = reader.GetGuidValue();
				break;
				case "SchemaType":
					if (!UseFlowEngineMode) {
						break;
					}
					SchemaType = reader.GetStringValue();
				break;
			}
		}

		#endregion

	}

	#endregion

}

