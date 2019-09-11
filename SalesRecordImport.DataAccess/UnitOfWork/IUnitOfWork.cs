using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
