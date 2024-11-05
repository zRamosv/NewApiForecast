using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiForecast.Services
{
    public class VentasService : IVentasService
    {
        private readonly ForecastContext _context;
        private IMapper _mapper;

        public VentasService(ForecastContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Ventas>> GetVentasAsync()
        {
            return await _context.Ventas.Include(x => x.Cliente).Include(x => x.Productos).Include(x => x.Vendedores).ToListAsync();
        }

        public async Task<Ventas?> GetVentaByIdAsync(int id)
        {
            return await _context.Ventas.Include(x => x.Cliente).Include(x => x.Productos).Include(x => x.Vendedores).FirstOrDefaultAsync(x => x.Venta_Id == id);
        }

        public async Task<Ventas> CreateVenta(VentasInsert venta)
        {
            var insert = _mapper.Map<Ventas>(venta);
            await _context.Ventas.AddAsync(insert);
            await _context.SaveChangesAsync();
            return insert;
        }

        public async Task<Ventas> UpdateVenta(int id, VentasDTO venta)
        {
            var update = await _context.Ventas.FirstOrDefaultAsync(x => x.Venta_Id == id);
            if (update == null)
            {
                return update;
            }
            foreach (var property in update.GetType().GetProperties())
            {
                var dtoProperty = venta.GetType().GetProperty(property.Name);
                if (dtoProperty != null)
                {
                    var newValue = dtoProperty.GetValue(venta);
                    if (newValue != null)
                    {
                        property.SetValue(update, newValue);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return update;
        }

        public async Task<Ventas> DeleteVenta(int id)
        {
            var delete = await _context.Ventas.FirstOrDefaultAsync(x => x.Venta_Id == id);
            if (delete == null)
            {
                return delete;
            }
            _context.Ventas.Remove(delete);
            await _context.SaveChangesAsync();
            return delete;
        }
    }
}