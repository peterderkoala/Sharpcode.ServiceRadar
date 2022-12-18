using Microsoft.Extensions.Logging;
using Polly;

namespace Sharpcode.ServiceRadar.Core.Policies
{
    public class ClientPolicy
    {
        private readonly ILogger<ClientPolicy> _logger;

        public IAsyncPolicy SignalrRetry { get; }
        public IAsyncPolicy SignalrWaitRetry { get; set; }

        public ClientPolicy(ILogger<ClientPolicy> logger)
        {
            _logger = logger;

            SignalrRetry = Policy.Handle<Exception>().RetryAsync(5);

            SignalrWaitRetry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                5,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                (exception, ts, count, context) =>
                {
                    _logger.LogError(exception, "Error Try {count} - Wait {ts} sek", count, ts);
                });
        }
    }
}
