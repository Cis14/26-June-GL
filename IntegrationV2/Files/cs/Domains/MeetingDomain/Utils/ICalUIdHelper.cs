namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    #region Class: ICalUIdHelper

    /// <summary>
    /// Helper class for iCalUId operations.
    /// </summary>
    public static class ICalUIdHelper
    {

        #region Constants: Private

        /// <summary>
        /// Regex pattern for recurring meeting occurrence date suffix (_YYYY_MM_DD).
        /// </summary>
        private const string OccurrenceIdPattern = @"_\d{4}_\d{2}_\d{2}";

        #endregion

        #region Methods: Public

        /// <summary>
        /// Get simplified id for occurrence.
        /// </summary>
        /// <param name="iCalUid">External meeting unique identifier.</param>
        /// <param name="simplifiedICalUid">Simplified identifier.</param>
        /// <returns>True if the iCalUId was an occurrence and was simplified, false otherwise.</returns>
        /// <remarks>Convert recurrence meeting iCalUid to typical.</remarks>
        public static bool TryGetSimplifiedICalUid(string iCalUid, out string simplifiedICalUid) {
            simplifiedICalUid = iCalUid;
            bool isOccurence = Regex.IsMatch(iCalUid, OccurrenceIdPattern);
            if (isOccurence) {
                simplifiedICalUid = Regex.Replace(iCalUid, OccurrenceIdPattern, string.Empty);
            }
            return isOccurence;
        }

        /// <summary>
        /// Simplifies a collection of iCalUIds by removing occurrence date suffixes.
        /// </summary>
        /// <param name="iCalUIds">Collection of iCalUIds to simplify.</param>
        /// <returns>List of simplified and distinct iCalUIds.</returns>
        public static List<string> SimplifyICalUIds(IEnumerable<string> iCalUIds) {
            if (iCalUIds == null) {
                return new List<string>();
            }
            return iCalUIds
                .Select(id => {
                    TryGetSimplifiedICalUid(id, out string simplified);
                    return simplified;
                })
                .Distinct()
                .ToList();
        }

        #endregion

    }

    #endregion

}
