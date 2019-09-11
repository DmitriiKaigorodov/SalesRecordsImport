using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Catel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLog;
using SalesRecordImport.BusinessLogic.Exceptions;
using SalesRecordImport.BusinessLogic.Settings;
using SalesRecordImport.CsvParsing;
using SalesRecordImport.CsvParsing.Exceptions;
using SalesRecordImport.DataAccess.Bulk;
using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.DataAccess.QueryResults;
using SalesRecordImport.DataAccess.Repositories;
using SalesRecordImport.DataAccess.UnitOfWork;
using SalesRecordImport.Domain;

[assembly: InternalsVisibleTo("SalesRecordImport.BusinessLogic.Tests")]
namespace SalesRecordImport.BusinessLogic.Services
{
    internal class SalesRecordsService : ISalesRecordsService
    {
        private readonly ISalesRecordsRepository _salesRecordsRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SalesRecordsParsingSettings _salesRecordsParsingSettings;
        private readonly Logger _logger = LogManager.GetLogger(nameof(SalesRecordsService));

        public SalesRecordsService(IOptions<SalesRecordsParsingSettings> salesRecordsParsingSettings,
                                   ISalesRecordsRepository salesRecordsRepository,
                                   IServiceProvider serviceProvider,
                                   IUnitOfWork unitOfWork)
        {
            _salesRecordsParsingSettings = salesRecordsParsingSettings.Value;
            _salesRecordsRepository = salesRecordsRepository;
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task ImportRecordsFromCsvFile(string csvFilePath)
        {
            Argument.IsNotNull(nameof(csvFilePath), csvFilePath);

            _logger.Debug($"Start import csv with path {csvFilePath}.");
            var rowNumber = 2;
            try
            {
                using (var csvParser = new SalesRecordCsvParser(csvFilePath, _salesRecordsParsingSettings.Delimiter))
                {
                    csvParser.FormatProvider =
                        new CultureInfo(_salesRecordsParsingSettings.TextLocale, useUserOverride: false);

                    var bulkInsert = _serviceProvider.GetRequiredService<IBulkInsert>();

                    while (csvParser.HasRows)
                    {
                        var record = await csvParser.ParseRowAsync();
                        bulkInsert.AddRecord(record);

                        if (rowNumber % _salesRecordsParsingSettings.PartitionSize == 0)
                        {
                            await bulkInsert.WriteToServer();
                        }

                        ++rowNumber;
                    }
                    await bulkInsert.WriteToServer();

                }

                _logger.Debug($"Csv file {csvFilePath} imported successfully.");
            }
            catch (CsvFieldConvertationException e)
            {
                throw new CsvImportException(
                    $"Unable to parse csv {csvFilePath} row#{rowNumber}. Due to convertation error. See inner exception for details.",
                    e);
            }

        }

        public async Task<IPagedResult<SalesRecord>> GetSalesRecords(SalesRecordsOptions options)
        {
            Argument.IsNotNull(nameof(options), options);

            _logger.Debug($"Get records with options: {options}.");
            var pagedResult = await _salesRecordsRepository.GetSalesRecords(options);
            _logger.Debug("Records got successfully.");
            return pagedResult;
        }

        public async Task<bool> UpdateSalesRecord(SalesRecord salesRecord)
        {
            Argument.IsNotNull(nameof(salesRecord), salesRecord);

            _logger.Debug($"Update record with id: {salesRecord.Id}.");
            var updatedCount = await _salesRecordsRepository.UpdateRecord(salesRecord);
            await _unitOfWork.SaveChangesAsync();

            var updated = updatedCount > 0;

            if (updated)
            {
                _logger.Debug($"Record with id: {salesRecord.Id} updated.");
            }
            else
            {
                _logger.Warn($"Attempt to update record with id {salesRecord.Id}: record doesn't exist.");
            }

            return updated;
        }

        public async Task<bool> DeleteSalesRecord(int recordId)
        {
            _logger.Debug($"Delete record with id: {recordId}.");
            var deletedCount = await _salesRecordsRepository.DeleteRecord(recordId);
            await _unitOfWork.SaveChangesAsync();

            var deleted = deletedCount > 0;

            if (deleted)
            {
                _logger.Debug($"Record with id: {deletedCount} deleted.");
            }
            else
            {
                _logger.Warn($"Attempt to delete record with id {deletedCount}: record doesn't exist.");
            }

            return deleted;
        }
    }
}