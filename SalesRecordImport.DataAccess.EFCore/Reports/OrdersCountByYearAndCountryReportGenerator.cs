using System;
using System.Linq;
using System.Threading.Tasks;
using Catel;
using Microsoft.EntityFrameworkCore;
using SalesRecordImport.DataAccess.Reports.Generators;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;

namespace SalesRecordImport.DataAccess.EFCore.Reports
{
    public class OrdersCountByYearAndCountryReportGenerator : IReportGenerator<OrdersCountByYearAndCountryReportRequest,
                                                                                OrdersCountByYearAndCountryReportResult>
    {
        private readonly SalesRecordDbContext _dbContext;

        public OrdersCountByYearAndCountryReportGenerator(SalesRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrdersCountByYearAndCountryReportResult> GenerateReport(OrdersCountByYearAndCountryReportRequest reportRequest)
        {
            Argument.IsNotNull(nameof(reportRequest), reportRequest);

            var fromDate = new DateTime(reportRequest.Year, 1, 1);
            var toDate = fromDate.AddYears(1);

            var country = reportRequest.Country;

            var count = await _dbContext.SalesRecords.Where(x => x.Country == country &&
                                                           x.OrderDate >= fromDate &&
                                                           x.OrderDate <= toDate).LongCountAsync();

            return new OrdersCountByYearAndCountryReportResult
            {
                OrdersCount = count
            };
        }
    }
}