using SalesRecordImport.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SalesRecordImport.DataAccess.Ordering
{
    public static class OrderingMaps
    {
        public static readonly IReadOnlyDictionary<string, Expression<Func<SalesRecord, object>>> SalesRecordMap
            = new Dictionary<string, Expression<Func<SalesRecord, object>>>
            {
                ["orderDate"] = sr => sr.OrderDate
            };
    }
}
