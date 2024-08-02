using YoutubeSummarizer.ServiceDefaults.APICommon;

namespace YoutubeSummarizer.Frontend.Services;

internal interface IApiClientService
{
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
    Task<string> SummarizeAsync(string videoUrl, string videoLanguageCode, string summaryLanguageCode);
}