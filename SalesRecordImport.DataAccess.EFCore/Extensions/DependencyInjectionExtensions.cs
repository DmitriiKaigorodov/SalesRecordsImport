using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesRecordImport.DataAccess.Bulk;
using SalesRecordImport.DataAccess.EFCore.Bulk;
using SalesRecordImport.DataAccess.EFCore.Reports;
using SalesRecordImport.DataAccess.EFCore.Repositories;
using SalesRecordImport.DataAccess.Reports.Generators;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;
using SalesRecordImport.DataAccess.Repositories;
using SalesRecordImport.DataAccess.UnitOfWork;
using Z.EntityFramework.Extensions;

namespace SalesRecordImport.DataAccess.EFCore.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddEfCoreDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SalesRecordDbContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            });

            EntityFrameworkManager.ContextFactory = context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<SalesRecordDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new SalesRecordDbContext(optionsBuilder.Options);
            };

            services.AddScoped<ISalesRecordsRepository, SalesRecordsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IBulkInsert, BulkInsert>();
            services.AddScoped<IReportGenerator<ProfitByYearAndCountryReportRequest, ProfitByYearAndCountryReportResult>,
                                                 ProfitByYearAndCountryReportGenerator>();

            services.AddScoped<IReportGenerator<OrdersCountByYearAndCountryReportRequest, OrdersCountByYearAndCountryReportResult>,
                                                    OrdersCountByYearAndCountryReportGenerator>();
        }
    }
}
