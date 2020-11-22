using System;

namespace Mego.WebClient.Models.Request
{
    /// <summary>
    /// Date range for filtering data
    /// </summary>
    public class FilterRequest
    {
        /// <summary>
        /// Start date of range
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// End date in range
        /// </summary>
        public DateTime? DateTo{ get; set; }
    }
}