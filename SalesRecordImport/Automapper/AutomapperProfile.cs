using AutoMapper;
using SalesRecordImport.DataAccess.Options;
using SalesRecordImport.DataAccess.QueryResults;
using SalesRecordImport.DataAccess.Reports.Requests;
using SalesRecordImport.DataAccess.Specifications.SalesRecords;
using SalesRecordImport.Domain;
using SalesRecordImport.WebApp.Dtos;
using SalesRecordImport.WebApp.Models;

namespace SalesRecordImport.WebApp.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<SalesRecord, SalesRecordDto>()
                .ReverseMap();

            CreateMap<SalesRecordsRequestModel, SalesRecordsOptions>()
                .ForMember(x => x.Filter, opt => opt.MapFrom(x => new FilterByCountrySpecification(x.Country)));

            CreateMap<OrdersCountReportModel, OrdersCountByYearAndCountryReportRequest>();
            CreateMap<TotalProfiltReportModel, ProfitByYearAndCountryReportRequest>();
            CreateMap<PagedResult<SalesRecord>, PagedResultDto<SalesRecordDto>>()
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.TotalCount, opt => opt.MapFrom(x => x.TotalCount));
        }
    }
}
