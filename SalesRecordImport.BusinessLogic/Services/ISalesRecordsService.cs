using System.Threading.Tasks;
using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.DataAccess.QueryResults;
using SalesRecordImport.Domain;

namespace SalesRecordImport.BusinessLogic.Services
{
    public interface ISalesRecordsService
    {
        Task ImportRecordsFromCsvFile(string csvFilePath);

        Task<IPagedResult<SalesRecord>> GetSalesRecords(SalesRecordsOptions options);

        Task<bool> UpdateSalesRecord(SalesRecord salesRecord);

        Task<bool> DeleteSalesRecord(int recordId);
    }
}