using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Core.Policies;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class BusinessIssueHubController : IAsyncDisposable
    {
        private readonly ILogger<BusinessIssueHubController> _logger;
        private readonly IHubConnectionBuilder _hubConnectionBuilder;
        private readonly ClientPolicy _clientPolicy;

        private HubConnection _hubConnection;
        public HubConnection HubConnection => _hubConnection;

        public BusinessIssueHubController(
            ILogger<BusinessIssueHubController> logger,
            IHubConnectionBuilder hubConnectionBuilder,
            ClientPolicy clientPolicy)
        {
            _logger = logger;
            _hubConnectionBuilder = hubConnectionBuilder;
            _clientPolicy = clientPolicy;

            _hubConnection = _hubConnectionBuilder
               .WithUrl("http://localhost:5554/issuehub")
               //.WithAutomaticReconnect((IRetryPolicy)_clientPolicy.SignalrWaitRetry)
               .Build();
        }

        public void Dispose()
        {
            if (_hubConnection is not null && _hubConnection.ConnectionId is not null)
            {
                _hubConnection.StopAsync().GetAwaiter().GetResult();
                _hubConnection = null!;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null && _hubConnection.ConnectionId is not null)
            {
                await _hubConnection.StopAsync();
                _hubConnection = null!;
            }
        }
    }
}
