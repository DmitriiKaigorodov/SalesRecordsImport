using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;
using System.Threading.Tasks;

namespace SalesRecordImport.BusinessLogic.Services
{
    public interface IReportsService
    {
        Task<TReportResult> GenerateReportAsync<TReportRequest, TReportResult>(TReportRequest reportRequest)
            where TReportRequest : IReportRequest where TReportResult : IReportResult;
    }
}
