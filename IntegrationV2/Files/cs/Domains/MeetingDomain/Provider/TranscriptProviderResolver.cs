namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Provider
{
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using System;
    using IntegrationApi.Interfaces;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;

    #region Class: TranscriptProviderResolver

    [DefaultBinding(typeof(ITranscriptProviderResolver))]
    public class TranscriptProviderResolver : ITranscriptProviderResolver
    {

        #region Fields: Private

        private readonly ICalendarLogger _log;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingTranscriptRepository _transcriptRepository;

        #endregion

        #region Constructors: Public

        public TranscriptProviderResolver(ICalendarLogger log, ICalendarRepository calendarRepository,
            IMeetingRepository meetingRepository, IMeetingTranscriptRepository transcriptRepository) {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _transcriptRepository =
                transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
        }

        #endregion

        #region Methods: Public

        public ITranscriptProvider Resolve(string providerName, UserConnection userConnection) {
            if (string.IsNullOrWhiteSpace(providerName))
                throw new ArgumentException("Provider name must be specified", nameof(providerName));
            string key = providerName.Trim().ToLowerInvariant();
            try {
                switch (key) {
                    case "teams":
                        return new TeamsTranscriptProvider(userConnection, _log, _calendarRepository,
                            _meetingRepository, _transcriptRepository);
                    default:
                        return ClassFactory.Get<ITranscriptProvider>(key,
                            new ConstructorArgument("uc", userConnection));
                }
            } catch (Exception ex) {
                throw new InvalidOperationException($"Unknown or unregistered provider '{providerName}'", ex);
            }
        }

        #endregion

    }

    #endregion

}
