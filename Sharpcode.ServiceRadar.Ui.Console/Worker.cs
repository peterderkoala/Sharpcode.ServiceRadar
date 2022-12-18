using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Worker : BackgroundService, IAsyncDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly BusinessIssueHubController _businessIssueHubController;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubConnectionBuilder _hubConnectionBuilder;
        private HubConnection _connection = null!;
        private List<BusinessIssue> _businessIssues = new List<BusinessIssue>();

        public Worker(
            ILogger<Worker> logger,
            BusinessIssueHubController businessIssueHubController)
        {
            _logger = logger;
            _businessIssueHubController = businessIssueHubController;
            //_serviceProvider = serviceProvider;
            //_hubConnectionBuilder = hubConnectionBuilder;
        }

        public async ValueTask DisposeAsync()
        {

            if (_connection is not null && _connection.ConnectionId is not null)
            {
                await _connection.StopAsync();
                _connection = null!;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Service");

            //_connection = _serviceProvider.GetService<BusinessHubConnectionBuilder>()
            //    .WithUrl("http://localhost:5554/issuehub")
            //    .Build();

            //_connection = _hubConnectionBuilder
            //    .WithUrl("http://localhost:5554/issuehub")
            //    .Build();

            _connection = _businessIssueHubController.HubConnection;

            _logger.LogInformation("Listening on {url}", "http://localhost:5554/issuehub");

            await _connection.StartAsync();
            //_connection.On<BusinessIssue>("NewBusinessIssue", data =>
            //{
            //    _businessIssues.Add(data);
            //    _logger.LogInformation("Got new Issue! BusinessIssueId: {id} \r\nBusinessIssue: {data}",
            //        data.BusinessIssueId,
            //        data);
            //});

            _connection.On<int>("PingTest", data =>
            {
                _logger.LogInformation("PingTest: {Value}", data);
            });
        }
    }
}
