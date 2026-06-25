namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Core.Entities;

	#region Class: ImportObject

	/// <summary>
	/// An instance of this class represents file import object.
	/// </summary>
	[DataContract]
	[Serializable]
	public class ImportObject
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportObject"/>.
		/// </summary>
		public ImportObject() {
		}

		/// <summary>
		/// Creates instance of type <see cref="ImportObject"/>.
		/// </summary>
		/// <param name="entitySchema">Entity schema.</param>
		/// <param name="isOtherObject"></param>
		public ImportObject(EntitySchema entitySchema, bool isOtherObject) {
			_uId = entitySchema.UId;
			_name = entitySchema.Name;
			_caption = entitySchema.Caption;
			_isOtherObject = isOtherObject;
		}

		#endregion

		#region Properties: Public

		private Guid _uId;

		[DataMember(Name = "uId")]
		public Guid UId {
			get {
				return _uId;
			}
			set {
				_uId = value;
			}
		}

		private string _name;

		[DataMember(Name = "name")]
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		private string _caption;

		[DataMember(Name = "caption")]
		public string Caption {
			get {
				return _caption;
			}
			set {
				_caption = value;
			}
		}

		private bool _isOtherObject;

		[DataMember(Name = "isOtherObject")]
		public bool IsOtherObject {
			get {
				return _isOtherObject;
			}
			set {
				_isOtherObject = value;
			}
		}

		#endregion

	}

	#endregion

}
