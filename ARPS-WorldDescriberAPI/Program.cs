using ARPS.AILib;
using ARPS.AILib.models;

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

app.MapPost("/describe", (Description description) => {
    var aiService = app.Services.GetRequiredService<IAIService>();
    if(description.Input == null) return new Description { Input = null, Output = null };   
    var response = aiService.GetTextCompletions(description.Input);
    return new Description { Input = description.Input, Output = response[0] };
});

app.Run();
