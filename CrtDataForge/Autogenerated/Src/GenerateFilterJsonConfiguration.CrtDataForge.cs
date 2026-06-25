namespace Terrasoft.Core.Process
{

	using Creatio.Copilot;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: GenerateFilterJsonConfigurationMethodsWrapper

	/// <exclude/>
	public class GenerateFilterJsonConfigurationMethodsWrapper : ProcessModel
	{

		public GenerateFilterJsonConfigurationMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ConvertToEsqFilterExecute", ConvertToEsqFilterExecute);
			AddScriptTaskMethod("ValidateJsonExecute", ValidateJsonExecute);
		}

		#region Methods: Private

		private bool ConvertToEsqFilterExecute(ProcessExecutingContext context) {
			var llmEsqConverter = ClassFactory.Get<ILlmEsqConverter>();
			var filterJson = Get<string>("FilterJson");
			var llmFilterRequest = JsonConvert.DeserializeObject<LlmFilterRequest>(filterJson);
			var serializedFilter = llmEsqConverter.ConvertToEsqFilter(llmFilterRequest.filter, llmFilterRequest.rootSchemaName);
			var filter = JsonConvert.SerializeObject(serializedFilter);
			Set("EsqFilter", filter);
			return true;
		}

		private bool ValidateJsonExecute(ProcessExecutingContext context) {
			var strInput = Get<string>("RefinedFilterJson");
			
			if (string.IsNullOrWhiteSpace(strInput)) {
				Set("RefinedFilterIsValid", false);
				Set("RefinedFilterValidationError", "Input is empty or whitespace.");
				return true;
			}
			
			try {
				JsonConvert.DeserializeObject<object>(strInput.Trim());
				Set("RefinedFilterIsValid", true);
			} catch (Exception e) {
				Set("RefinedFilterIsValid", false);
				Set("RefinedFilterValidationError", e.Message);
			}
			
			return true;
		}

		#endregion

	}

	#endregion

}

