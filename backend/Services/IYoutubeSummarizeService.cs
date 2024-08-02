using YoutubeSummarizer.ServiceDefaults.APICommon;

namespace YoutubeSummarizer.Backend.Services;

public interface IYoutubeSummarizeService
{
    public Task<string> SummariseAsync(SummaryRequest req);
}
