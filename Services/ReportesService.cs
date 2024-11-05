using ApiForecast.Data;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Models.ReportesModels.ReportesCompras;
using ApiForescast.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Services
{
    public class ReportesService : IReportesService
    {
        private readonly ForecastContext _context;
        private readonly IMapper _mapper;

        public ReportesService(ForecastContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Reportes> CreateReporte(ReportesInsert reporte)
        {
            Reportes insert = _mapper.Map<Reportes>(reporte);
            await _context.Reportes.AddAsync(insert);
            await _context.SaveChangesAsync();
            return insert;
        }

        public async Task<Reportes?> DeleteReporte(int id)
        {
            var delete = await _context.Reportes.FindAsync(id);
            if (delete is null) return null;
            _context.Reportes.Remove(delete);
            await _context.SaveChangesAsync();
            return delete;
        }

        public async Task<Reportes?> GetReporte(int id)
        {
            return await _context.Reportes.FirstOrDefaultAsync(x => x.Report_id == id);

        }

        public async Task<List<Reportes>> GetReportes()
        {
            return await _context.Reportes.ToListAsync();
        }
    }

}