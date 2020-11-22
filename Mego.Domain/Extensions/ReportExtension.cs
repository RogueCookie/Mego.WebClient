using Mego.Database.Models;
using System;
using System.Linq;

namespace Mego.Domain.Extensions
{
    public static class ReportExtension
    {
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