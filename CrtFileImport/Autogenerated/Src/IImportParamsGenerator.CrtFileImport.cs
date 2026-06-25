namespace Terrasoft.Configuration
{
	using System.Collections.Generic;
	using Terrasoft.Configuration.FileImport;
	using Terrasoft.Core.Entities;

	#region Interface: IImportParamsGenerator

	/// <summary>
	/// Provides methods for import params generation.
	/// </summary>
	public interface IImportParamsGenerator
	{
		#region Properties

		/// <summary>
		/// Gets the column values.
		/// </summary>
		/// <value>
		/// The column values.
		/// </value>
		Dictionary<string, string> ColumnValues {
			get;
		}

		#endregion

		#region Methods
		
		/// <summary>
		/// Generates the parameters.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <param name="entitySchema">The entity schema.</param>
		/// <returns></returns>
		ImportParameters GenerateParameters(EntitySchema entitySchema, Dictionary<string, string> values);

		/// <summary>
		/// Generates the parameters.
		/// </summary>
		/// <param name="entitySchema">The entity schema.</param>
		/// <returns></returns>
		ImportParameters GenerateParameters(EntitySchema entitySchema);

		#endregion

	}

	#endregion

}

