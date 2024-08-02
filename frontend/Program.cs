using YoutubeSummarizer.Frontend.Components;
using YoutubeSummarizer.Frontend.Services;
using YoutubeSummarizer.Frontend.Services.Impl;

using Aliencube.YouTubeSubtitlesExtractor;
using Aliencube.YouTubeSubtitlesExtractor.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#region Service Registration

builder.Services.AddHttpClient<IApiClientService, ApiClientService>(client => client.BaseAddress = new Uri(config["BaseURL"]!));
builder.Services.AddHttpClient<IYouTubeVideo, YouTubeVideo>();

#endregion

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
