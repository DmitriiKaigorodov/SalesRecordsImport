using System;

namespace SalesRecordImport.BusinessLogic.Exceptions
{
    public class CsvImportException : Exception
    {
        public CsvImportException(string message) : base(message)
        {
        }

        public CsvImportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}