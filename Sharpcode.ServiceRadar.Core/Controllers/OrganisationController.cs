using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class OrganisationDataController
    {
        private readonly ILogger<OrganisationDataController> logger;
        private readonly BrokerDbContext brokerDbContext;

        public OrganisationDataController(
            ILogger<OrganisationDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            this.logger = logger;
            this.brokerDbContext = brokerDbContext;
        }

        public IQueryable<Organisation> GetOrganisations() => brokerDbContext.Organizations;
    }
}
