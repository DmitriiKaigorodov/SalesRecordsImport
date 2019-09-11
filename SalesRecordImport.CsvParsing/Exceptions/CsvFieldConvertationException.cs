using System;

namespace SalesRecordImport.CsvParsing.Exceptions
{
    public class CsvFieldConvertationException : Exception
    {
        public int FieldIndex { get; }

        public Type ConversionType { get; }

        public CsvFieldConvertationException(int fieldIndex, Type conversionType)
           : base($"Can not convert field on index {fieldIndex} to type {conversionType.Name}.")
        {
            FieldIndex = fieldIndex;
            ConversionType = conversionType;
        }

        public CsvFieldConvertationException(int fieldIndex, Type conversionType, Exception innerException)
            : base($"Can not convert field on index {fieldIndex} to type {conversionType.Name}.", innerException)
        {
            FieldIndex = fieldIndex;
            ConversionType = conversionType;
        }
    }
}