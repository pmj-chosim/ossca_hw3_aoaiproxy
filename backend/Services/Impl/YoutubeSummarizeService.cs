using YoutubeSummarizer.ServiceDefaults.APICommon;

using Aliencube.YouTubeSubtitlesExtractor.Abstractions;

using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace YoutubeSummarizer.Backend.Services.Impl;

public class YoutubeSummarizeService : IYoutubeSummarizeService
{
    private readonly IYouTubeVideo _youtubeService;
    private readonly AzureOpenAIClient _openAiClient;
    private readonly IConfiguration _config;

    private readonly ChatCompletionOptions chatOptions;

    public YoutubeSummarizeService(IYouTubeVideo youtubeService,
                                AzureOpenAIClient openAiClient,
                                IConfiguration config)
    {
        _youtubeService = youtubeService
            ?? throw new ArgumentNullException(nameof(youtubeService), Errors.NULL_ERROR);
        _openAiClient = openAiClient
            ?? throw new ArgumentNullException(nameof(openAiClient), Errors.NULL_ERROR);
        _config = config
            ?? throw new ArgumentNullException(nameof(config), Errors.NULL_ERROR);

        chatOptions = new()
        {
            MaxTokens = int.TryParse(this._config["Prompt:MaxTokens"], out var maxTokens) ? maxTokens : 3000,
            Temperature = float.TryParse(this._config["Prompt:Temperature"], out var temperature) ? temperature : 0.7f,
        };
    }

    public async Task<string> SummariseAsync(SummaryRequest req)
    {
        if (string.IsNullOrEmpty(req.VideoURL))
            return "Invalid Youtube Video Url. Please check URL again, sorry.";

        var subtitle = await _youtubeService.ExtractSubtitleAsync(req.VideoURL!, req.VideoLanguageCode!);

        if (subtitle == null || subtitle.Content!.Count <= 0)
            return "No subtitle found for the video. Please check the video details, sorry.";

        string caption = subtitle.Content.Select(p => p.Text).Aggregate((a, b) => $"{a}\n{b}")!;

        var chatClient = _openAiClient.GetChatClient(_config["OpenAI:DeploymentName"]);
        var chatMessage = new List<ChatMessage>
        {
            new SystemChatMessage(_config["Prompt:System"]),
            new SystemChatMessage($"Here's the transcript. Summarise it in 5 bullet point items in the given language code of \"{req.SummaryLanguageCode}\"."),
            new UserChatMessage(caption)
        };

        var response = await chatClient.CompleteChatAsync(chatMessage, chatOptions);
        var summary = response.Value.Content[0].Text;

        return summary;
    }
}