namespace MotesAssistant.Api.Services
{
    public interface IAiService
    {
        Task<string> SummarizeAsync(string meetingNotes);
        Task<string> GenerateAgendaAsync(string purpose, string participants, int lengthMinutes);
        Task<string> DraftInvitationAsync(string meetingName, string time, string location, string purpose);
    }
}
