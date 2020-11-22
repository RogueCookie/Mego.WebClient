using Mego.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mego.Database.Abstraction
{
    /// <summary>
    /// Main methods for manipulation with Report table
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Get all exist reports
        /// </summary>
        /// <returns>List of reports</returns>
        Task<List<Report>> GetAllReportAsync();

        /// <summary>
        /// Add new report
        /// </summary>
        /// <param name="newReport">Report which need to add to table</param>
        /// <returns>Report which was add in Db</returns>
        Task<Report> CreateReportAsync(Report newReport);

        /// <summary>
        /// Update report
        /// </summary>
        /// <param name="report">Model with new data</param>
        /// <returns>Updated report entity</returns>
        Task<Report> UpdateReportAsync(Report report);

        /// <summary>
        /// Remove report with particular Id
        /// </summary>
        /// <param name="id">Id of record for being deleted</param>
        Task RemoveReportByIdAsync(int id);

        /// <summary>
        /// Get all reports filtered by date
        /// </summary>
        /// <param name="dateFrom">Start date for filtering</param>
        /// <param name="dateTo">End date for filtering</param>
        /// <returns>List of reports filtered by date</returns>
        Task <List<Report>> GetFilteredReportsAsync(DateTime? dateFrom, DateTime? dateTo);
    }
}