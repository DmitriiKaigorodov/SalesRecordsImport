using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLog;
using SalesRecordImport.BusinessLogic.Services;
using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.Domain;
using SalesRecordImport.WebApp.CsvImport;
using SalesRecordImport.WebApp.Dtos;
using SalesRecordImport.WebApp.Models;
using SalesRecordImport.WebApp.Settings;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SalesRecordImport.WebApp.Controllers
{
    [Route("api/records")]
    [ApiController]
    public class SalesRecordsController : ControllerBase
    {
        private readonly ISalesRecordsService _salesRecordsService;
        private readonly IHostingEnvironment _environment;
        private readonly ICsvFileValidator _csvFileValidator;
        private readonly ITemporaryFileNameGenerator _temporaryFileNameGenerator;
        private readonly IMapper _mapper;
        private readonly CsvImportSettings _csvImportSettings;
        private readonly Logger _logger = LogManager.GetLogger(nameof(SalesRecordsController));

        public SalesRecordsController(ISalesRecordsService salesRecordsService,
                                      IHostingEnvironment environment,
                                      ICsvFileValidator csvFileValidator,
                                      ITemporaryFileNameGenerator temporaryFileNameGenerator,
                                      IOptions<CsvImportSettings> csvImportSettings,
                                      IMapper mapper)
        {
            _salesRecordsService = salesRecordsService;
            _environment = environment;
            _csvFileValidator = csvFileValidator;
            _temporaryFileNameGenerator = temporaryFileNameGenerator;
            _mapper = mapper;
            _csvImportSettings = csvImportSettings.Value;
        }

        [HttpPost("import")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ImportRecordsFromCsv(IFormFile csvFile)
        {
            try
            {
                if (!_csvFileValidator.Validate(csvFile))
                {
                    return BadRequest("Please upload valid csv file.");
                }

                var fileName = _temporaryFileNameGenerator.GenerateFileName();

                var tmpFileDirectory = Path.Combine(_environment.WebRootPath, _csvImportSettings.TemporaryFilesFolder);

                if (!Directory.Exists(tmpFileDirectory))
                {
                    Directory.CreateDirectory(tmpFileDirectory);
                }

                var fullTmpCsvFilePath = Path.Combine(tmpFileDirectory, $"{fileName}.csv");


                using (var stream = new FileStream(fullTmpCsvFilePath, FileMode.Create))
                {
                    await csvFile.CopyToAsync(stream);
                }

                await _salesRecordsService.ImportRecordsFromCsvFile(fullTmpCsvFilePath);

                if (System.IO.File.Exists(fullTmpCsvFilePath))
                {
                    System.IO.File.Delete(fullTmpCsvFilePath);
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(ImportRecordsFromCsv)}.");
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetSalesRecords([FromQuery] SalesRecordsRequestModel salesRecordsRequest)
        {
            try
            {
                var salesRecordOptions = _mapper.Map<SalesRecordsOptions>(salesRecordsRequest);
                var records = await _salesRecordsService.GetSalesRecords(salesRecordOptions);
                var dtos = _mapper.Map<PagedResultDto<SalesRecordDto>>(records);

                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(GetSalesRecords)}.");
                throw;
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesRecord([FromBody] SalesRecordDto salesRecordDto)
        {
            try
            {
                var records = _mapper.Map<SalesRecord>(salesRecordDto);
                var updated = await _salesRecordsService.UpdateSalesRecord(records);

                return updated ? (IActionResult)Ok() : NotFound();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(UpdateSalesRecord)}.");
                throw;
            }
        }

        [HttpDelete("{recordId}")]
        public async Task<IActionResult> DeleteSalesRecord(int recordId)
        {
            try
            {
                var deleted = await _salesRecordsService.DeleteSalesRecord(recordId);
                return deleted ? (IActionResult)Ok() : NotFound();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(UpdateSalesRecord)}.");
                throw;
            }

        }
    }
}