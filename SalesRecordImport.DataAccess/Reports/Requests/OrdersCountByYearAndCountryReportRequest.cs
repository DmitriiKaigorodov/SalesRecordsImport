namespace SalesRecordImport.DataAccess.Reports.Requests
{
    public class OrdersCountByYearAndCountryReportRequest : IReportRequest
    {
        public int Year { get; set; }

        public string Country { get; set; }
    }
}