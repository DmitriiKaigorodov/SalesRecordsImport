using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Catel;
using Microsoft.EntityFrameworkCore;
using SalesRecordImport.DataAccess.Bulk;
using SalesRecordImport.Domain;

namespace SalesRecordImport.DataAccess.EFCore.Bulk
{
    internal class BulkInsert : IBulkInsert
    {
        private readonly SalesRecordDbContext _dbContext;
        private DataTable _dataTable = new DataTable();
        private List<PropertyInfo> _properties = new List<PropertyInfo>();
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private bool _errorOccured = false;
        private SqlBulkCopy _sqlBulkCopy;

        public BulkInsert(SalesRecordDbContext dbContext)
        {
            try
            {
                _dbContext = dbContext;
                _sqlConnection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString);
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
                var salesRecordType = _dbContext.Model.FindRuntimeEntityType(typeof(SalesRecord));
                _dataTable.TableName = salesRecordType.Relational().TableName;
                _sqlBulkCopy = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.Default, _sqlTransaction);
                foreach (var prop in salesRecordType.GetProperties())
                {
                    _dataTable.Columns.Add(prop.Name, prop.PropertyInfo.PropertyType);
                    _properties.Add(prop.PropertyInfo);
                    _sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(prop.PropertyInfo.Name, prop.Name));

                }
            }
            catch
            {
                _errorOccured = true;
                Dispose();
            }
        }

        public void AddRecord(SalesRecord salesRecord)
        {
            Argument.IsNotNull(nameof(salesRecord), salesRecord);

            var values = _properties.Select(x => x.GetValue(salesRecord)).ToArray();
            _dataTable.Rows.Add(values);
        }

        public void Dispose()
        {
            if (_errorOccured)
            {
                _sqlTransaction?.Rollback();
            }
            else
            {
                _sqlTransaction?.Commit();
            }

            _sqlTransaction?.Dispose();
            _sqlBulkCopy.Close();
            _sqlBulkCopy?.Close();

            _sqlConnection?.Close();
            _sqlConnection?.Dispose();
        }

        public async Task WriteToServer()
        {
            try
            {
                if (_dataTable.Rows.Count == 0)
                {
                    return;
                }

                _sqlBulkCopy.DestinationTableName = _dataTable.TableName;
                await _sqlBulkCopy.WriteToServerAsync(_dataTable);

                _dataTable.Clear();
            }
            catch
            {
                _errorOccured = true;
                throw;
            }

        }
    }
}
