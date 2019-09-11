using System;
using System.Linq.Expressions;
using SalesRecordImport.Domain;

namespace SalesRecordImport.DataAccess.Specifications.SalesRecords
{
    public class FilterByCountrySpecification : ISpecification<SalesRecord>
    {
        private readonly string _country;

        public FilterByCountrySpecification(string country)
        {
            _country = country;
        }

        public Expression<Func<SalesRecord, bool>> ToExpression() =>
            sr => _country == null || sr.Country.StartsWith(_country);

        public override string ToString()
        {
            return $"Country: {_country}";
        }
    }
}
