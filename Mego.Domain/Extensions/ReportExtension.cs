using Mego.Database.Models;
using System;
using System.Linq;

namespace Mego.Domain.Extensions
{
    public static class ReportExtension
    {
        /// <summary>
        /// Filtering reports in particular date range
        /// </summary>
        /// <param name="reports">List of reports</param>
        /// <param name="dateFrom">Start date for range</param>
        /// <param name="dateTo">End date of range</param>
        /// <returns>List of filtered reports</returns>
        public static IQueryable<Report> WhereReportFilteredByTime(this IQueryable<Report> reports, DateTime? dateFrom, DateTime? dateTo)
        {
            IQueryable<Report> result;

            if (!dateFrom.HasValue && !dateTo.HasValue) return reports;

            if (dateFrom.HasValue && dateTo.HasValue) return reports;

            if (dateTo.HasValue)
                result =  reports.Where(x => x.OrderDate <= dateTo);
            else 
                result = reports.Where(x => x.OrderDate >= dateFrom);

            return result;
        }
    }
}