using SalesRecordImport.Domain.Enums;
using System;

namespace SalesRecordImport.WebApp.Dtos
{
    public class SalesRecordDto
    {
        public int Id { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string ItemType { get; set; }

        public SalesChannel SalesChannel { get; set; }

        public OrderPriority OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public long ExternalId { get; set; }

        public DateTime ShipDate { get; set; }

        public int UnitsSold { get; set; }

        public int UnitPrice { get; set; }

        public int UnitCost { get; set; }

        public int TotalRevenue { get; set; }

        public int TotalCost { get; set; }

        public int TotalProfit { get; set; }
    }
}
