using Mego.Database;
using Mego.Database.Abstraction;
using Mego.Database.Models;
using Mego.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            if (addedReport == null) return null;//TODO
            return addedReport;
        }

        /// <inheritdoc />
        public async Task<Report> UpdateReportAsync(Report report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var existReport = await _context.Reports.FirstOrDefaultAsync(x => x.Id == report.Id);
            if (existReport == null) return null; //TODO

            existReport.OrderDate = report.OrderDate;
            existReport.SummaryPrice = report.SummaryPrice;

            var updatedEntity = _context.Reports.Update(existReport).Entity;
            await _context.SaveChangesAsync();

            if (updatedEntity == null) return null; //TODO
            return updatedEntity;
        }

        /// <inheritdoc />
        public async Task RemoveReportByIdAsync(int recordId)
        {
            var existReport = await _context.Reports.FirstOrDefaultAsync(x => x.Id == recordId);
            //if (existReport == null)  //TODO

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