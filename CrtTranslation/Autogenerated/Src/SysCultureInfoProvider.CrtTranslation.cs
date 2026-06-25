namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;

	#region Class: SysCultureInfoProvider

	public class SysCultureInfoProvider : ISysCultureInfoProvider
	{

		#region Constructors: Public

		public SysCultureInfoProvider(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		private UserConnection UserConnection {
			get;
			set;
		}

		/// <summary>
		/// Available cultures.
		/// </summary>
		private List<ISysCultureInfo> _cultures;
		private List<ISysCultureInfo> Cultures {
			get {
				return _cultures ?? (_cultures = GetCultures());
			}
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets available cultures.
		/// </summary>
		/// <returns>Cultures.</returns>
		private List<ISysCultureInfo> GetCultures() {
			var cultures = new List<ISysCultureInfo>();
			var sysCultureSelect = 
				new Select(UserConnection)
					.Column("Id")
					.Column("Index")
					.Column("LanguageId")
					.Column("Name")
					.Column("Active")
				.From("SysCulture") as Select;
			sysCultureSelect.ExecuteReader(dataReader => {
				Guid id = dataReader.GetColumnValue<Guid>("Id");
				int index = dataReader.GetColumnValue<int>("Index");
				Guid languageId = dataReader.GetColumnValue<Guid>("LanguageId");
				string name = dataReader.GetColumnValue<string>("Name");
				bool active = dataReader.GetColumnValue<bool>("Active");
				cultures.Add(new SysCultureInfo(id, index, languageId, name, active));
			});
			return cultures;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets system culture information.
		/// </summary>
		/// <param name="id">System culture identifier.</param>
		public ISysCultureInfo Read(Guid id) {
			return Cultures.FirstOrDefault(culture => culture.Id.Equals(id));
		}

		/// <summary>
		/// Gets system culture information.
		/// </summary>
		/// <param name="index">System culture index.</param>
		public ISysCultureInfo Read(int index) {
			return Cultures.FirstOrDefault(culture => culture.Index.Equals(index));
		}

		/// <inheritdoc />
		public ISysCultureInfo GetByLanguageId(Guid languageId) {
			return Cultures.FirstOrDefault(culture => culture.LanguageId.Equals(languageId));
		}

		/// <summary>
		/// Gets available cultures.
		/// </summary>
		/// <returns>Cultures.</returns>
		public List<ISysCultureInfo> Read() {
			return Cultures;
		}

		/// <summary>
		/// Gets number of available cultures.
		/// </summary>
		/// <returns>Number of available cultures.</returns>
		public int Count() {
			return Cultures.Count;
		}

		#endregion

	}

	#endregion

}

