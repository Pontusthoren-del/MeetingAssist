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
            // Systemprompten med reglerna från controllern
            SystemInstruction = new Content
            {
                Parts = [new Part { Text = systemPrompt }]
            }
        };

        // Skickar prompten till Gemini
        var response = await client.Models.GenerateContentAsync("gemini-3.5-flash", userPrompt, config);

        // ? och ?? gör att programmet inte kraschar om svaret är tomt
        var text = response.Candidates?[0].Content?.Parts?[0].Text ?? string.Empty;
        Console.WriteLine(text);

        return text;
    }
}