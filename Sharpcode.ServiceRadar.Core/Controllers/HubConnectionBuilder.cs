using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class BusinessHubConnectionBuilder : IHubConnectionBuilder
    {
        private readonly BusinessHubConnectionBuilder _builder;

        public BusinessHubConnectionBuilder()
        {
            _builder = new BusinessHubConnectionBuilder();
        }

        public IServiceCollection Services => throw new NotImplementedException();

        public HubConnection Build()
        {
            return _builder.Build();
        }

        public BusinessHubConnectionBuilder WithUrl(string url)
        {
            _builder.WithUrl(url);
            return this;
        }
    }
}
