using System;
using System.Threading.Tasks;

namespace SalesRecordImport.CsvParsing
{
    public interface ICsvParser<TModel> : IDisposable
    {
        Task<TModel> ParseRowAsync();

        bool HasRows { get; }
    }
}
