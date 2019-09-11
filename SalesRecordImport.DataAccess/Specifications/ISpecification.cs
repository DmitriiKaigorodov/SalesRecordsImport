using System;
using System.Linq.Expressions;

namespace SalesRecordImport.DataAccess.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}