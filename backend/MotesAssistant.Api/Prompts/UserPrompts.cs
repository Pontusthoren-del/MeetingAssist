using MotesAssistant.Api.DTO;

namespace MotesAssistant.Api.Prompts;

public static class UserPrompts
{
    public static string Summarize(SummarizeRequest request) => $"""
        Sammanfatta följande mötesanteckningar: {request.MeetingNotes}
        """;

    public static string Agenda(AgendaRequest request) => $"""
        Skapa en agenda för mötet "{request.Title}".
        Syfte: {request.Purpose}
        Deltagare: {request.Participants}
        Längd: {request.LengthMinutes} minuter
        """;

    public static string Invitation(InvitationRequest request) => $"""
        Skriv en mötesinbjudan.
        Mötets namn: {request.MeetingName}
        Tid: {request.Time}
        Plats: {request.Location}
        Syfte: {request.Purpose}
        """;
}