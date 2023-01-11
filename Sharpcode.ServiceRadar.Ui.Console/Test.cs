using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;
using TypedSignalR.Client;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Test
    {
        private readonly ILogger<Test> _logger;
        private readonly IServiceProvider _service;
        private Organization _organisation;

        public Test(ILogger<Test> logger, IServiceProvider service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task Auth(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Press any key to auth start...");

            System.Console.ReadKey();

            RemoteClient rc = new()
            {
                Mail = "tobias.gottwald@omexom.com",
                OrganisationId = 1,
                RemoteClientId = 1,
                RemoteClientKey = RsaController<string>.GetPublicRSA()
            };


            var conn = new HubConnectionBuilder()
                .WithUrl("http://localhost:5554/authHub")
                .Build();

            var t = conn.CreateHubProxy<IAuthHubClient>(cancellationToken);
            var rev = _service.GetRequiredService<AuthReceiver>();
            var subscriber = conn.Register<IAuthHubServer>(rev);

            await conn.StartAsync(cancellationToken);

            await t.GetApiKey(conn.ConnectionId);
            await t.SendClientKey(conn.ConnectionId, rc);
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Press any key to message start...");

                System.Console.ReadKey();

                RemoteClient rc = new()
                {
                    Mail = "tobias.gottwald@omexom.com",
                    OrganisationId = 1,
                    RemoteClientId = 1,
                    RemoteClientKey = "test"
                };

                _logger.LogInformation("Started Test!");

                var conn = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5554/messageHub")
                    .Build();

                var t = conn.CreateHubProxy<IBusinessIssueHubClient>(cancellationToken);

                var rev = _service.GetRequiredService<Receiver>();

                rev.OrganisationEvent += Rev_OrganisationEvent;
                rev.BusinessIssueEvent += Rev_BusinessIssueEvent;
                rev.NewBusinessIssueEvent += Rev_NewBusinessIssueEvent;

                var subscriber = conn.Register<IBusinessIssueHubServer>(rev);

                await conn.StartAsync(cancellationToken);

                await t.GetOrganisation(conn.ConnectionId, rc.Mail);

                while (_organisation is null)
                    await Task.Delay(1000);

                await t.GetPendingIssues(conn.ConnectionId, _organisation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ex!");
            }
        }

        private void Rev_NewBusinessIssueEvent(object? sender, BusinessIssue e)
        {
            _logger.LogInformation("New Issue: {title} Created: {date}", e.Title, e.IssuedAt.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        private void Rev_BusinessIssueEvent(object? sender, List<BusinessIssue> e)
        {
            _logger.LogInformation("Got pending Issues: {count}", e.Count);
            foreach (var item in e)
            {
                _logger.LogInformation("Issue: {title} Created: {date}", item.Title, item.IssuedAt.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }

        private void Rev_OrganisationEvent(object? sender, Organization e)
        {
            _logger.LogInformation("Organisation: [{org}]", e.Title);
            _organisation = e;
        }
    }
}
