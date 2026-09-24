using Google.GenAI;
using Google.GenAI.Types;

namespace MotesAssistant.Api.Services;

public class GeminiAiService : IAiService
{
    private readonly string _apiKey;

    // Hämtar API-nyckeln från user-secrets
    public GeminiAiService(IConfiguration config)
    {
        _apiKey = config["Gemini:ApiKey"]
            ?? throw new InvalidOperationException("Gemini:ApiKey saknas i user-secrets.");
    }

    public async Task<string> SendPromptAsync(string systemPrompt, string userPrompt)
    {
        var client = new Client(apiKey: _apiKey);
        GenerateContentConfig config = new()
        {
            SystemInstruction = new Content
            {
                Parts = [new Part { Text = systemPrompt }]
            }
        };
        string[] models = ["gemini-3.8-flash", "gemini-3.5-flash", "gemini-3.5-flash-lite"];
        foreach (var model in models)
        {
            try
            {
                var response = await client.Models.GenerateContentAsync(model, userPrompt, config);
                var text = response.Candidates?[0].Content?.Parts?[0].Text ?? string.Empty;
                Console.WriteLine(text);
                return text;
            }
            catch (ServerError)
            {
            }
        }
        throw new Exception("Alla modeller är överbelastade just nu.");
    }
}