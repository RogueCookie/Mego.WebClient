using Mego.Database;
using Mego.Database.Models;
using Mego.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mego.XUnitTest.Services
{
    public class ReportServiceTest
    {
        private readonly ReportService _reportService;
        private readonly MegoDbContext _context;

        public ReportServiceTest()
        {
            // for test create db in memory
            _context = new MegoDbContext(new DbContextOptionsBuilder<MegoDbContext>()
                .UseSqlite("Filename=:memory:")
                .Options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _reportService = new ReportService(_context);
        }

        [Fact]
        public async Task GetAllReportAsync_WhenGot_ListOfReportExpected()
        {
            // Arrange
            var init = InitReport();
            await _context.Reports.AddAsync(init);
            await _context.SaveChangesAsync();

            // Act
            var result = await _reportService.GetAllReportAsync();
            var report = result.FirstOrDefault(x => x.Id == init.Id);

            // Assert
            Assert.True(result.Count == 5);
            Assert.NotNull(report);
            Assert.Equal(init.SummaryPrice, report.SummaryPrice);
            Assert.Equal(init.OrderDate, report.OrderDate);
        }

        [Fact]
        public async Task CreateReportAsync_WhenDone_RecordExistInDbExpected()
        {
            // Arrange
            var init = InitReport();

            // Act
            var result = await _reportService.CreateReportAsync(init);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(init.SummaryPrice, result.SummaryPrice);
            Assert.Equal(init.OrderDate, result.OrderDate);
        }

        [Fact]
        public async Task UpdateReportAsync_WhenDone_UpdatedEntityExpected()
        {
            // Arrange
            var init = InitReport();
            await _context.Reports.AddAsync(init);
            await _context.SaveChangesAsync();

            var dateBeforeUpdates = init.OrderDate;
            var sumBeforeUpdates = init.SummaryPrice;

            var reportForUpdate = new Report()
            {
                Id = init.Id,
                OrderDate = DateTime.Now.AddDays(3),
                SummaryPrice = 24
            };

            // Act
            var result = await _reportService.UpdateReportAsync(reportForUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(sumBeforeUpdates, result.SummaryPrice);
            Assert.Equal(24, result.SummaryPrice);
            Assert.NotEqual(dateBeforeUpdates, result.OrderDate);
            Assert.Equal(init.Id, result.Id);
        }

        [Fact]
        public async Task RemoveReportByIdAsync_WhenDone_DefaultExpected()
        {
            // Arrange
            var init = InitReport();
            await _context.Reports.AddAsync(init);
            await _context.SaveChangesAsync();

            // Act
            await _reportService.RemoveReportByIdAsync(init.Id);
            var report = _context.Reports.FirstOrDefault(x => x.Id == init.Id);

            // Assert
            Assert.Null(report);
        }


        /// <summary>
        /// Init record for manipulation in tests
        /// </summary>
        /// <returns>One report</returns>
        private static Report InitReport()
        {
            return new Report()
            {
                OrderDate = DateTime.Now,
                SummaryPrice = 4554.4m
            };
        }
    }
}