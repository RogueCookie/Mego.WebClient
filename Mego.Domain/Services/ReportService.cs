using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mego.Database;
using Mego.Database.Abstraction;
using Mego.Database.Models;

namespace Mego.Domain.Services
{
    public class ReportService : IReport
    {
        private readonly MegoDbContext _context;

        public ReportService(MegoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Report>> GetAllReportAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Report> CreateReportAsync(Report newReport)
        {
            throw new NotImplementedException();
        }

        public Task<Report> UpdateReport(Report report)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReportByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}