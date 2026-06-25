namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces
{
    using System.Threading.Tasks;

    #region Interface: IGraphApiServiceAccountClient

    public interface IGraphApiServiceAccountClient
    {

        #region Methods: Public

        Task<string> ConvertToTeamsMeeting(string meetingId);

        #endregion

    }

    #endregion

}
