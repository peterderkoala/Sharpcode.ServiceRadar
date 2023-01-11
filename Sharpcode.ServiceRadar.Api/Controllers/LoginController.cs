﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly OrganizationDataController organisationController;

        public LoginController(
            ILogger<LoginController> logger,
            OrganizationDataController organisationController)
        {
            this.logger = logger;
            this.organisationController = organisationController;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganisationAsync(int id)
        {
            logger.LogInformation("Requesting Organisation with ID: {id}", id);
            var org = await organisationController.GetOrganizations().FirstOrDefaultAsync(x => x.OrganizationId == id);

            if (org is null)
            {
                logger.LogInformation("Organisation with ID: {id} not found", id);
                return StatusCode(StatusCodes.Status500InternalServerError, id);
            }

            logger.LogInformation("Returning Organisation with ID: {id}, Organisation: {org}", id, org);
            return Ok(new Organization());
        }
    }
}
