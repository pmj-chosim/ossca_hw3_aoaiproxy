using Microsoft.AspNetCore.Mvc;
using YoutubeSummarizer.ServiceDefaults.APICommon;

namespace YoutubeSummarizer.Backend.ApiControllers;

[ApiController]
[Route(ApiEndpoints.WeatherForecast)]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger), Errors.NULL_ERROR);

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}