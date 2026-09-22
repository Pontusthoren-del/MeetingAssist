using System.Text;
using System.Text.Json;

namespace MotesAssistant.Api.Services
{
    public class GeminiAiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiAiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException("Gemini:ApiKey saknas i konfigurationen.");
        }

        public async Task<string> SummarizeAsync(string meetingNotes)
        {
            var prompt = $"Sammanfatta följande mötesanteckningar kortfattat och sakligt:\n{meetingNotes}";
            return await CallGeminiAsync(prompt);
        }

        public async Task<string> GenerateAgendaAsync(string purpose, string participants, int lengthMinutes)
        {
            var prompt = $"Skapa en agenda för ett möte om {{purpose}} med deltagarna {{participants}}, längd {{lengthMinutes}} min.";
            return await CallGeminiAsync(prompt);
        }
        public async Task<string> DraftInvitationAsync(string meetingName, string time, string location, string purpose)
        {
            var prompt = $"Skriv ett professionellt utkast till mötesinbjudan för mötet {meetingName}, {time}, {location}, syfte: {purpose}."; 
            return await CallGeminiAsync(prompt);
        }


        private async Task<string> CallGeminiAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash-lite:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                new { parts = new[] { new { text = prompt } } }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? string.Empty;
        }
    }
}
