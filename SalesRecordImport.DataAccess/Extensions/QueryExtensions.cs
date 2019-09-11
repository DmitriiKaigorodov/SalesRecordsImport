using System.Linq;
using SalesRecordImport.DataAccess.Options.Abstract;
using SalesRecordImport.DataAccess.QueryResults;

namespace SalesRecordImport.DataAccess.Extensions
{
    public static class QueryExtensions
    {
        public static IPagedResult<TModel> GetPage<TModel>(this IQueryable<TModel> query,
            IPaginationOptions paginationOptions)
        {
            var page = paginationOptions?.Page ?? 1;
            var totalCount = query.Count();
            var size = paginationOptions?.Size ?? totalCount;

            var result = query.Skip((page - 1) * size).Take(size).ToList();

            return new PagedResult<TModel>(result, totalCount);
        }
    }
}