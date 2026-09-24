namespace MotesAssistant.Api.DTO
{
    public class AgendaRequest
    {
        public  string Title { get; set; } =string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string Participants { get; set; }=string.Empty;
        public int LengthMinutes { get; set; }
    }
}
