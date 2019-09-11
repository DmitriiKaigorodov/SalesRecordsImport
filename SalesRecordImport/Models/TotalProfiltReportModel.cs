using System.ComponentModel.DataAnnotations;

namespace SalesRecordImport.WebApp.Models
{
    public class TotalProfiltReportModel
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
