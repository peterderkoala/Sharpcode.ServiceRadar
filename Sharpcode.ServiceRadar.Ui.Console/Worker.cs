using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IHubConnectionBuilder hubConnectionBuilder;

        public Worker(ILogger<Worker> logger, IHubConnectionBuilder hubConnectionBuilder)
        {
            this.logger = logger;
            this.hubConnectionBuilder = hubConnectionBuilder;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var con = hubConnectionBuilder
                .WithUrl("")
                .Build();
        }
    }
}
