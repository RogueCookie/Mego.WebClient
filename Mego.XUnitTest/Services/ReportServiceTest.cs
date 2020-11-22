using Mego.Database;
using Mego.Database.Models;
using Mego.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


        [Fact]
        public async Task GetFilteredReportsAsync_WhenStartAndEndDateExist_ReportInTimeRangeExpected()
        {
            // Arrange
            var startDate = new DateTime(2020,11,17);
            var endDate = new DateTime(2020,11,22);

            // Act
            var result = await _reportService.GetFilteredReportsAsync(startDate, endDate);

            // Assert
            Assert.True(result.Count ==2);
        }

        [Fact]
        public async Task GetFilteredReportsAsync_WhenStartDateNull_ReportInTimeRangeExpected()
        {
            // Arrange
            var endDate = new DateTime(2020, 11, 22);

            // Act
            var result = await _reportService.GetFilteredReportsAsync(null, endDate);

            // Assert
            Assert.True(result.Count == 3);

            foreach (var report in result)
            {
                Assert.True(report.OrderDate <= endDate);
            }
        }

        [Fact]
        public async Task GetFilteredReportsAsync_WhenEndDateNull_ReportInTimeRangeExpected()
        {
            // Arrange
            var startDate = new DateTime(2020, 11, 17);

            // Act
            var result = await _reportService.GetFilteredReportsAsync(startDate, null);

            // Assert
            Assert.True(result.Count == 3);

            foreach (var report in result)
            {
                Assert.True(report.OrderDate >= startDate);
            }
        }

        [Fact]
        public async Task GetFilteredReportsAsync_WhenBothNull_GetAllExpected()
        {
            // Arrange

            // Act
            var result = await _reportService.GetFilteredReportsAsync(null, null);

            // Assert
            Assert.True(result.Count == 4);
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