using System;
using System.Collections.Generic;
using Catel;
using SalesRecordImport.Domain.Enums;

namespace SalesRecordImport.CsvParsing.Mapping.SalesChannels
{
    public class SalesChannelMapper : ISalesChannelMapper
    {
        private readonly IReadOnlyDictionary<string, SalesChannel> _salesChannelMap
            = new Dictionary<string, SalesChannel>
            {
                ["online"] = SalesChannel.Online,
                ["offline"] = SalesChannel.Offline
            };

        public SalesChannel MapFromString(string stringValue)
        {
            Argument.IsNotNull(nameof(stringValue), stringValue);

            if (_salesChannelMap.TryGetValue(stringValue.ToLowerInvariant(), out var orderPriority))
            {
                return orderPriority;
            }

            throw new ArgumentException($"Unable to map sales channel with value {stringValue}.");
        }
    }
}