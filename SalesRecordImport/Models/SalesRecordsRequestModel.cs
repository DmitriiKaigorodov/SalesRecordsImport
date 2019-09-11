namespace SalesRecordImport.WebApp.Models
{
    public class SalesRecordsRequestModel
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string OrderColumn { get; set; }

        public bool OrderAscending { get; set; }

        public string Country { get; set; }
    }
}
