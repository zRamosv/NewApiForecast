using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewApiForecast.Models.Entities.VentasModulo;

namespace NewApiForecast.Services.Ventas
{
    public interface IFacturasService
    {
        Task<List<Factura>> GetFacturasAsync();
        Task<List<Factura>> GetFacturasDeCliente(int idCliente);
        Task<Factura?> GetFacturaByIdAsync(int id);
        Task<Factura> CreateFactura(Factura factura);
        Task<Factura> UpdateFactura(int id, Factura factura);

    }
}