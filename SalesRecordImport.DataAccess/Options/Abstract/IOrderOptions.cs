namespace SalesRecordImport.DataAccess.Options.Abstract
{
    public interface IOrderOptions
    {
        string OrderColumn { get; set; }

        bool OrderAscending { get; set; }
    }
}