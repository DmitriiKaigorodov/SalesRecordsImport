using System.Threading.Tasks;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;

namespace SalesRecordImport.DataAccess.Reports.Generators
{
    public interface IReportGenerator<in TReportRequest, TReportResult> where TReportRequest : IReportRequest where TReportResult : IReportResult
    {
        Task<TReportResult> GenerateReport(TReportRequest reportRequest);
    }
}