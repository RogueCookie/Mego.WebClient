using Mego.Database.Abstraction;
using Mego.Database.Models;
using Mego.WebClient.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mego.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        /// <summary>
        /// Download report
        /// </summary>
        /// <returns>List of reports in date range</returns>
        [HttpPost("DownloadReport")]
        //[ProducesResponseType(typeof(IList<Report>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<Report>>> GetAllFilteredReportsAsync([FromQuery] DateTime? from, [FromQuery] DateTime? till)
        {
            var reports = await _reportService.GetFilteredReportsAsync(from, till);
            return Ok(reports);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(FilterRequest model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var result = await _reportService.GetFilteredReportsAsync(model.DateFrom, model.DateTo);

            return View();
        }
    }
}
