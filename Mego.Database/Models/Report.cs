using System;

namespace Mego.Database.Models
{
    /// <summary>
    /// Holds data about order
    /// </summary>
    public class Report : BaseModel
    {
        /// <summary>
        /// Order price
        /// </summary>
        public decimal SummaryPrice { get; set; }

        /// <summary>
        /// Date of ordering
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}