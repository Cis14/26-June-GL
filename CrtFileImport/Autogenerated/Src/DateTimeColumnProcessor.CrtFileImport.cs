namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Globalization;
	using System.Text.RegularExpressions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: DateTimeColumnProcessor

	/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
	/// <summary>
	///     An instance of this class is responsible for processing DateTime column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(DateTimeColumnProcessor))]
	public class DateTimeColumnProcessor : NonPersistentColumnProcessor<DateTime?>, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		/// <summary>
		///     Creates instance of type <see cref="DateTimeColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public DateTimeColumnProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override Guid DataValueTypeUId => DataValueType.DateTimeDataValueTypeUId;

		#endregion

		#region Methods: Private

		/// <summary>
		///     Tries to process value, assuming it is DateTime.
		/// </summary>
		/// <param name="value">Raw value.</param>
		/// <param name="convertedValue">Result value.</param>
		/// <returns>Processing result.</returns>
		private bool TryProcessValueAsDateTime(string value, out DateTime? convertedValue) {
			convertedValue = null;
			bool result = DateTime.TryParse(value, GeneralResourceStorage.CurrentCulture,
				DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out DateTime dateTime);
			if (result)
				convertedValue = dateTime;
			return result;
		}

		/// <summary>
		///     Tries to process value, assuming it is double.
		/// </summary>
		/// <param name="value">Raw value.</param>
		/// <param name="convertedValue">Result value.</param>
		/// <returns>Processing result.</returns>
		private bool TryProcessValueAsDouble(string value, out DateTime? convertedValue) {
			convertedValue = null;
			CultureInfo currentCulture = GeneralResourceStorage.CurrentCulture;
			string cultureSpecificValue = Regex.Replace(value, "[,.]",
				currentCulture.NumberFormat.NumberDecimalSeparator);
			bool result = double.TryParse(cultureSpecificValue, out double oADate);
			if (result) {
				DateTime dateTime = DateTime.FromOADate(oADate);
				DateTimeKind dateTimeKind = UserConnection.GetIsFeatureEnabled("ImportDateTimeKindUnspecified")
					? DateTimeKind.Unspecified
					: DateTimeKind.Local;
				DateTime localDateTime = DateTime.SpecifyKind(dateTime, dateTimeKind);
				convertedValue = localDateTime;
			}
			return result;
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="NonPersistentColumnProcessor{TResult}"/>
		protected override DateTime? ConvertValue(ImportColumnValue columnValue) {
			base.ConvertValue(columnValue);
			string value = columnValue.Value;
			if (!(TryProcessValueAsDouble(value, out var convertedValue) ||
				TryProcessValueAsDateTime(value, out convertedValue)))
				return null;
			return convertedValue;
		}

		#endregion

	}

	#endregion

}

