using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class OrganisationController
    {
        private readonly ILogger<OrganisationController> logger;
        private readonly BrokerDbContext brokerDbContext;

        public OrganisationController(ILogger<OrganisationController> logger, BrokerDbContext brokerDbContext)
        {
            this.logger = logger;
            this.brokerDbContext = brokerDbContext;
        }

        public IQueryable<Organisation> GetOrganisations() => brokerDbContext.Organizations;
    }
}
