using System;
using System.ComponentModel.DataAnnotations;

namespace Mego.WebClient.Models.Request
{
    /// <summary>
    /// Date range for filtering data
    /// </summary>
    public class FilterRequest
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        /// <summary>
        /// Start date of range
        /// </summary>
        public DateTime? DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        /// <summary>
        /// End date in range
        /// </summary>
        public DateTime? DateTo{ get; set; }
    }
}