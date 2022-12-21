using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sharpcode.ServiceRadar.Api.Hubs;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class BusinessIssueController : ControllerBase
    {
        private readonly ILogger<BusinessIssueController> _logger;
        private readonly IHubContext<MessageHub, IBusinessIssueHubServer> _hubContext;
        private readonly BusinessIssueDataController _businessIssueDataController;

        public BusinessIssueController(
            ILogger<BusinessIssueController> logger,
            IHubContext<MessageHub, IBusinessIssueHubServer> hubContext,
            BusinessIssueDataController businessIssueDataController)
        {
            _logger = logger;
            _hubContext = hubContext;
            _businessIssueDataController = businessIssueDataController;
        }

        [HttpGet("SendPingTest")]
        public async Task<IActionResult> SendPingTest(int value, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.PingTest(value);
                return Ok(value);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{method} - Error while sending ping value: {value}", nameof(SendPingTest), value);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while sending ping value");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOpenIssuesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var openIssues = await _businessIssueDataController
                        .GetBusinessIssues()
                        .Where(_ => _.ClosedAt != null)
                        .ToListAsync(cancellationToken);

                return Ok(openIssues);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{method} - Error while loading Open Issues", nameof(GetOpenIssuesAsync));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while loading Open Issues");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssueAsync(BusinessIssue data, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _businessIssueDataController.CreateOrUpdateBusinessIssueAsync(data, cancellationToken);
                await _hubContext.Clients.All.NewBusinessIssue(result);
                return Ok(result);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating BusinessIssue");
            }
        }
    }
}
