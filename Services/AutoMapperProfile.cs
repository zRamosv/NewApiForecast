using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using AutoMapper;

namespace ApiForecast.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Reportes, ReportesInsert>();
            CreateMap<ReportesInsert, Reportes>();
            CreateMap<Ventas, VentasInsert>();
            CreateMap<VentasInsert, Ventas>();
            
        }
    }
}