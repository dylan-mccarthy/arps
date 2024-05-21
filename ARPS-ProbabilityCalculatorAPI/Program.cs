using System.Text.Json;
using ARPS.AILib;
using ARPS.AILib.Models;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IChatCompletionService>(new OllamaChatCompletionService(config["Ollama:Url"] ?? "", config["Ollama:Model"] ?? ""));
builder.Services.AddSingleton<IAIService, AIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculate", async (PlayerAction PlayerAction) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    var prompt = "You are an AI that calculates the probability of success for a given action. The AI will return a number between 0 and 100 that represents the probability of success.";
    //Get probability
    var probability = await aiService.GetTextCompletionsAsync(JsonSerializer.Serialize(PlayerAction), prompt);
    //Roll dice to determine success
    try{
        if(probability == null)
        {
            return new PlayerAction { Description = PlayerAction.Description, Outcome = false };
        }  
        int.TryParse(probability.LastOrDefault(), out int probabilityInt);

        //int probabilityInt = 50;
        Random random = new();
        bool success = random.Next(1, 101) <= probabilityInt;
        
        //Return result
        var response = new PlayerAction { Description = PlayerAction.Description, Outcome = success };
        return response;
    }
    catch (Exception ex){
        Console.WriteLine("Error calculating probability: " + ex.Message);
        return new PlayerAction { Description = PlayerAction.Description, Outcome = false };
    }
});

app.Run();