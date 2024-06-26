using System.Collections.Generic;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;


namespace ARPS.AILib;
public interface IAIService
{
    Task<List<string?>?> GetTextCompletionsAsync(string text, string prompt);
}

public class AIService : IAIService
{
    private readonly IChatCompletionService _chatCompletionService;
    public AIService(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    public async Task<List<string?>?> GetTextCompletionsAsync(string text, string prompt)
    {
        var chatHistory = new ChatHistory();
        chatHistory.AddSystemMessage(prompt);
        chatHistory.AddUserMessage(text);
        var chatMessageContents = await _chatCompletionService.GetChatMessageContentsAsync(chatHistory);
        var completions = chatMessageContents.Select(x => x.Content).ToList();
        return completions;
    }
}