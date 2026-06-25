namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Terrasoft.Core;

	#region Class: ImportValueFormatting

	public class ImportValueFormatting
	{
		#region Const: Private

		private const string _dateFormat = "yyyy-MM-dd";
		private const string _timeFormat = "HH:mm:ss";
		private const string _timeSpanFormat = @"hh\:mm\:ss";
		private const string _decimalMaskMajor = "0.";
		private const char _decimalMaskMinor = '0';

		#endregion

		#region Fields: Protected

		protected KeyValuePair<string, DataValueType>[] _columnsType;

		#endregion

		#region Constructors: Public

		public ImportValueFormatting(KeyValuePair<string, DataValueType>[] columnsType) {
			_columnsType = columnsType;
		}

		#endregion

		#region Methods: Protected

		protected virtual TimeSpan GetTimeSpan(object valueForSave) {
			var valueType = valueForSave.GetType();
			if (valueType.Equals(typeof(TimeSpan))) {
				return (TimeSpan)valueForSave;
			} else {
				return new TimeSpan(((DateTime)valueForSave).Ticks);
			}
		}

		protected virtual object GetDateTimeValueType(DateTime valueForSave, string format) {
			return valueForSave.ToString(format);
		}

		protected virtual object GetTimeValueType(TimeSpan valueForSave) {
			return valueForSave.ToString(_timeSpanFormat);
		}

		protected virtual string GetDecimalFormat(FloatDataValueType colType) {
			var precision = colType.Precision;
			return _decimalMaskMajor.PadRight(_decimalMaskMajor.Length + precision, _decimalMaskMinor);
		}


		#endregion

		#region Methods: Public

		/// <summary>
		/// Value formatting for keys compare.
		/// </summary>
		/// <param name="columnName">Entity column name.</param>
		/// <param name="valueForSave">Entity value.</param>
		/// <returns></returns>
		public virtual object GetPostProcessingValueForSave(string columnName, object valueForSave) {
			var colType = _columnsType?.FirstOrDefault(c => c.Key == columnName).Value;
			if (string.IsNullOrEmpty(valueForSave?.ToString()) || colType == null) {
				return valueForSave;
			}
			switch (((DBDataValueType)colType).ClientDataValueType) {
				case "Terrasoft.DataValueType.TIME":
					return GetTimeValueType(GetTimeSpan(valueForSave));
				case "Terrasoft.DataValueType.DATE":
					return GetDateTimeValueType((DateTime)valueForSave, _dateFormat);
				case "Terrasoft.DataValueType.DATE_TIME":
					return GetDateTimeValueType((DateTime)valueForSave, $"{_dateFormat} {_timeFormat}");
				case "Terrasoft.DataValueType.MONEY":
				case "Terrasoft.DataValueType.FLOAT":
					return ((decimal)valueForSave).ToString(GetDecimalFormat((FloatDataValueType)colType));
				default:
					return valueForSave;
			}
		}

		#endregion
	}

	#endregion
}
