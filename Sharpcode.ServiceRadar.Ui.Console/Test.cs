using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Interfaces;
using TypedSignalR.Client;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Test
    {
        private readonly ILogger<Test> _logger;
        private readonly IServiceProvider _service;

        public Test(ILogger<Test> logger, IServiceProvider service)
        {
            _logger = logger;
            _service = service;
        }
        public async Task Start(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Test");
            var conn = new HubConnectionBuilder()
                .WithUrl("http://localhost:5554/messageHub")
                .Build();

            var t = conn.CreateHubProxy<IBusinessIssueHubClient>(cancellationToken);

            var rev = _service.GetRequiredService<Receiver>();
            var subscriber = conn.Register<IBusinessIssueHubServer>(rev);

            await conn.StartAsync();
        }
    }
}
