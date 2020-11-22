using Mego.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mego.Database.Abstraction
{
    /// <summary>
    /// Min methods for manipulation with Report table
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
    }
}