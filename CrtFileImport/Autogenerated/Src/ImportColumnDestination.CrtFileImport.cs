namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Runtime.Serialization;
	using Terrasoft.Common;

	#region Class: ImportColumnDestination

	/// <summary>
	/// An instance of this class represents import column destination.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("SchemaUId={SchemaUId}; ColumnName={ColumnName}")]
	[Serializable]
	public class ImportColumnDestination
	{

		#region Constants: Protected

		[NonSerialized]
		private const string TypeUIdPropertyName = "TypeUId";
		[NonSerialized]
		private const string ReferenceSchemaNamePropertyName = "ReferenceSchemaName";
		[NonSerialized]
		private const string ReferenceSchemaUIdPropertyName = "ReferenceSchemaUId";
		[NonSerialized]
		private const string ImportColumnNamePropertyName = "ImportColumnName";

		#endregion

		#region Fields: Public

		/// <summary>
		/// Schema unique identifier.
		/// </summary>
		[DataMember(Name = "schemaUId")]
		public Guid SchemaUId;

		/// <summary>
		/// Column name.
		/// </summary>
		[DataMember(Name = "columnName")]
		public string ColumnName;

		/// <summary>
		/// Column value name.
		/// </summary>
		[DataMember(Name = "columnValueName")]
		public string ColumnValueName;

		/// <summary>
		/// True if key for duplication search.
		/// </summary>
		[DataMember(Name = "isKey")]
		public bool IsKey;

		#endregion

		#region Properties: Public

		private Dictionary<string, object> _properties;

		/// <summary>
		/// Destination properties.
		/// </summary>
		[DataMember(Name = "properties")]
		public Dictionary<string, object> Properties {
			get {
				return _properties ?? (_properties = new Dictionary<string, object>());
			}
			set {
				_properties = value;
			}
		}

		private Dictionary<string, object> _attributes;

		/// <summary>
		/// Destination properties.
		/// </summary>
		[DataMember(Name = "attributes")]
		public Dictionary<string, object> Attributes {
			get {
				return _attributes ?? (_attributes = new Dictionary<string, object>());
			}
			set {
				_attributes = value;
			}
		}

		#endregion

		#region Methods: Private

		private string GetPropertyValue(string propertyName) {
			var propertyValue = Properties.GetValue<object>(propertyName, string.Empty);
			return propertyValue?.ToString() ?? string.Empty;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Finds destination column value.
		/// </summary>
		/// <returns>Destination column value</returns>
		public object FindAttributeColumnValue() {
			string attributeColumnValueName = FindAttributeColumnValueName();
			if (attributeColumnValueName.IsNotNullOrEmpty()) {
				return Attributes.GetValue<object>(attributeColumnValueName, default(object));
			}
			return null;
		}

		/// <summary>
		/// Finds destination column value name.
		/// </summary>
		/// <returns>Destination column value name.</returns>
		public string FindAttributeColumnValueName() => Properties.GetValue<string>("AttributeColumnValueName", default(string));

		/// <summary>
		/// Gets destination entity index.
		/// </summary>
		/// <returns>Child entity index.</returns>
		public int GetIndex() {
			return Convert.ToInt32(Properties["Index"]);
		}

		/// <summary>
		/// Gets key.
		/// </summary>
		/// <returns>Key.</returns>
		public string GetKey() {
			object attributeColumnValue = FindAttributeColumnValue();
			int index = GetIndex();
			return string.Concat(attributeColumnValue, index);
		}

		/// <summary>
		/// Returns value of "TypeUId" property.
		/// </summary>
		/// <returns>Guid<see cref="Guid"/></returns>
		public Guid FindColumnTypeUId() => Guid.Parse(GetPropertyValue(TypeUIdPropertyName));

		public Guid FindReferenceSchemaUId() {
			var propertyValue = GetPropertyValue(ReferenceSchemaUIdPropertyName);
			return string.IsNullOrEmpty(propertyValue) ? Guid.Empty : Guid.Parse(propertyValue);
		}

		public string FindReferenceSchemaName() => GetPropertyValue(ReferenceSchemaNamePropertyName);

		public string FindImportColumnName() => GetPropertyValue(ImportColumnNamePropertyName);

		#endregion

	}

	#endregion

}
