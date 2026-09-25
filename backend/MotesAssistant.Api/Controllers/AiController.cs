using Microsoft.AspNetCore.Mvc;
using MotesAssistant.Api.DTO;
using MotesAssistant.Api.Prompts;
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
        public async Task<IActionResult> Summarize([FromBody] SummarizeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MeetingNotes))
            {
                return BadRequest("Meetingnotes får inte vara tomt");
            }

            var systemPrompt = SystemPrompts.Summarize;
            var userPrompt = UserPrompts.Summarize(request);

            try
            {
                var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);
                return Ok(new { summary = answer });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AI-fel: {ex.Message}");
                return StatusCode(503, "AI-tjänsten är inte tillgänglig just nu. Försök igen senare.");
            }
        }

        [HttpPost("agenda")]
        public async Task<IActionResult> Agenda([FromBody] AgendaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Purpose))
            {
                return BadRequest("Purpose får inte vara tomt");
            }

            var systemPrompt = SystemPrompts.Agenda;
            var userPrompt = UserPrompts.Agenda(request);

            try
            {
                var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);
                return Ok(new { agenda = answer });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AI-fel: {ex.Message}");
                return StatusCode(503, "AI-tjänsten är inte tillgänglig just nu. Försök igen senare.");
            }
        }

        [HttpPost("invitation")]
        public async Task<IActionResult> Invitation([FromBody] InvitationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MeetingName))
            {
                return BadRequest("MeetingName får inte vara tomt.");
            }

            var systemPrompt = SystemPrompts.Invitation;
            var userPrompt = UserPrompts.Invitation(request);

            try
            {
                var answer = await _aiService.SendPromptAsync(systemPrompt, userPrompt);
                return Ok(new { invite = answer });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AI-fel: {ex.Message}");
                return StatusCode(503, "AI-tjänsten är inte tillgänglig just nu. Försök igen senare.");
            }
        }
    }
}