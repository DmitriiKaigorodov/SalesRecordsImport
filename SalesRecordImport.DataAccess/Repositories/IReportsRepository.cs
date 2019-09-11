using System.Threading.Tasks;

namespace SalesRecordImport.DataAccess.Repositories
{
    public interface IReportsRepository
    {
        Task<long> GetOrderCountForYearAndCountry(int year, string country);

        Task<decimal> GetProfitForYearAndCountry(int year, string country);
    }
}