using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Model.Interfaces
{
    public interface IAuthHubClient
    {
        Task GetApiKey(string connectionId);
        Task SendClientKey(string connectionId, RemoteClient client);
    }
}
