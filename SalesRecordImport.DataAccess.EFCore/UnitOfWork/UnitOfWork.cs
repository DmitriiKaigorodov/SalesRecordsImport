using SalesRecordImport.DataAccess.UnitOfWork;
using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.EFCore.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SalesRecordDbContext _salesRecordDbContext;

        public UnitOfWork(SalesRecordDbContext salesRecordDbContext)
        {
            _salesRecordDbContext = salesRecordDbContext;
        }

        public async Task SaveChangesAsync()
            => await _salesRecordDbContext.SaveChangesAsync();
    }
}
