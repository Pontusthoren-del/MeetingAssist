namespace MotesAssistant.Api.Services;

public interface IAiService
{
    Task<string> SendPromptAsync(string systemPrompt, string userPrompt);
}