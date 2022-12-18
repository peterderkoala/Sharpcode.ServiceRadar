using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;
using Sharpcode.ServiceRadar.Core;
using Sharpcode.ServiceRadar.Ui.Console;

var host = Host.CreateDefaultBuilder()
    .ConfigureLogging((context, logging) =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddNLog();
    })
    .ConfigureServices((context, service) =>
    {

        service.AddHubControllers();
        service.AddClientRetryPolicies();

        service.AddHostedService<Worker>()
        .Configure<HostOptions>(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(1);
        });
    })
    .UseNLog()
    .Build();

await host.RunAsync();