using AutoMapper;

namespace ApiForecast.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Reportes, ReportesInsert>();
            CreateMap<ReportesInsert, Reportes>();
        }
    }
}