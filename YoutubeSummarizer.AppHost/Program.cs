using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var config = builder.Configuration;

Console.WriteLine($"\n Aspire Start, Launch Type: {config["LaunchType"]}\n");

var backend = builder.AddProject<Projects.YoutubeSummarizer_Backend>("backend")
    .WithExternalHttpEndpoints()
    .WithEnvironment("OpenAI__Endpoint", config["OpenAI:Endpoint"])
    .WithEnvironment("OpenAI__ApiKey", config["OpenAI:ApiKey"])
    .WithEnvironment("OpenAI__DeploymentName", config["OpenAI:DeploymentName"]);

builder.AddProject<Projects.YoutubeSummarizer_Frontend>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(backend);

builder.Build().Run();