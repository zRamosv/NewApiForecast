
using ApiForecast.Models.DTOs.CajaModulo;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using NewApiForecast.Models.DTOs.VentasModulo;

namespace ApiForecast.Services.Caja
{
    public interface ICajaService
    {
        Task<VerFacturaDTO> VerFactura(int id, BuscarFacturaDTO buscarFactura);
        Task<VerFacturaDTO> VenderACliente(VentasInsert venta);
        
    }
}