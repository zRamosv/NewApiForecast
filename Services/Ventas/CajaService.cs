
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using ApiForecast.Services;
using ApiForecast.Services.Caja;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace ApiForecast.Controllers.Caja
{
    public class CajaService : ICajaService
    {
        private IVentasService _ventasService;

        public CajaService(IVentasService ventasService)
        {
            _ventasService = ventasService;
        }

        public Task<VerFacturaDTO> Devolver(int venta_id)
        {
            throw new NotImplementedException();
        }

        public async Task<VerFacturaDTO> VenderACliente(VentasInsert venta)
        {
            var ventaRegistrada = await _ventasService.CreateVenta(venta);
            return new VerFacturaDTO
            {
                Pedido = ventaRegistrada,
                cliente = ventaRegistrada.Cliente,
                Fecha = ventaRegistrada.Fecha,
                Estatus = "REGISTRADO",
                Importe = ventaRegistrada.Precio,
                Moneda = "MXN",
                TipoDeCambio = 0.16f
            };
        }

        public async Task<VerFacturaDTO> VerFactura(int id, BuscarFacturaDTO buscarFactura)
        {
            var ventaRegistrada = await _ventasService.GetVentaByIdAsync(id);
            if (ventaRegistrada == null)
            {
                throw new System.Exception("No se encontró la factura");
            }
            return new VerFacturaDTO
            {
                Pedido = ventaRegistrada,
                cliente = ventaRegistrada.Cliente,
                Fecha = ventaRegistrada.Fecha,
                Estatus = "REGISTRADO",
                Importe = ventaRegistrada.Precio,
                Moneda = "MXN",
                TipoDeCambio = 0.16f
            };
        }
    }
}