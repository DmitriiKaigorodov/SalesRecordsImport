using Microsoft.Extensions.DependencyInjection;
using SalesRecordImport.BusinessLogic.Services;

namespace SalesRecordImport.BusinessLogic.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddSalesRecordsImportBussinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ISalesRecordsService, SalesRecordsService>();
            services.AddScoped<IReportsService, ReportsService>();
        }
    }
}
