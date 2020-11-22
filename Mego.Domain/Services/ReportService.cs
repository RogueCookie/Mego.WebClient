using Mego.Database;
using Mego.Database.Abstraction;
using Mego.Database.Models;
using Mego.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mego.Database.Models.Exception;

namespace Mego.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly MegoDbContext _context;

        public ReportService(MegoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<List<Report>> GetAllReportAsync()
            => await _context.Reports.ToListAsync();


        /// <inheritdoc />
        public async Task<Report> CreateReportAsync(Report newReport)
        {
            if (newReport == null) throw new ArgumentNullException(nameof(newReport));

            await _context.Reports.AddAsync(newReport);
            await _context.SaveChangesAsync();

            var addedReport = await _context.Reports.FirstOrDefaultAsync(x => x.Id == newReport.Id);

            if (addedReport == null) throw new ItemNotFoundException($"Error occured while adding report from {newReport.OrderDate}");
            return addedReport;
        }

        /// <inheritdoc />
        public async Task<Report> UpdateReportAsync(Report report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var existReport = await _context.Reports.FirstOrDefaultAsync(x => x.Id == report.Id);
            if (existReport == null) throw new ItemNotFoundException($"Report with id {report.Id} does not exist");

            existReport.OrderDate = report.OrderDate;
            existReport.SummaryPrice = report.SummaryPrice;

            var updatedEntity = _context.Reports.Update(existReport).Entity;
            await _context.SaveChangesAsync();

            if (updatedEntity == null) throw new ItemNotFoundException($"Could not update report with id {report.Id}");
            return updatedEntity;
        }

        /// <inheritdoc />
        public async Task RemoveReportByIdAsync(int recordId)
        {
            var existReport = await _context.Reports.FirstOrDefaultAsync(x => x.Id == recordId);
            if (existReport == null) throw new ItemNotFoundException($"Report with id {recordId} does not exist");

            _context.Reports.Remove(existReport);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Report>> GetFilteredReportsAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            var result = await _context.Reports.WhereReportFilteredByTime(dateFrom, dateTo).ToListAsync();

            return result;
        }
    }
}