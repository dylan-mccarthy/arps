using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace ARPS.AILib;

public class OllamaChatCompletionService(string url, string model) : IChatCompletionService
{
    public string? ModelUrl { get; set; } = url;
    public string? ModelName { get; set; } = model;

    public IReadOnlyDictionary<string, object?> Attributes => throw new NotImplementedException();

    public async Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, CancellationToken cancellationToken = default)
    {
        if(ModelUrl == null || ModelName == null)
        {
            throw new InvalidOperationException("ModelUrl and ModelName must be set before calling GetChatMessageContentsAsync");
        }
        var ollamaClient = new OllamaApiClient(ModelUrl, ModelName);
        var chat = new Chat(ollamaClient, _ => { });
        foreach(var message in chatHistory)
        {
            if(message.Role == AuthorRole.System)
            {
                if(message.Content != null)
                {
                    await chat.SendAs(ChatRole.System, message.Content);
                }
                continue;
            }
        }
        var lastMessage = chatHistory.LastOrDefault();
        if(lastMessage != null && lastMessage.Content != null)
        {
            string question = lastMessage.Content;

            var chatResponse = "";
            var history = (await chat.Send(question, CancellationToken.None)).ToArray();
            var last = history.Last();
            chatResponse = last.Content;

            chatHistory.AddAssistantMessage(chatResponse);
        }

        return chatHistory;
    }

    public IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}