using System.ComponentModel.DataAnnotations;

namespace SalesRecordImport.WebApp.Models
{
    public class OrdersCountReportModel
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
