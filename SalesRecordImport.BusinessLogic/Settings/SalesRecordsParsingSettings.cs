namespace SalesRecordImport.BusinessLogic.Settings
{
    public class SalesRecordsParsingSettings
    {
        public int PartitionSize { get; set; } = 1000;

        public char Delimiter { get; set; } = ',';

        public string TextLocale { get; set; } = "en-US";
    }
}