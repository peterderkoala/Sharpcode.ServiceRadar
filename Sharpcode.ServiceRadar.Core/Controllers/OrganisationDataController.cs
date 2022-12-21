using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class OrganisationDataController
    {
        private readonly ILogger<OrganisationDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public OrganisationDataController(
            ILogger<OrganisationDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<Organisation> GetOrganisations() => _brokerDbContext.Organizations;
    }
}
