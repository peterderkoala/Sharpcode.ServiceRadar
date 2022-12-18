using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class BusinessHubConnectionBuilder : IHubConnectionBuilder
    {
        private readonly HubConnectionBuilder _builder;
        public IServiceCollection Services => ((ISignalRBuilder)_builder).Services;

        public BusinessHubConnectionBuilder()
        {
            _builder = new HubConnectionBuilder();
        }


        public HubConnection Build()
        {
            return _builder.Build();
        }

        public IHubConnectionBuilder WithUrl(string url)
        {
            _builder.WithUrl(url);
            return this;
        }
    }
}
