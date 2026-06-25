using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces
{

    #region Interface: IGraphApiUserClient

    public interface IGraphApiUserClient
    {

        #region Methods: Public

        Task<IEnumerable<TranscriptMetadata>> FetchTranscriptMetadata(IEnumerable<string> iCalUIds);

        Task<string> DownloadTranscriptContent(string transcriptContentUrl);

        #endregion

    }

    #endregion

}
