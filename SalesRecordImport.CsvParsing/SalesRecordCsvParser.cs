using SalesRecordImport.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SalesRecordImport.CsvParsing.Exceptions;
using SalesRecordImport.CsvParsing.Mapping.OrderPriorities;
using SalesRecordImport.CsvParsing.Mapping.SalesChannels;
using System.Globalization;
using Catel;

namespace SalesRecordImport.CsvParsing
{
    public class SalesRecordCsvParser : ICsvParser<SalesRecord>
    {
        #region Fields indexes in csv
        private const int _regionIndex = 0;
        private const int _countryIndex = 1;
        private const int _itemTypeIndex = 2;
        private const int _salesChannelIndex = 3;
        private const int _orderPriorityIndex = 4;
        private const int _orderDateIndex = 5;
        private const int _externalIdIndex = 6;
        private const int _shipDateIndex = 7;
        private const int _unitSoldIndex = 8;
        private const int _unitPriceIndex = 9;
        private const int _unitCostIndex = 10;
        private const int _totalRevenueIndex = 11;
        private const int _totalCostIndex = 12;
        private const int _totalProfitIndex = 13;
        #endregion
        private StreamReader _stream;
        private readonly char _delimiter;
        private readonly IOrderPriorityMapper _orderPriorityMapper = new OrderPriorityMapper();
        private readonly ISalesChannelMapper _salesChannelMapper = new SalesChannelMapper();

        public IFormatProvider FormatProvider { get; set; } = CultureInfo.InvariantCulture;

        public SalesRecordCsvParser(string csvFilePath, char delimiter = ',')
        {
            Argument.IsNotNull(nameof(csvFilePath), csvFilePath);

            if (!File.Exists(csvFilePath))
            {
                throw new ArgumentException($"File {csvFilePath} doesn't exist.");
            }

            try
            {
                _stream = new StreamReader(csvFilePath);
                _stream.ReadLine();
            }
            catch
            {
                Dispose();
            }

            _delimiter = delimiter;
        }

        public bool HasRows => _stream != null && !_stream.EndOfStream;

        public async Task<SalesRecord> ParseRowAsync()
        {
            var row = await _stream.ReadLineAsync();
            var fields = row.Split(_delimiter);

            return new SalesRecord
            {
                Country = fields[_countryIndex],
                Region = fields[_regionIndex],
                ExternalId = ConvertCsvField<long>(fields, _externalIdIndex),
                ItemType = fields[_itemTypeIndex],
                OrderDate = ConvertCsvField<DateTime>(fields, _orderDateIndex),
                ShipDate = ConvertCsvField<DateTime>(fields, _shipDateIndex),
                UnitCost = ConvertCsvField<decimal>(fields, _unitCostIndex),
                UnitPrice = ConvertCsvField<decimal>(fields, _unitPriceIndex),
                UnitsSold = ConvertCsvField<decimal>(fields, _unitSoldIndex),
                TotalCost = ConvertCsvField<decimal>(fields, _totalCostIndex),
                TotalProfit = ConvertCsvField<decimal>(fields, _totalProfitIndex),
                TotalRevenue = ConvertCsvField<decimal>(fields, _totalRevenueIndex),
                OrderPriority = _orderPriorityMapper.MapFromString(fields[_orderPriorityIndex]),
                SalesChannel = _salesChannelMapper.MapFromString(fields[_salesChannelIndex])
            };
        }

        private T ConvertCsvField<T>(IReadOnlyList<string> rows, int index)
        {
            if (index <= 0 || index > rows.Count)
            {
                throw new ArgumentOutOfRangeException($"Csv row doesn't contain field with index {index}.");
            }

            try
            {
                return (T)Convert.ChangeType(rows[index], typeof(T), FormatProvider);
            }
            catch (Exception e)
            {
                throw new CsvFieldConvertationException(index, typeof(T), e);
            }
        }

        public void Dispose()
        {
            _stream?.Close();
            _stream?.Dispose();
        }
    }
}
