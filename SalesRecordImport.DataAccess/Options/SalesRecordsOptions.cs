using System.Text;
using SalesRecordImport.DataAccess.Options.Abstract;
using SalesRecordImport.DataAccess.Specifications;
using SalesRecordImport.Domain;

namespace SalesRecordImport.DataAccess.Options
{
    public class SalesRecordsOptions : IPaginationOptions, IOrderOptions, IFilterOption<SalesRecord>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string OrderColumn { get; set; }
        public bool OrderAscending { get; set; }
        public ISpecification<SalesRecord> Filter { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{nameof(Page)}: {Page}");
            stringBuilder.Append($"{nameof(Size)}: {Size}");
            stringBuilder.Append($"{nameof(OrderColumn)}: {OrderColumn}");
            stringBuilder.Append($"{nameof(OrderAscending)}: {OrderAscending}");
            stringBuilder.Append($"{nameof(Filter)}: {Filter}");

            return stringBuilder.ToString();
        }
    }
}