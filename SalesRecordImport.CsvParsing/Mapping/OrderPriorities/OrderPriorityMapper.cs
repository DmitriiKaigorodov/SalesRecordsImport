using System;
using System.Collections.Generic;
using Catel;
using SalesRecordImport.Domain.Enums;

namespace SalesRecordImport.CsvParsing.Mapping.OrderPriorities
{
    public class OrderPriorityMapper : IOrderPriorityMapper
    {
        private readonly IReadOnlyDictionary<string, OrderPriority> _orderPrioritiesMap
            = new Dictionary<string, OrderPriority>
            {
                ["H"] = OrderPriority.High,
                ["M"] = OrderPriority.Medium,
                ["L"] = OrderPriority.Low,
                ["C"] = OrderPriority.C
            };

        public OrderPriority MapFromString(string stringValue)
        {
            Argument.IsNotNull(nameof(stringValue), stringValue);

            if (_orderPrioritiesMap.TryGetValue(stringValue.ToUpperInvariant(), out var orderPriority))
            {
                return orderPriority;
            }

            throw new ArgumentException($"Unable to map priority with value {stringValue}.");
        }
    }
}