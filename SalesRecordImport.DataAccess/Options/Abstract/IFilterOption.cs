using SalesRecordImport.DataAccess.Specifications;

namespace SalesRecordImport.DataAccess.Options.Abstract
{
    public interface IFilterOption<TModel> where TModel : class
    {
        ISpecification<TModel> Filter { get; set; }
    }
}