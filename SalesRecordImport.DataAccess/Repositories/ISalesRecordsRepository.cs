using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.DataAccess.QueryResults;
using SalesRecordImport.Domain;
using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.Repositories
{
    public interface ISalesRecordsRepository
    {
        Task<IPagedResult<SalesRecord>> GetSalesRecords(SalesRecordsOptions salesRecordsOptions);

        Task<int> UpdateRecord(SalesRecord record);

        Task<int> DeleteRecord(int recordId);
    }
}