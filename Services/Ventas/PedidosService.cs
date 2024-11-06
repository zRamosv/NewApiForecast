using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Data;
using ApiForecast.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NewApiForecast.Models.Entities;
using NewApiForecast.Services.Caja;

namespace NewApiForecast.Services.Ventas
{
    public class PedidosService: IPedidosService
    {
        private readonly ForecastContext _context;
        public PedidosService(ForecastContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CreatePedido(Pedido factura)
        {
            throw new NotImplementedException();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id_Pedido == id);
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            return await _context.Pedidos.Include(x=> x.Productos).ToListAsync();
        }

        public async Task<Pedido> UpdatePedido(int id, Pedido factura)
        {
            throw new NotImplementedException();
        }
    }
}