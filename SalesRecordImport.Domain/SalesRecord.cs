using System;
using SalesRecordImport.Domain.Enums;

namespace SalesRecordImport.Domain
{
    public class SalesRecord
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

        public decimal UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }
    }
}
