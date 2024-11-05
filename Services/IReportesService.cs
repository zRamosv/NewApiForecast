using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForescast.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Services
{
    public interface IReportesService
    {
        Task<List<Reportes>> GetReportes();
        Task<Reportes?> GetReporte (int id);
        Task<Reportes> CreateReporte(ReportesInsert reporte);
        Task<Reportes?> DeleteReporte(int id);
    }
}