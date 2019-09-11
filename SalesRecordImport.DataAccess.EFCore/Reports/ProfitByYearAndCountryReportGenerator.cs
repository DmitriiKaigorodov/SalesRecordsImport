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
    public class ProfitByYearAndCountryReportGenerator : IReportGenerator<ProfitByYearAndCountryReportRequest, ProfitByYearAndCountryReportResult>
    {
        private readonly SalesRecordDbContext _salesRecordDbContext;

        public ProfitByYearAndCountryReportGenerator(SalesRecordDbContext salesRecordDbContext)
        {
            _salesRecordDbContext = salesRecordDbContext;
        }

        public async Task<ProfitByYearAndCountryReportResult> GenerateReport(ProfitByYearAndCountryReportRequest reportRequest)
        {
            Argument.IsNotNull(nameof(reportRequest), reportRequest);

            var fromDate = new DateTime(reportRequest.Year, 1, 1);
            var toDate = fromDate.AddYears(1);

            var country = reportRequest.Country;

            var profit = await _salesRecordDbContext.SalesRecords.Where(x => x.Country == country &&
                                                                 x.OrderDate >= fromDate &&
                                                                 x.OrderDate <= toDate).SumAsync(x => x.TotalProfit);

            return new ProfitByYearAndCountryReportResult
            {
                Profit = profit
            };
        }
    }
}