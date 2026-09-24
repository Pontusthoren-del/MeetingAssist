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

        private const string SystemPrompt =
            "Du är en hjälpsam mötesassistent. Svara alltid på svenska. " +
            "Dina svar är förslag som användaren kan redigera. Fatta inga beslut åt användaren.";

        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

    }
}