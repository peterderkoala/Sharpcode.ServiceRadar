using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Test _test;

        public Worker(
            ILogger<Worker> logger,
            Test test)
        {
            _logger = logger;
            _test = test;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _test.Auth(stoppingToken);
            //await _test.Start(stoppingToken);
        }
    }
}
