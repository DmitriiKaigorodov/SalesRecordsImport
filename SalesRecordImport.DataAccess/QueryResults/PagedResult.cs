using System.Collections.Generic;

namespace SalesRecordImport.DataAccess.QueryResults
{
    public class PagedResult<TModel> : IPagedResult<TModel>
    {
        public IEnumerable<TModel> Result { get; }
        public int TotalCount { get; }

        public PagedResult(IEnumerable<TModel> result, int totalCount)
        {
            Result = result;
            TotalCount = totalCount;
        }
    }
}