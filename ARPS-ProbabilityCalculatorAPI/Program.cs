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

app.MapPost("/calculate", (PlayerAction PlayerAction) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    //Get probability
    var probability = aiService.GetTextCompletions(JsonSerializer.Serialize(PlayerAction));
    //Roll dice to determine success
    try{
        int probabilityInt = int.Parse(probability[0]);
        Random random = new();
        bool success = random.Next(1, 101) <= probabilityInt;
        //Return result
        var response = new PlayerAction { Description = PlayerAction.Description, Outcome = success };
        return response;
    }
    catch (Exception){
        return new PlayerAction { Description = PlayerAction.Description, Outcome = false };
    }
});

app.Run();