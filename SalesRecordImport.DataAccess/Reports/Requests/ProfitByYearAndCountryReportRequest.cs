namespace SalesRecordImport.DataAccess.Reports.Requests
{
    public class ProfitByYearAndCountryReportRequest : IReportRequest
    {
        public int Year { get; set; }

        public string Country { get; set; }
    }
}