using ClosedXML.Excel;
using Mego.Database.Abstraction;
using Mego.WebClient.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
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


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(FilterRequest model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var result = await _reportService.GetFilteredReportsAsync(model.DateFrom, model.DateTo);
        
            var priceTill1000 = result.Where(x => x.SummaryPrice <= 1000).Select(x => x.SummaryPrice).ToList();
            var priceTill5000 = result.Where(x => x.SummaryPrice > 1000 && x.SummaryPrice <= 5000).Select(x => x.SummaryPrice).ToList();
            var priceFrom5001 = result.Where(x => x.SummaryPrice >= 5001).Select(x => x.SummaryPrice).ToList();

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            
            var fileName = $"report_{DateTime.Now}.xlsx";
            
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                        workbook.Worksheets.Add("Report");
                    worksheet.Cell(1, 1).Value = "Orders in range 0-1000";
                    worksheet.Cell(1, 2).Value = "Orders in range 10001-5000";
                    worksheet.Cell(1, 3).Value = "Orders in range from 5001";

                    for (var index = 1; index <= priceTill1000.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = priceTill1000[index - 1];
                    }

                    for (var index = 1; index <= priceTill5000.Count; index++)
                    {
                        worksheet.Cell(index + 1, 2).Value = priceTill5000[index - 1];
                    }

                    for (var index = 1; index <= priceFrom5001.Count; index++)
                    {
                        worksheet.Cell(index + 1, 3).Value = priceFrom5001[index - 1];
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
