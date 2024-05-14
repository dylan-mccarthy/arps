using System.Collections.Generic;
using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;

namespace ARPS.AILib;
public interface IAIService
{
    List<string> GetTextCompletions(string text);
}

public class AIService : IAIService
{
    //TODO: Implement Azure OpenAI Connection
    public List<string> GetTextCompletions(string text)
    {

        return new List<string> { "Placeholder","Response","From","AI" };
    }
}