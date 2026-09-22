using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotesAssistant.Api.Models;
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
                return BadRequest("MeetingNotes får inte vara tomt");
            }
            var result = await _aiService.SummarizeAsync(request.MeetingNotes);
            return Ok(new { summary = result });
        }
    }
}
