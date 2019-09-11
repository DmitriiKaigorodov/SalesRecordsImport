namespace SalesRecordImport.DataAccess.Options.Abstract
{
    public interface IPaginationOptions
    {
        int? Page { get; set; }
        int? Size { get; set; }
    }
}