using Catel;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using SalesRecordImport.BusinessLogic.Exceptions;
using SalesRecordImport.DataAccess.Reports.Generators;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;
using System;
using System.Threading.Tasks;

namespace SalesRecordImport.BusinessLogic.Services
{
    internal class ReportsService : IReportsService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Logger _logger = LogManager.GetLogger(nameof(ReportsService));

        public ReportsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TReportResult> GenerateReportAsync<TReportRequest, TReportResult>(TReportRequest reportRequest)
            where TReportRequest : IReportRequest
            where TReportResult : IReportResult
        {
            _logger.Debug($"Generating report with for {typeof(TReportRequest).Name}.");

            Argument.IsNotNull(nameof(reportRequest), reportRequest);

            var generatorType = typeof(IReportGenerator<TReportRequest, TReportResult>);

            using (var scope = _serviceProvider.CreateScope())
            {
                var generator = scope.ServiceProvider.GetService(generatorType) as IReportGenerator<TReportRequest, TReportResult>;

                if (generator == null)
                {
                    _logger.Error($"Report for request {typeof(TReportRequest).Name} and result {typeof(TReportResult).Name} was not found.");
                    throw new ReportGeneratorNotFoundException(typeof(TReportRequest), typeof(TReportResult));
                }

                var report = await generator.GenerateReport(reportRequest);
                _logger.Debug("Report got successfully.");
                return report;
            }

        }
    }
}
