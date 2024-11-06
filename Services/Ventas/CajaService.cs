
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using NewApiForecast.Controllers.Caja;
using NewApiForecast.Models.DTOs.VentasModulo;
using NewApiForecast.Models.Entities;
using NewApiForecast.Models.Entities.VentasModulo;
using NewApiForecast.Services.Caja;
using NewApiForecast.Services.Ventas;

namespace ApiForecast.Services.Caja
{
    public class CajaService: ICajaService
    {
        private IVentasService _ventasService;
        private IDevolucionesService _devolucionesService;
        private IFacturasService _facturasService;
        private IPedidosService _pedidosService;

        public CajaService(IVentasService ventasService, IDevolucionesService devolucionesService, IFacturasService facturasService, IPedidosService pedidosService)
        {
            _ventasService = ventasService;
            _devolucionesService = devolucionesService;
            _facturasService = facturasService;
            _pedidosService = pedidosService;
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
            throw new System.NotImplementedException();
        }

        public async Task<List<VerFacturaDTO>> BuscarFactura(BuscarFacturaDTO buscarFactura)
        {
            var facturasEncontradas = new List<Factura>();
            if (buscarFactura.Id_Pedido is 0)
            {
                facturasEncontradas = await SearchByClientAndFilter(searchByClient: buscarFactura);
            }
            var factura = await _facturasService.GetFacturaByIdAsync(buscarFactura.Id_Pedido);
            if (factura != null)
            {
                facturasEncontradas.Add(factura);
            }
            if (facturasEncontradas.Count == 0)
            {
                throw new System.Exception("No se encontró la factura");
            }
            List<VerFacturaDTO> facturasAMostrar = new List<VerFacturaDTO>();
            foreach (var facturaEncontrada in facturasEncontradas)
            {
                var pedido = await GetPedidoAsociadoAFactura(buscarFactura);
                facturasAMostrar.Add(MapFacturaAndPedido(facturaEncontrada, pedido));
            }
            return facturasAMostrar;
        }

        private async Task<Pedido> GetPedidoAsociadoAFactura(BuscarFacturaDTO buscarFactura)
        {
            return await _pedidosService.GetPedidoByIdAsync(buscarFactura.Id_Pedido);
        }

        private async Task<List<Factura>> SearchByClientAndFilter(BuscarFacturaDTO searchByClient)
        {
            var facturasEncontradas = new List<Factura>();
            var todasLasFacturas = await _facturasService.GetFacturasDeCliente(searchByClient.ID_Cliente);
            if (todasLasFacturas.Count == 0)
            {
                return facturasEncontradas;
            }

            var facturaAMostrar = todasLasFacturas
                .Find(IsFacturaMatchingSearchCriteria(searchByClient));
            if (facturaAMostrar == null)
            {
                return facturasEncontradas;
            }
            facturasEncontradas.Add(facturaAMostrar);
            return facturasEncontradas;

        }
        private VerFacturaDTO MapFacturaAndPedido(Factura factura, Pedido pedido)
        {
            return new VerFacturaDTO
            {
                Pedido = pedido,
                Cliente = pedido.Cliente,
                Fecha = DateOnly.FromDateTime(factura.FechaFactura),
                Estatus = factura.Estado,
                Importe = factura.Total,
                Moneda = factura.Moneda,
                TipoDeCambio = (double)factura.TipoDeCambio
            };
        }
        private static Predicate<Factura> IsFacturaMatchingSearchCriteria(BuscarFacturaDTO searchCriteria)
        {
            return f => searchCriteria.Id_Pedido == f.Id_Pedido && f.FechaFactura >= searchCriteria.Fecha_Inicio && f.FechaFactura <= searchCriteria.Fecha_Fin;
        }
    }
}