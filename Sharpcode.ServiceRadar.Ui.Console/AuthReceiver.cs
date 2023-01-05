using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Interfaces;
using System.Text;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    public class AuthReceiver : IAuthHubServer
    {
        private readonly ILogger<AuthReceiver> _logger;

        public AuthReceiver(ILogger<AuthReceiver> logger)
        {
            _logger = logger;
        }

        public async Task ChallangeResponse(string encryptedMessage)
        {

            if (File.Exists("apikey.xml"))
            {
                var result = RsaController<bool>.PublicPrivateDecrypt(
                    encryptedMessage,
                    await File.ReadAllTextAsync("apikey.xml", Encoding.UTF8));

                _logger.LogInformation(
                    "{caller} - Response: {response} \r\nRaw: {raw}",
                    nameof(ChallangeResponse),
                    result,
                    encryptedMessage);
            }
            else
            {
                _logger.LogInformation(
                    "{caller} - Found no apikey! \r\nRaw: {raw}",
                    nameof(ChallangeResponse),
                    encryptedMessage);
            }

        }

        public async Task SendApiKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await File.WriteAllTextAsync(
                    "apikey.xml",
                    key,
                    Encoding.UTF8);
            }
        }
    }
}
