using System.Collections.Generic;

namespace Creatio.ComponentCopilot
{

    /// <summary>
    /// Contract for validating a view element config.
    /// </summary>
    public interface IViewElementConfigValidator
    {

        /// <summary>
        /// Validates the supplied config JSON.
        /// </summary>
        /// <param name="configJson">The config JSON to validate.</param>
        /// <returns>
        /// A list of validation error messages; empty if the config is valid.
        /// </returns>
        IList<string> Validate(string configJson);
    }

}

