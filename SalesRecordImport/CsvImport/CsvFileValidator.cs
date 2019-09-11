using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SalesRecordImport.WebApp.CsvImport
{
    public class CsvFileValidator : ICsvFileValidator
    {
        private static readonly string[] _acceptableContentTypes = { "text/csv", "application/vnd.ms-excel" };

        public bool Validate(IFormFile formFile) => _acceptableContentTypes.Contains(formFile.ContentType);
    }
}
