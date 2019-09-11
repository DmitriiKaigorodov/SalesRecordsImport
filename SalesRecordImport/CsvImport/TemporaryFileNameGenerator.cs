using System;

namespace SalesRecordImport.WebApp.CsvImport
{
    public class TemporaryFileNameGenerator : ITemporaryFileNameGenerator
    {
        public string GenerateFileName() => Guid.NewGuid().ToString();

    }
}
