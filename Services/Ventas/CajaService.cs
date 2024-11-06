
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;

using NewApiForecast.Models.DTOs.VentasModulo;

namespace ApiForecast.Services.Caja
{
    public class CajaService : ICajaService
    {
        private IVentasService _ventasService;
        private IDevolucionesService _devolucionesService;

        public CajaService(IVentasService ventasService, IDevolucionesService devolucionesService)
        {
            _ventasService = ventasService;
            _devolucionesService = devolucionesService;
        }

        public async Task<Devolucion> Devolver(int venta_id)
        {
            var venta = await _ventasService.GetVentaByIdAsync(venta_id);
            if (venta == null)
            {
                throw new System.Exception("No se encontró la venta");
            }
            var devolucion = new Devolucion
            {
                Pedido = venta,
                Pedido_ID = venta_id,
                Devuelto = System.DateTime.Now,
                Cliente = venta.Cliente,
                Importe = venta.Precio,
                Moneda = venta.MonedaUSD ? "USD" : "MXN",
                TipoDeCambio = (decimal)(venta.MonedaUSD ? 0.16f : 1)
            };
            await _devolucionesService.CreateDevolucion(devolucion);
            return devolucion;
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