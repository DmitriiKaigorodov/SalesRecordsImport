using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRecordImport.DataAccess.Repositories;

namespace SalesRecordImport.DataAccess.EFCore.Repositories
{
    internal class ReportsRepository : IReportsRepository
    {
        private readonly SalesRecordDbContext _dbContext;

        public ReportsRepository(SalesRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> GetOrderCountForYearAndCountry(int year, string country)
        {
            return await _dbContext.SalesRecords.AsNoTracking().Where(x =>
                             string.Equals(x.Country, country, StringComparison.InvariantCultureIgnoreCase)
                             && x.OrderDate.Year == year).LongCountAsync();
        }

        public async Task<decimal> GetProfitForYearAndCountry(int year, string country)
        {
            return await _dbContext.SalesRecords.AsNoTracking().Where(x =>
                string.Equals(x.Country, country, StringComparison.InvariantCultureIgnoreCase)
                && x.OrderDate.Year == year).SumAsync(x => x.TotalProfit);
        }
    }
}