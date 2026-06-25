namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;

	#region Interface: ISectionCacheManager

	/// <summary>
	/// Provides caching functionality for section data organized by platform scope and section type.
	/// </summary>
	public interface ISectionCacheManager {

		#region Methods: Public

		/// <summary>
		/// Retrieves all sections for the specified platform scope.
		/// </summary>
		/// <param name="scopeKey">The scope key identifying the platform type.</param>
		/// <param name="factory">The factory function to create the list of sections if not found in cache.</param>
		/// <returns>A list of sections for the specified scope,
		/// either from cache or newly created by the factory.</returns>
		List<Section> GetAll(string scopeKey, Func<List<Section>> factory);

		/// <summary>
		/// Retrieves sections filtered by type for the specified platform scope.
		/// </summary>
		/// <param name="scopeKey">The scope key identifying the platform type.</param>
		/// <param name="type">The section type to filter by.</param>
		/// <param name="factory">The factory function to create the list of sections if not found in cache.</param>
		/// <returns>A list of sections of the specified type for the specified scope,
		/// either from cache or newly created by the factory.</returns>
		List<Section> GetByType(string scopeKey, SectionType type, Func<List<Section>> factory);

		/// <summary>
		/// Removes all cached section data.
		/// </summary>
		void Clear();

		#endregion

	}

	#endregion

}
