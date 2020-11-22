using Mego.Database.Abstraction;
using Mego.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mego.WebClient.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        /// <summary>
        /// Get all reports
        /// </summary>
        /// <returns>List of reports</returns>
        [HttpGet("GetAllReports")]
        [ProducesResponseType(typeof(IList<Report>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<Report>>> GetAllGroups()
        {
            var reports = await _reportService.GetAllReportAsync();
            return Ok(reports);
        }
    }
}
