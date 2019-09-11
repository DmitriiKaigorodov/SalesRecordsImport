using Catel;
using Microsoft.EntityFrameworkCore;
using SalesRecordImport.DataAccess.Extensions;
using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.DataAccess.Ordering;
using SalesRecordImport.DataAccess.QueryResults;
using SalesRecordImport.DataAccess.Repositories;
using SalesRecordImport.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.EFCore.Repositories
{
    internal class SalesRecordsRepository : ISalesRecordsRepository
    {
        private readonly SalesRecordDbContext _dbContext;

        public SalesRecordsRepository(SalesRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IPagedResult<SalesRecord>> GetSalesRecords(SalesRecordsOptions salesRecordsOptions)
        {
            Argument.IsNotNull(nameof(salesRecordsOptions), salesRecordsOptions);
            var filteredRecords = _dbContext.SalesRecords.AsNoTracking().Where(salesRecordsOptions.Filter.ToExpression());
            var orderedRecords = filteredRecords.Order(salesRecordsOptions, OrderingMaps.SalesRecordMap);
            return await orderedRecords.GetPageAsync(salesRecordsOptions);
        }

        public Task<int> UpdateRecord(SalesRecord record)
        {
            Argument.IsNotNull(nameof(record), record);
            return _dbContext.SalesRecords.Where(x => x.Id == record.Id).UpdateFromQueryAsync(r => new SalesRecord
            {
                Country = record.Country,
                ExternalId = record.ExternalId,
                ItemType = record.ItemType,
                OrderDate = record.OrderDate,
                OrderPriority = record.OrderPriority,
                Region = record.Region,
                SalesChannel = record.SalesChannel,
                ShipDate = record.ShipDate,
                TotalCost = record.TotalCost,
                TotalProfit = record.TotalProfit,
                TotalRevenue = record.TotalRevenue,
                UnitCost = record.UnitCost,
                UnitPrice = record.UnitPrice,
                UnitsSold = record.UnitsSold
            });
        }

        public Task<int> DeleteRecord(int recordId)
        {
            return _dbContext.SalesRecords.Where(x => x.Id == recordId).DeleteFromQueryAsync();
        }
    }
}