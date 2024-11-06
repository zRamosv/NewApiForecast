
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace ApiForecast.Services.Caja
{
    public interface ICajaService
    {
        Task<List<VerFacturaDTO>> BuscarFactura(BuscarFacturaDTO buscarFactura);
        Task<VerFacturaDTO> VenderACliente(VentasInsert venta);
        
        
    }
}