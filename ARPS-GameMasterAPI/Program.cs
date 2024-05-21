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


app.MapPost("/describe/scene", (PlayerAction playerAction) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    var prompt = "You are a Game Master for a Dungeons and Dragons game. Describe the scene that happens next based on if the player was successful in their action or not. Do not describe the next actions of the player. Only describe NPC's and the world around the player.";
    var response = aiService.GetTextCompletionsAsync(JsonSerializer.Serialize(playerAction), prompt);
    return response;
});

app.MapPost("/describe/action", (PlayerAction playerAction) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    var prompt = "You are a Game Master for a Dungeons and Dragons game. Describe the action that the player is attempting to do. Describe the scene that happens next based on if the player was successful in their action or not. Do not describe the next actions of the player. Only describe NPC's and the world around the player.";
    var response = aiService.GetTextCompletionsAsync(JsonSerializer.Serialize(playerAction), prompt);
    return response;
});

app.Run();

