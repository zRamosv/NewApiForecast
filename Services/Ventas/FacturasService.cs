using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Data;
using Microsoft.EntityFrameworkCore;
using NewApiForecast.Models.Entities.VentasModulo;

namespace NewApiForecast.Services.Ventas
{
    public class FacturasService : IFacturasService
    {
        private readonly ForecastContext _context;
        public FacturasService(ForecastContext context)
        {
            _context = context;
        }
        public Task<Factura> CreateFactura(Factura factura)
        {
            throw new NotImplementedException();
        }

        public Task<Factura?> GetFacturaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Factura>> GetFacturasAsync()
        {
            var facturas = _context.Facturas.ToList();
            return facturas;
        }

        public async Task<List<Factura>> GetFacturasDeCliente(int idCliente)
        {
            var todosLosPedidosDeCliente = await _context.Pedidos
                .Where(p => p.Id_Cliente == idCliente).ToListAsync();
            var facturas = await _context.Facturas
                .Where(f => todosLosPedidosDeCliente.Any(p => p.Id_Pedido == f.Id_Pedido))
                .ToListAsync();
            return facturas;
        }

        public Task<Factura> UpdateFactura(int id, Factura factura)
        {
            throw new NotImplementedException();
        }
    }
}