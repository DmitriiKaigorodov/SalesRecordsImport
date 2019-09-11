using Microsoft.AspNetCore.Http;

namespace SalesRecordImport.WebApp.CsvImport
{
    public interface ICsvFileValidator
    {
        bool Validate(IFormFile formFile);
    }
}
