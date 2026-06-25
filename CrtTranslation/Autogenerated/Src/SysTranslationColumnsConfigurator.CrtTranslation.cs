namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: SysTranslationColumnsConfigurator

	/// <summary>
	/// <see cref="ISysTranslationColumnsConfigurator"/>
	/// </summary>
	[DefaultBinding(typeof(ISysTranslationColumnsConfigurator), Name = nameof(SysTranslationColumnsConfigurator))]
	public class SysTranslationColumnsConfigurator : ISysTranslationColumnsConfigurator
	{

		#region Fields: Private

		private readonly string _sysCultureSchemaName = "SysCulture";
		private readonly string _sysTranslationSchemaName = "SysTranslation";
		private readonly string _languageColumnName = "Language";
		private readonly string _isVerifiedColumnName = "IsVerified";
		private readonly string _isChangedColumnName = "IsChanged";
		private readonly string _resourceManagerName = "TranslationSection";
		private readonly string _resourceDefaultCaption = "LocalizableStrings.DefaultCaption.Value";
		private readonly string _resourceVerifiedColumnCaption = "LocalizableStrings.VerifiedColumnCaption.Value";
		private readonly string _resourceIsChangedColumnCaption = "LocalizableStrings.IsChangedColumnCaption.Value";
		private readonly UserConnection _userConnection;
		private EntitySchema _sysTranslationSchema;

		#endregion

		#region Properties: Private

		private EntitySchema SysTranslationSchema => _sysTranslationSchema ?? (
			_sysTranslationSchema = _userConnection.EntitySchemaManager.GetInstanceByName(_sysTranslationSchemaName));

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor SysTranlation columns configurator.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/></param>
		public SysTranslationColumnsConfigurator(UserConnection userConnection) {
			userConnection.CheckArgumentNull("userConnection");
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private string GetLocalizableDefaultString(string resourceItemName) {
			var resourceStorage = _userConnection.Workspace.ResourceStorage;
			return new LocalizableString(resourceStorage, _resourceManagerName, resourceItemName).Value;
		}

		private EntityCollection GetSysCultures() {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, _sysCultureSchemaName);
			esq.AddAllSchemaColumns();
			return esq.GetEntityCollection(_userConnection);
		}

		private string GetSysCultureDefaultText(Guid cultureId) {
			if(!_userConnection.CurrentUser.SysCultureId.Equals(cultureId)) {
				return string.Empty;
			}
			return $" - {GetLocalizableDefaultString(_resourceDefaultCaption)}";
		}

		private TranslationColumnConfigure GetTranslationColumnConfigure(EntitySchemaColumn column, string caption, bool isDefault) {
			var columnConfigure = new TranslationColumnConfigure();
			columnConfigure.Caption = caption;
			columnConfigure.UsageType = (int)EntitySchemaColumnUsageType.General;
			columnConfigure.Name = column.Name;
			columnConfigure.LanguageColumnPath = column.Name;
			columnConfigure.IsDefault = isDefault;
			return columnConfigure;
		}

		private TranslationColumnConfigure GetLanguageColumnConfigure(Entity sysCulture, int cultureIndex) {
			var language = sysCulture.GetTypedColumnValue<string>("LanguageName");
			var cultureId = sysCulture.PrimaryColumnValue;
			var defaultSufix = GetSysCultureDefaultText(cultureId);
			var caption = $"{language}{defaultSufix}";
			var languageColumn = SysTranslationSchema.Columns.First(c => c.Name.Equals($"{_languageColumnName}{cultureIndex}"));
			return GetTranslationColumnConfigure(languageColumn, caption, !string.IsNullOrEmpty(defaultSufix));
		}

		private TranslationColumnConfigure GetVerifiedColumnConfigure(Entity sysCulture, int cultureIndex) {
			var code = sysCulture.GetTypedColumnValue<string>("Name");
			var caption = GetLocalizableDefaultString(_resourceVerifiedColumnCaption);
			var verifiedColumn = SysTranslationSchema.Columns.First(c => c.Name.Equals($"{_isVerifiedColumnName}{cultureIndex}"));
			return GetTranslationColumnConfigure(verifiedColumn, $"{caption} ({code})", false);
		}

		private TranslationColumnConfigure GetIsChangedColumnConfigure(Entity sysCulture, int cultureIndex) {
			var code = sysCulture.GetTypedColumnValue<string>("Name");
			var caption = GetLocalizableDefaultString(_resourceIsChangedColumnCaption);
			var changedColumn = SysTranslationSchema.Columns.First(c => c.Name.Equals($"{_isChangedColumnName}{cultureIndex}"));
			return GetTranslationColumnConfigure(changedColumn, $"{caption} ({code})", false);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// <see cref="ISysTranslationColumnsConfigurator"/>
		/// </summary>
		/// <returns><see cref="TranslationColumnConfigure"/></returns>
		public IEnumerable<TranslationColumnConfigure> GetTranslationColumnsConfigured() {
			var entitiesSchemaColumn = new List<TranslationColumnConfigure>();
			var sysCultures = GetSysCultures();
			foreach(var sysCulture in sysCultures) {
				var index = sysCulture.GetTypedColumnValue<int>("Index");
				entitiesSchemaColumn.Add(GetLanguageColumnConfigure(sysCulture, index));
				entitiesSchemaColumn.Add(GetVerifiedColumnConfigure(sysCulture, index));
				entitiesSchemaColumn.Add(GetIsChangedColumnConfigure(sysCulture, index));
			}
			return entitiesSchemaColumn;
		}

		#endregion

	}

	#endregion

	#region Class: TranslationColumnConfigure

	/// <summary>
	/// SysTranlation column configure item.
	/// </summary>
	[DataContract]
	public class TranslationColumnConfigure
	{
		/// <summary>
		/// SysTranslation column name.
		/// </summary>
		[DataMember(Name = "Name")]
		public string Name { get; set; }
		/// <summary>
		/// SysTranslation column caption.
		/// </summary>
		[DataMember(Name = "Caption")]
		public string Caption { get; set; }
		/// <summary>
		/// SysTranslation usage type.
		/// </summary>
		[DataMember(Name = "UsageType")]
		public int UsageType { get; set; }
		/// <summary>
		/// SysTranslation language column path.
		/// </summary>
		[DataMember(Name = "LanguageColumnPath")]
		public string LanguageColumnPath { get; set; }
		/// <summary>
		/// Is default SysTranslation column caption flag.
		/// </summary>
		[DataMember(Name = "IsDefault")]
		public bool IsDefault { get; set; }
	}

	#endregion

}
