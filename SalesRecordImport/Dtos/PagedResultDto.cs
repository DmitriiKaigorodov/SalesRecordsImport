using System.Collections.Generic;

namespace SalesRecordImport.WebApp.Dtos
{
    public class PagedResultDto<TModel>
    {
        public IEnumerable<TModel> Result { get; set; }
        public int TotalCount { get; set; }
    }
}
