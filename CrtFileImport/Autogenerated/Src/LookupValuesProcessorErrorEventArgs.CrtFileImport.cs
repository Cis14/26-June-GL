namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region  Class: LookupValuesProcessorErrorEventArgs

	public class LookupValuesProcessorErrorEventArgs : EventArgs
	{
		#region Constructors: Public

		public LookupValuesProcessorErrorEventArgs(Guid entitySchemaUId, string columnName, string missingValue, Exception
				exception) {
			EntitySchemaUId = entitySchemaUId;
			ColumnName = columnName;
			MissingValue = missingValue;
			Exception = exception;
		}

		#endregion

		#region Properties: Public

		public Exception Exception { get; internal set; }

		public Guid EntitySchemaUId { get; internal set; }
		public string MissingValue { get; internal set; }

		public string ColumnName { get; internal set; }

		#endregion
	}

	#endregion
}

