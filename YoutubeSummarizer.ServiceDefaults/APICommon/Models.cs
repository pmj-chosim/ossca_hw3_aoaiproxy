namespace YoutubeSummarizer.ServiceDefaults.APICommon;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class SummaryRequest
{
    public string? VideoURL { get; set; }

    public string? VideoLanguageCode { get; set; }

    public string? SummaryLanguageCode { get; set; }
}