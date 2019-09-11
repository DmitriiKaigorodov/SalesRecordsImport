using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesRecordImport.BusinessLogic.Exceptions;
using SalesRecordImport.BusinessLogic.Services;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Reports.Results;
using SalesRecordImport.WebApp.Models;
using System.Threading.Tasks;
using NLog;

namespace SalesRecordImport.WebApp.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetLogger(nameof(ReportsController));

        public ReportsController(IReportsService reportsService, IMapper mapper)
        {
            _reportsService = reportsService;
            _mapper = mapper;
        }

        [HttpGet("ordersCount")]
        public async Task<IActionResult> GetOrdersCountReport([FromQuery] OrdersCountReportModel ordersCountReportModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = _mapper.Map<OrdersCountByYearAndCountryReportRequest>(ordersCountReportModel);
                return await GetReport<OrdersCountByYearAndCountryReportRequest, OrdersCountByYearAndCountryReportResult>(request);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(GetTotalProfitReport)}.");
                throw;
            }

        }

        [HttpGet("totalProfit")]
        public async Task<IActionResult> GetTotalProfitReport([FromQuery] TotalProfiltReportModel totalProfiltReportModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = _mapper.Map<ProfitByYearAndCountryReportRequest>(totalProfiltReportModel);
                return await GetReport<ProfitByYearAndCountryReportRequest, ProfitByYearAndCountryReportResult>(request);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected exception in method {nameof(GetTotalProfitReport)}.");
                throw;
            }
        }

        private async Task<IActionResult> GetReport<TRequest, TResult>(TRequest request) where TRequest : IReportRequest where TResult : IReportResult
        {
            try
            {
                var result = await _reportsService.GenerateReportAsync<TRequest, TResult>(request);
                return Ok(result);
            }
            catch (ReportGeneratorNotFoundException)
            {
                return NotFound();
            }
        }
    }
}