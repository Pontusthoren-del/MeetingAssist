using Microsoft.AspNetCore.Mvc;
using MotesAssistant.Api.DTO;
using MotesAssistant.Api.Services;

namespace MotesAssistant.Api
{
    [ApiController]
    [Route("api/ai")]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;
        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("summarize")]
        public async Task<ActionResult> Summarize([FromBody] SummarizeRequest request)
        { 
            if (string.IsNullOrWhiteSpace(request.MeetingNotes))
            {
                return BadRequest("Meetingnotes får inte vara tomt");
            }

            var systemPrompt = """
            Du är en mötesassistent som sammanfattar mötesanteckningar på svenska.
            Var kortfattad och saklig.
            Lyft fram beslut, ansvariga personer och deadlines om de finns.
            Hitta inte på något som inte står i anteckningarna.
            Sammanfattningen är ett förslag som användaren själv kan redigera.
            """;

            var userPrompt = $"""
                Sammanfatta följande mötesanteckningar: {request.MeetingNotes} 
                """;

            var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);

            return Ok(answer);
        }

        [HttpPost("agenda")]
        public async Task<IActionResult> Agenda([FromBody] AgendaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Purpose))
            {
                return BadRequest("Purpose får inte vara tomt");
            }

            var systemPrompt = """
            Du är en mötesassistent som skapar agendor på svenska.
            Svara ENDAST med agendan, ingen inledning eller avslutning.
            Använd ingen markdown, alltså inga stjärnor eller rubriktecken.
            Varje punkt skrivs på en egen rad i formatet: "10 min – Punktens rubrik: kort beskrivning av vad som ska diskuteras"
            Bryt ner syftet i konkreta, relevanta punkter.
            Tiderna ska tillsammans exakt motsvara mötets längd.
            Nämn bara deltagare om en punkt tydligt tillhör en viss person.
            Hitta inte på beslut, siffror eller ämnen som inte går att härleda från syftet.
            """;

            var userPrompt = $"""
            Skapa en agenda för mötet "{request.Title}".
            Syfte: {request.Purpose}
            Deltagare: {request.Participants}
            Längd: {request.LengthMinutes} minuter
            """;

            var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);

            return Ok(new {agenda = answer });
        }

        [HttpPost("invitation")]
        public async Task<IActionResult> Invitation([FromBody] InvitationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MeetingName))
            {
                return BadRequest("MeetingName får inte vara tomt.");
            }
            var systemPrompt = """
            Du är en mötesassistent som skriver utkast till mötesinbjudningar på svenska.
            Svara ENDAST med inbjudan, ingen inledning eller förklaring runt den.
            Använd ingen markdown, alltså inga stjärnor eller rubriktecken.
            Första raden ska vara "Ämne: " följt av en kort ämnesrad.
            Tonen ska vara professionell men vänlig, och texten kort och tydlig.
            Ta med mötets namn, tid, plats och syfte.
            Hitta inte på detaljer som inte finns i underlaget, till exempel länkar, telefonnummer eller en agenda.
            Avsluta med "Vänliga hälsningar" och "[Ditt namn]" på raden under, så att användaren kan fylla i det själv.
            """;

            var userPrompt = $"""
            Skriv en mötesinbjudan.
            Mötets namn: {request.MeetingName}
            Tid: {request.Time}
            Plats: {request.Location}
            Syfte: {request.Purpose}
            """;

            var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);

            return Ok(answer);
        }
    }
}