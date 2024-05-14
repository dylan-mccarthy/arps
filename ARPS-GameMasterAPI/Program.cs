using System.Text.Json;
using ARPS.AILib;
using ARPS.AILib.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAIService, AIService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/describe/scene", () => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    var response = aiService.GetTextCompletions("Hello");
    return response;
});

app.MapPost("/describe/action", (PlayerAction playerAction) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    var response = aiService.GetTextCompletions(JsonSerializer.Serialize(playerAction));
    return response;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
