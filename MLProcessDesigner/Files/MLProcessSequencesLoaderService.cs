namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using System.Text;
	using System.Web.SessionState;
	using CsvHelper;
	using CsvHelper.Configuration.Attributes;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Process;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Http.Abstractions;

	#region Class: MLProcessSequencesLoaderService

	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class MLProcessSequencesLoaderService: BaseService, IReadOnlySessionState
	{

		private class ProcessSequences
		{
			public ProcessSequences() {
			}

			public ProcessSequences(ProcessSequences otherProcessSequences) {
				SchemaName = otherProcessSequences.SchemaName;
				SchemaUId = otherProcessSequences.SchemaUId;
				Sequences = otherProcessSequences.Sequences;
			}

			public string SchemaName { get; set; }
			public Guid SchemaUId { get; set; }
			public List<List<SequenceItemValue>> Sequences { get; set; }
		} 

		private struct SequenceItemParseResult
		{
			public bool IsValid { get; set; }
			public SequenceItemValue Value { get; set; }
		}

		// ReSharper disable once ClassNeverInstantiated.Local
		private class ProcessDescriptor
		{
			[Name("UId")]
			public Guid UId { get; set; }

			[Optional]
			public string Metadata { get; set; }
			
			/// <summary>
			/// Sometimes csv field name may be in lower case and CsvHelper couldn't handle
			/// situation correctly
			/// </summary>
			[Optional]
			// ReSharper disable once InconsistentNaming
			public string metadata { get; set; }
		}

		#region Methods: Private

		private void Save(List<ProcessSequences> processSequencesList) {
			foreach (ProcessSequences processSequences in processSequencesList) {
				foreach (List<SequenceItemValue> sequence in processSequences.Sequences) {
					int counter = 0;
					Guid sequenceId = Guid.NewGuid();
					foreach (SequenceItemValue sequenceItemValue in sequence) {
						var elementValue = MLProcessElementValueSerializer.Serialize(sequenceItemValue.TypeName,
							sequenceItemValue.UserTaskSchemaName, sequenceItemValue.EntitySchemaName);
						var insert = new Insert(UserConnection)
							.Into("ProcessSequenceElement")
							.Set("CreatedOn", new QueryParameter(DateTime.UtcNow))
							.Set("ProcessSchemaUId", Column.Parameter(processSequences.SchemaUId))
							.Set("ProcessSchemaName", Column.Parameter(processSequences.SchemaName))
							.Set("ElementCode", Column.Parameter(elementValue))
							.Set("Position", Column.Parameter(counter))
							.Set("SequenceId", Column.Parameter(sequenceId));
						insert.Execute();
						counter++;
					}
				}

			}
		}

		private List<ProcessSequences> MergeCurrentConfigurationAndLoadedProcessSequences(
				List<ProcessSequences> currentConfigurationProcessSequencesList,
				List<ProcessSequences> loadedProcessSequencesList) {
			var resultProcessSequencesList = new List<ProcessSequences>(currentConfigurationProcessSequencesList);
			resultProcessSequencesList.AddRange(loadedProcessSequencesList.Select(FilterLoadedProcessSequences)
				.Where(filteredLoadedProcessSequences => filteredLoadedProcessSequences != null));
			return resultProcessSequencesList;
		}

		private ProcessSequences FilterLoadedProcessSequences(ProcessSequences loadedProcessSequences) {
			var filteredList = RemoveSequencesWithCustomUserTaskSchemas(loadedProcessSequences.Sequences);
			filteredList = RemoveSequencesWithCustomEntitySchemas(filteredList);
			filteredList = RemoveSequencesWithLowCountOfElements(filteredList);
			if (filteredList.IsNullOrEmpty()) {
				return null;
			}
			if (loadedProcessSequences.Sequences.Count == filteredList.Count) {
				return loadedProcessSequences;
			}
			return new ProcessSequences(loadedProcessSequences) {
				Sequences = filteredList
			};
		}

		private List<List<SequenceItemValue>> RemoveSequencesWithCustomUserTaskSchemas(
			List<List<SequenceItemValue>> sequenceItemValues) {
			var filteredList = sequenceItemValues.Where(list => list.All(value =>
				value.UserTaskSchemaName.IsNullOrEmpty() ||
				UserConnection.ProcessUserTaskSchemaManager.FindItemByName(value.UserTaskSchemaName) != null)).ToList();
			return filteredList;
		}
		
		private List<List<SequenceItemValue>> RemoveSequencesWithCustomEntitySchemas(
			List<List<SequenceItemValue>> sequenceItemValues) {
			var filteredList = sequenceItemValues.Where(list => list.All(value =>
				value.EntitySchemaName.IsNullOrEmpty() ||
				UserConnection.EntitySchemaManager.FindItemByName(value.EntitySchemaName) != null)).ToList();
			return filteredList;
		}

		private List<List<SequenceItemValue>> RemoveSequencesWithLowCountOfElements(
			List<List<SequenceItemValue>> sequenceItemValues) {
			var filteredList = sequenceItemValues.Where(list => list.Count > 2).ToList();
			return filteredList;
		}

		private List<ProcessSequences> ParseFile(Stream inputStream) {
			var processSequencesList = new List<ProcessSequences>();
			using (ZipArchive zip = new ZipArchive(inputStream)) {
				var entry = zip.Entries.First();
				var csvStream = entry.Open();
				using (var streamReader = new StreamReader(csvStream))
					using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture)) {
						while (csvReader.Read()) {
							var record = csvReader.GetRecord<ProcessDescriptor>();
							var metadata = record.Metadata ?? record.metadata;
							if (metadata.IsNullOrWhiteSpace()) {
								continue;
							}
							if (!string.IsNullOrWhiteSpace(record.metadata)) {
								// postgres cloud executor generates corrupted filter data - this is a way to fix it
								metadata = metadata.Replace("\\\\", "\\");
							}
							metadata = metadata.Substring(metadata.IndexOf("{", StringComparison.InvariantCulture));
							ProcessSequences processSequences = ParseProcess(metadata);
							if (processSequences != null) {
								processSequencesList.Add(processSequences);
							}
						}
					}
			}
			return processSequencesList;
		}
		
		private List<List<SequenceItemValue>> GetProcessSequencesItems(ProcessSchema processSchema,
				List<List<Guid>> sequenceList) {
			List<ProcessSchemaBaseElement> elements = processSchema.GetBaseElements().ToList();
			List<ProcessSchemaSequenceFlow> sequenceFlows = elements.OfType<ProcessSchemaSequenceFlow>().ToList();
			IEnumerable<Guid> startElementUIds = elements.Where(element =>
				sequenceFlows.All(inFlow => inFlow.TargetRefUId != element.UId) &&
				sequenceFlows.Any(outFlow => outFlow.SourceRefUId == element.UId)).Select(element => element.UId);
			foreach (Guid startElementUId in startElementUIds) {
				ParseSequences(startElementUId, sequenceList, sequenceFlows);
			}
			var processSequencesItems = sequenceList
				.Select(list => list
					.Select(elementUId => elements.Where(element => element.UId == elementUId)
							.Select(element => GetSequenceItemValue(processSchema, element))
							.FirstOrDefault())
				)
				.Where(list => list.All(element => element.IsValid && element.Value != null))
				.Select(list => list.Select(element => element.Value).ToList())
				.Where(list => !list.IsEmpty())
				.ToList();
			return processSequencesItems;
		}

		private List<MLSequenceItem> ItemValueListToMLSequenceList(
			List<List<SequenceItemValue>> stringItemSequenceList) {
			var mlSequenceList = new List<MLSequenceItem>();
			foreach (List<SequenceItemValue> sequenceItemValues in stringItemSequenceList) {
				string sequenceId = Guid.NewGuid().ToString();
				int position = 0;
				mlSequenceList.AddRange(sequenceItemValues.Select(itemValue => new MLSequenceItem {
					SequenceId = sequenceId,
					Position = position++,
					Value = itemValue
				}));
			}
			return mlSequenceList;
		}

		private bool IsEntityRequiredForElementType(string elementType) {
			const string startSignalEventTypeName = "Terrasoft.Core.Process.ProcessSchemaStartSignalEvent";
			return elementType != startSignalEventTypeName
				&& MLProcessElementParameterParser.TryGetEntityParameterName(elementType, out _);
		}

		private SequenceItemParseResult GetSequenceItemValue(ProcessSchema processSchema,
				ProcessSchemaBaseElement baseElement) {
			var parametrizedElements = processSchema.GetParametrizedElements();
			IParametrizedProcessSchemaElement parametrizedElement = parametrizedElements
				.FirstOrDefault(element => element.UId == baseElement.UId);
			ProcessSchemaFlowElement flowElement = processSchema.FlowElements
				.FirstOrDefault(schemaFlowElement => schemaFlowElement.UId == baseElement.UId);
			var schemaName = (flowElement as ProcessSchemaUserTask)?.Schema?.Name;
			var typeName = schemaName ?? baseElement.TypeName;
			var entitySchemaRequired = IsEntityRequiredForElementType(typeName);
			var entitySchemaUId = flowElement is ProcessSchemaStartSignalEvent startSignalEvent 
				? startSignalEvent.EntitySchemaUId
				: MLProcessElementParameterParser.GetElementEntitySchemaUId(typeName, parametrizedElement?.Parameters);
			string referenceSchemaName = null;
			if (entitySchemaUId.HasValue) {
				referenceSchemaName = UserConnection.EntitySchemaManager.FindItemByUId(entitySchemaUId.Value)?.Name;
			}
			if (entitySchemaRequired && string.IsNullOrWhiteSpace(referenceSchemaName)) {
				return new SequenceItemParseResult {
					Value = null,
					IsValid = false
				};
			}
			var value = new SequenceItemValue(baseElement.TypeName, schemaName,
				referenceSchemaName);
			return new SequenceItemParseResult {
				Value = value,
				IsValid = true
			};
		}

		private void ParseSequences(Guid startElementUId, List<List<Guid>> resultSequencesList,
				List<ProcessSchemaSequenceFlow> sequenceFlows) {
			var inProgressSequences = new List<List<Guid>> {
				new List<Guid> {
					startElementUId
				}
			};
			var processedElementUIds = new List<Guid> {
				startElementUId
			};
			while (true) {
				if (inProgressSequences.Count == 0) {
					break;
				}
				List<Guid> sequence = inProgressSequences.Last();
				if (sequence.IsEmpty()) {
					inProgressSequences.Remove(sequence);
					continue;
				}
				Guid elementUId = sequence.Last();
				var nextElementUIds = sequenceFlows 
					.Where(outFlows => outFlows.SourceRefUId == elementUId
						&& !processedElementUIds.Contains(outFlows.TargetRefUId))
					.Select(outFlow => outFlow.TargetRefUId)
					.ToList();
				if (nextElementUIds.IsEmpty()) {
					inProgressSequences.Remove(sequence);
					resultSequencesList.Add(sequence);
					continue;
				}
				bool isFirst = true;
				foreach (Guid nextElementUId in nextElementUIds) {
					if (isFirst) {
						isFirst = false;
						if (sequence.Contains(nextElementUId)) {
							inProgressSequences.Remove(sequence);
							resultSequencesList.Add(sequence);
						}
						sequence.Add(nextElementUId);
					} else {
						List<Guid> newSequence = sequence.GetRange(0, sequence.Count - 1);
						newSequence.Add(nextElementUId);
						inProgressSequences.Add(newSequence);
					}
					processedElementUIds.Add(nextElementUId);
				}
			}
		}
		
		private ProcessSequences ParseProcessSchema(ProcessSchema processSchema) {
			var sequenceList = new List<List<Guid>>();
			List<List<SequenceItemValue>> sequences = GetProcessSequencesItems(processSchema, sequenceList);
			return new ProcessSequences {
				SchemaName = processSchema.Name,
				SchemaUId = processSchema.UId,
				Sequences = sequences
			};
		}

		private List<ProcessSequences> LoadCurrentConfigurationProcessSequences() {
			ProcessSchemaManager processSchemaManager = UserConnection.ProcessSchemaManager;
			IEnumerable<ISchemaManagerItem<ProcessSchema>> managerItems = processSchemaManager.GetItems();
			IEnumerable<ProcessSchema> schemas = managerItems
				.Select(item => item.SafeInstance).Where(schema => schema != null);
			return schemas.Select(ParseProcessSchema).ToList();
		}


		private ProcessSequences ParseProcess(string metadata) {
			var processParser = new ProcessJsonMetaDataSerializer(UserConnection, new JsonDataWriterSettings());
			var stream = new MemoryStream();
			using (var streamWriter = new StreamWriter(stream, Encoding.UTF8, 1024, true)) {
				streamWriter.Write(metadata);
			}
			stream.Seek(0, SeekOrigin.Begin);
			var processItem = processParser.Deserialize(stream);
			ProcessSchema schema;
			try {
				schema = processItem.First().SafeInstance as ProcessSchema;
			} catch (Exception e) {
				Console.WriteLine($"Error {e} while parsing process metadata \n{metadata}");
				return null;
			}
			return ParseProcessSchema(schema);
		}

		#endregion

		#region Methods: Public

		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json, UriTemplate = "LoadProcesses")]
		public int LoadProcesses() {
			HttpFileCollection files = HttpContextAccessor.GetInstance().Request.Files;
			var currentConfigurationProcessSequencesList = LoadCurrentConfigurationProcessSequences();
			var loadedProcessSequencesList = new List<ProcessSequences>();
			foreach (string fileName in files.AllKeys) {
				var file = files[fileName];
				Stream inputStream = file.InputStream;
				if (inputStream.Length == 0) {
					continue;
				}
				List<ProcessSequences> fileProcessSequencesList = ParseFile(inputStream);
				if (fileProcessSequencesList != null) {
					loadedProcessSequencesList.AddRange(fileProcessSequencesList);
				}
			}
			List<ProcessSequences> resultProcessSequencesList = MergeCurrentConfigurationAndLoadedProcessSequences(
				currentConfigurationProcessSequencesList, loadedProcessSequencesList);
			Save(resultProcessSequencesList);
			return resultProcessSequencesList.Count;
		}

		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "GetProcessElementSequences")]
		public List<MLSequenceItem> GetProcessElementSequences() {
			ProcessSchemaManager processSchemaManager = UserConnection.ProcessSchemaManager;
			IEnumerable<ISchemaManagerItem<ProcessSchema>> managerItems = processSchemaManager.GetItems();
			IEnumerable<ProcessSchema> schemas = managerItems
				.Select(item => item.SafeInstance)
				.Where(schema => schema != null);
			var sequenceList = new List<List<Guid>>();
			var elementTypeSequenceList = new List<List<SequenceItemValue>>();
			foreach (ProcessSchema processSchema in schemas) {
				List<List<SequenceItemValue>> processElementTypeSequences = 
					GetProcessSequencesItems(processSchema, sequenceList);
				elementTypeSequenceList.AddRange(processElementTypeSequences);
			}
			var mlSequenceList = ItemValueListToMLSequenceList(elementTypeSequenceList);
			return mlSequenceList;
		}

		#endregion

	}

	#endregion

	#region Class: SequenceItemValue

	[DataContract]
	public class SequenceItemValue
	{
		public SequenceItemValue(string typeName, string userTaskSchemaName, string entitySchemaName) {
			TypeName = typeName;
			UserTaskSchemaName = userTaskSchemaName;
			EntitySchemaName = entitySchemaName;
		}
			
		[DataMember(Name = "typeName")]
		public string TypeName { get; set; }
		[DataMember(Name = "userTaskSchemaName")]
		public string UserTaskSchemaName { get; set; }
		[DataMember(Name = "entitySchemaName")]
		public string EntitySchemaName { get; set; }
	}

	#endregion

	#region Class: MLSequenceItem

	[DataContract]
	public class MLSequenceItem
	{
		[DataMember(Name = "sequenceId")]
		public string SequenceId { get; set; }
	
		[DataMember(Name = "position")]
		public long Position { get; set; }
	
		[DataMember(Name = "value")]
		public SequenceItemValue Value { get; set; }
	}

	#endregion

}
