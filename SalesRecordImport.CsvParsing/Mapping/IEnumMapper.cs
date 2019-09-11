using System;

namespace SalesRecordImport.CsvParsing.Mapping
{
    public interface IEnumMapper<TEnum> where TEnum : struct, IConvertible
    {
        TEnum MapFromString(string stringValue);
    }
}