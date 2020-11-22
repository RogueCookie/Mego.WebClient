using Mego.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mego.Database.Abstraction
{
    /// <summary>
    /// Min methods for manipulation with Report table
    /// </summary>
    public interface IReport
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
        Task<Report> UpdateReport(Report report);

        /// <summary>
        /// Remove record with particular Id
        /// </summary>
        Task RemoveReportByIdAsync();
    }
}