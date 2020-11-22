﻿using Mego.Database.Abstraction;
using Mego.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult<IList<Report>>> GetAllGroupsAsync()
        {
            var reports = await _reportService.GetAllReportAsync();
            return Ok(reports);
        }

        /// <summary>
        /// Add new report
        /// </summary>
        /// <returns>Added report reports</returns>
        [HttpPost("AddReport")]
        [ProducesResponseType(typeof(Report), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Report>> AddReportAsync(Report model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var reports = await _reportService.CreateReportAsync(model);
            return Ok(reports);
        }

        /// <summary>
        /// Add new report
        /// </summary>
        /// <returns>Added report reports</returns>
        [HttpPost("UpdateReport")]
        [ProducesResponseType(typeof(Report), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Report>> UpdateReportAsync(Report model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var reports = await _reportService.UpdateReportAsync(model);
            return Ok(reports);
        }


        /// <summary>
        /// Remove report
        /// </summary>
        [HttpDelete("DeleteReport")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Report>> UpdateReportAsync(int reportId)
        {
            await _reportService.RemoveReportByIdAsync(reportId);
            return NoContent();
        }
    }
}
