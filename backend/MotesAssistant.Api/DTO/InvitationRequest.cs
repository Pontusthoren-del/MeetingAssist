namespace MotesAssistant.Api.DTO
{
    public class InvitationRequest
    {
        public string MeetingName { get; set; } = string.Empty;
        public string Time { get; set; } =string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
    }
}
