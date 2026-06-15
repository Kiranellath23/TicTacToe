using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using TicTacToeBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         // ✅ Serialize enums as strings instead of numbers
         options.JsonSerializerOptions.Converters.Add(
             new JsonStringEnumConverter());
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocal", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register services
builder.Services.AddSingleton<IComputerAIService, ComputerAIService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API V1");
        c.RoutePrefix = string.Empty; // serve Swagger UI at app root (optional)
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAngularLocal");
app.MapControllers();

app.Run();