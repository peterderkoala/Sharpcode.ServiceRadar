using Microsoft.AspNetCore.SignalR;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.Api.Hubs
{
    public class AuthHub : Hub<IAuthHubServer>, IAuthHubClient
    {
        private readonly ILogger<AuthHub> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AuthHub(ILogger<AuthHub> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task GetApiKey(string connectionId)
        {
            _logger.LogInformation("Got public key request from: {client}", connectionId);
            await Clients.Client(connectionId)
                .SendApiKey(RsaController<string>.GetPublicRSA());
        }

        public async Task SendClientKey(string connectionId, RemoteClient client)
        {
            _logger.LogInformation("Got public key from client: {client}", client.Mail);
            await Clients.Client(connectionId)
                .ChallangeResponse(RsaController<bool>.PrivatePublicEncrypt(true, client.RemoteClientKey));
        }
    }
}
