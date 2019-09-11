using SalesRecordImport.Domain;
using System;
using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.Bulk
{
    public interface IBulkInsert : IDisposable
    {
        void AddRecord(SalesRecord salesRecord);
        Task WriteToServer();
    }
}
