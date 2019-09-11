using System.Collections.Generic;

namespace SalesRecordImport.DataAccess.QueryResults
{
    public interface IPagedResult<out TModel>
    {
        IEnumerable<TModel> Result { get; }
        int TotalCount { get; }
    }
}