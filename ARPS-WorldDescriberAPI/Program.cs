using ARPS.AILib;
using ARPS.AILib.models;
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

app.MapPost("/describe", async (Description description) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    if(description.Input == null) return new Description { Input = null, Output = null };
    var prompt = "You are an AI that describes the game world for this dungeons and dragons game. Create a detailed description that expands on the input provided. The game is set within the Forgotten Realms universe.";
    var response = await aiService.GetTextCompletionsAsync(description.Input, prompt);
    if(response == null) return new Description { Input = description.Input, Output = null };
    return new Description { Input = description.Input, Output = response.LastOrDefault() };
});

app.Run();
