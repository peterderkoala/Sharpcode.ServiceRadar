namespace Sharpcode.ServiceRadar.Model.Interfaces
{
    public interface IAuthHubServer
    {
        Task SendApiKey(string key);
        Task ChallangeResponse(string encryptedMessage);
    }
}
