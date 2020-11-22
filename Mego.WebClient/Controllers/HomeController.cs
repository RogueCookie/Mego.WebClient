using Mego.Database.Abstraction;
using Mego.Database.Models;
using Mego.WebClient.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace Mego.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
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
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"report_{DateTime.Now}.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                        workbook.Worksheets.Add("Authors");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "FirstName";
                    worksheet.Cell(1, 3).Value = "LastName";
                    for (int index = 1; index <= result.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = result[index - 1].Id;
                        worksheet.Cell(index + 1, 2).Value = result[index - 1].OrderDate;
                        worksheet.Cell(index + 1, 3).Value = result[index - 1].SummaryPrice;
                    }

                    await using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
